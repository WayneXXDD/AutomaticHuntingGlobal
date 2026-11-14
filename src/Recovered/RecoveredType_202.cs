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

	internal static readonly RecoveredType_204 field_1193 = System.Runtime.InteropServices.MemoryMarshal.Read<RecoveredType_204>(new byte[] { 0x30, 0x00, 0x48, 0x00, 0x45, 0x00, 0x4C, 0x00, 0x49, 0x00, 0x54, 0x00, 0x31, 0x00 });

	internal static readonly RecoveredType_206 field_1194 = System.Runtime.InteropServices.MemoryMarshal.Read<RecoveredType_206>(new byte[] { 0x0C, 0xFF, 0x02, 0x30, 0x01, 0x30, 0x1B, 0xFF, 0x1A, 0xFF, 0x1F, 0xFF, 0x01, 0xFF, 0x09, 0xFF, 0x0B, 0x30, 0x0D, 0x30, 0x0F, 0x30, 0x11, 0x30, 0x1D, 0x20, 0x19, 0x20, 0x26, 0x20, 0xB7, 0x00, 0x25, 0x00, 0x03, 0x21 });

	internal static readonly RecoveredType_203 field_1195 = System.Runtime.InteropServices.MemoryMarshal.Read<RecoveredType_203>(new byte[] { 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x40 });

	internal static readonly RecoveredType_205 field_1196 = System.Runtime.InteropServices.MemoryMarshal.Read<RecoveredType_205>(new byte[] { 0x2D, 0x00, 0x00, 0x00, 0x33, 0x00, 0x00, 0x00, 0x19, 0x00, 0x00, 0x00, 0x1E, 0x00, 0x00, 0x00, 0x21, 0x00, 0x00, 0x00, 0x26, 0x00, 0x00, 0x00, 0x27, 0x00, 0x00, 0x00, 0x1A, 0x00, 0x00, 0x00 });

	internal static readonly RecoveredType_203 field_1197 = System.Runtime.InteropServices.MemoryMarshal.Read<RecoveredType_203>(new byte[] { 0x03, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00 });

	internal static ReadOnlySpan<TMethod1> Method_1213<TMethod0, TMethod1>(in TMethod0 arg0, int arg1)
	{
		return MemoryMarshal.CreateReadOnlySpan(in Unsafe.As<TMethod0, TMethod1>(ref Unsafe.AsRef(in arg0)), arg1);
	}

	internal static ref TMethod1 Method_1214<TMethod0, TMethod1>(ref TMethod0 arg0, int arg1)
	{
		return ref Unsafe.Add(ref Unsafe.As<TMethod0, TMethod1>(ref arg0), arg1);
	}
}

