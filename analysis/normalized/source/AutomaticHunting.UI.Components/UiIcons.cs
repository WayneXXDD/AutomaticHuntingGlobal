using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiIcons
{
	public static void DrawReset(ImDrawListPtr arg0, Vector2 arg1, float arg2, uint arg3)
	{
		float arg4 = Math.Max(1f, arg2 * 0.22f);
		int arg5 = 14;
		Method_1159(arg0, arg1, arg2, -160f, 20f, arg3, arg4, arg5);
		Method_1159(arg0, arg1, arg2, 20f, 200f, arg3, arg4, arg5);
		Vector2 arg6 = arg1 + new Vector2(MathF.Cos(Method_1160(-160f)), MathF.Sin(Method_1160(-160f))) * arg2;
		Vector2 arg7 = arg1 + new Vector2(MathF.Cos(Method_1160(20f)), MathF.Sin(Method_1160(20f))) * arg2;
		AddTriangle(arg0, arg6, Method_1160(-70f), arg2 * 0.55f, arg3);
		AddTriangle(arg0, arg7, Method_1160(110f), arg2 * 0.55f, arg3);
	}

	public static void DrawGear(ImDrawListPtr arg0, Vector2 arg1, float arg2, uint arg3)
	{
		float num = arg2 * 0.64f;
		int num2 = 8;
		for (int i = 0; i < num2; i++)
		{
			float x = Method_1160((float)i * (360f / (float)num2));
			Vector2 vector = new Vector2(MathF.Cos(x), MathF.Sin(x));
			Vector2 p = arg1 + vector * (num * 0.92f);
			Vector2 p2 = arg1 + vector * arg2;
			arg0.AddLine(p, p2, arg3, Math.Max(1f, arg2 * 0.24f));
		}
		arg0.AddCircle(arg1, num * 0.92f, arg3, 0, Math.Max(1f, arg2 * 0.2f));
	}

	public static void DrawLock(ImDrawListPtr arg0, Vector2 arg1, float arg2, uint arg3)
	{
		float num = arg2 * 0.72f;
		float num2 = arg2 * 0.52f;
		Vector2 pMin = new Vector2(arg1.X - num * 0.5f, arg1.Y);
		arg0.AddRectFilled(pMax: new Vector2(arg1.X + num * 0.5f, arg1.Y + num2), pMin: pMin, col: arg3, rounding: arg2 * 0.12f);
		float num3 = num * 0.28f;
		Vector2 center = new Vector2(arg1.X, pMin.Y);
		arg0.AddCircle(center, num3, arg3, 0, Math.Max(1f, arg2 * 0.12f));
		arg0.AddLine(new Vector2(center.X - num3, center.Y), new Vector2(center.X - num3, pMin.Y), arg3, Math.Max(1f, arg2 * 0.12f));
		arg0.AddLine(new Vector2(center.X + num3, center.Y), new Vector2(center.X + num3, pMin.Y), arg3, Math.Max(1f, arg2 * 0.12f));
	}

	public static void DrawDragDots(ImDrawListPtr arg0, Vector2 arg1, Vector2 arg2, uint arg3)
	{
		float num = Math.Min(arg2.X - arg1.X, arg2.Y - arg1.Y);
		float num2 = Math.Max(1f, num * 0.075f);
		float num3 = num2 * 2.8f;
		int num4 = 2;
		int num5 = 3;
		float num6 = num3 * (float)(num4 - 1) + num2 * 2f;
		float num7 = num3 * (float)(num5 - 1) + num2 * 2f;
		float num8 = arg1.X + (arg2.X - arg1.X - num6) * 0.5f;
		float num9 = arg1.Y + (arg2.Y - arg1.Y - num7) * 0.5f;
		for (int i = 0; i < num5; i++)
		{
			for (int j = 0; j < num4; j++)
			{
				Vector2 center = new Vector2(num8 + (float)j * num3 + num2, num9 + (float)i * num3 + num2);
				arg0.AddCircleFilled(center, num2, arg3);
			}
		}
	}

	public static void DrawCheck(ImDrawListPtr arg0, Vector2 arg1, float arg2, uint arg3)
	{
		float thickness = Math.Max(1f, arg2 * 0.16f);
		arg0.AddLine(arg1 + new Vector2((0f - arg2) * 0.34f, 0f), arg1 + new Vector2((0f - arg2) * 0.08f, arg2 * 0.26f), arg3, thickness);
		arg0.AddLine(arg1 + new Vector2((0f - arg2) * 0.08f, arg2 * 0.26f), arg1 + new Vector2(arg2 * 0.36f, (0f - arg2) * 0.28f), arg3, thickness);
	}

	public static void DrawCross(ImDrawListPtr arg0, Vector2 arg1, float arg2, uint arg3)
	{
		float thickness = Math.Max(1f, arg2 * 0.16f);
		arg0.AddLine(arg1 + new Vector2((0f - arg2) * 0.3f, (0f - arg2) * 0.3f), arg1 + new Vector2(arg2 * 0.3f, arg2 * 0.3f), arg3, thickness);
		arg0.AddLine(arg1 + new Vector2(arg2 * 0.3f, (0f - arg2) * 0.3f), arg1 + new Vector2((0f - arg2) * 0.3f, arg2 * 0.3f), arg3, thickness);
	}

	public static void DrawPlus(ImDrawListPtr arg0, Vector2 arg1, float arg2, float arg3, uint arg4)
	{
		float num = arg2 * 0.5f;
		arg0.AddLine(arg1 + new Vector2(0f - num, 0f), arg1 + new Vector2(num, 0f), arg4, arg3);
		arg0.AddLine(arg1 + new Vector2(0f, 0f - num), arg1 + new Vector2(0f, num), arg4, arg3);
	}

	public static void DrawMinus(ImDrawListPtr arg0, Vector2 arg1, float arg2, float arg3, uint arg4)
	{
		float num = arg2 * 0.5f;
		arg0.AddLine(arg1 + new Vector2(0f - num, 0f), arg1 + new Vector2(num, 0f), arg4, arg3);
	}

	public static void DrawChevronUp(ImDrawListPtr arg0, Vector2 arg1, float arg2, uint arg3)
	{
		AddTriangle(arg0, arg1 + new Vector2(0f, (0f - arg2) * 0.5f), -(float)Math.PI / 2f, arg2, arg3);
	}

	public static void DrawChevronDown(ImDrawListPtr arg0, Vector2 arg1, float arg2, uint arg3)
	{
		AddTriangle(arg0, arg1 + new Vector2(0f, arg2 * 0.5f), (float)Math.PI / 2f, arg2, arg3);
	}

	public static void DrawExternalLink(ImDrawListPtr arg0, Vector2 arg1, float arg2, uint arg3)
	{
		float thickness = Math.Max(1f, arg2 * 0.11f);
		float num = arg2 * 0.5f;
		Vector2 p = new Vector2(arg1.X - num, arg1.Y - num);
		Vector2 vector = new Vector2(arg1.X + num, arg1.Y + num);
		float num2 = arg2 * 0.36f;
		arg0.AddLine(p, new Vector2(p.X, vector.Y), arg3, thickness);
		arg0.AddLine(new Vector2(p.X, vector.Y), vector, arg3, thickness);
		arg0.AddLine(vector, new Vector2(vector.X, vector.Y - num2), arg3, thickness);
		Vector2 vector2 = new Vector2(arg1.X + num, arg1.Y - num);
		Vector2 p2 = new Vector2(arg1.X + num - arg2 * 0.44f, arg1.Y - num + arg2 * 0.44f);
		arg0.AddLine(p2, vector2, arg3, thickness);
		float num3 = arg2 * 0.3f;
		arg0.AddLine(vector2, new Vector2(vector2.X - num3, vector2.Y), arg3, thickness);
		arg0.AddLine(vector2, new Vector2(vector2.X, vector2.Y + num3), arg3, thickness);
	}

	public static void AddTriangle(ImDrawListPtr arg0, Vector2 arg1, float arg2, float arg3, uint arg4)
	{
		Vector2 vector = new Vector2(MathF.Cos(arg2), MathF.Sin(arg2));
		Vector2 vector2 = new Vector2(0f - vector.Y, vector.X);
		Vector2 vector3 = arg1 - vector * arg3;
		Vector2 p = vector3 + vector2 * arg3 * 0.55f;
		Vector2 p2 = vector3 - vector2 * arg3 * 0.55f;
		arg0.AddTriangleFilled(arg1, p, p2, arg4);
	}

	private static void Method_1159(ImDrawListPtr arg0, Vector2 arg1, float arg2, float arg3, float arg4, uint arg5, float arg6, int arg7)
	{
		float num = (arg4 - arg3) / (float)arg7;
		Vector2 p = arg1 + new Vector2(MathF.Cos(Method_1160(arg3)), MathF.Sin(Method_1160(arg3))) * arg2;
		for (int i = 1; i <= arg7; i++)
		{
			float x = Method_1160(arg3 + num * (float)i);
			Vector2 vector = arg1 + new Vector2(MathF.Cos(x), MathF.Sin(x)) * arg2;
			arg0.AddLine(p, vector, arg5, arg6);
			p = vector;
		}
	}

	private static float Method_1160(float arg0)
	{
		return arg0 * ((float)Math.PI / 180f);
	}
}
