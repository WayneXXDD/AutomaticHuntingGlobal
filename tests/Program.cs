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
