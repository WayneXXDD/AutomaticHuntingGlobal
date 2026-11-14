using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Windowing;

namespace AutomaticHunting.UI;

public sealed class OnboardingWindow : Window
{
	private const float field_684 = 480f;

	private const float field_685 = 380f;

	private static readonly string[] field_686 = new string[4] { "歡迎", "前置檢查", "車頭設定", "完成" };

	private readonly AutomaticHunting field_687;

	private Theme.ThemeScope? field_688;

	private int field_689;

	private bool field_690;

	private string field_691 = string.Empty;

	private double field_692;

	private bool field_693;

	private bool field_694;

	private bool field_695;

	public OnboardingWindow(AutomaticHunting arg0)
		: base("首次執行設定精靈###AutomaticHunting.Onboarding")
	{
		field_687 = arg0;
		RespectCloseHotkey = false;
		Size = new Vector2(480f, 380f);
		SizeCondition = ImGuiCond.FirstUseEver;
		SizeConstraints = new WindowSizeConstraints
		{
			MinimumSize = new Vector2(480f, 380f),
			MaximumSize = new Vector2(2400f, 1600f)
		};
		Flags = ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse;
		BgAlpha = null;
		field_689 = Math.Clamp(arg0.Config.OnboardingStep, 0, field_686.Length - 1);
		field_690 = arg0.Config.DisclaimerAccepted;
	}

	public void Open()
	{
		field_689 = Math.Clamp(field_687.Config.OnboardingStep, 0, field_686.Length - 1);
		field_690 = field_687.Config.DisclaimerAccepted;
		IsOpen = true;
	}

	public override void PreDraw()
	{
		field_688?.Dispose();
		field_688 = Theme.Push(ThemeContext.Settings, 0.97f);
	}

	public override void PostDraw()
	{
		field_688?.Dispose();
		field_688 = null;
	}

	public override void Draw()
	{
		ErrorBoundary.Run(field_687, "首次執行設定精靈", Method_740);
	}

	private void Method_740()
	{
		float uiScale = Theme.UiScale;
		UiPanel.DrawWindowFrame(8f * uiScale);
		Method_741();
		Method_742();
		Method_748();
	}

