# 自動狩獵跟車（國際服）

由 SFY 的 AutomaticHunting 2.4.5.0 DLL 還原並移植。原作者署名與來源記錄保留於 [NOTICE.md](NOTICE.md) 和 [analysis](analysis/)。

## 安裝

在 Dalamud 加入插件庫：

`https://raw.githubusercontent.com/WayneXXDD/DalamudPlugins/main/pluginmaster.json`

開啟 **Get plugin testing builds（取得測試版插件）**，刷新清單後搜尋 **自動狩獵跟車（國際服）**。目前版本為 **2.4.5.2**，使用國際服 Dalamud API 15；仍需遊戲內實測。

需要 **vnavmesh** 導航，以及 **Lifestream** 跨服與切換分流。使用 `/auht` 顯示狀態浮窗，`/auht setup` 開啟設定精靈，`/auht stop` 停止導航。

## 繁體中文顯示

- 設定介面、提示、狀態與內嵌說明統一為繁體中文。
- 地圖、怪物與坐騎清單加入繁體中文譯文；顯示翻譯不會改動遊戲辨識、傳送目的地或儲存的原始名稱。
- 人物、伺服器、資料中心及相依插件的名稱保留原名，方便與遊戲和安裝清單對照。指令保持可直接執行的格式。
- 隨插件附上 Noto 繁體中文字型，並在編譯時檢查內建文字的字元涵蓋情況。

遊戲內容譯文根據 [中文遊戲資料](https://github.com/thewakingsands/ffxiv-datamining-cn) 轉為繁體中文，可能與其他繁體中文版本的官方譯名不同。

程式位於 [src](src/)，編譯與發布由 GitHub Actions 處理。原版已停用的跨服車頭功能維持停用；戰鬥輸出由玩家或其他插件負責。
