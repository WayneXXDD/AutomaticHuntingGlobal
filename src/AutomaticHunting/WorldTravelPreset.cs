using System;
using System.Runtime.CompilerServices;

namespace AutomaticHunting;

[Serializable]
public sealed class WorldTravelPreset
{
	[CompilerGenerated]
	private string field_563 = "";

	[CompilerGenerated]
	private int field_564 = 3;

	[CompilerGenerated]
	private uint field_565;

	[CompilerGenerated]
	private string field_566 = "";

	[CompilerGenerated]
	private uint field_567;

	[CompilerGenerated]
	private string field_568 = "";

	public string ServerName
	{
		[CompilerGenerated]
		get
		{
			return field_563;
		}
		[CompilerGenerated]
		set
		{
			field_563 = value;
		}
	}

	public int ExVersion
	{
		[CompilerGenerated]
		get
		{
			return field_564;
		}
		[CompilerGenerated]
		set
		{
			field_564 = value;
		}
	}

	public uint MapRowId
	{
		[CompilerGenerated]
		get
		{
			return field_565;
		}
		[CompilerGenerated]
		set
		{
			field_565 = value;
		}
	}

	public string MapName
	{
		[CompilerGenerated]
		get
		{
			return field_566;
		}
		[CompilerGenerated]
		set
		{
			field_566 = value;
		}
	}

	public uint AetheryteId
	{
		[CompilerGenerated]
		get
		{
			return field_567;
		}
		[CompilerGenerated]
		set
		{
			field_567 = value;
		}
	}

	public string AetheryteName
	{
		[CompilerGenerated]
		get
		{
			return field_568;
		}
		[CompilerGenerated]
		set
		{
			field_568 = value;
		}
	}
}
