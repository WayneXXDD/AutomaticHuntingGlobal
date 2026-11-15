$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$repository = 'WayneXXDD/AutomaticHuntingGlobal'
$manifest = Get-Content -LiteralPath 'manifest/AutomaticHunting.json' -Raw | ConvertFrom-Json
$version = [version]$manifest.AssemblyVersion
$tag = 'v' + $version
$work = Join-Path $env:RUNNER_TEMP ('automatic-hunting-global-' + [Guid]::NewGuid().ToString('N'))
[void](New-Item -ItemType Directory -Path $work)
foreach ($file in Get-ChildItem -LiteralPath src -Filter '*.cs' -Recurse -File) {
    $text = Get-Content -LiteralPath $file.FullName -Raw
    if ($text.Contains('/pdr ') -or $text.Contains('Not supported: data(')) { throw "Unported source: $($file.Name)" }
}
dotnet run --project tests/GlobalCompatibility.Tests.csproj -c Release -p:NuGetAudit=false
if ($LASTEXITCODE -ne 0) { throw 'Global compatibility policy tests failed.' }
$dalamudZip = Join-Path $work 'dalamud.zip'
$dalamudRoot = Join-Path $work 'dalamud'
Invoke-WebRequest -Uri 'https://goatcorp.github.io/dalamud-distrib/stg/latest.zip' -OutFile $dalamudZip
Expand-Archive -LiteralPath $dalamudZip -DestinationPath $dalamudRoot
$dalamud = Get-ChildItem -LiteralPath $dalamudRoot -Filter Dalamud.dll -Recurse -File | Select-Object -First 1
if (-not $dalamud -or [Reflection.AssemblyName]::GetAssemblyName($dalamud.FullName).Version.Major -ne 15) { throw 'Expected Global Dalamud API 15.' }
dotnet build src/AutomaticHunting.csproj -c Release "-p:DalamudLibPath=$($dalamud.Directory.FullName)/" -p:NuGetAudit=false
if ($LASTEXITCODE -ne 0) { throw 'Global plugin build failed.' }
$binary = Join-Path $env:GITHUB_WORKSPACE 'src/bin/Release/net10.0-windows7.0'
$dllPath = Join-Path $binary 'AutomaticHunting.dll'
$identity = [Reflection.AssemblyName]::GetAssemblyName($dllPath)
if ($identity.Name -cne $manifest.InternalName -or $identity.Version -ne $version) { throw 'Built DLL identity differs from the package manifest.' }
$package = Join-Path $work 'package'
[void](New-Item -ItemType Directory -Path $package)
foreach ($name in @('AutomaticHunting.dll','AutomaticHunting.deps.json','ECommons.dll')) {
    Copy-Item -LiteralPath (Join-Path $binary $name) -Destination (Join-Path $package $name)
}
Copy-Item -LiteralPath manifest/AutomaticHunting.json -Destination (Join-Path $package 'AutomaticHunting.json')
Copy-Item -LiteralPath README.md -Destination (Join-Path $package 'README.md')
Copy-Item -LiteralPath NOTICE.md -Destination (Join-Path $package 'NOTICE.md')
Copy-Item -LiteralPath third-party/ECommons.LICENSE.md -Destination (Join-Path $package 'ECommons.LICENSE.md')
Invoke-WebRequest -Uri $manifest.IconUrl -OutFile (Join-Path $package 'icon.png')
$fontDirectory = Join-Path $package 'fonts'
[void](New-Item -ItemType Directory -Path $fontDirectory)
$fontPath = Join-Path $fontDirectory 'NotoSansCJKtc-Regular.otf'
Invoke-WebRequest -Uri 'https://raw.githubusercontent.com/notofonts/noto-cjk/f8d157532fbfaeda587e826d4cd5b21a49186f7c/Sans/OTF/TraditionalChinese/NotoSansCJKtc-Regular.otf' -OutFile $fontPath
$fontBytes = [IO.File]::ReadAllBytes($fontPath)
if ($fontBytes.Length -ne 16435884) { throw 'Unexpected Traditional Chinese font size.' }
$gitHash = [Security.Cryptography.IncrementalHash]::CreateHash([Security.Cryptography.HashAlgorithmName]::SHA1)
try {
    $gitHash.AppendData([Text.Encoding]::ASCII.GetBytes("blob $($fontBytes.Length)" + [char]0))
    $gitHash.AppendData($fontBytes)
    $fontBlob = [Convert]::ToHexString($gitHash.GetHashAndReset()).ToLowerInvariant()
} finally { $gitHash.Dispose() }
if ($fontBlob -cne 'f9376bae1d421520f73a3c6c9d50b22f89548301') { throw 'Traditional Chinese font differs from the pinned source.' }
Copy-Item -LiteralPath third-party/NotoSansCJK.LICENSE -Destination (Join-Path $package 'NotoSansCJK.LICENSE')
python .github/scripts/Validate-ZhTW.py $fontPath
if ($LASTEXITCODE -ne 0) { throw 'Traditional Chinese text or font coverage validation failed.' }
$distribution = Join-Path $env:GITHUB_WORKSPACE 'dist'
[void](New-Item -ItemType Directory -Path $distribution -Force)
$archive = Join-Path $distribution 'AutomaticHunting.zip'
$zip = [IO.Compression.ZipFile]::Open($archive, [IO.Compression.ZipArchiveMode]::Create)
try {
    foreach ($file in Get-ChildItem -LiteralPath $package -File -Recurse | Sort-Object FullName) {
        $entryName = [IO.Path]::GetRelativePath($package, $file.FullName).Replace([char]92, [char]47)
        $entry = $zip.CreateEntry($entryName, [IO.Compression.CompressionLevel]::Optimal)
        $entry.LastWriteTime = [DateTimeOffset]::new(2000,1,1,0,0,0,[TimeSpan]::Zero)
        $inputStream = [IO.File]::OpenRead($file.FullName)
        $outputStream = $entry.Open()
        try { $inputStream.CopyTo($outputStream) } finally { $outputStream.Dispose(); $inputStream.Dispose() }
    }
} finally { $zip.Dispose() }
$verified = [IO.Compression.ZipFile]::OpenRead($archive)
try {
    $names = @($verified.Entries.FullName)
    foreach ($required in @('AutomaticHunting.dll','ECommons.dll','AutomaticHunting.json','NOTICE.md','fonts/NotoSansCJKtc-Regular.otf','NotoSansCJK.LICENSE')) {
        if (@($names | Where-Object { $_ -ceq $required }).Count -ne 1) { throw "Missing or duplicate package entry $required" }
    }
    if (@($names | Where-Object { $_ -match '(^|/)(Dalamud|FFXIVClientStructs|Lumina|InteropGenerator)\.' }).Count) { throw 'Package contains Global host libraries.' }
} finally { $verified.Dispose() }
if ($version -eq [version]'2.4.5.4') {
    $previousArchive = Join-Path $work 'AutomaticHunting-2.4.5.3.zip'
    Invoke-WebRequest -Uri 'https://github.com/WayneXXDD/AutomaticHuntingGlobal/releases/download/v2.4.5.3/AutomaticHunting.zip' -OutFile $previousArchive
    if ((Get-FileHash -LiteralPath $previousArchive -Algorithm SHA256).Hash.ToLowerInvariant() -cne 'bbc46356abe744d8959c6765634cc6f54c83d1b20b548883fc322df3b10063cb') {
        throw 'Regression package checksum changed.'
    }
    dotnet run --project tools/VerifyPluginResources/VerifyPluginResources.csproj -c Release -p:NuGetAudit=false -- $previousArchive (Join-Path $env:GITHUB_WORKSPACE 'src') --expect-missing-name-resource
    if ($LASTEXITCODE -ne 0) { throw 'The previous resource failure was not reproduced.' }
}
dotnet run --project tools/VerifyPluginResources/VerifyPluginResources.csproj -c Release -p:NuGetAudit=false -- $archive (Join-Path $env:GITHUB_WORKSPACE 'src')
if ($LASTEXITCODE -ne 0) { throw 'The packaged plugin is missing or corrupting its startup resources.' }
$hash = (Get-FileHash -LiteralPath $archive -Algorithm SHA256).Hash.ToLowerInvariant()
[ordered]@{version=$version.ToString(); api=15; sha256=$hash; gitCommit=$env:GITHUB_SHA; tests='compatibility, display, search, identity, full-name leader matching, glyph and packaged resource regression checks'; gameTested=$false} |
    ConvertTo-Json | Set-Content -LiteralPath (Join-Path $distribution 'build-info.json') -Encoding utf8
Write-Output "Verified Global package $version; SHA256 $hash"
