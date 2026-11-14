using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI;

public static class ErrorBoundary
{
	[CompilerGenerated]
	private sealed class RecoveredType_112
	{
		public string field_662;

		public Exception field_663;

		public AutomaticHunting field_664;

		internal void Method_695(int arg0)
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
			{
				if (UiButton.Draw("复制详情", UiVariant.Neutral, value, "复制异常类型与调用栈，便于反馈"))
				{
					ImGui.SetClipboardText(Method_693(field_662, field_663));
				}
			}
			else if (UiButton.Draw("关闭插件", UiVariant.DangerSolid, value, "反复出错时可用 /auht off 关闭"))
			{
				AutomaticHunting.Log.Error(field_663, "AutomaticHunting: 界面异常导致插件自动停用");
				field_664.Config.Enabled = false;
				field_664.ConfigService.Save();
			}
		}
	}

	public static void Run(AutomaticHunting arg0, string arg1, Action arg2)
	{
		try
		{
			arg2();
		}
		catch (Exception ex)
		{
			AutomaticHunting.Log.Error(ex, "AutomaticHunting: " + arg1 + " 绘制异常");
			Method_692(arg0, arg1, ex);
		}
	}

	private static void Method_692(AutomaticHunting arg0, string arg1, Exception arg2)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		Vector2 contentRegionAvail = ImGui.GetContentRegionAvail();
		float rounding = 6f * uiScale;
		windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + contentRegionAvail, Theme.U32(Theme.WithAlpha(Theme.StatusDanger, 0.1f)), rounding);
		windowDrawList.AddRect(cursorScreenPos, cursorScreenPos + contentRegionAvail, Theme.U32(Theme.WithAlpha(Theme.StatusDanger, 0.55f)), rounding);
		string text = "「" + arg1 + "」绘制出错";
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
			ImGui.SetClipboardText(Method_693(arg1, arg2));
		}
		Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X + 12f * uiScale, cursorScreenPos.Y + 12f * uiScale), Theme.StatusDanger, text, Theme.FsBody);
		ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + 12f * uiScale, cursorScreenPos.Y + 12f * uiScale + vector.Y + 4f * uiScale));
		ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextSecondary);
		ImGui.TextWrapped(arg2.Message);
		ImGui.PopStyleColor();
		ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + 12f * uiScale, cursorScreenPos.Y + contentRegionAvail.Y - 12f * uiScale - Theme.ControlHeight));
		UiLayout.EqualColumns("##errActions", 2, 8f, (int num2) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (num2 == 0)
			{
				if (UiButton.Draw("复制详情", UiVariant.Neutral, value, "复制异常类型与调用栈，便于反馈"))
				{
					ImGui.SetClipboardText(Method_693(arg1, arg2));
				}
			}
			else if (UiButton.Draw("关闭插件", UiVariant.DangerSolid, value, "反复出错时可用 /auht off 关闭"))
			{
				AutomaticHunting.Log.Error(arg2, "AutomaticHunting: 界面异常导致插件自动停用");
				arg0.Config.Enabled = false;
				arg0.ConfigService.Save();
			}
		});
		ImGui.SetCursorScreenPos(cursorScreenPos + new Vector2(0f, contentRegionAvail.Y));
	}

	private static string Method_693(string arg0, Exception arg1)
	{
		return $"[{arg0}] {arg1.GetType().Name}: {arg1.Message}\n{arg1.StackTrace}";
	}
}
