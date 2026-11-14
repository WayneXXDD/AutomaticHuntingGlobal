using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Utility;

namespace AutomaticHunting.UI;

public static class Theme
{
	public static class Hud
	{
		public const float TotalWidth = 236f;

		public const float PadX = 8f;

		public const float PadY = 8f;

		public const float Radius = 8f;

		public const float ContentWidth = 220f;

		public const float ItemSpacingX = 6f;

		public const float Gap = 6f;

		public const float StatusH = 20f;

		public const float RowH = 22f;

		public const float CellPadY = 3f;

		public const float CardPadX = 8f;

		public const float LabelColumnPad = 10f;

		public const float BtnH = 26f;

		public const float ProgressH = 4f;

		public const float GearColW = 28f;

		public const float GearIconW = 26f;

		public const float GearColWCompact = 40f;

		public const float GearIconWCompact = 40f;

		public const float BtnGap = 2f;

		public const float GearBtnW = 28f;

		public const float GearBtnWCompact = 40f;

		public const float BtnHCompact = 40f;
	}

	public struct ThemeScope : IDisposable
	{
		private int field_832;

		private int field_833;

		private int field_834;

		private bool field_835;

		private bool field_836;

		private Vector4 field_837;

		private float field_838;

		internal ThemeScope(int arg0, int arg1, int arg2, bool arg3, bool arg4, Vector4 arg5, float arg6)
		{
			field_832 = arg0;
			field_833 = arg1;
			field_834 = arg2;
			field_835 = arg3;
			field_836 = arg4;
			field_837 = arg5;
			field_838 = arg6;
		}

