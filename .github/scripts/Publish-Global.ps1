$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$manifest = Get-Content -LiteralPath 'manifest/AutomaticHunting.json' -Raw | ConvertFrom-Json
$tag = 'v' + $manifest.AssemblyVersion
$repository = 'WayneXXDD/AutomaticHuntingGlobal'
$notesPath = Join-Path $env:RUNNER_TEMP 'automatic-hunting-release.md'
@'
Global compatibility build reconstructed from SFY's AutomaticHunting 2.4.5.0 DLL.

- Global Dalamud API 15 / .NET 10.
- Global game data replaces Chinese server, map and hunt-name lists.
- Lifestream IPC replaces Chinese DailyRoutines world/instance commands.
- Restored C# source, provenance and repeatable decompilation are included in the repository.
- Original author: SFY.

This is a testing-channel build. Compilation and compatibility policy tests passed; in-game behavior has not yet been verified.
Requires vnavmesh for navigation and Lifestream for world / instance changes.
'@ | Set-Content -LiteralPath $notesPath -Encoding utf8
$existingText = gh release view $tag --repo $repository --json assets 2>$null
if ($LASTEXITCODE -eq 0) {
    $existing = $existingText | ConvertFrom-Json
    $asset = @($existing.assets | Where-Object { $_.name -ceq 'AutomaticHunting.zip' })
    if ($asset.Count -ne 1) { throw 'Existing release has an incomplete package.' }
    $expected = 'sha256:' + (Get-FileHash -LiteralPath dist/AutomaticHunting.zip -Algorithm SHA256).Hash.ToLowerInvariant()
    if ($asset[0].digest -cne $expected) { throw 'This version already exists with different content. Increase the version; do not overwrite it.' }
} else {
    gh release create $tag 'dist/AutomaticHunting.zip' 'dist/build-info.json' --repo $repository --target $env:GITHUB_SHA --title "AutomaticHunting Global $($manifest.AssemblyVersion)" --notes-file $notesPath
    if ($LASTEXITCODE -ne 0) { throw 'Unable to publish the verified Global package.' }
}
Copy-Item -LiteralPath manifest/AutomaticHunting.json -Destination AutomaticHunting.json
git config user.name 'github-actions[bot]'
git config user.email '41898282+github-actions[bot]@users.noreply.github.com'
git add -- AutomaticHunting.json
git diff --cached --quiet
if ($LASTEXITCODE -eq 0) { exit 0 }
if ($LASTEXITCODE -ne 1) { throw 'Unable to inspect the distribution manifest.' }
git commit -m 'Publish verified Global plugin download manifest'
if ($LASTEXITCODE -ne 0) { throw 'Unable to commit the distribution manifest.' }
git push origin HEAD:main
if ($LASTEXITCODE -ne 0) { throw 'Unable to publish the distribution manifest.' }
