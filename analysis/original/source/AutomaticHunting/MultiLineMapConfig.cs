using System;
using System.Runtime.CompilerServices;

namespace AutomaticHunting;

[Serializable]
public sealed class MultiLineMapConfig
{
	[CompilerGenerated]
	private string _1680_2011 = string.Empty;

	[CompilerGenerated]
	private int _1680_2012 = 3;

	[CompilerGenerated]
	private int _1680_2013 = 2;

	public string MapId
	{
		[CompilerGenerated]
		get
		{
			return _1680_2011;
		}
		[CompilerGenerated]
		set
		{
			_1680_2011 = value;
		}
	}

	public int InstanceCount
	{
		[CompilerGenerated]
		get
		{
			return _1680_2012;
		}
		[CompilerGenerated]
		set
		{
			_1680_2012 = value;
		}
	}

	public int AMobCount
	{
		[CompilerGenerated]
		get
		{
			return _1680_2013;
		}
		[CompilerGenerated]
		set
		{
			_1680_2013 = value;
		}
	}
}
