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
Check(global::AutomaticHunting.UI.Components.LeaderRoster.EnsureMigrated(legacy), "legacy migration reports new roster");
Check(legacy.LeaderEntries.Count == 1 && legacy.LeaderEntries[0].Name == "Solfege Nuit", "migration preserves complete character identity");
Check(global::AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(legacy, "Solfege Nuit"), "migrated name accepts actual conductor");
Check(!global::AutomaticHunting.UI.Components.LeaderRoster.EnsureMigrated(legacy) && legacy.LeaderEntries.Count == 1, "migration is idempotent");
var roster = new PluginConfiguration {
    LeaderName = "Disabled Leader",
    LeaderEntries = new() {
        new() { Name = "Solfege Nuit", Enabled = true },
        new() { Name = "Other Leader", Enabled = true },
        new() { Name = "Disabled Leader", Enabled = false }
    }
};
Check(global::AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(roster, "Solfege Nuit@Gilgamesh"), "selected full name from live roster");
Check(global::AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(roster, "Other Leader"), "second enabled full name");
Check(!global::AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(roster, "Disabled Leader"), "disabled entry cannot use stale legacy summary");
Check(!global::AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(roster, "Solfege"), "roster rejects first name alone");
global::AutomaticHunting.UI.Components.LeaderRoster.SyncLeaderName(roster);
Check(roster.LeaderName == "Solfege Nuit | Other Leader", "summary uses unambiguous delimiters");
Check(global::AutomaticHunting.UI.Components.LeaderRoster.Active(roster).SequenceEqual(new[] { "Solfege Nuit", "Other Leader" }), "position lookup receives enabled complete names");
global::AutomaticHunting.UI.Components.LeaderRoster.ClearChecks(roster);
roster.LeaderName = "Solfege Nuit";
Check(!global::AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(roster, "Solfege Nuit"), "all disabled rejects stale summary");
Check(global::AutomaticHunting.UI.Components.LeaderRoster.Add(roster, "solfege\u00A0nuit@Gilgamesh") == false && roster.LeaderEntries.Count == 3, "adding a normalized duplicate reuses existing entry");
Check(global::AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(roster, "Solfege Nuit"), "re-enabled conductor matches");
var explicitLegacy = new PluginConfiguration { LeaderName = "Solfege Nuit | Other Leader" };
Check(global::AutomaticHunting.UI.Components.LeaderRoster.MatchesSender(explicitLegacy, "Other Leader"), "legacy fallback preserves full names");
Check(global::AutomaticHunting.UI.Components.LeaderRoster.EnsureMigrated(explicitLegacy) && explicitLegacy.LeaderEntries.Count == 2, "explicitly delimited migration");
Console.WriteLine("Passed " + assertions + " total compatibility, localization, identity and full-name leader regression assertions.");

Check(FlagTravelPolicy.Decide(false, 100, 100) == FlagTravelAction.DirectNavigation, "teleport disabled still navigates on same map");
Check(FlagTravelPolicy.Decide(false, 100, 200) == FlagTravelAction.WaitForTerritory, "teleport disabled waits for party/manual travel across maps");
Check(FlagTravelPolicy.Decide(false, 0, 200) == FlagTravelAction.WaitForTerritory, "no navigation while territory is unavailable");
Check(FlagTravelPolicy.Decide(true, 100, 100) == FlagTravelAction.Automatic, "enabled retains same-map teleport optimization");
Check(FlagTravelPolicy.Decide(true, 100, 200) == FlagTravelAction.Automatic, "enabled retains cross-map teleport");
Check(FlagTravelPolicy.Decide(true, 100, 0) == FlagTravelAction.Reject && FlagTravelPolicy.Decide(false, 100, 0) == FlagTravelAction.Reject, "invalid flag territory rejected");
var pendingFlag = new PendingFlagTravel();
var firstFlag = new System.Numerics.Vector3(1, 0, 2);
var newestFlag = new System.Numerics.Vector3(3, 0, 4);
pendingFlag.Set(200, firstFlag);
Check(pendingFlag.IsWaiting && pendingFlag.TakeReady(false, 100, true) == null, "wait does not consume a destination on another map");
Check(pendingFlag.TakeReady(false, 200, false) == null && pendingFlag.IsWaiting, "loading/combat retains pending destination without navigating");
pendingFlag.Set(300, newestFlag);
Check(pendingFlag.TakeReady(false, 200, true) == null, "new conductor coordinate replaces an older pending map");
var resumed = pendingFlag.TakeReady(false, 300, true);
Check(resumed.HasValue && resumed.Value.Position == newestFlag && resumed.Value.TerritoryType == 300, "manual arrival resumes toward latest coordinates");
Check(!pendingFlag.IsWaiting && pendingFlag.TakeReady(false, 300, true) == null, "arrival starts navigation only once");
pendingFlag.Set(200, firstFlag);
pendingFlag.Clear();
Check(!pendingFlag.IsWaiting && pendingFlag.TakeReady(false, 200, true) == null, "stop/disable discards pending navigation");
pendingFlag.Set(200, firstFlag);
Check(pendingFlag.TakeReady(true, 100, true).HasValue && !pendingFlag.IsWaiting, "re-enabling teleport can resume pending navigation");
pendingFlag.Set(0, firstFlag);
Check(!pendingFlag.IsWaiting, "invalid pending territory does not remain queued");
Console.WriteLine("Passed " + assertions + " total compatibility, localization, leader and optional flag-teleport assertions.");

