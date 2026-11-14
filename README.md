# AutomaticHunting Global

由 SFY 的 AutomaticHunting 2.4.5.0 DLL 還原並移植至國際服。原作者署名保留，還原來源與步驟記錄在 [analysis](analysis/)。

## 安裝

在 Dalamud 加入你的插件庫：

`https://raw.githubusercontent.com/WayneXXDD/DalamudPlugins/main/pluginmaster.json`

此版本先以測試版提供，需開啟 Dalamud 的測試版插件選項。它已針對 Global Dalamud API 15 編譯；尚未完成遊戲內驗證。

需要 **vnavmesh** 尋路；跨服／切線需要 **Lifestream**。使用 `/auht` 開啟 HUD，`/auht setup` 設定車頭與功能，`/auht stop` 停止導航。

## Global 改動

- 伺服器、資料中心、地圖、水晶與 A／S 怪清單改從遊戲資料讀取，使用客戶端語言。
- 切線與跨服改用 Lifestream IPC，移除對中國服 DailyRoutines 指令的依賴。
- 保留原版設定介面、跟車、連車、S 怪懸停等流程；原版停用的跨服車頭功能維持停用。
- 預設突進指令依客戶端語言產生，一般技能距離限制仍適用。
- 戰鬥由玩家或其他插件處理；BMR AI 是原版可選整合。

程式位於 [src](src/)。GitHub Actions 自動測試、編譯並產生可下載 ZIP，沒有在你的電腦建立專案副本。來源與署名請見 [NOTICE.md](NOTICE.md)。
