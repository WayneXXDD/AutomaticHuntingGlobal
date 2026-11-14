using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AutomaticHunting;

[Serializable]
public sealed class HuntChainConfig
{
	[CompilerGenerated]
	private bool field_571 = true;

	[CompilerGenerated]
	private bool field_572;

	[CompilerGenerated]
	private List<HuntChainStep> field_573 = new List<HuntChainStep>();

	public bool Enabled
	{
		[CompilerGenerated]
		get
		{
			return field_571;
		}
		[CompilerGenerated]
		set
		{
			field_571 = value;
		}
	}

	public bool CreatePfAfterTravel
	{
		[CompilerGenerated]
		get
		{
			return field_572;
		}
		[CompilerGenerated]
		set
		{
			field_572 = value;
		}
	}

	public List<HuntChainStep> Steps
	{
		[CompilerGenerated]
		get
		{
			return field_573;
		}
		[CompilerGenerated]
		set
		{
			field_573 = value;
		}
	}
}
