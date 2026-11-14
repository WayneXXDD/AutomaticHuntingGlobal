using System;
using System.Runtime.CompilerServices;

namespace AutomaticHunting;

[Serializable]
public sealed class LeaderEntry
{
	[CompilerGenerated]
	private string field_576 = "";

	[CompilerGenerated]
	private bool field_577 = true;

	public string Name
	{
		[CompilerGenerated]
		get
		{
			return field_576;
		}
		[CompilerGenerated]
		set
		{
			field_576 = value;
		}
	}

	public bool Enabled
	{
		[CompilerGenerated]
		get
		{
			return field_577;
		}
		[CompilerGenerated]
		set
		{
			field_577 = value;
		}
	}
}
