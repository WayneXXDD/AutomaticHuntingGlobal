using AutomaticHunting;
var assertions = 0;
void Check(bool value, string label) { assertions++; if (!value) throw new Exception(label); }
for (var i = 1; i <= 9; i++) Check(GlobalCommandPolicy.TryParseInstance("/ahglobal instance " + i, out var actual) && actual == i, "instance " + i);
foreach (var value in new[] { "", "/pdr insc 1", "/ahglobal instance 0", "/ahglobal instance 10", "/ahglobal instance -1", "/ahglobal instance +1", "/ahglobal instance 1.0", "/ahglobal instance 1 x", "/ahglobal instance 2147483648" })
    Check(!GlobalCommandPolicy.TryParseInstance(value, out _), "invalid command: " + value);
Check(GlobalCommandPolicy.TryParseInstance("/AHGLOBAL INSTANCE 2", out var upper) && upper == 2, "case-independent commands");
Check(GlobalCommandPolicy.IsMark("Queen Hawk", "queen hawk"), "Global capitalization");
Check(GlobalCommandPolicy.IsMark("forgiven rebellion", "Forgiven Rebellion"), "S-rank capitalization");
Check(!GlobalCommandPolicy.IsMark("unrelated enemy", "queen hawk"), "unrelated monsters");
Check(!GlobalCommandPolicy.IsMark("unrelated enemy", ""), "empty mark");
Console.WriteLine("Passed " + assertions + " Global command and mark-matching assertions.");

var nativeNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
    ["Queen Hawk"] = "女王鷹蜂",
    ["Lakeland"] = "雷克蘭德",
    ["Ostall Imperative"] = "奧斯塔爾嚴命城",
    ["Phoenix"] = "不死鳥"
};
var original = "Queen Hawk";
var formatter = new TraditionalNameFormatter(nativeNames, new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Phoenix", "Lifestream" });
Check(formatter.Translate(original) == "女王鷹蜂", "localized monster name");
Check(original == "Queen Hawk", "native name remains unchanged");
Check(formatter.Translate("queen hawk") == "女王鷹蜂", "localized name capitalization");
Check(formatter.Translate("目標：Queen Hawk（90%）") == "目標：女王鷹蜂（90%）", "localized status text");
Check(formatter.Translate("Lakeland · Ostall Imperative") == "雷克蘭德 · 奧斯塔爾嚴命城", "localized destination");
Check(formatter.Translate("Queen Hawk##native-id") == "女王鷹蜂##native-id", "hidden widget identity preserved");
Check(formatter.Translate("Phoenix") == "Phoenix", "server identity preserved");
Check(formatter.Translate("Lifestream") == "Lifestream", "dependency identity preserved");
Check(formatter.Translate("/ac Queen Hawk") == "/ac Queen Hawk", "commands preserved");
Check(formatter.Translate("未知名稱") == "未知名稱", "unknown name preserved");
for (var i = 0; i < 1100; i++) formatter.Translate("測試文字" + i);
Check(formatter.Translate("Queen Hawk") == "女王鷹蜂", "translation remains correct after cache eviction");
Console.WriteLine("Passed " + assertions + " total compatibility and Traditional Chinese display assertions.");

Check(formatter.Matches("Queen Hawk", "女王"), "Traditional Chinese search");
Check(formatter.Matches("Queen Hawk", "hawk"), "native name search");
Check(!formatter.Matches("Queen Hawk", "不死鳥"), "unrelated search");
Check(formatter.Translate("\u8fde\u8f66\u914d\u7f6e 1") == "連車設定 1", "old generated preset title");
Check(formatter.Translate("狀態\uFFFD\uE0BB") == "狀態", "unsupported display markers removed");
formatter.PreserveName("Queen Hawk");
Check(formatter.Translate("Queen Hawk") == "Queen Hawk", "new player identity preserved");
Check(nativeNames["Queen Hawk"] == "女王鷹蜂", "source catalogue remains unchanged");
Console.WriteLine("Passed " + assertions + " total compatibility, localization, search and identity assertions.");

Check(LeaderNamePolicy.Matches("Solfege Nuit", "Solfege Nuit"), "actual full Global conductor name");
Check(LeaderNamePolicy.Matches("solfege nuit", "Solfege Nuit"), "case-insensitive full name");
Check(LeaderNamePolicy.Matches(" Solfege  Nuit ", "Solfege Nuit"), "collapsed name whitespace");
Check(LeaderNamePolicy.Matches("Solfege\u00A0Nuit", "Solfege\u3000Nuit"), "nonbreaking and fullwidth spaces preserve word boundary");
Check(LeaderNamePolicy.Matches("\uE000Solfege Nuit\u200B@Gilgamesh:", "Solfege Nuit"), "sender icon, invisible formatting and world suffix");
Check(LeaderNamePolicy.Matches("Solfege Nuit＠Gilgamesh", "Solfege Nuit"), "fullwidth world separator");
Check(LeaderNamePolicy.Matches("T'ala Moon-light", "T'ala Moon-light"), "apostrophe and hyphen preserved");
foreach (var wrong in new[] { "", "Solfege", "Nuit", "Solfege Nuitx", "Other Nuit" })
    Check(!LeaderNamePolicy.Matches(wrong, "Solfege Nuit"), "reject partial or unrelated conductor: " + wrong);
Check(!LeaderNamePolicy.Matches(null, null), "empty names never match");
Check(LeaderNamePolicy.ParseLegacy("Solfege Nuit").SequenceEqual(new[] { "Solfege Nuit" }), "legacy full name is not split");
Check(LeaderNamePolicy.ParseLegacy("Solfege Nuit@Gilgamesh | Other Leader; Third Leader\nFourth Leader；Fifth Leader").SequenceEqual(new[] { "Solfege Nuit", "Other Leader", "Third Leader", "Fourth Leader", "Fifth Leader" }), "explicit legacy separators");
var legacy = new PluginConfiguration { LeaderName = "Solfege Nuit" };
Check(AutomaticHunting.UI.Components.LeaderRoster.EnsureMigrated(legacy), "legacy migration reports new roster");
Check(legacy.LeaderEntries.Count == 1 && legacy.LeaderEntries[0].Name == "Solfege Nuit", "migration preserves complete character identity");
Check(AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(legacy, "Solfege Nuit"), "migrated name accepts actual conductor");
Check(!AutomaticHunting.UI.Components.LeaderRoster.EnsureMigrated(legacy) && legacy.LeaderEntries.Count == 1, "migration is idempotent");
var roster = new PluginConfiguration {
    LeaderName = "Disabled Leader",
    LeaderEntries = new() {
        new() { Name = "Solfege Nuit", Enabled = true },
        new() { Name = "Other Leader", Enabled = true },
        new() { Name = "Disabled Leader", Enabled = false }
    }
};
Check(AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(roster, "Solfege Nuit@Gilgamesh"), "selected full name from live roster");
Check(AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(roster, "Other Leader"), "second enabled full name");
Check(!AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(roster, "Disabled Leader"), "disabled entry cannot use stale legacy summary");
Check(!AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(roster, "Solfege"), "roster rejects first name alone");
AutomaticHunting.UI.Components.LeaderRoster.SyncLeaderName(roster);
Check(roster.LeaderName == "Solfege Nuit | Other Leader", "summary uses unambiguous delimiters");
Check(AutomaticHunting.UI.Components.LeaderRoster.Active(roster).SequenceEqual(new[] { "Solfege Nuit", "Other Leader" }), "position lookup receives enabled complete names");
AutomaticHunting.UI.Components.LeaderRoster.ClearChecks(roster);
roster.LeaderName = "Solfege Nuit";
Check(!AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(roster, "Solfege Nuit"), "all disabled rejects stale summary");
Check(AutomaticHunting.UI.Components.LeaderRoster.Add(roster, "solfege\u00A0nuit@Gilgamesh") == false && roster.LeaderEntries.Count == 3, "adding a normalized duplicate reuses existing entry");
Check(AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(roster, "Solfege Nuit"), "re-enabled conductor matches");
var explicitLegacy = new PluginConfiguration { LeaderName = "Solfege Nuit | Other Leader" };
Check(AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(explicitLegacy, "Other Leader"), "legacy fallback preserves full names");
Check(AutomaticHunting.UI.Components.LeaderRoster.EnsureMigrated(explicitLegacy) && explicitLegacy.LeaderEntries.Count == 2, "explicitly delimited migration");
Console.WriteLine("Passed " + assertions + " total compatibility, localization, identity and full-name leader regression assertions.");