Check(AMarkSearchPolicy.DefaultRadius == 200f, "larger default A-mark search radius");
Check(AMarkSearchPolicy.NormalizeRadius(350f) == 350f, "custom search radius retained");
Check(AMarkSearchPolicy.NormalizeRadius(0f) == 50f && AMarkSearchPolicy.NormalizeRadius(1000f) == 500f, "out-of-range saved search values bounded");
foreach (var invalidRadius in new[] { float.NaN, float.PositiveInfinity, float.NegativeInfinity })
    Check(AMarkSearchPolicy.NormalizeRadius(invalidRadius) == 200f, "invalid radius uses finite default");
Check(AMarkSearchPolicy.ShouldScan(150f, 200f), "search begins beyond the former 100m boundary");
Check(AMarkSearchPolicy.ShouldScan(350f, 350f) && !AMarkSearchPolicy.ShouldScan(350.1f, 350f), "custom acquisition boundary is applied");
Check(!AMarkSearchPolicy.ShouldScan(float.NaN, 200f) && !AMarkSearchPolicy.ShouldScan(-1f, 200f), "invalid distance cannot trigger scanning");
var arrivalSearch = new AMarkArrivalSearch();
var arrivalTime = new DateTime(2026, 11, 15, 0, 0, 0, DateTimeKind.Utc);
Check(arrivalSearch.Observe(49f, arrivalTime) == AMarkArrivalState.Approaching, "does not stop at the former 50m threshold");
Check(arrivalSearch.Observe(10f, arrivalTime) == AMarkArrivalState.Approaching, "arrival threshold does not stop early");
Check(arrivalSearch.Observe(9f, arrivalTime) == AMarkArrivalState.BeginWait, "arrival starts a load grace period");
Check(arrivalSearch.Observe(9f, arrivalTime.AddSeconds(0.5)) == AMarkArrivalState.Waiting, "continues rescanning after first miss");
Check(arrivalSearch.Observe(9f, arrivalTime.AddSeconds(4.9)) == AMarkArrivalState.Waiting, "does not expire before five seconds");
Check(arrivalSearch.Observe(9f, arrivalTime.AddSeconds(5)) == AMarkArrivalState.Expired, "arrival search eventually ends");
Check(arrivalSearch.Observe(20f, arrivalTime.AddSeconds(6)) == AMarkArrivalState.Approaching, "moving away resets arrival grace");
Check(arrivalSearch.Observe(9f, arrivalTime.AddSeconds(7)) == AMarkArrivalState.BeginWait, "new arrival receives a fresh grace period");
Console.WriteLine("Passed " + assertions + " total compatibility, localization, navigation and expanded A-mark search assertions.");

