using System;
using System.Runtime.CompilerServices;

namespace AutomaticHunting;

[Serializable]
public sealed class HuntChainStep
{
	[CompilerGenerated]
	private WorldTravelPreset _1680_2028 = new WorldTravelPreset();

	[CompilerGenerated]
	private int _1680_2029 = 12;

	public WorldTravelPreset Preset
	{
		[CompilerGenerated]
		get
		{
			return _1680_2028;
		}
		[CompilerGenerated]
		set
		{
			_1680_2028 = value;
		}
	}

	public int KillTarget
	{
		[CompilerGenerated]
		get
		{
			return _1680_2029;
		}
		[CompilerGenerated]
		set
		{
			_1680_2029 = value;
		}
	}
}
