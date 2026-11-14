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
	private const float _2000_2049 = 480f;

	private const float _2000_204A = 380f;

	private static readonly string[] _2000_204B = new string[4] { "欢迎", "前置检查", "车头设置", "完成" };

	private readonly AutomaticHunting _2000_204C;

	private Theme.ThemeScope? _2000_204D;

	private int _2000_204E;

	private bool _2000_204F;

	private string _2000_2050 = string.Empty;

	private double _2000_2051;

	private bool _2000_2052;

	private bool _2000_2053;

	private bool _2000_2054;

	public OnboardingWindow(AutomaticHunting plugin)
		: base("首次运行向导###AutomaticHunting.Onboarding")
	{
		_2000_204C = plugin;
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
		_2000_204E = Math.Clamp(plugin.Config.OnboardingStep, 0, _2000_204B.Length - 1);
		_2000_204F = plugin.Config.DisclaimerAccepted;
	}

	public void Open()
	{
		_2000_204E = Math.Clamp(_2000_204C.Config.OnboardingStep, 0, _2000_204B.Length - 1);
		_2000_204F = _2000_204C.Config.DisclaimerAccepted;
		IsOpen = true;
	}

	public override void PreDraw()
	{
		_2000_204D?.Dispose();
		_2000_204D = Theme.Push(ThemeContext.Settings, 0.97f);
	}

	public override void PostDraw()
	{
		_2000_204D?.Dispose();
		_2000_204D = null;
	}

	public override void Draw()
	{
		ErrorBoundary.Run(_2000_204C, "首次运行向导", _00A0);
	}

	private void _00A0()
	{
		float uiScale = Theme.UiScale;
		UiPanel.DrawWindowFrame(8f * uiScale);
		_1680();
		_2000();
		_2005();
	}

	private void _1680()
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
		string text = $"第 {_2000_204E + 1} / {_2000_204B.Length} 步 · {_2000_204B[_2000_204E]}";
		Vector2 vector2 = ImGui.CalcTextSize(text);
		Theme.DrawText(windowDrawList, new Vector2(Theme.ContentRightX() - vector2.X, cursorScreenPos.Y + (num - vector2.Y) * 0.5f), Theme.TextSecondary, text, Theme.FsCaption);
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImGui.Dummy(new Vector2(1f, 44f * uiScale));
		UiSection.Divider();
	}

	private void _2000()
	{
		switch (_2000_204E)
		{
		case 0:
			_2001();
			break;
		case 1:
			_2002();
			break;
		case 2:
			_2003();
			break;
		default:
			_2004();
			break;
		}
	}

	private void _2001()
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
		float maxWidth = x - 2f * num;
		float fsCaption = Theme.FsCaption;
		float textLineHeight = ImGui.GetTextLineHeight();
		List<string> list = Theme.WrapText("自动化功能只限跟落地开的车，ET 和倒数车会产生不可预知的行为，玩家自行承担后果。反馈 bug 及建议请去 DC 频道「DR-插件讨论 / omni-社区插件」搜索 AutomaticHunting。", maxWidth, Theme.FsBody);
		float num4 = (float)list.Count * textLineHeight;
		float y = num2 + fsCaption + num3 + num4 + num2;
		Vector2 max = cursorScreenPos + new Vector2(x, y);
		UiPanel.DrawCard(cursorScreenPos, max, 6f * uiScale, Theme.WithAlpha(Theme.StatusWarning, 0.1f));
		Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X + num, cursorScreenPos.Y + num2), Theme.StatusWarning, "免责声明", Theme.FsCaption);
		float num5 = cursorScreenPos.Y + num2 + fsCaption + num3;
		for (int i = 0; i < list.Count; i++)
		{
			ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + num, num5 + (float)i * textLineHeight));
			ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextSecondary);
			ImGui.TextUnformatted(list[i]);
			ImGui.PopStyleColor();
		}
		ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + num, max.Y + 8f * uiScale));
		bool v = _2000_204F;
		if (ImGui.Checkbox("##acceptDisclaimer", ref v))
		{
			_2000_204F = v;
		}
		ImGui.SameLine();
		ImGui.TextUnformatted("我已阅读并同意上述免责声明");
	}

	private void _2002()
	{
		float uiScale = Theme.UiScale;
		double time = ImGui.GetTime();
		if (time - _2000_2051 >= 2.0)
		{
			_2000_2051 = time;
			_2000_2052 = AutomaticHunting.IsPluginLoaded("Lifestream");
			_2000_2053 = AutomaticHunting.IsPluginLoaded("vnavmesh");
			_2000_2054 = _2000_204C.IsBmrLoaded();
		}
		ImGui.TextUnformatted("以下插件是自动狩猎的运行前提，每 2 秒自动复查一次：");
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		UiForm.Begin("##onboardDeps");
		_00A0("Lifestream", _2000_2052, true);
		_00A0("vnavmesh", _2000_2053, true);
		_00A0("BossModReborn", _2000_2054, false);
		UiForm.End();
		if (!_2000_2052 || !_2000_2053)
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

	private static void _00A0(string P_0, bool P_1, bool P_2)
	{
		var (text, glyph, color) = UiState.DependencyState(P_0, P_1, P_2);
		UiForm.Row(P_0);
		UiBadge.Dot(glyph, text, color);
	}

	private void _2003()
	{
		float uiScale = Theme.UiScale;
		ImGui.TextWrapped("设置车头名（可留空）。留空时插件处于「待设置车头」的中性态，除跟车外的高级功能仍可使用。");
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		UiForm.Begin("##onboardLeader");
		UiForm.Row("车头名称", "输入名字后按 回车 添加（可添加多个，设置页里可勾选启用哪些）");
		UiForm.Fill();
		if (ImGui.InputText("##onboardLeaderName", ref _2000_2050, 128, ImGuiInputTextFlags.EnterReturnsTrue))
		{
			_2006();
		}
		UiForm.End();
		var (value, num) = LeaderRoster.Counts(_2000_204C.Config);
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
		UiLayout.EqualColumns("##onboardQuickBtns", 2, 8f, (int P_0) =>
		{
			Vector2 value2 = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (P_0 == 0)
			{
				if (UiButton.Draw("用当前目标", UiVariant.Neutral, value2, "把当前选中的目标加进车头列表"))
				{
					IGameObject target = AutomaticHunting.TargetManager.Target;
					if (target != null)
					{
						LeaderRoster.Add(_2000_204C.Config, target.Name.TextValue);
						_2000_204C.ConfigService.MarkDirty();
					}
					else
					{
						_2000_204C.PrintEcho("未选中目标");
					}
				}
			}
			else if (UiButton.Draw("用我自己", UiVariant.Neutral, value2, "把自己设为车头"))
			{
				_2000_204C.SetLeaderToSelf();
			}
		});
		UiForm.End();
	}

	private void _2004()
	{
		float uiScale = Theme.UiScale;
		UiForm.Begin("##onboardSummary");
		UiForm.Row("免责声明");
		ImGui.TextUnformatted(_2000_204C.Config.DisclaimerAccepted ? "已同意" : "未同意（插件保持受限）");
		UiForm.Row("前置插件");
		ImGui.TextUnformatted((_2000_2052 && _2000_2053) ? "已就绪" : "未就绪（功能会降级）");
		UiForm.Row("车头");
		ImGui.TextUnformatted(string.IsNullOrWhiteSpace(_2000_204C.Config.LeaderName) ? "未设置（可稍后设置）" : _2000_204C.Config.LeaderName);
		UiForm.End();
		ImGui.Dummy(new Vector2(1f, 8f * uiScale));
		ImGui.TextWrapped("完成后会立即启用自动狩猎。你随时可以用 /auht setup 重新打开本向导，或用 /auht off 进入受限模式。");
	}

	private void _2005()
	{
		float uiScale = Theme.UiScale;
		ImGui.Separator();
		ImGui.Dummy(new Vector2(1f, 4f * uiScale));
		UiLayout.WeightedColumns("##onboardFooter", new float[3] { 1f, 1f, 2f }, 8f, 0f, (int P_0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			switch (P_0)
			{
			case 0:
				if (UiButton.Draw("稍后再说", UiVariant.Ghost, value, "关闭向导；插件保持受限模式，可随时回来"))
				{
					IsOpen = false;
				}
				break;
			case 1:
			{
				bool enabled2 = _2000_204E > 0;
				if (UiButton.Draw("上一步", UiVariant.Neutral, value, "返回上一步", enabled2))
				{
					_00A0(_2000_204E - 1);
				}
				break;
			}
			default:
			{
				bool flag = _2000_204E == _2000_204B.Length - 1;
				bool enabled = _2000_204E != 0 || _2000_204F;
				if (UiButton.Draw(flag ? "完成" : "下一步", UiVariant.Primary, value, flag ? "完成向导并启用插件" : "进入下一步", enabled))
				{
					if (flag)
					{
						_2007();
					}
					else
					{
						_00A0(_2000_204E + 1);
					}
				}
				break;
			}
			}
		});
		ImGui.Dummy(new Vector2(1f, 4f * uiScale));
	}

	private void _00A0(int P_0)
	{
		_2006();
		_2000_204E = Math.Clamp(P_0, 0, _2000_204B.Length - 1);
		_2000_204C.Config.OnboardingStep = _2000_204E;
		_2000_204C.ConfigService.MarkDirty();
	}

	private void _2006()
	{
		if (!string.IsNullOrWhiteSpace(_2000_2050))
		{
			LeaderRoster.Add(_2000_204C.Config, _2000_2050);
			_2000_2050 = string.Empty;
			_2000_204C.ConfigService.MarkDirty();
		}
	}

	private void _2007()
	{
		PluginConfiguration config = _2000_204C.Config;
		_2006();
		config.DisclaimerAccepted = _2000_204F;
		config.OnboardingCompleted = true;
		config.OnboardingStep = 0;
		config.Enabled = _2000_204F;
		config.HudVisible = true;
		_2000_204C.ConfigService.Save();
		_2000_204C.PrintEcho(_2000_204F ? "首次设置已完成，自动狩猎已启用" : "首次设置已完成（受限模式）");
		_2000_204C.HelpWindow.IsOpen = true;
		_2000_204C.SettingsWindow.IsOpen = true;
		_2000_204C.HudWindow.IsOpen = true;
		IsOpen = false;
	}

	[CompilerGenerated]
	private void _1680(int P_0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (P_0 == 0)
		{
			if (UiButton.Draw("用当前目标", UiVariant.Neutral, value, "把当前选中的目标加进车头列表"))
			{
				IGameObject target = AutomaticHunting.TargetManager.Target;
				if (target != null)
				{
					LeaderRoster.Add(_2000_204C.Config, target.Name.TextValue);
					_2000_204C.ConfigService.MarkDirty();
				}
				else
				{
					_2000_204C.PrintEcho("未选中目标");
				}
			}
		}
		else if (UiButton.Draw("用我自己", UiVariant.Neutral, value, "把自己设为车头"))
		{
			_2000_204C.SetLeaderToSelf();
		}
	}

	[CompilerGenerated]
	private void _2000(int P_0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		switch (P_0)
		{
		case 0:
			if (UiButton.Draw("稍后再说", UiVariant.Ghost, value, "关闭向导；插件保持受限模式，可随时回来"))
			{
				IsOpen = false;
			}
			return;
		case 1:
		{
			bool enabled = _2000_204E > 0;
			if (UiButton.Draw("上一步", UiVariant.Neutral, value, "返回上一步", enabled))
			{
				_00A0(_2000_204E - 1);
			}
			return;
		}
		}
		bool flag = _2000_204E == _2000_204B.Length - 1;
		bool enabled2 = _2000_204E != 0 || _2000_204F;
		if (UiButton.Draw(flag ? "完成" : "下一步", UiVariant.Primary, value, flag ? "完成向导并启用插件" : "进入下一步", enabled2))
		{
			if (flag)
			{
				_2007();
			}
			else
			{
				_00A0(_2000_204E + 1);
			}
		}
	}
}
