using System;
using System.Numerics;

namespace AutomaticHunting.UI.Components;

public sealed class HudCardModel
{
	public string StatusText = "运行中";

	public string StatusGlyph = "●";

	public Vector4 StatusColor = Theme.StatusSuccess;

	public string? ZoneName;

	public string TargetLabel = "等待目标";

	public Vector4 TargetColor = Theme.TextSecondary;

	public string? TargetDistance;

	public string LeaderText = "未设置";

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

	public string MainButtonLabel = "开始导航";

	public UiVariant MainButtonVariant = UiVariant.Primary;

	public bool MainButtonEnabled = true;

	public bool MainButtonIsStop;

	public string MainButtonTooltip = "开始跟车导航";

	public Action? OnMain;

	public Action? OnSettings;

	public Action? OnCloseHud;

	public bool SettingsOpen;

	public Action<Vector2>? OnDragDelta;

	public Action? OnToggleEnabled;

	public string ToggleEnabledTooltip = "右键：关闭自动狩猎";
}
