using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AutomaticHunting;

[Serializable]
public sealed class HuntChainConfig
{
	[CompilerGenerated]
	private bool _1680_202A = true;

	[CompilerGenerated]
	private bool _1680_202B;

	[CompilerGenerated]
	private List<HuntChainStep> _1680_202C = new List<HuntChainStep>();

	public bool Enabled
	{
		[CompilerGenerated]
		get
		{
			return _1680_202A;
		}
		[CompilerGenerated]
		set
		{
			_1680_202A = value;
		}
	}

	public bool CreatePfAfterTravel
	{
		[CompilerGenerated]
		get
		{
			return _1680_202B;
		}
		[CompilerGenerated]
		set
		{
			_1680_202B = value;
		}
	}

	public List<HuntChainStep> Steps
	{
		[CompilerGenerated]
		get
		{
			return _1680_202C;
		}
		[CompilerGenerated]
		set
		{
			_1680_202C = value;
		}
	}
}
