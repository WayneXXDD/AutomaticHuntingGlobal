using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI;

public static class ErrorBoundary
{
	[CompilerGenerated]
	private sealed class _00A0
	{
		public string _2008_1680;

		public Exception _2008_2000;

		public AutomaticHunting _2008_2001;

		internal void _00A0(int P_0)
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (P_0 == 0)
			{
				if (UiButton.Draw("复制详情", UiVariant.Neutral, value, "复制异常类型与调用栈，便于反馈"))
				{
					ImGui.SetClipboardText(ErrorBoundary._00A0(_2008_1680, _2008_2000));
				}
			}
			else if (UiButton.Draw("关闭插件", UiVariant.DangerSolid, value, "反复出错时可用 /auht off 关闭"))
			{
				AutomaticHunting.Log.Error(_2008_2000, "AutomaticHunting: 界面异常导致插件自动停用");
				_2008_2001.Config.Enabled = false;
				_2008_2001.ConfigService.Save();
			}
		}
	}

	public static void Run(AutomaticHunting plugin, string scope, Action action)
	{
		try
		{
			action();
		}
		catch (Exception ex)
		{
			AutomaticHunting.Log.Error(ex, "AutomaticHunting: " + scope + " 绘制异常");
			_00A0(plugin, scope, ex);
		}
	}

	private static void _00A0(AutomaticHunting P_0, string P_1, Exception P_2)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		Vector2 contentRegionAvail = ImGui.GetContentRegionAvail();
		float rounding = 6f * uiScale;
		windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + contentRegionAvail, Theme.U32(Theme.WithAlpha(Theme.StatusDanger, 0.1f)), rounding);
		windowDrawList.AddRect(cursorScreenPos, cursorScreenPos + contentRegionAvail, Theme.U32(Theme.WithAlpha(Theme.StatusDanger, 0.55f)), rounding);
		string text = "「" + P_1 + "」绘制出错";
		Vector2 vector = ImGui.CalcTextSize(text);
		float num = 12f * uiScale;
		Vector2 vector2 = new Vector2(cursorScreenPos.X + contentRegionAvail.X - 12f * uiScale - num, cursorScreenPos.Y + 12f * uiScale);
		ImGui.SetCursorScreenPos(vector2);
		bool flag = ImGui.InvisibleButton("##errCopyIcon", new Vector2(num, num));
		bool flag2 = ImGui.IsItemHovered();
		UiIcons.DrawCross(windowDrawList, vector2 + new Vector2(num * 0.5f, num * 0.5f), num * 0.8f, Theme.U32(flag2 ? Theme.TextPrimary : Theme.TextDisabled));
		if (flag2)
		{
			ImGui.SetTooltip("复制完整堆栈");
		}
		if (flag)
		{
			ImGui.SetClipboardText(_00A0(P_1, P_2));
		}
		Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X + 12f * uiScale, cursorScreenPos.Y + 12f * uiScale), Theme.StatusDanger, text, Theme.FsBody);
		ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + 12f * uiScale, cursorScreenPos.Y + 12f * uiScale + vector.Y + 4f * uiScale));
		ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextSecondary);
		ImGui.TextWrapped(P_2.Message);
		ImGui.PopStyleColor();
		ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + 12f * uiScale, cursorScreenPos.Y + contentRegionAvail.Y - 12f * uiScale - Theme.ControlHeight));
		UiLayout.EqualColumns("##errActions", 2, 8f, (int num2) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (num2 == 0)
			{
				if (UiButton.Draw("复制详情", UiVariant.Neutral, value, "复制异常类型与调用栈，便于反馈"))
				{
					ImGui.SetClipboardText(_00A0(P_1, P_2));
				}
			}
			else if (UiButton.Draw("关闭插件", UiVariant.DangerSolid, value, "反复出错时可用 /auht off 关闭"))
			{
				AutomaticHunting.Log.Error(P_2, "AutomaticHunting: 界面异常导致插件自动停用");
				P_0.Config.Enabled = false;
				P_0.ConfigService.Save();
			}
		});
		ImGui.SetCursorScreenPos(cursorScreenPos + new Vector2(0f, contentRegionAvail.Y));
	}

	private static string _00A0(string P_0, Exception P_1)
	{
		return $"[{P_0}] {P_1.GetType().Name}: {P_1.Message}\n{P_1.StackTrace}";
	}
}
