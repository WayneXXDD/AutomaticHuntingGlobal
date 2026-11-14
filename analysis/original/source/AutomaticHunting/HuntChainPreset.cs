using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AutomaticHunting;

[Serializable]
public sealed class HuntChainPreset
{
	[CompilerGenerated]
	private string _1680_202D = "";

	[CompilerGenerated]
	private List<HuntChainStep> _1680_202E = new List<HuntChainStep>();

	public string Name
	{
		[CompilerGenerated]
		get
		{
			return _1680_202D;
		}
		[CompilerGenerated]
		set
		{
			_1680_202D = value;
		}
	}

	public List<HuntChainStep> Steps
	{
		[CompilerGenerated]
		get
		{
			return _1680_202E;
		}
		[CompilerGenerated]
		set
		{
			_1680_202E = value;
		}
	}
}
