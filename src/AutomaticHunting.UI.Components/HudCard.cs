using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class HudCard
{
	[CompilerGenerated]
	public sealed class RecoveredType_157
	{
		public float field_976;

		public bool field_977;

		public HudCardModel field_978;

		public float field_979;

		public Action field_980;

		internal void Method_1008(int arg0)
		{
			if (arg0 == 0)
			{
				float x = ImGui.GetContentRegionAvail().X;
				if (UiButton.Draw(arg2: new Vector2(x, field_976), arg0: field_977 ? Method_1005(field_978.MainButtonLabel) : field_978.MainButtonLabel, arg1: field_978.MainButtonVariant, arg3: field_978.MainButtonTooltip, arg4: field_978.MainButtonEnabled))
				{
					field_978.OnMain?.Invoke();
				}
				return;
			}
			float x2 = ImGui.GetContentRegionAvail().X;
			float val = (field_977 ? 40f : 26f) * field_979;
			float num = Math.Min(x2, Math.Min(field_976, val));
			Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
			ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + (x2 - num) * 0.5f, cursorScreenPos.Y + (field_976 - num) * 0.5f));
			string arg1 = GearTooltip(field_978.SettingsOpen);
			if (UiButton.DrawGear(new Vector2(num, num), arg1, arg2: true, () =>
			{
				field_978.OnCloseHud?.Invoke();
			}))
			{
				field_978.OnSettings?.Invoke();
			}
		}

		internal void Method_1009()
		{
			field_978.OnCloseHud?.Invoke();
		}
	}

	public static Action<HudInfoRowGeometry>? InfoRowProbe;

	public static Action<HudStatusRowGeometry>? StatusRowProbe;

	public static Action<Vector2, Vector2>? DragRegionProbe;

	public static Action<Vector2, Vector2>? ChainStopButtonProbe;

	public static Action<AdjustRowGeometry>? ChainRowGeometryProbe;

	private static readonly UiAdjustRow field_972 = new UiAdjustRow();

	private const float field_973 = 12f;

	private const float field_974 = 3.5f;

	private const float field_975 = 2f;

	public static string GearTooltip(bool arg0)
	{
		if (!arg0)
		{
			return "開啟插件設定 · 右鍵關閉狀態浮窗";
		}
		return "關閉插件設定 · 右鍵關閉狀態浮窗";
	}

	public static bool ToggleSettingsOpen(bool arg0)
	{
		return !arg0;
	}

	public static void Draw(HudCardModel arg0)
	{
		float uiScale = Theme.UiScale;
		float num = 220f * uiScale;
		Method_993(arg0, num);
		if (!arg0.Compact)
		{
			ImGui.Dummy(new Vector2(1f, 6f * uiScale));
			UiSection.Divider();
			ImGui.Dummy(new Vector2(1f, 6f * uiScale));
			if (arg0.OnboardingPending)
			{
				Method_994(num);
			}
			else
			{
				Method_995(arg0, num);
			}
			if (!string.IsNullOrEmpty(arg0.StageText))
			{
				ImGui.Dummy(new Vector2(1f, 6f * uiScale));
				Method_1000(arg0, num);
			}
		}
		else
		{
			ImGui.Dummy(new Vector2(1f, 6f * uiScale));
			Method_1001(arg0, num);
		}
		if (arg0.HasChainRow)
		{
			ImGui.Dummy(new Vector2(1f, 6f * uiScale));
			Method_1003(arg0, num);
		}
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		Method_1004(arg0, num);
		ImGui.Dummy(new Vector2(num, 0f));
	}

	private static void Method_993(HudCardModel arg0, float arg1)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = 20f * uiScale;
		windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(arg1, num), Theme.U32(Theme.SurfaceCard), 8f * uiScale);
		ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(0f, 0f));
		ImGui.BeginTable("##hudStatus", 3, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings);
		ImGui.TableSetupColumn("##pill", ImGuiTableColumnFlags.WidthStretch, 3.5f);
		ImGui.TableSetupColumn("##zone", ImGuiTableColumnFlags.WidthStretch, 2f);
		ImGui.TableSetupColumn("##lock", ImGuiTableColumnFlags.WidthFixed, 12f * uiScale);
		ImGui.TableNextRow();
		ImGui.TableNextColumn();
		float x = ImGui.GetContentRegionAvail().X;
		string text = Theme.Ellipsis(arg0.StatusGlyph + " " + arg0.StatusText, Math.Max(20f, x));
		Vector2 cursorScreenPos2 = ImGui.GetCursorScreenPos();
		Vector2 vector = cursorScreenPos2 + new Vector2(UiBadge.PillWidthOf(text), UiBadge.PillHeight());
		UiBadge.Pill(text, arg0.StatusColor);
		ImGui.TableNextColumn();
		Vector2 cursorScreenPos3 = ImGui.GetCursorScreenPos();
		float x2 = ImGui.GetContentRegionAvail().X;
		float arg2 = 0f;
		if (!string.IsNullOrEmpty(arg0.ZoneName) && x2 > 10f * uiScale)
		{
			string text2 = Theme.Ellipsis(arg0.ZoneName, x2, Theme.FsCaption);
			float num2 = Theme.MeasureTextWidth(text2, Theme.FsCaption);
			float num3 = Theme.MeasureTextHeight(Theme.FsCaption);
			float num4 = cursorScreenPos3.X + x2 - num2;
			float y = cursorScreenPos3.Y + (num - num3) * 0.5f;
			arg2 = num4 + num2;
			Theme.DrawText(windowDrawList, new Vector2(num4, y), Theme.HudSecondaryText, text2, Theme.FsCaption, Theme.HudTextShadow);
		}
		ImGui.TableNextColumn();
		Vector2 cursorScreenPos4 = ImGui.GetCursorScreenPos();
		Vector2 arg3 = cursorScreenPos4 + new Vector2(12f * uiScale, num);
		if (arg0.Locked)
		{
			Vector2 cursorScreenPos5 = ImGui.GetCursorScreenPos();
			float num5 = 12f * uiScale;
			UiIcons.DrawLock(windowDrawList, cursorScreenPos5 + new Vector2(num5 * 0.5f, num * 0.5f), num5, Theme.U32(Theme.BrandGold));
		}
		StatusRowProbe?.Invoke(new HudStatusRowGeometry(cursorScreenPos, cursorScreenPos + new Vector2(arg1, num), cursorScreenPos2, vector, cursorScreenPos4, arg3, arg0.Locked, text, arg2));
		ImGui.EndTable();
		ImGui.PopStyleVar();
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImGui.InvisibleButton("##hudDrag", new Vector2(arg1, num));
		DragRegionProbe?.Invoke(ImGui.GetItemRectMin(), ImGui.GetItemRectMax());
		bool flag = ImGui.IsItemHovered();
		if (flag)
		{
			ImGui.SetMouseCursor(ImGuiMouseCursor.Hand);
		}
		ImGuiIOPtr iO;
		if (!arg0.Locked && ImGui.IsItemActive() && ImGui.IsMouseDragging(ImGuiMouseButton.Left))
		{
			Action<Vector2>? onDragDelta = arg0.OnDragDelta;
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
				arg0.OnToggleEnabled?.Invoke();
			}
		}
		if (flag)
		{
			string? text3;
			if (flag2)
			{
				text3 = arg0.ToggleEnabledTooltip + " · 拖動可移動 狀態浮窗";
			}
			else
			{
				text3 = (arg0.Locked ? "位置已鎖定 · 在「概覽」頁解鎖" : "拖動頂部狀態行可移動 狀態浮窗");
			}
			TraditionalText.SetTooltip(text3);
		}
		ImGui.SetCursorScreenPos(cursorScreenPos + new Vector2(0f, num));
	}

	private static void Method_994(float arg0)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		string text = "尚未完成首次設定";
		string text2 = "需要先確認前置插件與免責宣告，";
		string arg1 = "並設定車頭";
		Vector2 vector = TraditionalText.CalcTextSize(text);
		Vector2 vector2 = TraditionalText.CalcTextSize(text2);
		float y = vector.Y + vector2.Y * 2f + 16f * uiScale;
		Vector2 arg2 = cursorScreenPos;
		Vector2 vector3 = cursorScreenPos + new Vector2(arg0, y);
		UiPanel.DrawCard(arg2, vector3, 6f * uiScale);
		float num = arg2.Y + 10f * uiScale;
		Theme.DrawText(windowDrawList, new Vector2(arg2.X + (arg0 - vector.X) * 0.5f, num), Theme.TextSecondary, text);
		num += vector.Y + 4f * uiScale;
		Theme.DrawText(windowDrawList, new Vector2(arg2.X + (arg0 - vector2.X) * 0.5f, num), Theme.TextDisabled, text2, Theme.FsCaption);
		Theme.DrawText(windowDrawList, new Vector2(y: num + vector2.Y, x: arg2.X + (arg0 - vector2.X) * 0.5f), Theme.TextDisabled, arg1, Theme.FsCaption);
		ImGui.SetCursorScreenPos(vector3);
	}

	private static void Method_995(HudCardModel arg0, float arg1)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		int num = 2 + (arg0.HasSMob ? 1 : 0);
		float num2 = (arg0.HasSMob ? (6f * uiScale) : 0f);
		float num3 = Math.Max(22f * uiScale - 6f * uiScale, Theme.FsBody);
		float num4 = num3 + 6f * uiScale;
		float y = (float)num * num4 + num2;
		Vector2 vector = cursorScreenPos + new Vector2(arg1, y);
		UiPanel.DrawCard(cursorScreenPos, vector, 6f * uiScale);
		float initWidthOrWeight = 8f * uiScale + Method_996() + 10f * uiScale;
		ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(0f, 3f * uiScale));
		ImGui.BeginTable("##hudInfo", 2, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings);
		ImGui.TableSetupColumn("##label", ImGuiTableColumnFlags.WidthFixed, initWidthOrWeight);
		ImGui.TableSetupColumn("##value", ImGuiTableColumnFlags.WidthStretch, 1f);
		ImGui.TableNextRow();
		ImGui.TableNextColumn();
		Vector2 cursorScreenPos2 = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		Method_997(windowDrawList, "目標", num3);
		ImGui.TableNextColumn();
		Vector2 cursorScreenPos3 = ImGui.GetCursorScreenPos();
		float x2 = ImGui.GetContentRegionAvail().X;
		Method_998(windowDrawList, arg0.TargetLabel, arg0.TargetColor, arg0.TargetDistance, num3);
		InfoRowProbe?.Invoke(new HudInfoRowGeometry(0, cursorScreenPos2, x, cursorScreenPos3, x2, num4));
		ImGui.TableNextRow();
		ImGui.TableNextColumn();
		cursorScreenPos2 = ImGui.GetCursorScreenPos();
		x = ImGui.GetContentRegionAvail().X;
		Method_997(windowDrawList, "車頭", num3);
		ImGui.TableNextColumn();
		cursorScreenPos3 = ImGui.GetCursorScreenPos();
		x2 = ImGui.GetContentRegionAvail().X;
		Method_998(windowDrawList, arg0.LeaderText, arg0.LeaderColor, null, num3);
		InfoRowProbe?.Invoke(new HudInfoRowGeometry(1, cursorScreenPos2, x, cursorScreenPos3, x2, num4));
		if (arg0.HasSMob)
		{
			ImGui.TableNextRow();
			ImGui.TableNextColumn();
			cursorScreenPos2 = ImGui.GetCursorScreenPos();
			x = ImGui.GetContentRegionAvail().X;
			Method_997(windowDrawList, "S怪", num3);
			ImGui.TableNextColumn();
			cursorScreenPos3 = ImGui.GetCursorScreenPos();
			x2 = ImGui.GetContentRegionAvail().X;
			Method_999(windowDrawList, arg0, num3 + num2);
			InfoRowProbe?.Invoke(new HudInfoRowGeometry(2, cursorScreenPos2, x, cursorScreenPos3, x2, num4 + num2));
		}
		ImGui.EndTable();
		ImGui.PopStyleVar();
		ImGui.SetCursorScreenPos(vector);
	}

	private static float Method_996()
	{
		float num = ((Theme.FsBody > 0f) ? (Theme.FsCaption / Theme.FsBody) : 1f);
		return Math.Max(Math.Max(TraditionalText.CalcTextSize("目標").X * num, TraditionalText.CalcTextSize("車頭").X * num), TraditionalText.CalcTextSize("S怪").X * num);
	}

	private static void Method_997(ImDrawListPtr arg0, string arg1, float arg2)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImGui.Dummy(new Vector2(1f, arg2));
		float x = cursorScreenPos.X + 8f * uiScale;
		float y = cursorScreenPos.Y + (arg2 - Theme.FsCaption) * 0.5f;
		Theme.DrawText(arg0, new Vector2(x, y), Theme.HudSecondaryText, arg1, Theme.FsCaption, Theme.HudTextShadow);
	}

	private static void Method_998(ImDrawListPtr arg0, string arg1, Vector4 arg2, string? arg3, float arg4)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		ImGui.Dummy(new Vector2(1f, arg4));
		float num = cursorScreenPos.X + x - 8f * uiScale;
		float num2 = ((Theme.FsBody > 0f) ? (Theme.FsCaption / Theme.FsBody) : 1f);
		float num3 = (string.IsNullOrEmpty(arg3) ? 0f : (TraditionalText.CalcTextSize(arg3).X * num2 + 8f * uiScale));
		float arg5 = Math.Max(10f, num - cursorScreenPos.X - num3);
		string arg6 = Theme.Ellipsis(arg1, arg5);
		float y = cursorScreenPos.Y + (arg4 - Theme.FsBody) * 0.5f;
		Theme.DrawText(arg0, new Vector2(cursorScreenPos.X, y), arg2, arg6, Theme.FsBody, Theme.HudTextShadow);
		if (!string.IsNullOrEmpty(arg3))
		{
			float num4 = TraditionalText.CalcTextSize(arg3).X * num2;
			Theme.DrawText(arg0, new Vector2(num - num4, cursorScreenPos.Y + (arg4 - Theme.FsCaption) * 0.5f), Theme.HudSecondaryText, arg3, Theme.FsCaption, Theme.HudTextShadow);
		}
	}

	private static void Method_999(ImDrawListPtr arg0, HudCardModel arg1, float arg2)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		ImGui.Dummy(new Vector2(1f, arg2));
		float num = cursorScreenPos.X + x - 8f * uiScale;
		float num2 = ((Theme.FsBody > 0f) ? (Theme.FsCaption / Theme.FsBody) : 1f);
		string text = $"{arg1.SMobHpPercent:0.0}%";
		float num3 = TraditionalText.CalcTextSize(text).X * num2;
		float arg3 = Math.Max(10f, num - cursorScreenPos.X - num3 - 8f * uiScale);
		string arg4 = Theme.Ellipsis(arg1.SMobName, arg3);
		float num4 = 4f * uiScale;
		float num5 = arg2 - num4;
		Theme.DrawText(arg0, new Vector2(cursorScreenPos.X, cursorScreenPos.Y + (num5 - Theme.FsBody) * 0.5f), arg1.SMobColor, arg4, Theme.FsBody, Theme.HudTextShadow);
		Theme.DrawText(arg0, new Vector2(num - num3, cursorScreenPos.Y + (num5 - Theme.FsCaption) * 0.5f), Theme.HudSecondaryText, text, Theme.FsCaption, Theme.HudTextShadow);
		float num6 = cursorScreenPos.Y + num5;
		arg0.AddRectFilled(new Vector2(cursorScreenPos.X, num6), new Vector2(num, num6 + num4), Theme.U32(Theme.ProgressTrack), num4 * 0.5f);
		float num7 = Math.Clamp(arg1.SMobHpPercent / 100f, 0f, 1f);
		Vector4 arg5;
		if (arg1.SMobHpPercent > 60f)
		{
			arg5 = Theme.ProgressFill;
		}
		else
		{
			arg5 = ((arg1.SMobHpPercent >= 30f) ? Theme.ProgressFillWarn : Theme.ProgressFillDanger);
		}
		float num8 = num7 * (num - cursorScreenPos.X);
		if (num8 > 0.5f)
		{
			arg0.AddRectFilled(new Vector2(cursorScreenPos.X, num6), new Vector2(cursorScreenPos.X + num8, num6 + num4), Theme.U32(arg5), num4 * 0.5f);
		}
	}

	private static void Method_1000(HudCardModel arg0, float arg1)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float num = 22f * uiScale;
		Method_1002(windowDrawList, cursorScreenPos, num, arg1, "> " + arg0.StageText, Theme.StatusInfo, Theme.FsCaption, 2f * uiScale);
		ImGui.SetCursorScreenPos(cursorScreenPos + new Vector2(0f, num));
	}

	private static void Method_1001(HudCardModel arg0, float arg1)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float num = 22f * uiScale;
		string arg2 = ((!string.IsNullOrEmpty(arg0.TargetDistance)) ? (arg0.TargetLabel + "  " + arg0.TargetDistance) : arg0.TargetLabel);
		Method_1002(windowDrawList, cursorScreenPos, num, arg1, arg2, arg0.TargetColor, Theme.FsBody);
		ImGui.SetCursorScreenPos(cursorScreenPos + new Vector2(0f, num));
	}

	private static void Method_1002(ImDrawListPtr arg0, Vector2 arg1, float arg2, float arg3, string arg4, Vector4 arg5, float arg6, float arg7 = 0f)
	{
		float uiScale = Theme.UiScale;
		if (!string.IsNullOrEmpty(arg4))
		{
			float fontSize = ImGui.GetFontSize();
			float num = ((fontSize > 0f) ? (arg6 / fontSize) : 1f);
			string text = Theme.Ellipsis(arg4, Math.Max(8f, (arg3 - arg7) / num));
			Vector2 vector = TraditionalText.CalcTextSize(text);
			float num2 = vector.X * num;
			float num3 = vector.Y * num;
			float num4 = 4f * uiScale;
			float num5 = 1f * uiScale;
			Vector2 arg8 = new Vector2(arg1.X + arg7, arg1.Y + (arg2 - num3) * 0.5f);
			arg0.AddRectFilled(new Vector2(arg8.X - num4, arg8.Y - num5), new Vector2(arg8.X + num2 + num4, arg8.Y + num3 + num5), Theme.U32(Theme.SurfaceCard), 3f * uiScale);
			Theme.DrawText(arg0, arg8, arg5, text, arg6, Theme.HudTextShadow);
		}
	}

	private static void Method_1003(HudCardModel arg0, float arg1)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float y = 26f * uiScale;
		windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(arg1, y), Theme.U32(Theme.SurfaceCard), 6f * uiScale);
		field_972.Text = arg0.ChainText;
		field_972.TextColor = Theme.TextSecondary;
		field_972.ShowIncrease = arg0.ChainCanAdjust;
		field_972.ShowDecrease = arg0.ChainCanAdjust;
		field_972.IncreaseEnabled = arg0.ChainCanAdjust;
		field_972.DecreaseEnabled = arg0.ChainCanDecrease;
		field_972.IncreaseTooltip = "目前站已擊殺數量 +1；達標後自動進入下一站";
		field_972.DecreaseTooltip = "目前站已擊殺數量 -1，最低為 0";
		field_972.OnAdjust = arg0.OnChainAdjust;
		field_972.TrailingLabel = "停止";
		field_972.TrailingTooltip = "停止目前連車";
		field_972.TrailingEnabled = true;
		field_972.OnTrailing = arg0.OnChainStop;
		field_972.TrailingButtonProbe = ChainStopButtonProbe;
		field_972.GeometryProbe = ChainRowGeometryProbe;
		field_972.Draw("##hudChainRow");
	}

	private static void Method_1004(HudCardModel arg0, float arg1)
	{
		float field_979 = Theme.UiScale;
		bool field_980 = arg0.Compact;
		float arg2 = (field_980 ? 40f : 28f);
		float field_981 = (field_980 ? 40f : 26f) * field_979;
		UiLayout.MainAndSide("##hudButtons", arg2, 2f, (int num) =>
		{
			if (num == 0)
			{
				float x = ImGui.GetContentRegionAvail().X;
				if (UiButton.Draw(arg2: new Vector2(x, field_981), arg0: field_980 ? Method_1005(arg0.MainButtonLabel) : arg0.MainButtonLabel, arg1: arg0.MainButtonVariant, arg3: arg0.MainButtonTooltip, arg4: arg0.MainButtonEnabled))
				{
					arg0.OnMain?.Invoke();
				}
			}
			else
			{
				float x2 = ImGui.GetContentRegionAvail().X;
				float val = (field_980 ? 40f : 26f) * field_979;
				float num2 = Math.Min(x2, Math.Min(field_981, val));
				Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
				ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + (x2 - num2) * 0.5f, cursorScreenPos.Y + (field_981 - num2) * 0.5f));
				string arg3 = GearTooltip(arg0.SettingsOpen);
				if (UiButton.DrawGear(new Vector2(num2, num2), arg3, arg2: true, () =>
				{
					arg0.OnCloseHud?.Invoke();
				}))
				{
					arg0.OnSettings?.Invoke();
				}
			}
		});
	}

	private static string Method_1005(string arg0)
	{
		if (!string.IsNullOrEmpty(arg0) && arg0.Length > 2)
		{
			return arg0.Substring(0, 2);
		}
		return arg0;
	}
}