var travelRecovery = new NavigationTravelRecovery();
var travelTime = new DateTime(2026, 11, 20, 0, 0, 0, DateTimeKind.Utc);
var travelDestination = new NavigationRecoveryDestination(200, new System.Numerics.Vector3(100, 30, 200), false);
travelRecovery.Begin(travelDestination);
var normalTravelStep = travelRecovery.Observe(200, false, true, false, false, travelTime);
Check(!normalTravelStep.StopOldPath && !normalTravelStep.Restart.HasValue, "normal flight does not restart its route");
var loadingTravelStep = travelRecovery.Observe(200, true, true, false, true, travelTime);
Check(loadingTravelStep.StopOldPath && !loadingTravelStep.Restart.HasValue && travelRecovery.IsWaiting, "same-map party teleport invalidates old waypoints");
var stillComputingTravelStep = travelRecovery.Observe(200, false, true, true, true, travelTime.AddSeconds(10));
Check(stillComputingTravelStep.StopOldPath && !stillComputingTravelStep.Restart.HasValue, "old asynchronous path result must drain before recovery");
var drainedTravelStep = travelRecovery.Observe(200, false, true, true, false, travelTime.AddSeconds(11));
Check(drainedTravelStep.StopOldPath && !drainedTravelStep.Restart.HasValue, "clear the last old result before starting the stability timer");
Check(!travelRecovery.Observe(200, false, true, true, false, travelTime.AddSeconds(11.9)).Restart.HasValue, "do not restart during landing stabilization");
var recoveredTravelStep = travelRecovery.Observe(200, false, true, true, false, travelTime.AddSeconds(12));
Check(recoveredTravelStep.Restart == travelDestination && recoveredTravelStep.StopOldPath, "restart preserves destination instead of any old waypoint");
Check(!travelRecovery.HasDestination && !travelRecovery.Observe(200, false, true, true, false, travelTime.AddSeconds(13)).Restart.HasValue, "exactly one restart per teleport");
travelRecovery.Begin(travelDestination);
Check(travelRecovery.Observe(300, false, true, true, false, travelTime).StopOldPath, "territory change detects travel even if loading flag was missed");
var wrongMapStep = travelRecovery.Observe(300, false, true, true, false, travelTime.AddMinutes(2));
Check(!wrongMapStep.Restart.HasValue && travelRecovery.IsWaiting, "different map waits without teleporting back");
Check(!wrongMapStep.StopOldPath, "waiting in another map does not keep stopping unrelated routes after the old query drained");
travelRecovery.Observe(200, false, true, true, false, travelTime.AddMinutes(3));
Check(travelRecovery.Observe(200, false, true, true, false, travelTime.AddMinutes(3).AddSeconds(1)).Restart.HasValue, "returning to destination map allows fresh navigation");
travelRecovery.Begin(travelDestination);
Check(travelRecovery.Observe(200, false, false, false, false, travelTime).StopOldPath, "missing local player during loading pauses navigation");
travelRecovery.Observe(200, false, true, true, false, travelTime.AddSeconds(1));
travelRecovery.Observe(200, true, true, false, false, travelTime.AddSeconds(1.9));
travelRecovery.Observe(200, false, true, true, false, travelTime.AddSeconds(2));
Check(!travelRecovery.Observe(200, false, true, true, false, travelTime.AddSeconds(2.5)).Restart.HasValue, "second loading screen resets readiness timer");
Check(travelRecovery.Observe(200, false, true, true, false, travelTime.AddSeconds(3)).Restart.HasValue, "one restart after consecutive loading screens stabilize");
travelRecovery.Begin(travelDestination);
travelRecovery.Observe(200, true, true, false, false, travelTime);
travelRecovery.Clear();
Check(!travelRecovery.HasDestination && !travelRecovery.Observe(200, false, true, true, false, travelTime.AddSeconds(20)).Restart.HasValue, "stop/disable cancels recovery");
travelRecovery.Begin(travelDestination);
travelRecovery.Observe(200, true, true, false, false, travelTime);
var newTravelDestination = new NavigationRecoveryDestination(300, new System.Numerics.Vector3(400, 0, 500), true);
travelRecovery.Begin(newTravelDestination);
Check(!travelRecovery.IsWaiting && !travelRecovery.Observe(300, false, true, true, false, travelTime.AddSeconds(20)).Restart.HasValue, "new conductor/manual destination supersedes interrupted destination");
travelRecovery.UpdateDestination(300, new System.Numerics.Vector3(450, 10, 550));
travelRecovery.Observe(300, true, true, false, false, travelTime.AddSeconds(21));
travelRecovery.UpdateDestination(200, System.Numerics.Vector3.Zero);
travelRecovery.Observe(300, false, true, true, false, travelTime.AddSeconds(22));
var updatedTravelStep = travelRecovery.Observe(300, false, true, true, false, travelTime.AddSeconds(23));
Check(updatedTravelStep.Restart.HasValue && updatedTravelStep.Restart.Value.Position == new System.Numerics.Vector3(450, 10, 550) && updatedTravelStep.Restart.Value.ManualFlag, "known monster endpoint and manual navigation intent survive teleport; late old endpoint cannot overwrite them");
travelRecovery.Begin(travelDestination);
travelRecovery.Observe(200, true, true, false, false, travelTime);
travelRecovery.Observe(200, false, true, false, false, travelTime.AddSeconds(10));
Check(!travelRecovery.Observe(200, false, true, true, false, travelTime.AddSeconds(10.5)).Restart.HasValue, "do not resume while old navigation work, combat or mesh readiness blocks restart");
Check(travelRecovery.Observe(200, false, true, true, false, travelTime.AddSeconds(11.5)).Restart.HasValue, "restart after blockers clear and stable delay elapses");
travelRecovery.Begin(new NavigationRecoveryDestination(0, System.Numerics.Vector3.Zero, false));
Check(!travelRecovery.HasDestination, "invalid destination cannot leave recovery armed");
Console.WriteLine("Passed " + assertions + " total compatibility, localization, navigation, search and party-teleport recovery assertions.");

