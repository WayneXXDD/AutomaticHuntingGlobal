$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$manifest = Get-Content -LiteralPath 'manifest/AutomaticHunting.json' -Raw | ConvertFrom-Json
$tag = 'v' + $manifest.AssemblyVersion
$repository = 'WayneXXDD/AutomaticHuntingGlobal'
$notesPath = Join-Path $env:RUNNER_TEMP 'automatic-hunting-release.md'
@'
繁體中文介面的國際服狩獵跟車插件。

2.4.5.7：新增「A 怪搜尋半徑」，預設 200 公尺，可調整 50～500 公尺。接近標點時依設定半徑提早搜尋，找不到會繼續靠近到 10 公尺內，最多再搜尋 5 秒。仍只辨識遊戲已載入的附近怪物，不會繞圈搜尋。保持每 0.5 秒檢查一次，S 怪辨識半徑維持原值。

2.4.5.6：跟車設定新增「自動傳送」，預設開啟。關閉後同張地圖仍自動飛向車頭標點；不同地圖保留最新標點，等待隊伍或手動傳送抵達後再導航。此時略過自動切線與其水晶傳送重試，手動跨服與傳送工具仍可使用。

2.4.5.5：修正國際服車頭的完整姓名被空白拆開，導致已收到喊話仍不跟隨的問題。喊話辨識與車頭位置查找皆依勾選清單比對完整姓名，並停止大量寫入空白發話者的除錯紀錄。

- 設定介面、狀態訊息、提示與說明統一為繁體中文。
- 加入地圖、怪物和坐騎名稱的繁體中文顯示譯文；遊戲辨識與儲存的原始名稱保持不變。
- 隨插件附上 Noto 繁體中文字型，並檢查全部內建文字所需的字元。
- 保留原作者 SFY 署名，以及 vnavmesh 導航與 Lifestream 跨服／分流功能。

此版本以測試版提供；已通過編譯、自動檢查與字型涵蓋檢查，仍需遊戲內實測。
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
