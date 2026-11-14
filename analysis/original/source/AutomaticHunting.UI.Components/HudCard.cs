using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class HudCard
{
	[CompilerGenerated]
	private sealed class _00A0
	{
		public float _2008_2048;

		public bool _2008_2049;

		public HudCardModel _2008_204A;

		public float _2008_204B;

		public Action _2008_204C;

		internal void _00A0(int P_0)
		{
			if (P_0 == 0)
			{
				float x = ImGui.GetContentRegionAvail().X;
				if (UiButton.Draw(size: new Vector2(x, _2008_2048), label: _2008_2049 ? HudCard._00A0(_2008_204A.MainButtonLabel) : _2008_204A.MainButtonLabel, variant: _2008_204A.MainButtonVariant, tooltip: _2008_204A.MainButtonTooltip, enabled: _2008_204A.MainButtonEnabled))
				{
					_2008_204A.OnMain?.Invoke();
				}
				return;
			}
			float x2 = ImGui.GetContentRegionAvail().X;
			float val = (_2008_2049 ? 40f : 26f) * _2008_204B;
			float num = Math.Min(x2, Math.Min(_2008_2048, val));
			Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
			ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + (x2 - num) * 0.5f, cursorScreenPos.Y + (_2008_2048 - num) * 0.5f));
			string tooltip = GearTooltip(_2008_204A.SettingsOpen);
			if (UiButton.DrawGear(new Vector2(num, num), tooltip, enabled: true, () =>
			{
				_2008_204A.OnCloseHud?.Invoke();
			}))
			{
				_2008_204A.OnSettings?.Invoke();
			}
		}

		internal void _00A0()
		{
			_2008_204A.OnCloseHud?.Invoke();
		}
	}

	public static Action<HudInfoRowGeometry>? InfoRowProbe;

	public static Action<HudStatusRowGeometry>? StatusRowProbe;

	public static Action<Vector2, Vector2>? DragRegionProbe;

	public static Action<Vector2, Vector2>? ChainStopButtonProbe;

	public static Action<AdjustRowGeometry>? ChainRowGeometryProbe;

	private static readonly UiAdjustRow _2000_206D = new UiAdjustRow();

	private const float _2000_206E = 12f;

	private const float _2000_206F = 3.5f;

	private const float _2000_3000 = 2f;

	public static string GearTooltip(bool settingsOpen)
	{
		if (!settingsOpen)
		{
			return "打开插件设置 · 右键关闭悬浮窗";
		}
		return "关闭插件设置 · 右键关闭悬浮窗";
	}

	public static bool ToggleSettingsOpen(bool currentlyOpen)
	{
		return !currentlyOpen;
	}

	public static void Draw(HudCardModel model)
	{
		float uiScale = Theme.UiScale;
		float num = 220f * uiScale;
		_00A0(model, num);
		if (!model.Compact)
		{
			ImGui.Dummy(new Vector2(1f, 6f * uiScale));
			UiSection.Divider();
			ImGui.Dummy(new Vector2(1f, 6f * uiScale));
			if (model.OnboardingPending)
			{
				_00A0(num);
			}
			else
			{
				_1680(model, num);
			}
			if (!string.IsNullOrEmpty(model.StageText))
			{
				ImGui.Dummy(new Vector2(1f, 6f * uiScale));
				_2000(model, num);
			}
		}
		else
		{
			ImGui.Dummy(new Vector2(1f, 6f * uiScale));
			_2001(model, num);
		}
		if (model.HasChainRow)
		{
			ImGui.Dummy(new Vector2(1f, 6f * uiScale));
			_2002(model, num);
		}
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		_2003(model, num);
		ImGui.Dummy(new Vector2(num, 0f));
	}

	private static void _00A0(HudCardModel P_0, float P_1)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = 20f * uiScale;
		windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(P_1, num), Theme.U32(Theme.SurfaceCard), 8f * uiScale);
		ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(0f, 0f));
		ImGui.BeginTable("##hudStatus", 3, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings);
		ImGui.TableSetupColumn("##pill", ImGuiTableColumnFlags.WidthStretch, 3.5f);
		ImGui.TableSetupColumn("##zone", ImGuiTableColumnFlags.WidthStretch, 2f);
		ImGui.TableSetupColumn("##lock", ImGuiTableColumnFlags.WidthFixed, 12f * uiScale);
		ImGui.TableNextRow();
		ImGui.TableNextColumn();
		float x = ImGui.GetContentRegionAvail().X;
		string text = Theme.Ellipsis(P_0.StatusGlyph + " " + P_0.StatusText, Math.Max(20f, x));
		Vector2 cursorScreenPos2 = ImGui.GetCursorScreenPos();
		Vector2 vector = cursorScreenPos2 + new Vector2(UiBadge.PillWidthOf(text), UiBadge.PillHeight());
		UiBadge.Pill(text, P_0.StatusColor);
		ImGui.TableNextColumn();
		Vector2 cursorScreenPos3 = ImGui.GetCursorScreenPos();
		float x2 = ImGui.GetContentRegionAvail().X;
		float zoneRight = 0f;
		if (!string.IsNullOrEmpty(P_0.ZoneName) && x2 > 10f * uiScale)
		{
			string text2 = Theme.Ellipsis(P_0.ZoneName, x2, Theme.FsCaption);
			float num2 = Theme.MeasureTextWidth(text2, Theme.FsCaption);
			float num3 = Theme.MeasureTextHeight(Theme.FsCaption);
			float num4 = cursorScreenPos3.X + x2 - num2;
			float y = cursorScreenPos3.Y + (num - num3) * 0.5f;
			zoneRight = num4 + num2;
			Theme.DrawText(windowDrawList, new Vector2(num4, y), Theme.HudSecondaryText, text2, Theme.FsCaption, Theme.HudTextShadow);
		}
		ImGui.TableNextColumn();
		Vector2 cursorScreenPos4 = ImGui.GetCursorScreenPos();
		Vector2 lockMax = cursorScreenPos4 + new Vector2(12f * uiScale, num);
		if (P_0.Locked)
		{
			Vector2 cursorScreenPos5 = ImGui.GetCursorScreenPos();
			float num5 = 12f * uiScale;
			UiIcons.DrawLock(windowDrawList, cursorScreenPos5 + new Vector2(num5 * 0.5f, num * 0.5f), num5, Theme.U32(Theme.BrandGold));
		}
		StatusRowProbe?.Invoke(new HudStatusRowGeometry(cursorScreenPos, cursorScreenPos + new Vector2(P_1, num), cursorScreenPos2, vector, cursorScreenPos4, lockMax, P_0.Locked, text, zoneRight));
		ImGui.EndTable();
		ImGui.PopStyleVar();
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImGui.InvisibleButton("##hudDrag", new Vector2(P_1, num));
		DragRegionProbe?.Invoke(ImGui.GetItemRectMin(), ImGui.GetItemRectMax());
		bool flag = ImGui.IsItemHovered();
		if (flag)
		{
			ImGui.SetMouseCursor(ImGuiMouseCursor.Hand);
		}
		ImGuiIOPtr iO;
		if (!P_0.Locked && ImGui.IsItemActive() && ImGui.IsMouseDragging(ImGuiMouseButton.Left))
		{
			Action<Vector2>? onDragDelta = P_0.OnDragDelta;
			if (onDragDelta != null)
			{
				iO = ImGui.GetIO();
				onDragDelta(iO.MouseDelta);
			}
		}
		bool flag2 = Theme.IsHoveredInWindow(cursorScreenPos2, vector);
		if (flag2)
		{
			iO = ImGui.GetIO();
			if (iO.MouseClicked[1])
			{
				P_0.OnToggleEnabled?.Invoke();
			}
		}
		if (flag)
		{
			string? text3;
			if (flag2)
			{
				text3 = P_0.ToggleEnabledTooltip + " · 拖动可移动 HUD";
			}
			else
			{
				text3 = (P_0.Locked ? "位置已锁定 · 在「概览」页解锁" : "拖动顶部状态行可移动 HUD");
			}
			ImGui.SetTooltip(text3);
		}
		ImGui.SetCursorScreenPos(cursorScreenPos + new Vector2(0f, num));
	}

	private static void _00A0(float P_0)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		string text = "尚未完成首次设置";
		string text2 = "需要先确认前置插件与免责声明，";
		string text3 = "并设置车头";
		Vector2 vector = ImGui.CalcTextSize(text);
		Vector2 vector2 = ImGui.CalcTextSize(text2);
		float y = vector.Y + vector2.Y * 2f + 16f * uiScale;
		Vector2 min = cursorScreenPos;
		Vector2 vector3 = cursorScreenPos + new Vector2(P_0, y);
		UiPanel.DrawCard(min, vector3, 6f * uiScale);
		float num = min.Y + 10f * uiScale;
		Theme.DrawText(windowDrawList, new Vector2(min.X + (P_0 - vector.X) * 0.5f, num), Theme.TextSecondary, text);
		num += vector.Y + 4f * uiScale;
		Theme.DrawText(windowDrawList, new Vector2(min.X + (P_0 - vector2.X) * 0.5f, num), Theme.TextDisabled, text2, Theme.FsCaption);
		Theme.DrawText(windowDrawList, new Vector2(y: num + vector2.Y, x: min.X + (P_0 - vector2.X) * 0.5f), Theme.TextDisabled, text3, Theme.FsCaption);
		ImGui.SetCursorScreenPos(vector3);
	}

	private static void _1680(HudCardModel P_0, float P_1)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		int num = 2 + (P_0.HasSMob ? 1 : 0);
		float num2 = (P_0.HasSMob ? (6f * uiScale) : 0f);
		float num3 = Math.Max(22f * uiScale - 6f * uiScale, Theme.FsBody);
		float num4 = num3 + 6f * uiScale;
		float y = (float)num * num4 + num2;
		Vector2 vector = cursorScreenPos + new Vector2(P_1, y);
		UiPanel.DrawCard(cursorScreenPos, vector, 6f * uiScale);
		float initWidthOrWeight = 8f * uiScale + _00A0() + 10f * uiScale;
		ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(0f, 3f * uiScale));
		ImGui.BeginTable("##hudInfo", 2, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings);
		ImGui.TableSetupColumn("##label", ImGuiTableColumnFlags.WidthFixed, initWidthOrWeight);
		ImGui.TableSetupColumn("##value", ImGuiTableColumnFlags.WidthStretch, 1f);
		ImGui.TableNextRow();
		ImGui.TableNextColumn();
		Vector2 cursorScreenPos2 = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		_00A0(windowDrawList, "目标", num3);
		ImGui.TableNextColumn();
		Vector2 cursorScreenPos3 = ImGui.GetCursorScreenPos();
		float x2 = ImGui.GetContentRegionAvail().X;
		_00A0(windowDrawList, P_0.TargetLabel, P_0.TargetColor, P_0.TargetDistance, num3);
		InfoRowProbe?.Invoke(new HudInfoRowGeometry(0, cursorScreenPos2, x, cursorScreenPos3, x2, num4));
		ImGui.TableNextRow();
		ImGui.TableNextColumn();
		cursorScreenPos2 = ImGui.GetCursorScreenPos();
		x = ImGui.GetContentRegionAvail().X;
		_00A0(windowDrawList, "车头", num3);
		ImGui.TableNextColumn();
		cursorScreenPos3 = ImGui.GetCursorScreenPos();
		x2 = ImGui.GetContentRegionAvail().X;
		_00A0(windowDrawList, P_0.LeaderText, P_0.LeaderColor, null, num3);
		InfoRowProbe?.Invoke(new HudInfoRowGeometry(1, cursorScreenPos2, x, cursorScreenPos3, x2, num4));
		if (P_0.HasSMob)
		{
			ImGui.TableNextRow();
			ImGui.TableNextColumn();
			cursorScreenPos2 = ImGui.GetCursorScreenPos();
			x = ImGui.GetContentRegionAvail().X;
			_00A0(windowDrawList, "S怪", num3);
			ImGui.TableNextColumn();
			cursorScreenPos3 = ImGui.GetCursorScreenPos();
			x2 = ImGui.GetContentRegionAvail().X;
			_00A0(windowDrawList, P_0, num3 + num2);
			InfoRowProbe?.Invoke(new HudInfoRowGeometry(2, cursorScreenPos2, x, cursorScreenPos3, x2, num4 + num2));
		}
		ImGui.EndTable();
		ImGui.PopStyleVar();
		ImGui.SetCursorScreenPos(vector);
	}

	private static float _00A0()
	{
		float num = ((Theme.FsBody > 0f) ? (Theme.FsCaption / Theme.FsBody) : 1f);
		return Math.Max(Math.Max(ImGui.CalcTextSize("目标").X * num, ImGui.CalcTextSize("车头").X * num), ImGui.CalcTextSize("S怪").X * num);
	}

	private static void _00A0(ImDrawListPtr P_0, string P_1, float P_2)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImGui.Dummy(new Vector2(1f, P_2));
		float x = cursorScreenPos.X + 8f * uiScale;
		float y = cursorScreenPos.Y + (P_2 - Theme.FsCaption) * 0.5f;
		Theme.DrawText(P_0, new Vector2(x, y), Theme.HudSecondaryText, P_1, Theme.FsCaption, Theme.HudTextShadow);
	}

	private static void _00A0(ImDrawListPtr P_0, string P_1, Vector4 P_2, string? distance, float P_4)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		ImGui.Dummy(new Vector2(1f, P_4));
		float num = cursorScreenPos.X + x - 8f * uiScale;
		float num2 = ((Theme.FsBody > 0f) ? (Theme.FsCaption / Theme.FsBody) : 1f);
		float num3 = (string.IsNullOrEmpty(distance) ? 0f : (ImGui.CalcTextSize(distance).X * num2 + 8f * uiScale));
		float maxWidth = Math.Max(10f, num - cursorScreenPos.X - num3);
		string text = Theme.Ellipsis(P_1, maxWidth);
		float y = cursorScreenPos.Y + (P_4 - Theme.FsBody) * 0.5f;
		Theme.DrawText(P_0, new Vector2(cursorScreenPos.X, y), P_2, text, Theme.FsBody, Theme.HudTextShadow);
		if (!string.IsNullOrEmpty(distance))
		{
			float num4 = ImGui.CalcTextSize(distance).X * num2;
			Theme.DrawText(P_0, new Vector2(num - num4, cursorScreenPos.Y + (P_4 - Theme.FsCaption) * 0.5f), Theme.HudSecondaryText, distance, Theme.FsCaption, Theme.HudTextShadow);
		}
	}

	private static void _00A0(ImDrawListPtr P_0, HudCardModel P_1, float P_2)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		ImGui.Dummy(new Vector2(1f, P_2));
		float num = cursorScreenPos.X + x - 8f * uiScale;
		float num2 = ((Theme.FsBody > 0f) ? (Theme.FsCaption / Theme.FsBody) : 1f);
		string text = $"{P_1.SMobHpPercent:0.0}%";
		float num3 = ImGui.CalcTextSize(text).X * num2;
		float maxWidth = Math.Max(10f, num - cursorScreenPos.X - num3 - 8f * uiScale);
		string text2 = Theme.Ellipsis(P_1.SMobName, maxWidth);
		float num4 = 4f * uiScale;
		float num5 = P_2 - num4;
		Theme.DrawText(P_0, new Vector2(cursorScreenPos.X, cursorScreenPos.Y + (num5 - Theme.FsBody) * 0.5f), P_1.SMobColor, text2, Theme.FsBody, Theme.HudTextShadow);
		Theme.DrawText(P_0, new Vector2(num - num3, cursorScreenPos.Y + (num5 - Theme.FsCaption) * 0.5f), Theme.HudSecondaryText, text, Theme.FsCaption, Theme.HudTextShadow);
		float num6 = cursorScreenPos.Y + num5;
		P_0.AddRectFilled(new Vector2(cursorScreenPos.X, num6), new Vector2(num, num6 + num4), Theme.U32(Theme.ProgressTrack), num4 * 0.5f);
		float num7 = Math.Clamp(P_1.SMobHpPercent / 100f, 0f, 1f);
		Vector4 c;
		if (P_1.SMobHpPercent > 60f)
		{
			c = Theme.ProgressFill;
		}
		else
		{
			c = ((P_1.SMobHpPercent >= 30f) ? Theme.ProgressFillWarn : Theme.ProgressFillDanger);
		}
		float num8 = num7 * (num - cursorScreenPos.X);
		if (num8 > 0.5f)
		{
			P_0.AddRectFilled(new Vector2(cursorScreenPos.X, num6), new Vector2(cursorScreenPos.X + num8, num6 + num4), Theme.U32(c), num4 * 0.5f);
		}
	}

	private static void _2000(HudCardModel P_0, float P_1)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float num = 22f * uiScale;
		_00A0(windowDrawList, cursorScreenPos, num, P_1, "▸ " + P_0.StageText, Theme.StatusInfo, Theme.FsCaption, 2f * uiScale);
		ImGui.SetCursorScreenPos(cursorScreenPos + new Vector2(0f, num));
	}

	private static void _2001(HudCardModel P_0, float P_1)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float num = 22f * uiScale;
		string text = ((!string.IsNullOrEmpty(P_0.TargetDistance)) ? (P_0.TargetLabel + "  " + P_0.TargetDistance) : P_0.TargetLabel);
		_00A0(windowDrawList, cursorScreenPos, num, P_1, text, P_0.TargetColor, Theme.FsBody);
		ImGui.SetCursorScreenPos(cursorScreenPos + new Vector2(0f, num));
	}

	private static void _00A0(ImDrawListPtr P_0, Vector2 P_1, float P_2, float P_3, string P_4, Vector4 P_5, float P_6, float P_7 = 0f)
	{
		float uiScale = Theme.UiScale;
		if (!string.IsNullOrEmpty(P_4))
		{
			float fontSize = ImGui.GetFontSize();
			float num = ((fontSize > 0f) ? (P_6 / fontSize) : 1f);
			string text = Theme.Ellipsis(P_4, Math.Max(8f, (P_3 - P_7) / num));
			Vector2 vector = ImGui.CalcTextSize(text);
			float num2 = vector.X * num;
			float num3 = vector.Y * num;
			float num4 = 4f * uiScale;
			float num5 = 1f * uiScale;
			Vector2 pos = new Vector2(P_1.X + P_7, P_1.Y + (P_2 - num3) * 0.5f);
			P_0.AddRectFilled(new Vector2(pos.X - num4, pos.Y - num5), new Vector2(pos.X + num2 + num4, pos.Y + num3 + num5), Theme.U32(Theme.SurfaceCard), 3f * uiScale);
			Theme.DrawText(P_0, pos, P_5, text, P_6, Theme.HudTextShadow);
		}
	}

	private static void _2002(HudCardModel P_0, float P_1)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float y = 26f * uiScale;
		windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(P_1, y), Theme.U32(Theme.SurfaceCard), 6f * uiScale);
		_2000_206D.Text = P_0.ChainText;
		_2000_206D.TextColor = Theme.TextSecondary;
		_2000_206D.ShowIncrease = P_0.ChainCanAdjust;
		_2000_206D.ShowDecrease = P_0.ChainCanAdjust;
		_2000_206D.IncreaseEnabled = P_0.ChainCanAdjust;
		_2000_206D.DecreaseEnabled = P_0.ChainCanDecrease;
		_2000_206D.IncreaseTooltip = "当前站已击杀数量 +1；达标后自动进入下一站";
		_2000_206D.DecreaseTooltip = "当前站已击杀数量 -1，最低为 0";
		_2000_206D.OnAdjust = P_0.OnChainAdjust;
		_2000_206D.TrailingLabel = "停止";
		_2000_206D.TrailingTooltip = "停止当前连车";
		_2000_206D.TrailingEnabled = true;
		_2000_206D.OnTrailing = P_0.OnChainStop;
		_2000_206D.TrailingButtonProbe = ChainStopButtonProbe;
		_2000_206D.GeometryProbe = ChainRowGeometryProbe;
		_2000_206D.Draw("##hudChainRow");
	}

	private static void _2003(HudCardModel P_0, float P_1)
	{
		float uiScale = Theme.UiScale;
		bool compact = P_0.Compact;
		float sideWidth = (compact ? 40f : 28f);
		float num = (compact ? 40f : 26f) * uiScale;
		UiLayout.MainAndSide("##hudButtons", sideWidth, 2f, (int num2) =>
		{
			if (num2 == 0)
			{
				float x = ImGui.GetContentRegionAvail().X;
				if (UiButton.Draw(size: new Vector2(x, num), label: compact ? _00A0(P_0.MainButtonLabel) : P_0.MainButtonLabel, variant: P_0.MainButtonVariant, tooltip: P_0.MainButtonTooltip, enabled: P_0.MainButtonEnabled))
				{
					P_0.OnMain?.Invoke();
				}
			}
			else
			{
				float x2 = ImGui.GetContentRegionAvail().X;
				float val = (compact ? 40f : 26f) * uiScale;
				float num3 = Math.Min(x2, Math.Min(num, val));
				Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
				ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + (x2 - num3) * 0.5f, cursorScreenPos.Y + (num - num3) * 0.5f));
				string tooltip = GearTooltip(P_0.SettingsOpen);
				if (UiButton.DrawGear(new Vector2(num3, num3), tooltip, enabled: true, () =>
				{
					P_0.OnCloseHud?.Invoke();
				}))
				{
					P_0.OnSettings?.Invoke();
				}
			}
		});
	}

	private static string _00A0(string P_0)
	{
		if (!string.IsNullOrEmpty(P_0) && P_0.Length > 2)
		{
			return P_0.Substring(0, 2);
		}
		return P_0;
	}
}
