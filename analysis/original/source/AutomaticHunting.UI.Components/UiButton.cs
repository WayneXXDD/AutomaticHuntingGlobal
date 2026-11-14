using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiButton
{
	private readonly struct _00A0(Vector4 P_0, Vector4 P_1, Vector4 P_2, Vector4 P_3, Vector4? P_4 = null)
	{
		public readonly Vector4 _2008_204D = P_0;

		public readonly Vector4 _2008_204E = P_1;

		public readonly Vector4 _2008_204F = P_2;

		public readonly Vector4 _2008_2050 = P_3;

		public readonly Vector4? _2008_2051 = P_4;
	}

	public static bool Draw(string label, UiVariant variant = UiVariant.Neutral, Vector2? size = null, string? tooltip = null, bool enabled = true)
	{
		Vector2 vector = size ?? new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (!enabled)
		{
			ImGui.BeginDisabled();
		}
		bool result = _00A0(label, variant, vector, enabled);
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		if (!enabled)
		{
			ImGui.EndDisabled();
		}
		if (tooltip != null && Theme.IsHoveredInWindow(itemRectMin, itemRectMax))
		{
			ShowTooltip(tooltip);
		}
		return result;
	}

	public static bool DrawInline(string label, float width, UiVariant variant = UiVariant.Neutral, string? tooltip = null, bool enabled = true)
	{
		return Draw(label, variant, new Vector2(width, Theme.ControlHeight), tooltip, enabled);
	}

	public static bool DrawIcon(string icon, Vector2 size, UiVariant variant = UiVariant.Neutral, string? tooltip = null, bool enabled = true)
	{
		if (!enabled)
		{
			ImGui.BeginDisabled();
		}
		bool result = _00A0(icon, variant, size, enabled);
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		if (!enabled)
		{
			ImGui.EndDisabled();
		}
		if (tooltip != null && Theme.IsHoveredInWindow(itemRectMin, itemRectMax))
		{
			ShowTooltip(tooltip);
		}
		return result;
	}

	public static bool DrawGear(Vector2 size, string? tooltip = null, bool enabled = true, Action? onRightClick = null)
	{
		Vector4 c = _00A0(UiVariant.Ghost)._2008_2050;
		Vector4 vector = Theme.SurfaceCard;
		Vector4 col = _00A0(vector, new Vector4(1f, 1f, 1f, 1f), 0.07f);
		Vector4 col2 = _00A0(vector, new Vector4(1f, 1f, 1f, 1f), 0.14f);
		if (!enabled)
		{
			vector = Theme.WithAlpha(vector, vector.W * 0.45f);
			col = vector;
			col2 = vector;
			c = Theme.WithAlpha(c, c.W * 0.45f);
		}
		if (!enabled)
		{
			ImGui.BeginDisabled();
		}
		ImGui.PushStyleColor(ImGuiCol.Button, vector);
		ImGui.PushStyleColor(ImGuiCol.ButtonHovered, col);
		ImGui.PushStyleColor(ImGuiCol.ButtonActive, col2);
		ImGui.PushStyleColor(ImGuiCol.Border, Theme.BorderControl);
		ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 1f);
		bool result = ImGui.Button("##gear", size);
		ImGui.PopStyleVar();
		ImGui.PopStyleColor(4);
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		bool flag = Theme.IsHoveredInWindow(itemRectMin, itemRectMax);
		if (!enabled)
		{
			ImGui.EndDisabled();
		}
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 center = (itemRectMin + itemRectMax) * 0.5f;
		float radius = MathF.Min(itemRectMax.X - itemRectMin.X, itemRectMax.Y - itemRectMin.Y) * 0.3f;
		uint color = (flag ? Theme.U32(c) : Theme.U32(Theme.WithAlpha(c, c.W * 0.85f)));
		UiIcons.DrawGear(windowDrawList, center, radius, color);
		if (((onRightClick != null) & enabled & flag) && ImGui.GetIO().MouseClicked[1])
		{
			onRightClick();
		}
		if ((tooltip != null) & flag)
		{
			ShowTooltip(tooltip);
		}
		return result;
	}

	public static void ShowTooltip(string text)
	{
		ImGui.BeginTooltip();
		ImGui.TextUnformatted(text);
		ImGui.EndTooltip();
	}

	private static Vector4 _00A0(Vector4 P_0, Vector4 P_1, float P_2)
	{
		float num = Math.Clamp(P_2, 0f, 1f);
		return new Vector4(P_0.X + (P_1.X - P_0.X) * num, P_0.Y + (P_1.Y - P_0.Y) * num, P_0.Z + (P_1.Z - P_0.Z) * num, 1f);
	}

	public static bool DrawLink(string label, float width, string? tooltip = null)
	{
		ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0f, 0f, 0f, 0f));
		ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0f, 0f, 0f, 0f));
		ImGui.PushStyleColor(ImGuiCol.Text, Theme.BrandGold);
		bool result = ImGui.Button(label, new Vector2(width, 0f));
		bool flag = ImGui.IsItemHovered();
		ImGui.PopStyleColor(3);
		if (flag)
		{
			Vector2 itemRectMin = ImGui.GetItemRectMin();
			Vector2 itemRectMax = ImGui.GetItemRectMax();
			ImGui.GetWindowDrawList().AddLine(new Vector2(itemRectMin.X, itemRectMax.Y - 1f), new Vector2(itemRectMax.X, itemRectMax.Y - 1f), Theme.U32(Theme.BrandGoldLight), 1f);
			if (tooltip != null)
			{
				ImGui.SetTooltip(tooltip);
			}
		}
		return result;
	}

	private static _00A0 _00A0(UiVariant P_0)
	{
		return P_0 switch
		{
			UiVariant.Primary => new _00A0(Theme.BtnPrimary, Theme.BtnPrimaryHover, Theme.BtnPrimaryActive, Theme.TextOnBrand), 
			UiVariant.Success => new _00A0(Theme.BtnSuccess, Theme.BtnSuccessHover, Theme.BtnSuccessActive, new Vector4(1f, 1f, 1f, 1f)), 
			UiVariant.Danger => new _00A0(Theme.BtnDanger, Theme.BtnDangerHover, Theme.BtnDangerActive, Theme.BtnDangerText, Theme.BorderDanger), 
			UiVariant.Accent => new _00A0(Theme.BtnAccent, Theme.BtnAccentHover, Theme.BtnAccentActive, Theme.BtnAccentText), 
			UiVariant.DangerSolid => new _00A0(Theme.BtnDangerSolid, Theme.BtnDangerSolidHover, Theme.BtnDangerSolidActive, new Vector4(1f, 1f, 1f, 1f)), 
			UiVariant.Brand => new _00A0(new Vector4(0f, 0f, 0f, 0f), Theme.BtnBrandHover, Theme.BtnBrandActive, Theme.BrandGold, Theme.BrandGoldDim), 
			UiVariant.Ghost => new _00A0(new Vector4(0f, 0f, 0f, 0f), Theme.BtnGhostHover, Theme.BtnGhostActive, Theme.BtnGhostText), 
			UiVariant.Link => new _00A0(new Vector4(0f, 0f, 0f, 0f), Theme.WithAlpha(Theme.BrandGold, 0.12f), Theme.WithAlpha(Theme.BrandGold, 0.2f), Theme.BrandGold), 
			UiVariant.Icon => new _00A0(new Vector4(0f, 0f, 0f, 0f), Theme.BtnGhostHover, Theme.BtnGhostActive, Theme.BtnGhostText), 
			_ => new _00A0(Theme.BtnNeutral, Theme.BtnNeutralHover, Theme.BtnNeutralActive, Theme.TextPrimary, Theme.BorderControl), 
		};
	}

	private static bool _00A0(string P_0, UiVariant P_1, Vector2 P_2, bool P_3)
	{
		_00A0 obj = _00A0(P_1);
		Vector4 vector = obj._2008_204D;
		Vector4 col = obj._2008_204E;
		Vector4 col2 = obj._2008_204F;
		Vector4 vector2 = obj._2008_2050;
		Vector4? vector3 = obj._2008_2051;
		if (!P_3)
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
			result = ImGui.Button(P_0, P_2);
			ImGui.PopStyleVar();
			ImGui.PopStyleColor(5);
		}
		else
		{
			result = ImGui.Button(P_0, P_2);
			ImGui.PopStyleColor(4);
		}
		return result;
	}
}
