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
		private int _2008_2006;

		private int _2008_2007;

		private int _2008_2008;

		private bool _2008_2009;

		private bool _2008_200A;

		private Vector4 _2008_200B;

		private float _2008_2010;

		internal ThemeScope(int P_0, int P_1, int P_2, bool P_3, bool P_4, Vector4 P_5, float P_6)
		{
			_2008_2006 = P_0;
			_2008_2007 = P_1;
			_2008_2008 = P_2;
			_2008_2009 = P_3;
			_2008_200A = P_4;
			_2008_200B = P_5;
			_2008_2010 = P_6;
		}

		public void Dispose()
		{
			if (_2008_2006 != 0)
			{
				if (_2008_2008 > 0)
				{
					ImGui.PopStyleVar(_2008_2008);
				}
				if (_2008_2007 > 0)
				{
					ImGui.PopStyleColor(_2008_2007);
				}
				_2000_2064.Remove(_2008_2006);
				HudTextShadow = _2008_2009;
				HudNeedsCompensation = _2008_200A;
				HudSecondaryText = _2008_200B;
				ItemSpacingX = _2008_2010;
				_2008_2006 = 0;
				_2008_2007 = 0;
				_2008_2008 = 0;
			}
		}
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct _2000
	{
		public StringBuilder _2008_2011;

		public float _2008_2012;

		public float _2008_2013;

		public List<string> _2008_2014;

		public bool _2008_2015;

		public float _2008_2022;

		public float _2008_2024;
	}

	public const float FontBase = 16f;

	public static readonly Vector4 SurfaceWindow = _00A0(11, 14, 20);

	public static readonly Vector4 SurfaceCard = _00A0(23, 29, 38);

	public static readonly Vector4 SurfaceCardAlt = _00A0(18, 22, 30);

	public static readonly Vector4 SurfaceCardHover = _00A0(28, 34, 45);

	public static readonly Vector4 SurfacePopup = _00A0(18, 22, 31, 0.98f);

	public static readonly Vector4 SurfaceTableHeader = _00A0(27, 34, 48);

	public static readonly Vector4 SurfaceHeader = _00A0(16, 20, 28);

	public static readonly Vector4 BorderCard = _00A0(58, 68, 83);

	public static readonly Vector4 BorderControl = _00A0(90, 102, 118);

	public static readonly Vector4 BorderLine = _00A0(35, 42, 53, 0.85f);

	public static readonly Vector4 BorderLineStrong = _00A0(51, 60, 74);

	public static readonly Vector4 BorderEdgeLight = _00A0(126, 140, 163, 0.55f);

	public static readonly Vector4 BorderFocus = _00A0(227, 181, 75);

	public static readonly Vector4 BorderDanger = _00A0(170, 80, 77);

	public const float BorderThickness = 1f;

	public static readonly Vector4 TextPrimary = _00A0(231, 235, 242);

	public static readonly Vector4 TextSecondary = _00A0(155, 166, 182);

	public static readonly Vector4 TextDisabled = _00A0(102, 112, 130);

	public static readonly Vector4 TextOnBrand = _00A0(16, 20, 27);

	public static readonly Vector4 TextShadow = _00A0(0, 0, 0, 0.75f);

	public static readonly Vector4 BrandGold = _00A0(227, 181, 75);

	public static readonly Vector4 BrandGoldLight = _00A0(242, 206, 122);

	public static readonly Vector4 BrandGoldDim = _00A0(122, 97, 40);

	public static readonly Vector4 BrandGoldFill = _00A0(58, 47, 19);

	public static readonly Vector4 StatusSuccess = _00A0(82, 206, 147);

	public static readonly Vector4 StatusWarning = _00A0(240, 160, 60);

	public static readonly Vector4 StatusDanger = _00A0(241, 95, 90);

	public static readonly Vector4 StatusInfo = _00A0(90, 169, 255);

	public static readonly Vector4 StatusIdle = _00A0(126, 136, 153);

	public static readonly Vector4 InputBg = _00A0(14, 18, 24);

	public static readonly Vector4 InputBgHover = _00A0(23, 28, 37);

	public static readonly Vector4 InputBgActive = _00A0(29, 36, 48);

	public static readonly Vector4 InputBorder = _00A0(90, 102, 118);

	public static readonly Vector4 InputBorderFocus = BorderFocus;

	public static readonly Vector4 BtnNeutral = _00A0(26, 32, 42);

	public static readonly Vector4 BtnNeutralHover = _00A0(35, 43, 55);

	public static readonly Vector4 BtnNeutralActive = _00A0(20, 25, 34);

	public static readonly Vector4 BtnPrimary = _00A0(217, 168, 62);

	public static readonly Vector4 BtnPrimaryHover = _00A0(232, 184, 75);

	public static readonly Vector4 BtnPrimaryActive = _00A0(194, 145, 47);

	public static readonly Vector4 BtnSuccess = _00A0(28, 106, 72);

	public static readonly Vector4 BtnSuccessHover = _00A0(36, 126, 85);

	public static readonly Vector4 BtnSuccessActive = _00A0(23, 89, 60);

	public static readonly Vector4 BtnDanger = _00A0(42, 22, 24);

	public static readonly Vector4 BtnDangerHover = _00A0(58, 28, 31);

	public static readonly Vector4 BtnDangerActive = _00A0(33, 15, 17);

	public static readonly Vector4 BtnAccent = _00A0(23, 69, 78);

	public static readonly Vector4 BtnAccentHover = _00A0(29, 88, 98);

	public static readonly Vector4 BtnAccentActive = _00A0(18, 55, 62);

	public static readonly Vector4 BtnBrandHover = _00A0(227, 181, 75, 0.14f);

	public static readonly Vector4 BtnBrandActive = _00A0(227, 181, 75, 0.22f);

	public static readonly Vector4 BtnGhostHover = _00A0(255, 255, 255, 0.07f);

	public static readonly Vector4 BtnGhostActive = _00A0(255, 255, 255, 0.12f);

	public static readonly Vector4 BtnDangerSolid = _00A0(179, 58, 52);

	public static readonly Vector4 BtnDangerSolidHover = _00A0(201, 72, 63);

	public static readonly Vector4 BtnDangerSolidActive = _00A0(154, 46, 41);

	public static readonly Vector4 BtnGhostText = _00A0(195, 203, 216);

	public static readonly Vector4 BtnDangerText = _00A0(255, 158, 154);

	public static readonly Vector4 BtnAccentText = _00A0(191, 237, 242);

	public static readonly Vector4 StateHover = _00A0(255, 255, 255, 0.06f);

	public static readonly Vector4 StateActive = _00A0(255, 255, 255, 0.11f);

	public static readonly Vector4 StateSelected = _00A0(35, 45, 60);

	public static readonly Vector4 ScrollbarTrack = _00A0(10, 13, 18, 0f);

	public static readonly Vector4 ScrollbarGrab = _00A0(60, 70, 85, 0.85f);

	public static readonly Vector4 ScrollbarGrabHover = _00A0(86, 99, 122, 0.95f);

	public static readonly Vector4 ScrollbarGrabActive = _00A0(119, 135, 158);

	public static readonly Vector4 ProgressTrack = _00A0(10, 13, 18, 0.85f);

	public static readonly Vector4 ProgressFill = StatusSuccess;

	public static readonly Vector4 ProgressFillWarn = StatusWarning;

	public static readonly Vector4 ProgressFillDanger = StatusDanger;

	public static readonly Vector4 TabSelectedBg = _00A0(227, 181, 75, 0.14f);

	public static readonly Vector4 TabHoverBg = _00A0(255, 255, 255, 0.05f);

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
	private static bool _2000_2060;

	[CompilerGenerated]
	private static Vector4 _2000_2061 = TextSecondary;

	[CompilerGenerated]
	private static float _2000_2062 = 8f;

	[CompilerGenerated]
	private static bool _2000_2063;

	private static readonly Dictionary<int, (int Colors, int Vars)> _2000_2064 = new Dictionary<int, (int, int)>();

	private static int _2000_206A;

	private static readonly char[] _2000_206B = new char[7] { '0', 'H', 'E', 'L', 'I', 'T', '1' };

	private static readonly char[] _2000_206C = new char[18]
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
			return _2000_2060;
		}
		[CompilerGenerated]
		private set
		{
			_2000_2060 = flag;
		}
	}

	public static Vector4 HudSecondaryText
	{
		[CompilerGenerated]
		get
		{
			return _2000_2061;
		}
		[CompilerGenerated]
		private set
		{
			_2000_2061 = vector;
		}
	}

	public static float ItemSpacingX
	{
		[CompilerGenerated]
		get
		{
			return _2000_2062;
		}
		[CompilerGenerated]
		private set
		{
			_2000_2062 = num;
		}
	}

	public static bool HudNeedsCompensation
	{
		[CompilerGenerated]
		get
		{
			return _2000_2063;
		}
		[CompilerGenerated]
		private set
		{
			_2000_2063 = flag;
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
			char[] array = _2000_206B;
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

	public static float Px(float nominal)
	{
		return nominal * Scale;
	}

	public static float Em(float em)
	{
		return em * 16f * Scale;
	}

	public static float S(float logical)
	{
		return Px(logical);
	}

	public static ThemeScope Push(ThemeContext ctx = ThemeContext.Settings, float windowAlpha = 1f)
	{
		int num = 0;
		int num2 = 0;
		float scale = Scale;
		float num3 = Math.Clamp(windowAlpha, 0f, 1f);
		bool hudTextShadow = HudTextShadow;
		bool hudNeedsCompensation = HudNeedsCompensation;
		Vector4 hudSecondaryText = HudSecondaryText;
		float itemSpacingX = ItemSpacingX;
		if (ctx == ThemeContext.Hud)
		{
			HudTextShadow = num3 < 0.82f;
			HudNeedsCompensation = false;
			HudSecondaryText = TextSecondary;
		}
		else
		{
			HudTextShadow = false;
			HudNeedsCompensation = false;
			HudSecondaryText = TextSecondary;
		}
		_00A0(ref num, num3);
		switch (ctx)
		{
		case ThemeContext.Hud:
			_2000(ref num2, scale);
			break;
		case ThemeContext.Popup:
			_2001(ref num2, scale);
			break;
		default:
			_1680(ref num2, scale);
			break;
		}
		int num4 = ++_2000_206A;
		_2000_2064[num4] = (num, num2);
		return new ThemeScope(num4, num, num2, hudTextShadow, hudNeedsCompensation, hudSecondaryText, itemSpacingX);
	}

	private static void _00A0(ref int P_0, float P_1)
	{
		_00A0(ref P_0, ImGuiCol.Text, TextPrimary);
		_00A0(ref P_0, ImGuiCol.TextDisabled, TextDisabled);
		_00A0(ref P_0, ImGuiCol.WindowBg, WithAlpha(SurfaceWindow, P_1));
		_00A0(ref P_0, ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
		_00A0(ref P_0, ImGuiCol.PopupBg, SurfacePopup);
		_00A0(ref P_0, ImGuiCol.TitleBg, SurfaceHeader);
		_00A0(ref P_0, ImGuiCol.TitleBgActive, SurfaceHeader);
		_00A0(ref P_0, ImGuiCol.TitleBgCollapsed, SurfaceHeader);
		_00A0(ref P_0, ImGuiCol.MenuBarBg, SurfaceHeader);
		_00A0(ref P_0, ImGuiCol.DockingEmptyBg, WithAlpha(SurfaceWindow, 0.6f));
		_00A0(ref P_0, ImGuiCol.Border, BorderControl);
		_00A0(ref P_0, ImGuiCol.BorderShadow, new Vector4(0f, 0f, 0f, 0f));
		_00A0(ref P_0, ImGuiCol.FrameBg, InputBg);
		_00A0(ref P_0, ImGuiCol.FrameBgHovered, InputBgHover);
		_00A0(ref P_0, ImGuiCol.FrameBgActive, InputBgActive);
		_00A0(ref P_0, ImGuiCol.ScrollbarBg, ScrollbarTrack);
		_00A0(ref P_0, ImGuiCol.ScrollbarGrab, ScrollbarGrab);
		_00A0(ref P_0, ImGuiCol.ScrollbarGrabHovered, ScrollbarGrabHover);
		_00A0(ref P_0, ImGuiCol.ScrollbarGrabActive, ScrollbarGrabActive);
		_00A0(ref P_0, ImGuiCol.CheckMark, BrandGold);
		_00A0(ref P_0, ImGuiCol.SliderGrab, BrandGold);
		_00A0(ref P_0, ImGuiCol.SliderGrabActive, BrandGoldLight);
		_00A0(ref P_0, ImGuiCol.Button, BtnNeutral);
		_00A0(ref P_0, ImGuiCol.ButtonHovered, BtnNeutralHover);
		_00A0(ref P_0, ImGuiCol.ButtonActive, BtnNeutralActive);
		_00A0(ref P_0, ImGuiCol.Header, StateHover);
		_00A0(ref P_0, ImGuiCol.HeaderHovered, _00A0(255, 255, 255, 0.1f));
		_00A0(ref P_0, ImGuiCol.HeaderActive, StateActive);
		_00A0(ref P_0, ImGuiCol.Separator, BorderLine);
		_00A0(ref P_0, ImGuiCol.SeparatorHovered, BorderLineStrong);
		_00A0(ref P_0, ImGuiCol.SeparatorActive, BorderLineStrong);
		_00A0(ref P_0, ImGuiCol.ResizeGrip, WithAlpha(BorderEdgeLight, 0.35f));
		_00A0(ref P_0, ImGuiCol.ResizeGripHovered, WithAlpha(BrandGold, 0.6f));
		_00A0(ref P_0, ImGuiCol.ResizeGripActive, WithAlpha(BrandGold, 0.8f));
		_00A0(ref P_0, ImGuiCol.Tab, new Vector4(0f, 0f, 0f, 0f));
		_00A0(ref P_0, ImGuiCol.TabHovered, TabHoverBg);
		_00A0(ref P_0, ImGuiCol.TabActive, TabSelectedBg);
		_00A0(ref P_0, ImGuiCol.TabUnfocused, new Vector4(0f, 0f, 0f, 0f));
		_00A0(ref P_0, ImGuiCol.TabUnfocusedActive, _00A0(227, 181, 75, 0.1f));
		_00A0(ref P_0, ImGuiCol.DockingPreview, WithAlpha(BrandGold, 0.25f));
		_00A0(ref P_0, ImGuiCol.NavHighlight, WithAlpha(BrandGold, 0.35f));
		_00A0(ref P_0, ImGuiCol.NavWindowingHighlight, WithAlpha(BrandGold, 0.55f));
		_00A0(ref P_0, ImGuiCol.NavWindowingDimBg, _00A0(5, 7, 10, 0.35f));
		_00A0(ref P_0, ImGuiCol.ModalWindowDimBg, _00A0(5, 7, 10, 0.6f));
		_00A0(ref P_0, ImGuiCol.PlotLines, BorderLineStrong);
		_00A0(ref P_0, ImGuiCol.PlotLinesHovered, BrandGold);
		_00A0(ref P_0, ImGuiCol.PlotHistogram, BorderCard);
		_00A0(ref P_0, ImGuiCol.PlotHistogramHovered, BrandGold);
		_00A0(ref P_0, ImGuiCol.TableHeaderBg, SurfaceTableHeader);
		_00A0(ref P_0, ImGuiCol.TableBorderStrong, BorderLineStrong);
		_00A0(ref P_0, ImGuiCol.TableBorderLight, WithAlpha(BorderCard, 0.8f));
		_00A0(ref P_0, ImGuiCol.TableRowBg, new Vector4(0f, 0f, 0f, 0f));
		_00A0(ref P_0, ImGuiCol.TableRowBgAlt, _00A0(255, 255, 255, 0.022f));
		_00A0(ref P_0, ImGuiCol.TextSelectedBg, StateSelected);
		_00A0(ref P_0, ImGuiCol.DragDropTarget, WithAlpha(BrandGold, 0.55f));
	}

	private static void _1680(ref int P_0, float P_1)
	{
		_00A0(ref P_0, ImGuiStyleVar.WindowPadding, new Vector2(16f * P_1, 12f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.FramePadding, new Vector2(8f * P_1, 5f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.FrameRounding, 4f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.FrameBorderSize, 1f);
		_00A0(ref P_0, ImGuiStyleVar.ChildRounding, 6f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.ChildBorderSize, 1f);
		_00A0(ref P_0, ImGuiStyleVar.PopupRounding, 8f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.PopupBorderSize, 1f);
		_00A0(ref P_0, ImGuiStyleVar.ItemSpacing, new Vector2(8f * P_1, 6f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.ItemInnerSpacing, new Vector2(6f * P_1, 4f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.CellPadding, new Vector2(8f * P_1, 4f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.IndentSpacing, 16f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.ScrollbarSize, 10f * P_1);
		ItemSpacingX = 8f;
		_00A0(ref P_0, ImGuiStyleVar.ScrollbarRounding, 5f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.GrabRounding, 4f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.GrabMinSize, 10f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.WindowRounding, 8f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.WindowBorderSize, 0f);
		_00A0(ref P_0, ImGuiStyleVar.WindowTitleAlign, new Vector2(0f, 0.5f));
		_00A0(ref P_0, ImGuiStyleVar.DisabledAlpha, 0.45f);
		_00A0(ref P_0, ImGuiStyleVar.Alpha, 1f);
	}

	private static void _2000(ref int P_0, float P_1)
	{
		_00A0(ref P_0, ImGuiStyleVar.WindowPadding, new Vector2(8f * P_1, 8f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.FramePadding, new Vector2(6f * P_1, 4f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.FrameRounding, 4f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.FrameBorderSize, 1f);
		_00A0(ref P_0, ImGuiStyleVar.ChildRounding, 6f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.ChildBorderSize, 1f);
		_00A0(ref P_0, ImGuiStyleVar.PopupRounding, 8f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.PopupBorderSize, 1f);
		_00A0(ref P_0, ImGuiStyleVar.ItemSpacing, new Vector2(6f * P_1, 0f));
		_00A0(ref P_0, ImGuiStyleVar.ItemInnerSpacing, new Vector2(6f * P_1, 4f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.CellPadding, new Vector2(8f * P_1, 3f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.IndentSpacing, 16f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.ScrollbarSize, 8f * P_1);
		ItemSpacingX = 6f;
		_00A0(ref P_0, ImGuiStyleVar.ScrollbarRounding, 4f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.GrabRounding, 4f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.GrabMinSize, 10f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.WindowRounding, 8f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.WindowBorderSize, 0f);
		_00A0(ref P_0, ImGuiStyleVar.WindowTitleAlign, new Vector2(0f, 0.5f));
		_00A0(ref P_0, ImGuiStyleVar.DisabledAlpha, 0.45f);
		_00A0(ref P_0, ImGuiStyleVar.Alpha, 1f);
	}

	private static void _2001(ref int P_0, float P_1)
	{
		_00A0(ref P_0, ImGuiStyleVar.WindowPadding, new Vector2(14f * P_1, 12f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.FramePadding, new Vector2(8f * P_1, 5f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.FrameRounding, 4f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.FrameBorderSize, 1f);
		_00A0(ref P_0, ImGuiStyleVar.PopupRounding, 8f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.PopupBorderSize, 1f);
		_00A0(ref P_0, ImGuiStyleVar.ItemSpacing, new Vector2(8f * P_1, 6f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.ItemInnerSpacing, new Vector2(6f * P_1, 4f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.CellPadding, new Vector2(8f * P_1, 4f * P_1));
		_00A0(ref P_0, ImGuiStyleVar.GrabRounding, 4f * P_1);
		ItemSpacingX = 8f;
		_00A0(ref P_0, ImGuiStyleVar.ScrollbarRounding, 5f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.ScrollbarSize, 10f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.WindowRounding, 8f * P_1);
		_00A0(ref P_0, ImGuiStyleVar.WindowBorderSize, 1f);
		_00A0(ref P_0, ImGuiStyleVar.DisabledAlpha, 0.45f);
		_00A0(ref P_0, ImGuiStyleVar.Alpha, 1f);
	}

	private static void _00A0(ref int P_0, ImGuiCol P_1, Vector4 P_2)
	{
		ImGui.PushStyleColor(P_1, P_2);
		P_0++;
	}

	private static void _00A0(ref int P_0, ImGuiStyleVar P_1, Vector2 P_2)
	{
		ImGui.PushStyleVar(P_1, P_2);
		P_0++;
	}

	private static void _00A0(ref int P_0, ImGuiStyleVar P_1, float P_2)
	{
		ImGui.PushStyleVar(P_1, P_2);
		P_0++;
	}

	public static void PopAll()
	{
		if (_2000_2064.Count == 0)
		{
			return;
		}
		List<int> list = new List<int>(_2000_2064.Keys);
		list.Sort();
		for (int num = list.Count - 1; num >= 0; num--)
		{
			(int, int) tuple = _2000_2064[list[num]];
			if (tuple.Item2 > 0)
			{
				ImGui.PopStyleVar(tuple.Item2);
			}
			if (tuple.Item1 > 0)
			{
				ImGui.PopStyleColor(tuple.Item1);
			}
		}
		_2000_2064.Clear();
		HudTextShadow = false;
		HudNeedsCompensation = false;
		HudSecondaryText = TextSecondary;
	}

	public static float ContentRightX()
	{
		return ImGui.GetWindowPos().X + ImGui.GetContentRegionMax().X;
	}

	public static bool IsHoveredInWindow(Vector2 min, Vector2 max)
	{
		if (ImGui.IsWindowHovered())
		{
			return ImGui.IsMouseHoveringRect(min, max, clip: true);
		}
		return false;
	}

	public static Vector4 Mix(Vector4 a, Vector4 b, float t)
	{
		return new Vector4(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t, a.W + (b.W - a.W) * t);
	}

	public static Vector4 WithAlpha(Vector4 c, float a)
	{
		return new Vector4(c.X, c.Y, c.Z, a);
	}

	public static Vector4 Shade(Vector4 c, float k)
	{
		return new Vector4(c.X * k, c.Y * k, c.Z * k, c.W);
	}

	public static uint U32(Vector4 c)
	{
		return ImGui.ColorConvertFloat4ToU32(c);
	}

	public static uint U32(byte r, byte g, byte b, byte a = byte.MaxValue)
	{
		return (uint)((a << 24) | (b << 16) | (g << 8) | r);
	}

	private static Vector4 _00A0(int P_0, int P_1, int P_2, float P_3 = 1f)
	{
		return new Vector4((float)P_0 / 255f, (float)P_1 / 255f, (float)P_2 / 255f, P_3);
	}

	public static float MeasureTextWidth(string text, float fontSize)
	{
		if (string.IsNullOrEmpty(text))
		{
			return 0f;
		}
		float x = ImGui.CalcTextSize(text).X;
		float fsBody = FsBody;
		if (fontSize <= 0f || fsBody <= 0f || Math.Abs(fontSize - fsBody) < 0.0001f)
		{
			return x;
		}
		return x * (fontSize / fsBody);
	}

	public static float MeasureTextHeight(float fontSize = 0f)
	{
		float y = ImGui.CalcTextSize("Ag中").Y;
		float fsBody = FsBody;
		if (fontSize <= 0f || fsBody <= 0f || Math.Abs(fontSize - fsBody) < 0.0001f)
		{
			return y;
		}
		return y * (fontSize / fsBody);
	}

	public static float BaselineOffset(float baselineUnits, float fontSize)
	{
		ImFontPtr font = ImGui.GetFont();
		if (font.IsNull || font.FontSize <= 0f)
		{
			return 0f;
		}
		return baselineUnits * (fontSize / font.FontSize);
	}

	public static string Ellipsis(string? text, float maxWidth, float fontSize = 0f)
	{
		if (string.IsNullOrEmpty(text))
		{
			return string.Empty;
		}
		if (maxWidth <= 0f)
		{
			return string.Empty;
		}
		float fontSize2 = ((fontSize > 0f) ? fontSize : FsBody);
		if (MeasureTextWidth(text, fontSize2) <= maxWidth)
		{
			return text;
		}
		float num = MeasureTextWidth("...", fontSize2);
		float num2 = maxWidth - num;
		if (num2 <= 0f)
		{
			return "...";
		}
		char[] array = text.ToCharArray();
		for (int num3 = array.Length; num3 > 0; num3--)
		{
			string text2 = new string(array, 0, num3);
			if (MeasureTextWidth(text2, fontSize2) <= num2)
			{
				return text2 + "...";
			}
		}
		return "...";
	}

	private static bool _00A0(char P_0)
	{
		if ((P_0 < 'a' || P_0 > 'z') && (P_0 < 'A' || P_0 > 'Z') && (P_0 < '0' || P_0 > '9') && P_0 != '_' && P_0 != '-' && P_0 != '.' && P_0 != '/' && P_0 != '#' && P_0 != '%' && P_0 != '&' && P_0 != '@' && P_0 != '+')
		{
			return P_0 == '\'';
		}
		return true;
	}

	public static List<string> WrapText(string? text, float maxWidth, float fontSize = 0f)
	{
		_2000 obj = default;
		obj._2008_2022 = maxWidth;
		obj._2008_2014 = new List<string>();
		if (text == null)
		{
			return obj._2008_2014;
		}
		if (text.Length == 0)
		{
			obj._2008_2014.Add(string.Empty);
			return obj._2008_2014;
		}
		obj._2008_2013 = ((fontSize > 0f) ? fontSize : FsBody);
		if (obj._2008_2022 <= 0f)
		{
			obj._2008_2014.Add(text);
			return obj._2008_2014;
		}
		obj._2008_2024 = MeasureTextWidth(" ", obj._2008_2013);
		obj._2008_2011 = new StringBuilder();
		obj._2008_2012 = 0f;
		obj._2008_2015 = false;
		int num = 0;
		while (num < text.Length)
		{
			char c = text[num];
			if (c == ' ')
			{
				if (obj._2008_2011.Length > 0)
				{
					obj._2008_2015 = true;
				}
				num++;
			}
			else if (_00A0(c))
			{
				int i;
				for (i = num; i < text.Length && _00A0(text[i]); i++)
				{
				}
				_1680(text.Substring(num, i - num), ref obj);
				num = i;
			}
			else
			{
				_1680(c.ToString(), ref obj);
				num++;
			}
		}
		obj._2008_2014.Add(obj._2008_2011.ToString());
		return obj._2008_2014;
	}

	public static void DrawText(ImDrawListPtr dl, Vector2 pos, Vector4 color, string text, float fontSize = 0f, bool shadow = false)
	{
		if (!string.IsNullOrEmpty(text))
		{
			float fontSize2 = ((fontSize > 0f) ? fontSize : FsBody);
			uint col = U32(color);
			if (shadow)
			{
				uint col2 = U32(TextShadow);
				dl.AddText(ImGui.GetFont(), fontSize2, pos + new Vector2(-1f, 0f), col2, text);
				dl.AddText(ImGui.GetFont(), fontSize2, pos + new Vector2(1f, 0f), col2, text);
				dl.AddText(ImGui.GetFont(), fontSize2, pos + new Vector2(0f, -1f), col2, text);
				dl.AddText(ImGui.GetFont(), fontSize2, pos + new Vector2(0f, 1f), col2, text);
			}
			dl.AddText(ImGui.GetFont(), fontSize2, pos, col, text);
		}
	}

	[CompilerGenerated]
	internal static void _00A0(string P_0, ref _2000 P_1)
	{
		P_1._2008_2011.Append(P_0);
		P_1._2008_2012 += MeasureTextWidth(P_0, P_1._2008_2013);
	}

	[CompilerGenerated]
	internal static void _00A0(ref _2000 P_0)
	{
		P_0._2008_2014.Add(P_0._2008_2011.ToString());
		P_0._2008_2011.Clear();
		P_0._2008_2012 = 0f;
		P_0._2008_2015 = false;
	}

	[CompilerGenerated]
	internal static void _1680(string P_0, ref _2000 P_1)
	{
		float num = MeasureTextWidth(P_0, P_1._2008_2013);
		if (P_1._2008_2011.Length == 0)
		{
			if (num <= P_1._2008_2022)
			{
				_00A0(P_0, ref P_1);
				return;
			}
			for (int i = 0; i < P_0.Length; i++)
			{
				char c = P_0[i];
				float num2 = MeasureTextWidth(c.ToString(), P_1._2008_2013);
				if (P_1._2008_2011.Length > 0 && P_1._2008_2012 + num2 > P_1._2008_2022)
				{
					_00A0(ref P_1);
				}
				_00A0(c.ToString(), ref P_1);
			}
			return;
		}
		float num3 = num + (P_1._2008_2015 ? P_1._2008_2024 : 0f);
		if (P_1._2008_2012 + num3 <= P_1._2008_2022)
		{
			if (P_1._2008_2015)
			{
				_00A0(" ", ref P_1);
			}
			_00A0(P_0, ref P_1);
			P_1._2008_2015 = false;
		}
		else if (P_0.Length > 0 && Array.IndexOf(_2000_206C, P_0[0]) >= 0 && P_1._2008_2011.Length >= 2)
		{
			char c2 = P_1._2008_2011[P_1._2008_2011.Length - 1];
			P_1._2008_2011.Length--;
			P_1._2008_2012 -= MeasureTextWidth(c2.ToString(), P_1._2008_2013);
			_00A0(ref P_1);
			_00A0(c2.ToString(), ref P_1);
			_00A0(P_0, ref P_1);
			P_1._2008_2015 = false;
		}
		else
		{
			_00A0(ref P_1);
			_1680(P_0, ref P_1);
		}
	}
}
