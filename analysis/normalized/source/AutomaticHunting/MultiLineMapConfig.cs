using System;
using System.Runtime.CompilerServices;

namespace AutomaticHunting;

[Serializable]
public sealed class MultiLineMapConfig
{
	[CompilerGenerated]
	private string field_560 = string.Empty;

	[CompilerGenerated]
	private int field_561 = 3;

	[CompilerGenerated]
	private int field_562 = 2;

	public string MapId
	{
		[CompilerGenerated]
		get
		{
			return field_560;
		}
		[CompilerGenerated]
		set
		{
			field_560 = value;
		}
	}

	public int InstanceCount
	{
		[CompilerGenerated]
		get
		{
			return field_561;
		}
		[CompilerGenerated]
		set
		{
			field_561 = value;
		}
	}

	public int AMobCount
	{
		[CompilerGenerated]
		get
		{
			return field_562;
		}
		[CompilerGenerated]
		set
		{
			field_562 = value;
		}
	}
}
