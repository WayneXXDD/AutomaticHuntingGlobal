using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Recovered;

[CompilerGenerated]
public sealed class RecoveredType_202
{
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 12)]
	public struct RecoveredType_203
	{
	}

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 14)]
	public struct RecoveredType_204
	{
	}

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 32)]
	public struct RecoveredType_205
	{
	}

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 36)]
	public struct RecoveredType_206
	{
	}

	internal static readonly RecoveredType_204 field_1193/* Not supported: data(30 00 48 00 45 00 4C 00 49 00 54 00 31 00) */;

	internal static readonly RecoveredType_206 field_1194/* Not supported: data(0C FF 02 30 01 30 1B FF 1A FF 1F FF 01 FF 09 FF 0B 30 0D 30 0F 30 11 30 1D 20 19 20 26 20 B7 00 25 00 03 21) */;

	internal static readonly RecoveredType_203 field_1195/* Not supported: data(00 00 80 3F 00 00 80 3F 00 00 00 40) */;

	internal static readonly RecoveredType_205 field_1196/* Not supported: data(2D 00 00 00 33 00 00 00 19 00 00 00 1E 00 00 00 21 00 00 00 26 00 00 00 27 00 00 00 1A 00 00 00) */;

	internal static readonly RecoveredType_203 field_1197/* Not supported: data(03 00 00 00 04 00 00 00 05 00 00 00) */;

	internal static ReadOnlySpan<TMethod1> Method_1213<TMethod0, TMethod1>(in TMethod0 arg0, int arg1)
	{
		return MemoryMarshal.CreateReadOnlySpan(in Unsafe.As<TMethod0, TMethod1>(ref Unsafe.AsRef(in arg0)), arg1);
	}

	internal static ref TMethod1 Method_1214<TMethod0, TMethod1>(ref TMethod0 arg0, int arg1)
	{
		return ref Unsafe.Add(ref Unsafe.As<TMethod0, TMethod1>(ref arg0), arg1);
	}
}