	private void Method_741()
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = 32f * uiScale;
		float num2 = 18f * uiScale;
		float num3 = cursorScreenPos.Y + (num - num2) * 0.5f;
		windowDrawList.AddRectFilled(new Vector2(cursorScreenPos.X, num3), new Vector2(cursorScreenPos.X + 3f * uiScale, num3 + num2), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		Vector2 vector = new Vector2(cursorScreenPos.X + 3f * uiScale + 9f * uiScale, num3 + num2 * 0.5f);
		float num4 = 5f * uiScale;
		windowDrawList.AddLine(new Vector2(vector.X, vector.Y - num4), new Vector2(vector.X + num4, vector.Y), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		windowDrawList.AddLine(new Vector2(vector.X + num4, vector.Y), new Vector2(vector.X, vector.Y + num4), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		windowDrawList.AddLine(new Vector2(vector.X, vector.Y + num4), new Vector2(vector.X - num4, vector.Y), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		windowDrawList.AddLine(new Vector2(vector.X - num4, vector.Y), new Vector2(vector.X, vector.Y - num4), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		Theme.DrawText(windowDrawList, new Vector2(vector.X + num4 + 9f * uiScale, cursorScreenPos.Y + (num - Theme.FsTitle) * 0.5f), Theme.TextPrimary, "首次執行設定精靈", Theme.FsTitle);
		string text = $"第 {field_689 + 1} / {field_686.Length} 步 · {field_686[field_689]}";
		Vector2 vector2 = TraditionalText.CalcTextSize(text);
		Theme.DrawText(windowDrawList, new Vector2(Theme.ContentRightX() - vector2.X, cursorScreenPos.Y + (num - vector2.Y) * 0.5f), Theme.TextSecondary, text, Theme.FsCaption);
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImGui.Dummy(new Vector2(1f, 44f * uiScale));
		UiSection.Divider();
	}

	private void Method_742()
	{
		switch (field_689)
		{
		case 0:
			Method_743();
			break;
		case 1:
			Method_744();
			break;
		case 2:
			Method_746();
			break;
		default:
			Method_747();
			break;
		}
	}

	private void Method_743()
	{
		float uiScale = Theme.UiScale;
		TraditionalText.TextWrapped("歡迎使用「自動狩獵跟車」。本設定精靈會帶你完成 3 件一次性設定；全部完成後即可直接用 狀態浮窗 狀態浮窗控制跟車。");
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = 12f * uiScale;
		float num2 = 12f * uiScale;
		float num3 = 6f * uiScale;
		float x = ImGui.GetContentRegionAvail().X;
		float arg = x - 2f * num;
		float fsCaption = Theme.FsCaption;
		float textLineHeight = ImGui.GetTextLineHeight();
		List<string> list = Theme.WrapText("此功能適用抵達後立即開戰的狩獵隊伍。使用艾奧傑亞時間或倒數開戰的隊伍時，請自行確認動作時機。問題回報請至本專案的 GitHub 頁面。", arg, Theme.FsBody);
		float num4 = (float)list.Count * textLineHeight;
		float y = num2 + fsCaption + num3 + num4 + num2;
		Vector2 arg2 = cursorScreenPos + new Vector2(x, y);
		UiPanel.DrawCard(cursorScreenPos, arg2, 6f * uiScale, Theme.WithAlpha(Theme.StatusWarning, 0.1f));
		Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X + num, cursorScreenPos.Y + num2), Theme.StatusWarning, "免責宣告", Theme.FsCaption);
		float num5 = cursorScreenPos.Y + num2 + fsCaption + num3;
		for (int i = 0; i < list.Count; i++)
		{
			ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + num, num5 + (float)i * textLineHeight));
			ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextSecondary);
			TraditionalText.TextUnformatted(list[i]);
			ImGui.PopStyleColor();
		}
		ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + num, arg2.Y + 8f * uiScale));
		bool v = field_690;
		if (ImGui.Checkbox("##acceptDisclaimer", ref v))
		{
			field_690 = v;
		}
		ImGui.SameLine();
		TraditionalText.TextUnformatted("我已閱讀並同意上述免責宣告");
	}

	private void Method_744()
	{
		float uiScale = Theme.UiScale;
		double time = ImGui.GetTime();
		if (time - field_692 >= 2.0)
		{
			field_692 = time;
			field_693 = AutomaticHunting.IsPluginLoaded("Lifestream");
			field_694 = AutomaticHunting.IsPluginLoaded("vnavmesh");
			field_695 = field_687.IsBmrLoaded();
		}
		TraditionalText.TextUnformatted("以下插件是自動狩獵的執行前提，每 2 秒自動複查一次：");
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		UiForm.Begin("##onboardDeps");
		Method_745("Lifestream", field_693, arg2: true);
		Method_745("vnavmesh", field_694, arg2: true);
		Method_745("BossModReborn", field_695, arg2: false);
		UiForm.End();
		if (!field_693 || !field_694)
		{
			ImGui.Dummy(new Vector2(1f, 4f * uiScale));
			UiBanner.Show("前置插件尚未就緒", "可以先繼續完成設定；插件會在前置就緒後自動恢復可用（相關功能在缺失時會單獨報錯）。", Theme.StatusWarning);
		}
		else
		{
			ImGui.Dummy(new Vector2(1f, 4f * uiScale));
			UiBanner.Show("前置插件已就緒", "全部必需插件已載入，可以繼續。", Theme.StatusSuccess);
		}
	}

	private static void Method_745(string arg0, bool arg1, bool arg2)
	{
		var (arg3, arg4, arg5) = UiState.DependencyState(arg0, arg1, arg2);
		UiForm.Row(arg0);
		UiBadge.Dot(arg4, arg3, arg5);
	}

	private void Method_746()
	{
		float uiScale = Theme.UiScale;
		TraditionalText.TextWrapped("設定車頭名（可留空）。留空時插件處於「待設定車頭」的中性態，除跟車外的高級功能仍可使用。");
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		UiForm.Begin("##onboardLeader");
		UiForm.Row("車頭名稱", "輸入名字後按 回車 新增（可新增多個，設定頁裡可勾選啟用哪些）");
		UiForm.Fill();
		if (ImGui.InputText("##onboardLeaderName", ref field_691, 128, ImGuiInputTextFlags.EnterReturnsTrue))
		{
			Method_750();
		}
		UiForm.End();
		var (value, num) = LeaderRoster.Counts(field_687.Config);
		if (num > 0)
		{
			ImGui.Dummy(new Vector2(1f, 4f * uiScale));
			ImU8String text = new ImU8String(16, 2);
			text.AppendLiteral("已新增 ");
			text.AppendFormatted(num);
			text.AppendLiteral(" 個車頭，其中 ");
			text.AppendFormatted(value);
			text.AppendLiteral(" 個生效");
			ImGui.TextDisabled(text);
		}
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		UiForm.Begin("##onboardQuick");
		UiForm.Row("快速設定");
		UiLayout.EqualColumns("##onboardQuickBtns", 2, 8f, (int arg0) =>
		{
			Vector2 value2 = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
			{
				if (UiButton.Draw("用目前目標", UiVariant.Neutral, value2, "把目前選中的目標加進車頭列表"))
				{
					IGameObject target = AutomaticHunting.TargetManager.Target;
					if (target != null)
					{
						LeaderRoster.Add(field_687.Config, target.Name.TextValue);
						field_687.ConfigService.MarkDirty();
					}
					else
					{
						field_687.PrintEcho("未選中目標");
					}
				}
			}
			else if (UiButton.Draw("用我自己", UiVariant.Neutral, value2, "把自己設為車頭"))
			{
				field_687.SetLeaderToSelf();
			}
		});
		UiForm.End();
	}

	private void Method_747()
	{
		float uiScale = Theme.UiScale;
		UiForm.Begin("##onboardSummary");
		UiForm.Row("免責宣告");
		TraditionalText.TextUnformatted(field_687.Config.DisclaimerAccepted ? "已同意" : "未同意（插件保持受限）");
		UiForm.Row("前置插件");
		TraditionalText.TextUnformatted((field_693 && field_694) ? "已就緒" : "未就緒（功能會降級）");
		UiForm.Row("車頭");
		TraditionalText.TextUnformatted(string.IsNullOrWhiteSpace(field_687.Config.LeaderName) ? "未設定（可稍後設定）" : field_687.Config.LeaderName);
		UiForm.End();
		ImGui.Dummy(new Vector2(1f, 8f * uiScale));
		TraditionalText.TextWrapped("完成後會立即啟用自動狩獵。你隨時可以用 /auht setup 重新開啟本設定精靈，或用 /auht off 進入受限模式。");
	}

	private void Method_748()
	{
		float uiScale = Theme.UiScale;
		ImGui.Separator();
		ImGui.Dummy(new Vector2(1f, 4f * uiScale));
		UiLayout.WeightedColumns("##onboardFooter", new float[3] { 1f, 1f, 2f }, 8f, 0f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			switch (arg0)
			{
			case 0:
				if (UiButton.Draw("稍後再說", UiVariant.Ghost, value, "關閉設定精靈；插件保持受限模式，可隨時回來"))
				{
					IsOpen = false;
				}
				break;
			case 1:
			{
				bool arg2 = field_689 > 0;
				if (UiButton.Draw("上一步", UiVariant.Neutral, value, "返回上一步", arg2))
				{
					Method_749(field_689 - 1);
				}
				break;
			}
			default:
			{
				bool flag = field_689 == field_686.Length - 1;
				bool arg1 = field_689 != 0 || field_690;
				if (UiButton.Draw(flag ? "完成" : "下一步", UiVariant.Primary, value, flag ? "完成設定精靈並啟用插件" : "進入下一步", arg1))
				{
					if (flag)
					{
						Method_751();
					}
					else
					{
						Method_749(field_689 + 1);
					}
				}
				break;
			}
			}
		});
		ImGui.Dummy(new Vector2(1f, 4f * uiScale));
	}

	private void Method_749(int arg0)
	{
		Method_750();
		field_689 = Math.Clamp(arg0, 0, field_686.Length - 1);
		field_687.Config.OnboardingStep = field_689;
		field_687.ConfigService.MarkDirty();
	}

	private void Method_750()
	{
		if (!string.IsNullOrWhiteSpace(field_691))
		{
			LeaderRoster.Add(field_687.Config, field_691);
			field_691 = string.Empty;
			field_687.ConfigService.MarkDirty();
		}
	}

	private void Method_751()
	{
		PluginConfiguration config = field_687.Config;
		Method_750();
		config.DisclaimerAccepted = field_690;
		config.OnboardingCompleted = true;
		config.OnboardingStep = 0;
		config.Enabled = field_690;
		config.HudVisible = true;
		field_687.ConfigService.Save();
		field_687.PrintEcho(field_690 ? "首次設定已完成，自動狩獵已啟用" : "首次設定已完成（受限模式）");
		field_687.HelpWindow.IsOpen = true;
		field_687.SettingsWindow.IsOpen = true;
		field_687.HudWindow.IsOpen = true;
		IsOpen = false;
	}

	[CompilerGenerated]
	private void Method_753(int arg0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (arg0 == 0)
		{
			if (UiButton.Draw("用目前目標", UiVariant.Neutral, value, "把目前選中的目標加進車頭列表"))
			{
				IGameObject target = AutomaticHunting.TargetManager.Target;
				if (target != null)
				{
					LeaderRoster.Add(field_687.Config, target.Name.TextValue);
					field_687.ConfigService.MarkDirty();
				}
				else
				{
					field_687.PrintEcho("未選中目標");
				}
			}
		}
		else if (UiButton.Draw("用我自己", UiVariant.Neutral, value, "把自己設為車頭"))
		{
			field_687.SetLeaderToSelf();
		}
	}

	[CompilerGenerated]
	private void Method_754(int arg0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		switch (arg0)
		{
		case 0:
			if (UiButton.Draw("稍後再說", UiVariant.Ghost, value, "關閉設定精靈；插件保持受限模式，可隨時回來"))
			{
				IsOpen = false;
			}
			return;
		case 1:
		{
			bool arg1 = field_689 > 0;
			if (UiButton.Draw("上一步", UiVariant.Neutral, value, "返回上一步", arg1))
			{
				Method_749(field_689 - 1);
			}
			return;
		}
		}
		bool flag = field_689 == field_686.Length - 1;
		bool arg2 = field_689 != 0 || field_690;
		if (UiButton.Draw(flag ? "完成" : "下一步", UiVariant.Primary, value, flag ? "完成設定精靈並啟用插件" : "進入下一步", arg2))
		{
			if (flag)
			{
				Method_751();
			}
			else
			{
				Method_749(field_689 + 1);
			}
		}
	}
}
