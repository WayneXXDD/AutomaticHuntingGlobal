using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiIcons
{
	public static void DrawReset(ImDrawListPtr dl, Vector2 center, float radius, uint color)
	{
		float num = Math.Max(1f, radius * 0.22f);
		int num2 = 14;
		_00A0(dl, center, radius, -160f, 20f, color, num, num2);
		_00A0(dl, center, radius, 20f, 200f, color, num, num2);
		Vector2 tip = center + new Vector2(MathF.Cos(_00A0(-160f)), MathF.Sin(_00A0(-160f))) * radius;
		Vector2 tip2 = center + new Vector2(MathF.Cos(_00A0(20f)), MathF.Sin(_00A0(20f))) * radius;
		AddTriangle(dl, tip, _00A0(-70f), radius * 0.55f, color);
		AddTriangle(dl, tip2, _00A0(110f), radius * 0.55f, color);
	}

	public static void DrawGear(ImDrawListPtr dl, Vector2 center, float radius, uint color)
	{
		float num = radius * 0.64f;
		int num2 = 8;
		for (int i = 0; i < num2; i++)
		{
			float x = _00A0((float)i * (360f / (float)num2));
			Vector2 vector = new Vector2(MathF.Cos(x), MathF.Sin(x));
			Vector2 p = center + vector * (num * 0.92f);
			Vector2 p2 = center + vector * radius;
			dl.AddLine(p, p2, color, Math.Max(1f, radius * 0.24f));
		}
		dl.AddCircle(center, num * 0.92f, color, 0, Math.Max(1f, radius * 0.2f));
	}

	public static void DrawLock(ImDrawListPtr dl, Vector2 center, float size, uint color)
	{
		float num = size * 0.72f;
		float num2 = size * 0.52f;
		Vector2 pMin = new Vector2(center.X - num * 0.5f, center.Y);
		dl.AddRectFilled(pMax: new Vector2(center.X + num * 0.5f, center.Y + num2), pMin: pMin, col: color, rounding: size * 0.12f);
		float num3 = num * 0.28f;
		Vector2 center2 = new Vector2(center.X, pMin.Y);
		dl.AddCircle(center2, num3, color, 0, Math.Max(1f, size * 0.12f));
		dl.AddLine(new Vector2(center2.X - num3, center2.Y), new Vector2(center2.X - num3, pMin.Y), color, Math.Max(1f, size * 0.12f));
		dl.AddLine(new Vector2(center2.X + num3, center2.Y), new Vector2(center2.X + num3, pMin.Y), color, Math.Max(1f, size * 0.12f));
	}

	public static void DrawDragDots(ImDrawListPtr dl, Vector2 min, Vector2 max, uint color)
	{
		float num = Math.Min(max.X - min.X, max.Y - min.Y);
		float num2 = Math.Max(1f, num * 0.075f);
		float num3 = num2 * 2.8f;
		int num4 = 2;
		int num5 = 3;
		float num6 = num3 * (float)(num4 - 1) + num2 * 2f;
		float num7 = num3 * (float)(num5 - 1) + num2 * 2f;
		float num8 = min.X + (max.X - min.X - num6) * 0.5f;
		float num9 = min.Y + (max.Y - min.Y - num7) * 0.5f;
		for (int i = 0; i < num5; i++)
		{
			for (int j = 0; j < num4; j++)
			{
				Vector2 center = new Vector2(num8 + (float)j * num3 + num2, num9 + (float)i * num3 + num2);
				dl.AddCircleFilled(center, num2, color);
			}
		}
	}

	public static void DrawCheck(ImDrawListPtr dl, Vector2 center, float size, uint color)
	{
		float thickness = Math.Max(1f, size * 0.16f);
		dl.AddLine(center + new Vector2((0f - size) * 0.34f, 0f), center + new Vector2((0f - size) * 0.08f, size * 0.26f), color, thickness);
		dl.AddLine(center + new Vector2((0f - size) * 0.08f, size * 0.26f), center + new Vector2(size * 0.36f, (0f - size) * 0.28f), color, thickness);
	}

	public static void DrawCross(ImDrawListPtr dl, Vector2 center, float size, uint color)
	{
		float thickness = Math.Max(1f, size * 0.16f);
		dl.AddLine(center + new Vector2((0f - size) * 0.3f, (0f - size) * 0.3f), center + new Vector2(size * 0.3f, size * 0.3f), color, thickness);
		dl.AddLine(center + new Vector2(size * 0.3f, (0f - size) * 0.3f), center + new Vector2((0f - size) * 0.3f, size * 0.3f), color, thickness);
	}

	public static void DrawPlus(ImDrawListPtr dl, Vector2 center, float size, float thickness, uint color)
	{
		float num = size * 0.5f;
		dl.AddLine(center + new Vector2(0f - num, 0f), center + new Vector2(num, 0f), color, thickness);
		dl.AddLine(center + new Vector2(0f, 0f - num), center + new Vector2(0f, num), color, thickness);
	}

	public static void DrawMinus(ImDrawListPtr dl, Vector2 center, float size, float thickness, uint color)
	{
		float num = size * 0.5f;
		dl.AddLine(center + new Vector2(0f - num, 0f), center + new Vector2(num, 0f), color, thickness);
	}

	public static void DrawChevronUp(ImDrawListPtr dl, Vector2 center, float size, uint color)
	{
		AddTriangle(dl, center + new Vector2(0f, (0f - size) * 0.5f), -(float)Math.PI / 2f, size, color);
	}

	public static void DrawChevronDown(ImDrawListPtr dl, Vector2 center, float size, uint color)
	{
		AddTriangle(dl, center + new Vector2(0f, size * 0.5f), (float)Math.PI / 2f, size, color);
	}

	public static void DrawExternalLink(ImDrawListPtr dl, Vector2 center, float size, uint color)
	{
		float thickness = Math.Max(1f, size * 0.11f);
		float num = size * 0.5f;
		Vector2 p = new Vector2(center.X - num, center.Y - num);
		Vector2 vector = new Vector2(center.X + num, center.Y + num);
		float num2 = size * 0.36f;
		dl.AddLine(p, new Vector2(p.X, vector.Y), color, thickness);
		dl.AddLine(new Vector2(p.X, vector.Y), vector, color, thickness);
		dl.AddLine(vector, new Vector2(vector.X, vector.Y - num2), color, thickness);
		Vector2 vector2 = new Vector2(center.X + num, center.Y - num);
		Vector2 p2 = new Vector2(center.X + num - size * 0.44f, center.Y - num + size * 0.44f);
		dl.AddLine(p2, vector2, color, thickness);
		float num3 = size * 0.3f;
		dl.AddLine(vector2, new Vector2(vector2.X - num3, vector2.Y), color, thickness);
		dl.AddLine(vector2, new Vector2(vector2.X, vector2.Y + num3), color, thickness);
	}

	public static void AddTriangle(ImDrawListPtr dl, Vector2 tip, float directionRad, float size, uint color)
	{
		Vector2 vector = new Vector2(MathF.Cos(directionRad), MathF.Sin(directionRad));
		Vector2 vector2 = new Vector2(0f - vector.Y, vector.X);
		Vector2 vector3 = tip - vector * size;
		Vector2 p = vector3 + vector2 * size * 0.55f;
		Vector2 p2 = vector3 - vector2 * size * 0.55f;
		dl.AddTriangleFilled(tip, p, p2, color);
	}

	private static void _00A0(ImDrawListPtr P_0, Vector2 P_1, float P_2, float P_3, float P_4, uint P_5, float P_6, int P_7)
	{
		float num = (P_4 - P_3) / (float)P_7;
		Vector2 p = P_1 + new Vector2(MathF.Cos(_00A0(P_3)), MathF.Sin(_00A0(P_3))) * P_2;
		for (int i = 1; i <= P_7; i++)
		{
			float x = _00A0(P_3 + num * (float)i);
			Vector2 vector = P_1 + new Vector2(MathF.Cos(x), MathF.Sin(x)) * P_2;
			P_0.AddLine(p, vector, P_5, P_6);
			p = vector;
		}
	}

	private static float _00A0(float P_0)
	{
		return P_0 * ((float)Math.PI / 180f);
	}
}