var delayedRoute = new NavigationTravelRecovery();
delayedRoute.Begin(travelDestination);
delayedRoute.Observe(200, true, false, false, false, travelTime, oldWorkerRunning: true);
Check(delayedRoute.Observe(300, false, true, false, true, travelTime.AddMilliseconds(100)).StopOldPath,
    "query submitted after teleport started still belongs to old worker");
Check(delayedRoute.Observe(300, false, true, false, false, travelTime.AddMilliseconds(200)).StopOldPath,
    "late result is stopped even in the wrong territory");
Check(!delayedRoute.Observe(300, false, true, false, false, travelTime.AddMilliseconds(300)).StopOldPath,
    "old result was drained once");
var sameMap = new NavigationTravelRecovery();
sameMap.Begin(travelDestination);
var origin = new System.Numerics.Vector3(10, 5, 10);
sameMap.Observe(200, false, true, false, false, travelTime, true, origin);
Check(!sameMap.Observe(200, false, true, false, false, travelTime.AddMilliseconds(300), true, origin + new System.Numerics.Vector3(5, 0, 5)).StopOldPath,
    "normal movement does not interrupt a route");
Check(sameMap.Observe(200, false, true, false, false, travelTime.AddMilliseconds(600), true, origin + new System.Numerics.Vector3(200, 0, 0)).StopOldPath,
    "same-map position jump between frames interrupts a route without a loading flag");
var preservedTarget = new NavigationTravelRecovery();
preservedTarget.Begin(travelDestination);
preservedTarget.UpdateDestination(999, System.Numerics.Vector3.Zero);
preservedTarget.Observe(200, true, false, false, false, travelTime);
preservedTarget.Observe(200, false, true, true, false, travelTime.AddSeconds(1));
Check(preservedTarget.Observe(200, false, true, true, false, travelTime.AddSeconds(2)).Restart == travelDestination,
    "a late update from another territory cannot turn flag coordinates into a world destination");
Check(updatedTravelStep.Restart.HasValue && !updatedTravelStep.Restart.Value.IsMapFlag,
    "known monster landing height is preserved separately from a map flag");
Console.WriteLine("Passed " + assertions + " assertions including delayed path completion and same-territory relocations.");

using (var session = new AutomationSession())
{
    Check(session.Token.IsCancellationRequested, "a newly loaded plugin starts OFF");
    var startup = session.Token;
    Check(session.Start(), "explicit start succeeds");
    var firstRun = session.Token;
    Check(!firstRun.IsCancellationRequested, "explicit start allows work");
    Check(startup.IsCancellationRequested, "starting cannot revive callbacks from before start");
    session.Start();
    Check(session.Token == firstRun, "repeated ON does not orphan running work");
    using var navigation = CancellationTokenSource.CreateLinkedTokenSource(firstRun);
    using var worldTravel = CancellationTokenSource.CreateLinkedTokenSource(firstRun);
    var delayedSwitch = Task.Delay(Timeout.Infinite, firstRun);
    session.Stop();
    Check(firstRun.IsCancellationRequested && navigation.IsCancellationRequested && worldTravel.IsCancellationRequested,
        "OFF cancels navigation and world travel through their linked tokens");
    try { await delayedSwitch; Check(false, "delayed switching must be cancelled"); }
    catch (OperationCanceledException) { Check(true, "OFF interrupts waiting instance switch"); }
    Check(session.Token.IsCancellationRequested, "work requested while OFF receives a cancelled token");
    session.Stop();
    Check(session.Token.IsCancellationRequested, "repeated OFF remains stopped");
    session.Start();
    Check(!session.Token.IsCancellationRequested && session.Token != firstRun,
        "OFF then ON creates a fresh session");
    Check(firstRun.IsCancellationRequested && navigation.IsCancellationRequested && worldTravel.IsCancellationRequested,
        "re-enabling cannot revive old movement, teleport or switch callbacks");
    var latest = session.Token;
    session.Dispose();
    Check(latest.IsCancellationRequested && session.Token.IsCancellationRequested, "unload cancels all remaining work");
    Check(!session.Start(), "unloaded sessions cannot restart");
}
Console.WriteLine("Passed " + assertions + " assertions including the master power switch and delayed-work cancellation.");

