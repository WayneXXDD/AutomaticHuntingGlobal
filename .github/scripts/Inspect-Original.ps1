$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$taskRoot = Join-Path $env:RUNNER_TEMP ('automatic-hunting-inspect-' + [Guid]::NewGuid().ToString('N'))
[void](New-Item -ItemType Directory -Path $taskRoot)
$archive = Join-Path $taskRoot 'AutomaticHunting.zip'
$package = Join-Path $taskRoot 'package'
[void](New-Item -ItemType Directory -Path $package)
$url = 'https://github.com/Whswa-river/River/releases/download/AutomaticHunting-2.4.5.0/AutomaticHunting.zip'
$expectedHash = '972065548e172c4ed33e77daa8e1f250bbc0401d6ba61a7bfd88428ff23632c3'
Invoke-WebRequest -Uri $url -OutFile $archive
if ((Get-Item -LiteralPath $archive).Length -ne 470315) { throw 'Original asset size changed.' }
if ((Get-FileHash -LiteralPath $archive -Algorithm SHA256).Hash.ToLowerInvariant() -ne $expectedHash) { throw 'Original asset SHA256 changed.' }
$allowed = @('AutomaticHunting.dll', 'ECommons.dll', 'AutomaticHunting.json', 'AutomaticHunting.deps.json', '使用说明.md')
$zip = [IO.Compression.ZipFile]::OpenRead($archive)
try {
    foreach ($name in $allowed) {
        $entries = @($zip.Entries | Where-Object { $_.FullName -ceq $name })
        if ($entries.Count -ne 1) { throw "Expected exactly one root entry: $name" }
        if ($entries[0].Length -gt 20MB) { throw "Entry is too large: $name" }
        [IO.Compression.ZipFileExtensions]::ExtractToFile($entries[0], (Join-Path $package $name), $false)
    }
    $entriesReport = @($zip.Entries | ForEach-Object { [ordered]@{ name = $_.FullName; bytes = $_.Length } })
} finally { $zip.Dispose() }
$dll = Join-Path $package 'AutomaticHunting.dll'
$assembly = [Reflection.AssemblyName]::GetAssemblyName($dll)
if ($assembly.Name -cne 'AutomaticHunting' -or $assembly.Version.ToString() -ne '2.4.5.0') { throw 'Unexpected assembly identity.' }
$dalamudZip = Join-Path $taskRoot 'dalamud.zip'
$dalamudRoot = Join-Path $taskRoot 'dalamud'
Invoke-WebRequest -Uri 'https://goatcorp.github.io/dalamud-distrib/stg/latest.zip' -OutFile $dalamudZip
Expand-Archive -LiteralPath $dalamudZip -DestinationPath $dalamudRoot
$dalamud = Get-ChildItem -LiteralPath $dalamudRoot -Filter Dalamud.dll -Recurse -File | Select-Object -First 1
if (-not $dalamud) { throw 'Global Dalamud.dll is missing.' }
$toolsRoot = Join-Path $taskRoot 'tools'
dotnet tool install --tool-path $toolsRoot ilspycmd --version 11.2.0.10050
if ($LASTEXITCODE -ne 0) { throw 'ILSpy installation failed.' }
$output = Join-Path $env:GITHUB_WORKSPACE 'analysis/original'
[void](New-Item -ItemType Directory -Path $output -Force)
$sources = Join-Path $taskRoot 'decompiled'
$ilspy = @('ilspycmd.exe', 'ilspycmd.cmd', 'ilspycmd') | ForEach-Object { Join-Path $toolsRoot $_ } | Where-Object { Test-Path -LiteralPath $_ -PathType Leaf } | Select-Object -First 1
if (-not $ilspy) {
    Get-ChildItem -LiteralPath $toolsRoot -Recurse -File | Select-Object -ExpandProperty FullName
    throw 'ILSpy command shim is missing.'
}
& $ilspy --disable-updatecheck -p -o $sources -r $dalamud.Directory.FullName -r $package $dll
if ($LASTEXITCODE -ne 0) { throw 'ILSpy decompilation failed. No recovered source will be published.' }
$sourceRoot = Join-Path $output 'source'
[void](New-Item -ItemType Directory -Path $sourceRoot -Force)
foreach ($file in Get-ChildItem -LiteralPath $sources -Recurse -File) {
    if ($file.Extension -notin @('.cs', '.csproj', '.resx', '.json')) { continue }
    $relative = [IO.Path]::GetRelativePath($sources, $file.FullName)
    $destination = Join-Path $sourceRoot $relative
    [void](New-Item -ItemType Directory -Path ([IO.Path]::GetDirectoryName($destination)) -Force)
    Copy-Item -LiteralPath $file.FullName -Destination $destination
}
foreach ($name in @('AutomaticHunting.json','AutomaticHunting.deps.json','使用说明.md')) {
    Copy-Item -LiteralPath (Join-Path $package $name) -Destination (Join-Path $output $name)
}
[ordered]@{
    originalAuthor = 'SFY'
    upstream = 'https://github.com/Whswa-river/River'
    asset = $url
    version = $assembly.Version.ToString()
    sha256 = $expectedHash
    decompiler = 'ilspycmd 11.2.0.10050'
    globalReferenceAssembly = [Reflection.AssemblyName]::GetAssemblyName($dalamud.FullName).FullName
    files = $entriesReport
    recoveredSourceFiles = @(Get-ChildItem -LiteralPath $sourceRoot -Filter '*.cs' -Recurse -File).Count
} | ConvertTo-Json -Depth 6 | Set-Content -LiteralPath (Join-Path $output 'provenance.json') -Encoding utf8
@'
# Original DLL inspection

This directory contains C# reconstructed with ILSpy from SFY's published AutomaticHunting 2.4.5.0 DLL.
It is reconstructed code, not the original source project; comments and some source-level information are unavailable.
The plugin DLL was inspected statically and was not loaded or executed.

Upstream: https://github.com/Whswa-river/River
The original author attribution is retained. No additional license grant is made for the upstream code.
These sources have not yet been ported or verified in Global FFXIV. Do not install this analysis as a plugin.
'@ | Set-Content -LiteralPath (Join-Path $output 'README.md') -Encoding utf8
Get-Content -LiteralPath (Join-Path $output 'provenance.json')
