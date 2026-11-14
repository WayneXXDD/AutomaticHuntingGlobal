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

	private static readonly string[] field_686 = new string[4] { "欢迎", "前置检查", "车头设置", "完成" };

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
		: base("首次运行向导###AutomaticHunting.Onboarding")
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
		ErrorBoundary.Run(field_687, "首次运行向导", Method_740);
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
		Theme.DrawText(windowDrawList, new Vector2(vector.X + num4 + 9f * uiScale, cursorScreenPos.Y + (num - Theme.FsTitle) * 0.5f), Theme.TextPrimary, "首次运行向导", Theme.FsTitle);
		string text = $"第 {field_689 + 1} / {field_686.Length} 步 · {field_686[field_689]}";
		Vector2 vector2 = ImGui.CalcTextSize(text);
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
		ImGui.TextWrapped("欢迎使用「自动狩猎跟车」。本向导会带你完成 3 件一次性设置；全部完成后即可直接用 HUD 悬浮窗控制跟车。");
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
		List<string> list = Theme.WrapText("自动化功能只限跟落地开的车，ET 和倒数车会产生不可预知的行为，玩家自行承担后果。反馈 bug 及建议请去 DC 频道「DR-插件讨论 / omni-社区插件」搜索 AutomaticHunting。", arg, Theme.FsBody);
		float num4 = (float)list.Count * textLineHeight;
		float y = num2 + fsCaption + num3 + num4 + num2;
		Vector2 arg2 = cursorScreenPos + new Vector2(x, y);
		UiPanel.DrawCard(cursorScreenPos, arg2, 6f * uiScale, Theme.WithAlpha(Theme.StatusWarning, 0.1f));
		Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X + num, cursorScreenPos.Y + num2), Theme.StatusWarning, "免责声明", Theme.FsCaption);
		float num5 = cursorScreenPos.Y + num2 + fsCaption + num3;
		for (int i = 0; i < list.Count; i++)
		{
			ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + num, num5 + (float)i * textLineHeight));
			ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextSecondary);
			ImGui.TextUnformatted(list[i]);
			ImGui.PopStyleColor();
		}
		ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + num, arg2.Y + 8f * uiScale));
		bool v = field_690;
		if (ImGui.Checkbox("##acceptDisclaimer", ref v))
		{
			field_690 = v;
		}
		ImGui.SameLine();
		ImGui.TextUnformatted("我已阅读并同意上述免责声明");
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
		ImGui.TextUnformatted("以下插件是自动狩猎的运行前提，每 2 秒自动复查一次：");
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		UiForm.Begin("##onboardDeps");
		Method_745("Lifestream", field_693, arg2: true);
		Method_745("vnavmesh", field_694, arg2: true);
		Method_745("BossModReborn", field_695, arg2: false);
		UiForm.End();
		if (!field_693 || !field_694)
		{
			ImGui.Dummy(new Vector2(1f, 4f * uiScale));
			UiBanner.Show("前置插件尚未就绪", "可以先继续完成设置；插件会在前置就绪后自动恢复可用（相关功能在缺失时会单独报错）。", Theme.StatusWarning);
		}
		else
		{
			ImGui.Dummy(new Vector2(1f, 4f * uiScale));
			UiBanner.Show("前置插件已就绪", "全部必需插件已加载，可以继续。", Theme.StatusSuccess);
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
		ImGui.TextWrapped("设置车头名（可留空）。留空时插件处于「待设置车头」的中性态，除跟车外的高级功能仍可使用。");
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		UiForm.Begin("##onboardLeader");
		UiForm.Row("车头名称", "输入名字后按 回车 添加（可添加多个，设置页里可勾选启用哪些）");
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
			text.AppendLiteral("已添加 ");
			text.AppendFormatted(num);
			text.AppendLiteral(" 个车头，其中 ");
			text.AppendFormatted(value);
			text.AppendLiteral(" 个生效");
			ImGui.TextDisabled(text);
		}
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		UiForm.Begin("##onboardQuick");
		UiForm.Row("快速设置");
		UiLayout.EqualColumns("##onboardQuickBtns", 2, 8f, (int arg0) =>
		{
			Vector2 value2 = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
			{
				if (UiButton.Draw("用当前目标", UiVariant.Neutral, value2, "把当前选中的目标加进车头列表"))
				{
					IGameObject target = AutomaticHunting.TargetManager.Target;
					if (target != null)
					{
						LeaderRoster.Add(field_687.Config, target.Name.TextValue);
						field_687.ConfigService.MarkDirty();
					}
					else
					{
						field_687.PrintEcho("未选中目标");
					}
				}
			}
			else if (UiButton.Draw("用我自己", UiVariant.Neutral, value2, "把自己设为车头"))
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
		UiForm.Row("免责声明");
		ImGui.TextUnformatted(field_687.Config.DisclaimerAccepted ? "已同意" : "未同意（插件保持受限）");
		UiForm.Row("前置插件");
		ImGui.TextUnformatted((field_693 && field_694) ? "已就绪" : "未就绪（功能会降级）");
		UiForm.Row("车头");
		ImGui.TextUnformatted(string.IsNullOrWhiteSpace(field_687.Config.LeaderName) ? "未设置（可稍后设置）" : field_687.Config.LeaderName);
		UiForm.End();
		ImGui.Dummy(new Vector2(1f, 8f * uiScale));
		ImGui.TextWrapped("完成后会立即启用自动狩猎。你随时可以用 /auht setup 重新打开本向导，或用 /auht off 进入受限模式。");
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
				if (UiButton.Draw("稍后再说", UiVariant.Ghost, value, "关闭向导；插件保持受限模式，可随时回来"))
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
				if (UiButton.Draw(flag ? "完成" : "下一步", UiVariant.Primary, value, flag ? "完成向导并启用插件" : "进入下一步", arg1))
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
		field_687.PrintEcho(field_690 ? "首次设置已完成，自动狩猎已启用" : "首次设置已完成（受限模式）");
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
			if (UiButton.Draw("用当前目标", UiVariant.Neutral, value, "把当前选中的目标加进车头列表"))
			{
				IGameObject target = AutomaticHunting.TargetManager.Target;
				if (target != null)
				{
					LeaderRoster.Add(field_687.Config, target.Name.TextValue);
					field_687.ConfigService.MarkDirty();
				}
				else
				{
					field_687.PrintEcho("未选中目标");
				}
			}
		}
		else if (UiButton.Draw("用我自己", UiVariant.Neutral, value, "把自己设为车头"))
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
			if (UiButton.Draw("稍后再说", UiVariant.Ghost, value, "关闭向导；插件保持受限模式，可随时回来"))
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
		if (UiButton.Draw(flag ? "完成" : "下一步", UiVariant.Primary, value, flag ? "完成向导并启用插件" : "进入下一步", arg2))
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