var allowCommands = false;
GlobalCommands.AutomationAllowed = () => allowCommands;
var host = global::AutomaticHunting.AutomaticHunting.PluginInterface;
var commands = global::AutomaticHunting.AutomaticHunting.CommandManager.Commands;
GlobalCommands.Process("/ac Test");
GlobalCommands.Process("/ahglobal instance 2");
GlobalCommands.Process("/ahglobal world Test World");
Check(commands.Count == 0 && host.InstanceCount == 0 && host.WorldCount == 0,
    "OFF blocks action commands and both travel IPCs");
GlobalCommands.Process("/vnav stop");
GlobalCommands.Process("/bmrai off");
Check(commands.SequenceEqual(new[] { "/vnav stop", "/bmrai off" }), "OFF still permits cleanup commands");
allowCommands = true;
GlobalCommands.Process("/ac Test");
Check(commands.Last() == "/ac Test", "ON permits normal commands");
GlobalCommands.Process("/ahglobal instance 2");
Check(host.InstanceCount == 1 && host.Busy, "ON starts an owned instance change");
GlobalCommands.StopOwnedTravel();
Check(host.AbortCount == 1 && !host.Busy, "master cleanup aborts the owned instance change");
GlobalCommands.StopOwnedTravel();
Check(host.AbortCount == 1, "cleanup does not abort twice");
host.Busy = true;
GlobalCommands.Process("/ahglobal instance 3");
GlobalCommands.StopOwnedTravel();
Check(host.InstanceCount == 1 && host.AbortCount == 1 && host.Busy,
    "a travel job already running elsewhere is not claimed or aborted");
host.Busy = false;
host.AcceptWorld = false;
try { GlobalCommands.Process("/ahglobal world Test World"); Check(false, "rejected world travel must fail"); }
catch (InvalidOperationException) { Check(true, "world-travel rejection reported"); }
GlobalCommands.StopOwnedTravel();
Check(host.AbortCount == 1, "rejected travel is never owned");
host.AcceptWorld = true;
GlobalCommands.Process("/ahglobal world Test World");
allowCommands = false;
GlobalCommands.StopOwnedTravel();
Check(host.AbortCount == 2 && !host.Busy, "OFF can stop an owned world change");
allowCommands = true;
GlobalCommands.Process("/ahglobal instance 4");
host.Busy = false;
GlobalCommands.ObserveTravelCompletion();
host.Busy = true;
GlobalCommands.StopOwnedTravel();
Check(host.AbortCount == 2 && host.Busy, "completed plugin travel cannot claim a later external job");
host.Busy = false;
GlobalCommands.AutomationAllowed = () => false;

var powerClicks = 0;
global::AutomaticHunting.UI.Components.UiButton.Click = false;
global::AutomaticHunting.UI.Components.AutomationPowerButton.Draw(false, () => powerClicks++, new(220, 28));
Check(global::AutomaticHunting.UI.Components.UiButton.Label.StartsWith("啟動插件###") && powerClicks == 0,
    "OFF draws an explicit start button and drawing never starts automation");
global::AutomaticHunting.UI.Components.UiButton.Click = true;
global::AutomaticHunting.UI.Components.AutomationPowerButton.Draw(true, () => powerClicks++, new(220, 28));
Check(global::AutomaticHunting.UI.Components.UiButton.Label.StartsWith("關閉插件###")
    && global::AutomaticHunting.UI.Components.UiButton.Variant == global::AutomaticHunting.UI.Components.UiVariant.Danger
    && powerClicks == 1, "ON draws a stop button and one click invokes the shared switch once");
Console.WriteLine("Passed " + assertions + " assertions including real command gating, owned-travel cleanup and power-button clicks.");
