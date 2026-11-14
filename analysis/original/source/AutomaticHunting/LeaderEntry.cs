using System;
using System.Runtime.CompilerServices;

namespace AutomaticHunting;

[Serializable]
public sealed class LeaderEntry
{
	[CompilerGenerated]
	private string _1680_202F = "";

	[CompilerGenerated]
	private bool _1680_2032 = true;

	public string Name
	{
		[CompilerGenerated]
		get
		{
			return _1680_202F;
		}
		[CompilerGenerated]
		set
		{
			_1680_202F = value;
		}
	}

	public bool Enabled
	{
		[CompilerGenerated]
		get
		{
			return _1680_2032;
		}
		[CompilerGenerated]
		set
		{
			_1680_2032 = value;
		}
	}
}
