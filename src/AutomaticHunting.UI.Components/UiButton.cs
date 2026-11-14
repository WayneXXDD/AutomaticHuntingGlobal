using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiButton
{
	public readonly struct RecoveredType_168
	{
		public readonly Vector4 field_1031;

		public readonly Vector4 field_1032;

		public readonly Vector4 field_1033;

		public readonly Vector4 field_1034;

		public readonly Vector4? field_1035;

		public RecoveredType_168(Vector4 arg0, Vector4 arg1, Vector4 arg2, Vector4 arg3, Vector4? arg4 = null)
		{
			field_1031 = arg0;
			field_1032 = arg1;
			field_1033 = arg2;
			field_1034 = arg3;
			field_1035 = arg4;
		}
	}

	public static bool Draw(string arg0, UiVariant arg1 = UiVariant.Neutral, Vector2? arg2 = null, string? arg3 = null, bool arg4 = true)
	{
		Vector2 arg5 = arg2 ?? new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (!arg4)
		{
			ImGui.BeginDisabled();
		}
		bool result = Method_1058(arg0, arg1, arg5, arg4);
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		if (!arg4)
		{
			ImGui.EndDisabled();
		}
		if (arg3 != null && Theme.IsHoveredInWindow(itemRectMin, itemRectMax))
		{
			ShowTooltip(arg3);
		}
		return result;
	}

	public static bool DrawInline(string arg0, float arg1, UiVariant arg2 = UiVariant.Neutral, string? arg3 = null, bool arg4 = true)
	{
		return Draw(arg0, arg2, new Vector2(arg1, Theme.ControlHeight), arg3, arg4);
	}

	public static bool DrawIcon(string arg0, Vector2 arg1, UiVariant arg2 = UiVariant.Neutral, string? arg3 = null, bool arg4 = true)
	{
		if (!arg4)
		{
			ImGui.BeginDisabled();
		}
		bool result = Method_1058(arg0, arg2, arg1, arg4);
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		if (!arg4)
		{
			ImGui.EndDisabled();
		}
		if (arg3 != null && Theme.IsHoveredInWindow(itemRectMin, itemRectMax))
		{
			ShowTooltip(arg3);
		}
		return result;
	}

	public static bool DrawGear(Vector2 arg0, string? arg1 = null, bool arg2 = true, Action? arg3 = null)
	{
		Vector4 arg4 = Method_1057(UiVariant.Ghost).field_1034;
		Vector4 vector = Theme.SurfaceCard;
		Vector4 col = Method_1055(vector, new Vector4(1f, 1f, 1f, 1f), 0.07f);
		Vector4 col2 = Method_1055(vector, new Vector4(1f, 1f, 1f, 1f), 0.14f);
		if (!arg2)
		{
			vector = Theme.WithAlpha(vector, vector.W * 0.45f);
			col = vector;
			col2 = vector;
			arg4 = Theme.WithAlpha(arg4, arg4.W * 0.45f);
		}
		if (!arg2)
		{
			ImGui.BeginDisabled();
		}
		ImGui.PushStyleColor(ImGuiCol.Button, vector);
		ImGui.PushStyleColor(ImGuiCol.ButtonHovered, col);
		ImGui.PushStyleColor(ImGuiCol.ButtonActive, col2);
		ImGui.PushStyleColor(ImGuiCol.Border, Theme.BorderControl);
		ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 1f);
		bool result = ImGui.Button("##gear", arg0);
		ImGui.PopStyleVar();
		ImGui.PopStyleColor(4);
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		bool flag = Theme.IsHoveredInWindow(itemRectMin, itemRectMax);
		if (!arg2)
		{
			ImGui.EndDisabled();
		}
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 arg5 = (itemRectMin + itemRectMax) * 0.5f;
		float arg6 = MathF.Min(itemRectMax.X - itemRectMin.X, itemRectMax.Y - itemRectMin.Y) * 0.3f;
		uint arg7 = (flag ? Theme.U32(arg4) : Theme.U32(Theme.WithAlpha(arg4, arg4.W * 0.85f)));
		UiIcons.DrawGear(windowDrawList, arg5, arg6, arg7);
		if (((arg3 != null) & arg2 & flag) && ImGui.GetIO().MouseClicked[1])
		{
			arg3();
		}
		if ((arg1 != null) & flag)
		{
			ShowTooltip(arg1);
		}
		return result;
	}

	public static void ShowTooltip(string arg0)
	{
		ImGui.BeginTooltip();
		TraditionalText.TextUnformatted(arg0);
		ImGui.EndTooltip();
	}

	private static Vector4 Method_1055(Vector4 arg0, Vector4 arg1, float arg2)
	{
		float num = Math.Clamp(arg2, 0f, 1f);
		return new Vector4(arg0.X + (arg1.X - arg0.X) * num, arg0.Y + (arg1.Y - arg0.Y) * num, arg0.Z + (arg1.Z - arg0.Z) * num, 1f);
	}

	public static bool DrawLink(string arg0, float arg1, string? arg2 = null)
	{
		ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0f, 0f, 0f, 0f));
		ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0f, 0f, 0f, 0f));
		ImGui.PushStyleColor(ImGuiCol.Text, Theme.BrandGold);
		bool result = ImGui.Button(arg0, new Vector2(arg1, 0f));
		bool flag = ImGui.IsItemHovered();
		ImGui.PopStyleColor(3);
		if (flag)
		{
			Vector2 itemRectMin = ImGui.GetItemRectMin();
			Vector2 itemRectMax = ImGui.GetItemRectMax();
			ImGui.GetWindowDrawList().AddLine(new Vector2(itemRectMin.X, itemRectMax.Y - 1f), new Vector2(itemRectMax.X, itemRectMax.Y - 1f), Theme.U32(Theme.BrandGoldLight), 1f);
			if (arg2 != null)
			{
				TraditionalText.SetTooltip(arg2);
			}
		}
		return result;
	}

	private static RecoveredType_168 Method_1057(UiVariant arg0)
	{
		return arg0 switch
		{
			UiVariant.Primary => new RecoveredType_168(Theme.BtnPrimary, Theme.BtnPrimaryHover, Theme.BtnPrimaryActive, Theme.TextOnBrand), 
			UiVariant.Success => new RecoveredType_168(Theme.BtnSuccess, Theme.BtnSuccessHover, Theme.BtnSuccessActive, new Vector4(1f, 1f, 1f, 1f)), 
			UiVariant.Danger => new RecoveredType_168(Theme.BtnDanger, Theme.BtnDangerHover, Theme.BtnDangerActive, Theme.BtnDangerText, Theme.BorderDanger), 
			UiVariant.Accent => new RecoveredType_168(Theme.BtnAccent, Theme.BtnAccentHover, Theme.BtnAccentActive, Theme.BtnAccentText), 
			UiVariant.DangerSolid => new RecoveredType_168(Theme.BtnDangerSolid, Theme.BtnDangerSolidHover, Theme.BtnDangerSolidActive, new Vector4(1f, 1f, 1f, 1f)), 
			UiVariant.Brand => new RecoveredType_168(new Vector4(0f, 0f, 0f, 0f), Theme.BtnBrandHover, Theme.BtnBrandActive, Theme.BrandGold, Theme.BrandGoldDim), 
			UiVariant.Ghost => new RecoveredType_168(new Vector4(0f, 0f, 0f, 0f), Theme.BtnGhostHover, Theme.BtnGhostActive, Theme.BtnGhostText), 
			UiVariant.Link => new RecoveredType_168(new Vector4(0f, 0f, 0f, 0f), Theme.WithAlpha(Theme.BrandGold, 0.12f), Theme.WithAlpha(Theme.BrandGold, 0.2f), Theme.BrandGold), 
			UiVariant.Icon => new RecoveredType_168(new Vector4(0f, 0f, 0f, 0f), Theme.BtnGhostHover, Theme.BtnGhostActive, Theme.BtnGhostText), 
			_ => new RecoveredType_168(Theme.BtnNeutral, Theme.BtnNeutralHover, Theme.BtnNeutralActive, Theme.TextPrimary, Theme.BorderControl), 
		};
	}

	private static bool Method_1058(string arg0, UiVariant arg1, Vector2 arg2, bool arg3)
	{
		RecoveredType_168 recoveredType_ = Method_1057(arg1);
		Vector4 vector = recoveredType_.field_1031;
		Vector4 col = recoveredType_.field_1032;
		Vector4 col2 = recoveredType_.field_1033;
		Vector4 vector2 = recoveredType_.field_1034;
		Vector4? vector3 = recoveredType_.field_1035;
		if (!arg3)
		{
			vector = Theme.WithAlpha(vector, vector.W * 0.45f);
			col = vector;
			col2 = vector;
			vector2 = Theme.WithAlpha(vector2, vector2.W * 0.45f);
			vector3 = null;
		}
		ImGui.PushStyleColor(ImGuiCol.Button, vector);
		ImGui.PushStyleColor(ImGuiCol.ButtonHovered, col);
		ImGui.PushStyleColor(ImGuiCol.ButtonActive, col2);
		ImGui.PushStyleColor(ImGuiCol.Text, vector2);
		bool result;
		if (vector3.HasValue)
		{
			ImGui.PushStyleColor(ImGuiCol.Border, vector3.Value);
			ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 1f);
			result = ImGui.Button(arg0, arg2);
			ImGui.PopStyleVar();
			ImGui.PopStyleColor(5);
		}
		else
		{
			result = ImGui.Button(arg0, arg2);
			ImGui.PopStyleColor(4);
		}
		return result;
	}
}