		public void Dispose()
		{
			if (field_832 != 0)
			{
				if (field_834 > 0)
				{
					ImGui.PopStyleVar(field_834);
				}
				if (field_833 > 0)
				{
					ImGui.PopStyleColor(field_833);
				}
				field_806.Remove(field_832);
				HudTextShadow = field_835;
				HudNeedsCompensation = field_836;
				HudSecondaryText = field_837;
				ItemSpacingX = field_838;
				field_832 = 0;
				field_833 = 0;
				field_834 = 0;
			}
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct RecoveredType_127
	{
		public StringBuilder field_839;

		public float field_840;

		public float field_841;

		public List<string> field_842;

		public bool field_843;

		public float field_844;

		public float field_845;
	}

	public const float FontBase = 16f;

	public static readonly Vector4 SurfaceWindow = Method_832(11, 14, 20);

	public static readonly Vector4 SurfaceCard = Method_832(23, 29, 38);

	public static readonly Vector4 SurfaceCardAlt = Method_832(18, 22, 30);

	public static readonly Vector4 SurfaceCardHover = Method_832(28, 34, 45);

	public static readonly Vector4 SurfacePopup = Method_832(18, 22, 31, 0.98f);

	public static readonly Vector4 SurfaceTableHeader = Method_832(27, 34, 48);

	public static readonly Vector4 SurfaceHeader = Method_832(16, 20, 28);

	public static readonly Vector4 BorderCard = Method_832(58, 68, 83);

	public static readonly Vector4 BorderControl = Method_832(90, 102, 118);

	public static readonly Vector4 BorderLine = Method_832(35, 42, 53, 0.85f);

	public static readonly Vector4 BorderLineStrong = Method_832(51, 60, 74);

	public static readonly Vector4 BorderEdgeLight = Method_832(126, 140, 163, 0.55f);

	public static readonly Vector4 BorderFocus = Method_832(227, 181, 75);

	public static readonly Vector4 BorderDanger = Method_832(170, 80, 77);

	public const float BorderThickness = 1f;

	public static readonly Vector4 TextPrimary = Method_832(231, 235, 242);

	public static readonly Vector4 TextSecondary = Method_832(155, 166, 182);

	public static readonly Vector4 TextDisabled = Method_832(102, 112, 130);

	public static readonly Vector4 TextOnBrand = Method_832(16, 20, 27);

	public static readonly Vector4 TextShadow = Method_832(0, 0, 0, 0.75f);

	public static readonly Vector4 BrandGold = Method_832(227, 181, 75);

	public static readonly Vector4 BrandGoldLight = Method_832(242, 206, 122);

	public static readonly Vector4 BrandGoldDim = Method_832(122, 97, 40);

	public static readonly Vector4 BrandGoldFill = Method_832(58, 47, 19);

	public static readonly Vector4 StatusSuccess = Method_832(82, 206, 147);

	public static readonly Vector4 StatusWarning = Method_832(240, 160, 60);

	public static readonly Vector4 StatusDanger = Method_832(241, 95, 90);

	public static readonly Vector4 StatusInfo = Method_832(90, 169, 255);

	public static readonly Vector4 StatusIdle = Method_832(126, 136, 153);

	public static readonly Vector4 InputBg = Method_832(14, 18, 24);

	public static readonly Vector4 InputBgHover = Method_832(23, 28, 37);

	public static readonly Vector4 InputBgActive = Method_832(29, 36, 48);

	public static readonly Vector4 InputBorder = Method_832(90, 102, 118);

	public static readonly Vector4 InputBorderFocus = BorderFocus;

	public static readonly Vector4 BtnNeutral = Method_832(26, 32, 42);

	public static readonly Vector4 BtnNeutralHover = Method_832(35, 43, 55);

	public static readonly Vector4 BtnNeutralActive = Method_832(20, 25, 34);

	public static readonly Vector4 BtnPrimary = Method_832(217, 168, 62);

	public static readonly Vector4 BtnPrimaryHover = Method_832(232, 184, 75);

	public static readonly Vector4 BtnPrimaryActive = Method_832(194, 145, 47);

	public static readonly Vector4 BtnSuccess = Method_832(28, 106, 72);

	public static readonly Vector4 BtnSuccessHover = Method_832(36, 126, 85);

	public static readonly Vector4 BtnSuccessActive = Method_832(23, 89, 60);

	public static readonly Vector4 BtnDanger = Method_832(42, 22, 24);

	public static readonly Vector4 BtnDangerHover = Method_832(58, 28, 31);

	public static readonly Vector4 BtnDangerActive = Method_832(33, 15, 17);

	public static readonly Vector4 BtnAccent = Method_832(23, 69, 78);

	public static readonly Vector4 BtnAccentHover = Method_832(29, 88, 98);

	public static readonly Vector4 BtnAccentActive = Method_832(18, 55, 62);

	public static readonly Vector4 BtnBrandHover = Method_832(227, 181, 75, 0.14f);

	public static readonly Vector4 BtnBrandActive = Method_832(227, 181, 75, 0.22f);

	public static readonly Vector4 BtnGhostHover = Method_832(255, 255, 255, 0.07f);

	public static readonly Vector4 BtnGhostActive = Method_832(255, 255, 255, 0.12f);

	public static readonly Vector4 BtnDangerSolid = Method_832(179, 58, 52);

	public static readonly Vector4 BtnDangerSolidHover = Method_832(201, 72, 63);

	public static readonly Vector4 BtnDangerSolidActive = Method_832(154, 46, 41);

	public static readonly Vector4 BtnGhostText = Method_832(195, 203, 216);

	public static readonly Vector4 BtnDangerText = Method_832(255, 158, 154);

	public static readonly Vector4 BtnAccentText = Method_832(191, 237, 242);

	public static readonly Vector4 StateHover = Method_832(255, 255, 255, 0.06f);

	public static readonly Vector4 StateActive = Method_832(255, 255, 255, 0.11f);

	public static readonly Vector4 StateSelected = Method_832(35, 45, 60);

	public static readonly Vector4 ScrollbarTrack = Method_832(10, 13, 18, 0f);

	public static readonly Vector4 ScrollbarGrab = Method_832(60, 70, 85, 0.85f);

	public static readonly Vector4 ScrollbarGrabHover = Method_832(86, 99, 122, 0.95f);

	public static readonly Vector4 ScrollbarGrabActive = Method_832(119, 135, 158);

	public static readonly Vector4 ProgressTrack = Method_832(10, 13, 18, 0.85f);

	public static readonly Vector4 ProgressFill = StatusSuccess;

	public static readonly Vector4 ProgressFillWarn = StatusWarning;

	public static readonly Vector4 ProgressFillDanger = StatusDanger;

	public static readonly Vector4 TabSelectedBg = Method_832(227, 181, 75, 0.14f);

	public static readonly Vector4 TabHoverBg = Method_832(255, 255, 255, 0.05f);

	public static readonly Vector4 TabIndicator = BrandGold;

	public static readonly Vector4 TabIdleText = TextSecondary;

	public static readonly Vector4 TabSelectedText = BrandGoldLight;

	public const float CardRadius = 6f;

	public const float WindowRadius = 8f;

	public const float FrameRadius = 4f;

	public const float PopupRadius = 8f;

	public const float SectionGap = 16f;

	public const float Gap = 6f;

	public const float GapSmall = 4f;

	public const float LabelGap = 12f;

	public const float ControlHeightNominal = 26f;

	public const float SettingsAlpha = 0.97f;

	public const float HudAlphaDefault = 0.88f;

	public const float HudAlphaMin = 0.62f;

	public const float HudAlphaMax = 1f;

	[CompilerGenerated]
	private static bool field_802;

	[CompilerGenerated]
	private static Vector4 field_803 = TextSecondary;

	[CompilerGenerated]
	private static float field_804 = 8f;

	[CompilerGenerated]
	private static bool field_805;

	private static readonly Dictionary<int, (int Colors, int Vars)> field_806 = new Dictionary<int, (int, int)>();

	private static int field_807;

	private static readonly char[] field_808 = new char[7] { '0', 'H', 'E', 'L', 'I', 'T', '1' };

	private static readonly char[] field_809 = new char[18]
	{
		'，', '。', '、', '；', '：', '？', '！', '）', '》', '」',
		'』', '】', '”', '’', '…', '·', '%', '℃'
	};

	public static float Scale
	{
		get
		{
			try
			{
				return ImGuiHelpers.GlobalScale;
			}
			catch
			{
				return 1f;
			}
		}
	}

	public static float UiScale => Scale;

	public static Vector4 Brand => BrandGold;

	public static Vector4 BrandDim => BrandGoldDim;

	public static Vector4 Ok => StatusSuccess;

	public static Vector4 Warn => StatusWarning;

	public static Vector4 Bad => StatusDanger;

	public static Vector4 Info => StatusInfo;

	public static Vector4 Idle => StatusIdle;

	public static Vector4 Text => TextPrimary;

	public static Vector4 TextMuted => TextSecondary;

	public static Vector4 Line => BorderLine;

	public static Vector4 CardBg => SurfaceCard;

	public static Vector4 CardBorder => BorderCard;

	public static Vector4 Surface => SurfaceWindow;

	public static Vector4 PopupBg => SurfacePopup;

	public static Vector4 Hover => StateHover;

	public static Vector4 Active => StateActive;

	public static Vector4 FillPrimary => BtnPrimary;

	public static Vector4 FillSuccess => BtnSuccess;

	public static Vector4 FillDanger => BtnDanger;

	public static Vector4 FillAccent => BtnAccent;

	public static Vector4 FillBrand => BrandGoldFill;

	public static float ControlHeight => ImGui.GetTextLineHeight() + 10f * Scale;

	public static float ButtonHeight => ControlHeight;

	public static float FsMicro => ImGui.GetFontSize() * 0.78f;

	public static float FsCaption => ImGui.GetFontSize() * 0.92f;

	public static float FsBody => ImGui.GetFontSize();

	public static float FsSubtitle => ImGui.GetFontSize() * 1.08f;

	public static float FsTitle => ImGui.GetFontSize() * 1.3f;

	public static bool HudTextShadow
	{
		[CompilerGenerated]
		get
		{
			return field_802;
		}
		[CompilerGenerated]
		private set
		{
			field_802 = value;
		}
	}

	public static Vector4 HudSecondaryText
	{
		[CompilerGenerated]
		get
		{
			return field_803;
		}
		[CompilerGenerated]
		private set
		{
			field_803 = value;
		}
	}

	public static float ItemSpacingX
	{
		[CompilerGenerated]
		get
		{
			return field_804;
		}
		[CompilerGenerated]
		private set
		{
			field_804 = value;
		}
	}

	public static bool HudNeedsCompensation
	{
		[CompilerGenerated]
		get
		{
			return field_805;
		}
		[CompilerGenerated]
		private set
		{
			field_805 = value;
		}
	}

	public unsafe static float BaselineUnits
	{
		get
		{
			ImFontPtr font = ImGui.GetFont();
			if (font.IsNull)
			{
				return 0f;
			}
			float num = 0f;
			char[] array = field_808;
			foreach (char c in array)
			{
				ImFontGlyph* ptr = font.FindGlyph(c);
				if (ptr != null && ptr->Y1 > num)
				{
					num = ptr->Y1;
				}
			}
			return num;
		}
	}

	public static float Px(float arg0)
	{
		return arg0 * Scale;
	}

	public static float Em(float arg0)
	{
		return arg0 * 16f * Scale;
	}

	public static float S(float arg0)
	{
		return Px(arg0);
	}

	public static ThemeScope Push(ThemeContext arg0 = ThemeContext.Settings, float arg1 = 1f)
	{
		int arg2 = 0;
		int arg3 = 0;
		float scale = Scale;
		float num = Math.Clamp(arg1, 0f, 1f);
		bool hudTextShadow = HudTextShadow;
		bool hudNeedsCompensation = HudNeedsCompensation;
		Vector4 hudSecondaryText = HudSecondaryText;
		float itemSpacingX = ItemSpacingX;
		if (arg0 == ThemeContext.Hud)
		{
			HudTextShadow = num < 0.82f;
			HudNeedsCompensation = false;
			HudSecondaryText = TextSecondary;
		}
		else
		{
			HudTextShadow = false;
			HudNeedsCompensation = false;
			HudSecondaryText = TextSecondary;
		}
		Method_817(ref arg2, num);
		switch (arg0)
		{
		case ThemeContext.Hud:
			Method_819(ref arg3, scale);
			break;
		case ThemeContext.Popup:
			Method_820(ref arg3, scale);
			break;
		default:
			Method_818(ref arg3, scale);
			break;
		}
		int num2 = ++field_807;
		field_806[num2] = (arg2, arg3);
		return new ThemeScope(num2, arg2, arg3, hudTextShadow, hudNeedsCompensation, hudSecondaryText, itemSpacingX);
	}

	private static void Method_817(ref int arg0, float arg1)
	{
		Method_821(ref arg0, ImGuiCol.Text, TextPrimary);
		Method_821(ref arg0, ImGuiCol.TextDisabled, TextDisabled);
		Method_821(ref arg0, ImGuiCol.WindowBg, WithAlpha(SurfaceWindow, arg1));
		Method_821(ref arg0, ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
		Method_821(ref arg0, ImGuiCol.PopupBg, SurfacePopup);
		Method_821(ref arg0, ImGuiCol.TitleBg, SurfaceHeader);
		Method_821(ref arg0, ImGuiCol.TitleBgActive, SurfaceHeader);
		Method_821(ref arg0, ImGuiCol.TitleBgCollapsed, SurfaceHeader);
		Method_821(ref arg0, ImGuiCol.MenuBarBg, SurfaceHeader);
		Method_821(ref arg0, ImGuiCol.DockingEmptyBg, WithAlpha(SurfaceWindow, 0.6f));
		Method_821(ref arg0, ImGuiCol.Border, BorderControl);
		Method_821(ref arg0, ImGuiCol.BorderShadow, new Vector4(0f, 0f, 0f, 0f));
		Method_821(ref arg0, ImGuiCol.FrameBg, InputBg);
		Method_821(ref arg0, ImGuiCol.FrameBgHovered, InputBgHover);
		Method_821(ref arg0, ImGuiCol.FrameBgActive, InputBgActive);
		Method_821(ref arg0, ImGuiCol.ScrollbarBg, ScrollbarTrack);
		Method_821(ref arg0, ImGuiCol.ScrollbarGrab, ScrollbarGrab);
		Method_821(ref arg0, ImGuiCol.ScrollbarGrabHovered, ScrollbarGrabHover);
		Method_821(ref arg0, ImGuiCol.ScrollbarGrabActive, ScrollbarGrabActive);
		Method_821(ref arg0, ImGuiCol.CheckMark, BrandGold);
		Method_821(ref arg0, ImGuiCol.SliderGrab, BrandGold);
		Method_821(ref arg0, ImGuiCol.SliderGrabActive, BrandGoldLight);
		Method_821(ref arg0, ImGuiCol.Button, BtnNeutral);
		Method_821(ref arg0, ImGuiCol.ButtonHovered, BtnNeutralHover);
		Method_821(ref arg0, ImGuiCol.ButtonActive, BtnNeutralActive);
		Method_821(ref arg0, ImGuiCol.Header, StateHover);
		Method_821(ref arg0, ImGuiCol.HeaderHovered, Method_832(255, 255, 255, 0.1f));
		Method_821(ref arg0, ImGuiCol.HeaderActive, StateActive);
		Method_821(ref arg0, ImGuiCol.Separator, BorderLine);
		Method_821(ref arg0, ImGuiCol.SeparatorHovered, BorderLineStrong);
		Method_821(ref arg0, ImGuiCol.SeparatorActive, BorderLineStrong);
		Method_821(ref arg0, ImGuiCol.ResizeGrip, WithAlpha(BorderEdgeLight, 0.35f));
		Method_821(ref arg0, ImGuiCol.ResizeGripHovered, WithAlpha(BrandGold, 0.6f));
		Method_821(ref arg0, ImGuiCol.ResizeGripActive, WithAlpha(BrandGold, 0.8f));
		Method_821(ref arg0, ImGuiCol.Tab, new Vector4(0f, 0f, 0f, 0f));
		Method_821(ref arg0, ImGuiCol.TabHovered, TabHoverBg);
		Method_821(ref arg0, ImGuiCol.TabActive, TabSelectedBg);
		Method_821(ref arg0, ImGuiCol.TabUnfocused, new Vector4(0f, 0f, 0f, 0f));
		Method_821(ref arg0, ImGuiCol.TabUnfocusedActive, Method_832(227, 181, 75, 0.1f));
		Method_821(ref arg0, ImGuiCol.DockingPreview, WithAlpha(BrandGold, 0.25f));
		Method_821(ref arg0, ImGuiCol.NavHighlight, WithAlpha(BrandGold, 0.35f));
		Method_821(ref arg0, ImGuiCol.NavWindowingHighlight, WithAlpha(BrandGold, 0.55f));
		Method_821(ref arg0, ImGuiCol.NavWindowingDimBg, Method_832(5, 7, 10, 0.35f));
		Method_821(ref arg0, ImGuiCol.ModalWindowDimBg, Method_832(5, 7, 10, 0.6f));
		Method_821(ref arg0, ImGuiCol.PlotLines, BorderLineStrong);
		Method_821(ref arg0, ImGuiCol.PlotLinesHovered, BrandGold);
		Method_821(ref arg0, ImGuiCol.PlotHistogram, BorderCard);
		Method_821(ref arg0, ImGuiCol.PlotHistogramHovered, BrandGold);
		Method_821(ref arg0, ImGuiCol.TableHeaderBg, SurfaceTableHeader);
		Method_821(ref arg0, ImGuiCol.TableBorderStrong, BorderLineStrong);
		Method_821(ref arg0, ImGuiCol.TableBorderLight, WithAlpha(BorderCard, 0.8f));
		Method_821(ref arg0, ImGuiCol.TableRowBg, new Vector4(0f, 0f, 0f, 0f));
		Method_821(ref arg0, ImGuiCol.TableRowBgAlt, Method_832(255, 255, 255, 0.022f));
		Method_821(ref arg0, ImGuiCol.TextSelectedBg, StateSelected);
		Method_821(ref arg0, ImGuiCol.DragDropTarget, WithAlpha(BrandGold, 0.55f));
	}

	private static void Method_818(ref int arg0, float arg1)
	{
		Method_822(ref arg0, ImGuiStyleVar.WindowPadding, new Vector2(16f * arg1, 12f * arg1));
		Method_822(ref arg0, ImGuiStyleVar.FramePadding, new Vector2(8f * arg1, 5f * arg1));
		Method_823(ref arg0, ImGuiStyleVar.FrameRounding, 4f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.FrameBorderSize, 1f);
		Method_823(ref arg0, ImGuiStyleVar.ChildRounding, 6f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.ChildBorderSize, 1f);
		Method_823(ref arg0, ImGuiStyleVar.PopupRounding, 8f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.PopupBorderSize, 1f);
		Method_822(ref arg0, ImGuiStyleVar.ItemSpacing, new Vector2(8f * arg1, 6f * arg1));
		Method_822(ref arg0, ImGuiStyleVar.ItemInnerSpacing, new Vector2(6f * arg1, 4f * arg1));
		Method_822(ref arg0, ImGuiStyleVar.CellPadding, new Vector2(8f * arg1, 4f * arg1));
		Method_823(ref arg0, ImGuiStyleVar.IndentSpacing, 16f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.ScrollbarSize, 10f * arg1);
		ItemSpacingX = 8f;
		Method_823(ref arg0, ImGuiStyleVar.ScrollbarRounding, 5f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.GrabRounding, 4f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.GrabMinSize, 10f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.WindowRounding, 8f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.WindowBorderSize, 0f);
		Method_822(ref arg0, ImGuiStyleVar.WindowTitleAlign, new Vector2(0f, 0.5f));
		Method_823(ref arg0, ImGuiStyleVar.DisabledAlpha, 0.45f);
		Method_823(ref arg0, ImGuiStyleVar.Alpha, 1f);
	}

	private static void Method_819(ref int arg0, float arg1)
	{
		Method_822(ref arg0, ImGuiStyleVar.WindowPadding, new Vector2(8f * arg1, 8f * arg1));
		Method_822(ref arg0, ImGuiStyleVar.FramePadding, new Vector2(6f * arg1, 4f * arg1));
		Method_823(ref arg0, ImGuiStyleVar.FrameRounding, 4f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.FrameBorderSize, 1f);
		Method_823(ref arg0, ImGuiStyleVar.ChildRounding, 6f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.ChildBorderSize, 1f);
		Method_823(ref arg0, ImGuiStyleVar.PopupRounding, 8f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.PopupBorderSize, 1f);
		Method_822(ref arg0, ImGuiStyleVar.ItemSpacing, new Vector2(6f * arg1, 0f));
		Method_822(ref arg0, ImGuiStyleVar.ItemInnerSpacing, new Vector2(6f * arg1, 4f * arg1));
		Method_822(ref arg0, ImGuiStyleVar.CellPadding, new Vector2(8f * arg1, 3f * arg1));
		Method_823(ref arg0, ImGuiStyleVar.IndentSpacing, 16f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.ScrollbarSize, 8f * arg1);
		ItemSpacingX = 6f;
		Method_823(ref arg0, ImGuiStyleVar.ScrollbarRounding, 4f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.GrabRounding, 4f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.GrabMinSize, 10f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.WindowRounding, 8f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.WindowBorderSize, 0f);
		Method_822(ref arg0, ImGuiStyleVar.WindowTitleAlign, new Vector2(0f, 0.5f));
		Method_823(ref arg0, ImGuiStyleVar.DisabledAlpha, 0.45f);
		Method_823(ref arg0, ImGuiStyleVar.Alpha, 1f);
	}

	private static void Method_820(ref int arg0, float arg1)
	{
		Method_822(ref arg0, ImGuiStyleVar.WindowPadding, new Vector2(14f * arg1, 12f * arg1));
		Method_822(ref arg0, ImGuiStyleVar.FramePadding, new Vector2(8f * arg1, 5f * arg1));
		Method_823(ref arg0, ImGuiStyleVar.FrameRounding, 4f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.FrameBorderSize, 1f);
		Method_823(ref arg0, ImGuiStyleVar.PopupRounding, 8f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.PopupBorderSize, 1f);
		Method_822(ref arg0, ImGuiStyleVar.ItemSpacing, new Vector2(8f * arg1, 6f * arg1));
		Method_822(ref arg0, ImGuiStyleVar.ItemInnerSpacing, new Vector2(6f * arg1, 4f * arg1));
		Method_822(ref arg0, ImGuiStyleVar.CellPadding, new Vector2(8f * arg1, 4f * arg1));
		Method_823(ref arg0, ImGuiStyleVar.GrabRounding, 4f * arg1);
		ItemSpacingX = 8f;
		Method_823(ref arg0, ImGuiStyleVar.ScrollbarRounding, 5f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.ScrollbarSize, 10f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.WindowRounding, 8f * arg1);
		Method_823(ref arg0, ImGuiStyleVar.WindowBorderSize, 1f);
		Method_823(ref arg0, ImGuiStyleVar.DisabledAlpha, 0.45f);
		Method_823(ref arg0, ImGuiStyleVar.Alpha, 1f);
	}

	private static void Method_821(ref int arg0, ImGuiCol arg1, Vector4 arg2)
	{
		ImGui.PushStyleColor(arg1, arg2);
		arg0++;
	}

	private static void Method_822(ref int arg0, ImGuiStyleVar arg1, Vector2 arg2)
	{
		ImGui.PushStyleVar(arg1, arg2);
		arg0++;
	}

	private static void Method_823(ref int arg0, ImGuiStyleVar arg1, float arg2)
	{
		ImGui.PushStyleVar(arg1, arg2);
		arg0++;
	}

	public static void PopAll()
	{
		if (field_806.Count == 0)
		{
			return;
		}
		List<int> list = new List<int>(field_806.Keys);
		list.Sort();
		for (int num = list.Count - 1; num >= 0; num--)
		{
			(int, int) tuple = field_806[list[num]];
			if (tuple.Item2 > 0)
			{
				ImGui.PopStyleVar(tuple.Item2);
			}
			if (tuple.Item1 > 0)
			{
				ImGui.PopStyleColor(tuple.Item1);
			}
		}
		field_806.Clear();
		HudTextShadow = false;
		HudNeedsCompensation = false;
		HudSecondaryText = TextSecondary;
	}

	public static float ContentRightX()
	{
		return ImGui.GetWindowPos().X + ImGui.GetContentRegionMax().X;
	}

	public static bool IsHoveredInWindow(Vector2 arg0, Vector2 arg1)
	{
		if (ImGui.IsWindowHovered())
		{
			return ImGui.IsMouseHoveringRect(arg0, arg1, clip: true);
		}
		return false;
	}

	public static Vector4 Mix(Vector4 arg0, Vector4 arg1, float arg2)
	{
		return new Vector4(arg0.X + (arg1.X - arg0.X) * arg2, arg0.Y + (arg1.Y - arg0.Y) * arg2, arg0.Z + (arg1.Z - arg0.Z) * arg2, arg0.W + (arg1.W - arg0.W) * arg2);
	}

	public static Vector4 WithAlpha(Vector4 arg0, float arg1)
	{
		return new Vector4(arg0.X, arg0.Y, arg0.Z, arg1);
	}

	public static Vector4 Shade(Vector4 arg0, float arg1)
	{
		return new Vector4(arg0.X * arg1, arg0.Y * arg1, arg0.Z * arg1, arg0.W);
	}

	public static uint U32(Vector4 arg0)
	{
		return ImGui.ColorConvertFloat4ToU32(arg0);
	}

	public static uint U32(byte arg0, byte arg1, byte arg2, byte arg3 = byte.MaxValue)
	{
		return (uint)((arg3 << 24) | (arg2 << 16) | (arg1 << 8) | arg0);
	}

	private static Vector4 Method_832(int arg0, int arg1, int arg2, float arg3 = 1f)
	{
		return new Vector4((float)arg0 / 255f, (float)arg1 / 255f, (float)arg2 / 255f, arg3);
	}

	public static float MeasureTextWidth(string arg0, float arg1)
	{
		if (string.IsNullOrEmpty(arg0))
		{
			return 0f;
		}
		float x = ImGui.CalcTextSize(arg0).X;
		float fsBody = FsBody;
		if (arg1 <= 0f || fsBody <= 0f || Math.Abs(arg1 - fsBody) < 0.0001f)
		{
			return x;
		}
		return x * (arg1 / fsBody);
	}

	public static float MeasureTextHeight(float arg0 = 0f)
	{
		float y = ImGui.CalcTextSize("Ag中").Y;
		float fsBody = FsBody;
		if (arg0 <= 0f || fsBody <= 0f || Math.Abs(arg0 - fsBody) < 0.0001f)
		{
			return y;
		}
		return y * (arg0 / fsBody);
	}

	public static float BaselineOffset(float arg0, float arg1)
	{
		ImFontPtr font = ImGui.GetFont();
		if (font.IsNull || font.FontSize <= 0f)
		{
			return 0f;
		}
		return arg0 * (arg1 / font.FontSize);
	}

	public static string Ellipsis(string? arg0, float arg1, float arg2 = 0f)
	{
		if (string.IsNullOrEmpty(arg0))
		{
			return string.Empty;
		}
		if (arg1 <= 0f)
		{
			return string.Empty;
		}
		float arg3 = ((arg2 > 0f) ? arg2 : FsBody);
		if (MeasureTextWidth(arg0, arg3) <= arg1)
		{
			return arg0;
		}
		float num = MeasureTextWidth("...", arg3);
		float num2 = arg1 - num;
		if (num2 <= 0f)
		{
			return "...";
		}
		char[] array = arg0.ToCharArray();
		for (int num3 = array.Length; num3 > 0; num3--)
		{
			string text = new string(array, 0, num3);
			if (MeasureTextWidth(text, arg3) <= num2)
			{
				return text + "...";
			}
		}
		return "...";
	}

	private static bool Method_838(char arg0)
	{
		if ((arg0 < 'a' || arg0 > 'z') && (arg0 < 'A' || arg0 > 'Z') && (arg0 < '0' || arg0 > '9') && arg0 != '_' && arg0 != '-' && arg0 != '.' && arg0 != '/' && arg0 != '#' && arg0 != '%' && arg0 != '&' && arg0 != '@' && arg0 != '+')
		{
			return arg0 == '\'';
		}
		return true;
	}

	public static List<string> WrapText(string? arg0, float arg1, float arg2 = 0f)
	{
		RecoveredType_127 arg3 = default;
		arg3.field_844 = arg1;
		arg3.field_842 = new List<string>();
		if (arg0 == null)
		{
			return arg3.field_842;
		}
		if (arg0.Length == 0)
		{
			arg3.field_842.Add(string.Empty);
			return arg3.field_842;
		}
		arg3.field_841 = ((arg2 > 0f) ? arg2 : FsBody);
		if (arg3.field_844 <= 0f)
		{
			arg3.field_842.Add(arg0);
			return arg3.field_842;
		}
		arg3.field_845 = MeasureTextWidth(" ", arg3.field_841);
		arg3.field_839 = new StringBuilder();
		arg3.field_840 = 0f;
		arg3.field_843 = false;
		int num = 0;
		while (num < arg0.Length)
		{
			char c = arg0[num];
			if (c == ' ')
			{
				if (arg3.field_839.Length > 0)
				{
					arg3.field_843 = true;
				}
				num++;
			}
			else if (Method_838(c))
			{
				int i;
				for (i = num; i < arg0.Length && Method_838(arg0[i]); i++)
				{
				}
				Method_844(arg0.Substring(num, i - num), ref arg3);
				num = i;
			}
			else
			{
				Method_844(c.ToString(), ref arg3);
				num++;
			}
		}
		arg3.field_842.Add(arg3.field_839.ToString());
		return arg3.field_842;
	}

	public static void DrawText(ImDrawListPtr arg0, Vector2 arg1, Vector4 arg2, string arg3, float arg4 = 0f, bool arg5 = false)
	{
		if (!string.IsNullOrEmpty(arg3))
		{
			float fontSize = ((arg4 > 0f) ? arg4 : FsBody);
			uint col = U32(arg2);
			if (arg5)
			{
				uint col2 = U32(TextShadow);
				arg0.AddText(ImGui.GetFont(), fontSize, arg1 + new Vector2(-1f, 0f), col2, arg3);
				arg0.AddText(ImGui.GetFont(), fontSize, arg1 + new Vector2(1f, 0f), col2, arg3);
				arg0.AddText(ImGui.GetFont(), fontSize, arg1 + new Vector2(0f, -1f), col2, arg3);
				arg0.AddText(ImGui.GetFont(), fontSize, arg1 + new Vector2(0f, 1f), col2, arg3);
			}
			arg0.AddText(ImGui.GetFont(), fontSize, arg1, col, arg3);
		}
	}

	[CompilerGenerated]
	internal static void Method_842(string arg0, ref RecoveredType_127 arg1)
	{
		arg1.field_839.Append(arg0);
		arg1.field_840 += MeasureTextWidth(arg0, arg1.field_841);
	}

	[CompilerGenerated]
	internal static void Method_843(ref RecoveredType_127 arg0)
	{
		arg0.field_842.Add(arg0.field_839.ToString());
		arg0.field_839.Clear();
		arg0.field_840 = 0f;
		arg0.field_843 = false;
	}

	[CompilerGenerated]
	internal static void Method_844(string arg0, ref RecoveredType_127 arg1)
	{
		float num = MeasureTextWidth(arg0, arg1.field_841);
		if (arg1.field_839.Length == 0)
		{
			if (num <= arg1.field_844)
			{
				Method_842(arg0, ref arg1);
				return;
			}
			for (int i = 0; i < arg0.Length; i++)
			{
				char c = arg0[i];
				float num2 = MeasureTextWidth(c.ToString(), arg1.field_841);
				if (arg1.field_839.Length > 0 && arg1.field_840 + num2 > arg1.field_844)
				{
					Method_843(ref arg1);
				}
				Method_842(c.ToString(), ref arg1);
			}
			return;
		}
		float num3 = num + (arg1.field_843 ? arg1.field_845 : 0f);
		if (arg1.field_840 + num3 <= arg1.field_844)
		{
			if (arg1.field_843)
			{
				Method_842(" ", ref arg1);
			}
			Method_842(arg0, ref arg1);
			arg1.field_843 = false;
		}
		else if (arg0.Length > 0 && Array.IndexOf(field_809, arg0[0]) >= 0 && arg1.field_839.Length >= 2)
		{
			char c2 = arg1.field_839[arg1.field_839.Length - 1];
			arg1.field_839.Length--;
			arg1.field_840 -= MeasureTextWidth(c2.ToString(), arg1.field_841);
			Method_843(ref arg1);
			Method_842(c2.ToString(), ref arg1);
			Method_842(arg0, ref arg1);
			arg1.field_843 = false;
		}
		else
		{
			Method_843(ref arg1);
			Method_844(arg0, ref arg1);
		}
	}
}
