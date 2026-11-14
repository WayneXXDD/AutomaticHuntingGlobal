using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AutomaticHunting;

[Serializable]
public sealed class HuntChainPreset
{
	[CompilerGenerated]
	private string field_574 = "";

	[CompilerGenerated]
	private List<HuntChainStep> field_575 = new List<HuntChainStep>();

	public string Name
	{
		[CompilerGenerated]
		get
		{
			return field_574;
		}
		[CompilerGenerated]
		set
		{
			field_574 = value;
		}
	}

	public List<HuntChainStep> Steps
	{
		[CompilerGenerated]
		get
		{
			return field_575;
		}
		[CompilerGenerated]
		set
		{
			field_575 = value;
		}
	}
}
