using System;
using System.Runtime.CompilerServices;

namespace AutomaticHunting;

[Serializable]
public sealed class HuntChainStep
{
	[CompilerGenerated]
	private WorldTravelPreset field_569 = new WorldTravelPreset();

	[CompilerGenerated]
	private int field_570 = 12;

	public WorldTravelPreset Preset
	{
		[CompilerGenerated]
		get
		{
			return field_569;
		}
		[CompilerGenerated]
		set
		{
			field_569 = value;
		}
	}

	public int KillTarget
	{
		[CompilerGenerated]
		get
		{
			return field_570;
		}
		[CompilerGenerated]
		set
		{
			field_570 = value;
		}
	}
}
