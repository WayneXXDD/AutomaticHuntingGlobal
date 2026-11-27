using System;
using System.Numerics;

namespace AutomaticHunting.UI.Components;

public sealed class HudCardModel
{
	public string StatusText = "已關閉";

	public bool AutomationEnabled;

	public string StatusGlyph = "●";

	public Vector4 StatusColor = Theme.StatusIdle;

	public string? ZoneName;

	public string TargetLabel = "等待目標";

	public Vector4 TargetColor = Theme.TextSecondary;

	public string? TargetDistance;

	public string LeaderText = "未設定";

	public Vector4 LeaderColor = Theme.TextDisabled;

	public bool HasSMob;

	public string SMobName = string.Empty;

	public float SMobHpPercent;

	public Vector4 SMobColor = Theme.BrandGold;

	public string? StageText;

	public bool Locked;

	public float Alpha = 0.88f;

	public bool Compact;

	public bool OnboardingPending;

	public bool Restricted;

	public bool HasChainRow;

	public string ChainText = string.Empty;

	public bool ChainCanAdjust;

	public bool ChainCanDecrease;

	public Action<int>? OnChainAdjust;

	public Action? OnChainStop;

	public string MainButtonLabel = "開始導航";

	public UiVariant MainButtonVariant = UiVariant.Primary;

	public bool MainButtonEnabled = true;

	public bool MainButtonIsStop;

	public string MainButtonTooltip = "開始跟車導航";

	public Action? OnMain;

	public Action? OnSettings;

	public Action? OnCloseHud;

	public bool SettingsOpen;

	public Action<Vector2>? OnDragDelta;

	public Action? OnToggleEnabled;

	public string ToggleEnabledTooltip = "右鍵：關閉自動狩獵";
}
