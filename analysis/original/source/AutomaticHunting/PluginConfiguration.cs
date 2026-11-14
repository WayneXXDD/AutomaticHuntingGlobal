using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Dalamud.Configuration;

namespace AutomaticHunting;

[Serializable]
public sealed class PluginConfiguration : IPluginConfiguration
{
	[CompilerGenerated]
	private int _1680_2035 = 2;

	[CompilerGenerated]
	private bool _1680_2033 = true;

	[CompilerGenerated]
	private string _1680_2036 = "";

	[CompilerGenerated]
	private List<LeaderEntry> _1680_203E = new List<LeaderEntry>();

	[CompilerGenerated]
	private float _1680_2047 = 300f;

	[CompilerGenerated]
	private float _1680_2048 = 1f;

	[CompilerGenerated]
	private bool _1680_2049 = true;

	[CompilerGenerated]
	private bool _1680_204A = true;

	[CompilerGenerated]
	private bool _1680_204B = true;

	[CompilerGenerated]
	private bool _1680_204C = true;

	[CompilerGenerated]
	private bool _1680_204D;

	[CompilerGenerated]
	private uint _1680_204E;

	[CompilerGenerated]
	private string _1680_204F = "/共通技能 随机飞行坐骑";

	[CompilerGenerated]
	private bool _1680_2050;

	[CompilerGenerated]
	private bool _1680_2051;

	[CompilerGenerated]
	private bool _1680_2052;

	[CompilerGenerated]
	private bool _1680_2053;

	[CompilerGenerated]
	private bool _1680_2054;

	[CompilerGenerated]
	private bool _1680_2055;

	[CompilerGenerated]
	private bool _1680_2056;

	[CompilerGenerated]
	private string _1680_2057 = "";

	[CompilerGenerated]
	private int _1680_2058 = 3;

	[CompilerGenerated]
	private uint _1680_2059;

	[CompilerGenerated]
	private uint _1680_205A;

	[CompilerGenerated]
	private WorldTravelPreset? _1680_205B;

	[CompilerGenerated]
	private List<WorldTravelPreset> _1680_205C = new List<WorldTravelPreset>();

	[CompilerGenerated]
	private bool _1680_205D;

	[CompilerGenerated]
	private HuntChainConfig _1680_205E = new HuntChainConfig();

	[CompilerGenerated]
	private List<HuntChainPreset> _1680_205F = new List<HuntChainPreset>();

	[CompilerGenerated]
	private bool _1680_2060 = true;

	[CompilerGenerated]
	private string _1680_2061 = string.Empty;

	[CompilerGenerated]
	private int _1680_2062 = 1;

	[CompilerGenerated]
	private bool _1680_2063 = true;

	[CompilerGenerated]
	private bool _1680_2064 = true;

	[CompilerGenerated]
	private bool _1680_206A = true;

	[CompilerGenerated]
	private bool _1680_206B;

	[CompilerGenerated]
	private float _1680_206C = 80f;

	[CompilerGenerated]
	private float _1680_206D = 50f;

	[CompilerGenerated]
	private float _1680_206E = 50f;

	[CompilerGenerated]
	private string _1680_206F = "/ac 调停";

	[CompilerGenerated]
	private bool _1680_3000;

	[CompilerGenerated]
	private string _2000_00A0 = "";

	[CompilerGenerated]
	private string _2000_1680 = "";

	[CompilerGenerated]
	private List<MultiLineMapConfig> _2000_2000 = new List<MultiLineMapConfig>();

	[CompilerGenerated]
	private bool _2000_2001 = true;

	[CompilerGenerated]
	private HashSet<uint> _2000_2002 = new HashSet<uint> { 148u, 175u };

	[CompilerGenerated]
	private Dictionary<uint, uint> _2000_2003 = new Dictionary<uint, uint>
	{
		[148u] = 147u,
		[175u] = 174u
	};

	[CompilerGenerated]
	private uint _2000_2004 = 173u;

	[CompilerGenerated]
	private float _2000_2005 = 10f;

	[CompilerGenerated]
	private Dictionary<string, string[]> _2000_2006 = new Dictionary<string, string[]>
	{
		["迷津"] = new string[2] { "胡睹", "斯图希" },
		["萨维奈岛"] = new string[2] { "须羯里婆", "尤兰" },
		["加雷马"] = new string[2] { "黑杨树精", "密涅瓦" },
		["叹息海"] = new string[2] { "慕斯公主", "月面仙人刺女王" },
		["厄尔庇斯"] = new string[2] { "瓣齿鲨", "固兰盖奇" },
		["天外天垓"] = new string[2] { "伊塔总领", "凡·艾尔" },
		["雷克兰德"] = new string[2] { "纳克拉维", "纳里蓬" },
		["珂露西亚岛"] = new string[2] { "小小杀手", "乌拉坎" },
		["安穆·艾兰"] = new string[2] { "马利克巨人掌", "休格尔" },
		["伊尔美格"] = new string[2] { "保尔迪雅", "泥人" },
		["拉凯提卡大森林"] = new string[2] { "格拉斯曼", "苏帕伊" },
		["黑风海"] = new string[2] { "巴力", "卢莎卡" },
		["奥阔帕恰山"] = new string[2] { "内丘奇霍", "女王鹰蜂" },
		["克扎玛乌卡湿地"] = new string[2] { "惊雨蟾蜍", "普库恰" },
		["亚克特尔树海"] = new string[2] { "幻煌鸟", "血鸣鼠" },
		["夏劳尼荒野"] = new string[2] { "艾海海陶瓦泡", "凯海尼海亚麦尤伊" },
		["遗产之地"] = new string[2] { "海休瓦拉", "多变装置" },
		["活着的记忆"] = new string[2] { "清扫者萨莉", "猫眼" }
	};

	[CompilerGenerated]
	private Dictionary<string, string[]> _2000_2007 = new Dictionary<string, string[]>
	{
		["雷克兰德"] = new string[2] { "戾虫", "得到宽恕的叛乱" },
		["珂露西亚岛"] = new string[2] { "得到宽恕的炫学", "得到宽恕的叛乱" },
		["安穆·艾兰"] = new string[2] { "多智兽", "得到宽恕的叛乱" },
		["伊尔美格"] = new string[2] { "阿格拉俄珀", "得到宽恕的叛乱" },
		["拉凯提卡大森林"] = new string[2] { "伊休妲", "得到宽恕的叛乱" },
		["黑风海"] = new string[2] { "顾尼图", "得到宽恕的叛乱" },
		["迷津"] = new string[2] { "布弗鲁", "克尔" },
		["萨维奈岛"] = new string[2] { "颇胝迦", "克尔" },
		["加雷马"] = new string[2] { "阿姆斯特朗", "克尔" },
		["叹息海"] = new string[2] { "沉思之物", "克尔" },
		["厄尔庇斯"] = new string[2] { "俄菲翁尼厄斯", "克尔" },
		["天外天垓"] = new string[2] { "狭缝", "克尔" },
		["奥阔帕恰山"] = new string[3] { "厌忌之人", "奇里格", "水晶化身之王" },
		["克扎玛乌卡湿地"] = new string[2] { "伊努索奇", "水晶化身之王" },
		["亚克特尔树海"] = new string[2] { "内尤佐缇", "水晶化身之王" },
		["夏劳尼荒野"] = new string[2] { "山谢亚", "水晶化身之王" },
		["遗产之地"] = new string[2] { "先驱勇士 阿提卡斯", "水晶化身之王" },
		["活着的记忆"] = new string[2] { "天气预报机器人", "水晶化身之王" }
	};

	[CompilerGenerated]
	private bool _2000_2008 = true;

	[CompilerGenerated]
	private float _2000_2009 = -1f;

	[CompilerGenerated]
	private float _2000_200A = -1f;

	[CompilerGenerated]
	private float _2000_200B = 540f;

	[CompilerGenerated]
	private float _2000_2010 = 660f;

	[CompilerGenerated]
	private int _2000_2011;

	[CompilerGenerated]
	private bool _2000_2012;

	[CompilerGenerated]
	private long _2000_2013;

	[CompilerGenerated]
	private float _2000_2014 = 0.88f;

	[CompilerGenerated]
	private bool _2000_2015;

	[CompilerGenerated]
	private bool _2000_2022;

	[CompilerGenerated]
	private bool _2000_2024;

	[CompilerGenerated]
	private int _2000_2025;

	[CompilerGenerated]
	private bool _2000_2027;

	public int Version
	{
		[CompilerGenerated]
		get
		{
			return _1680_2035;
		}
		[CompilerGenerated]
		set
		{
			_1680_2035 = value;
		}
	}

	public bool Enabled
	{
		[CompilerGenerated]
		get
		{
			return _1680_2033;
		}
		[CompilerGenerated]
		set
		{
			_1680_2033 = value;
		}
	}

	public string LeaderName
	{
		[CompilerGenerated]
		get
		{
			return _1680_2036;
		}
		[CompilerGenerated]
		set
		{
			_1680_2036 = value;
		}
	}

	public List<LeaderEntry> LeaderEntries
	{
		[CompilerGenerated]
		get
		{
			return _1680_203E;
		}
		[CompilerGenerated]
		set
		{
			_1680_203E = value;
		}
	}

	public float TeleportThreshold
	{
		[CompilerGenerated]
		get
		{
			return _1680_2047;
		}
		[CompilerGenerated]
		set
		{
			_1680_2047 = value;
		}
	}

	public float WaitAfterTeleport
	{
		[CompilerGenerated]
		get
		{
			return _1680_2048;
		}
		[CompilerGenerated]
		set
		{
			_1680_2048 = value;
		}
	}

	public bool UseFlightNavigation
	{
		[CompilerGenerated]
		get
		{
			return _1680_2049;
		}
		[CompilerGenerated]
		set
		{
			_1680_2049 = value;
		}
	}

	public bool EchoMessages
	{
		[CompilerGenerated]
		get
		{
			return _1680_204A;
		}
		[CompilerGenerated]
		set
		{
			_1680_204A = value;
		}
	}

	public bool SetFlagOnNavigation
	{
		[CompilerGenerated]
		get
		{
			return _1680_204B;
		}
		[CompilerGenerated]
		set
		{
			_1680_204B = value;
		}
	}

	public bool ShowOverlay
	{
		[CompilerGenerated]
		get
		{
			return _1680_204C;
		}
		[CompilerGenerated]
		set
		{
			_1680_204C = value;
		}
	}

	public bool ShowDistances
	{
		[CompilerGenerated]
		get
		{
			return _1680_204D;
		}
		[CompilerGenerated]
		set
		{
			_1680_204D = value;
		}
	}

	public uint SelectedMountId
	{
		[CompilerGenerated]
		get
		{
			return _1680_204E;
		}
		[CompilerGenerated]
		set
		{
			_1680_204E = value;
		}
	}

	public string MountCommand
	{
		[CompilerGenerated]
		get
		{
			return _1680_204F;
		}
		[CompilerGenerated]
		set
		{
			_1680_204F = value;
		}
	}

	public bool ShowDebugSection
	{
		[CompilerGenerated]
		get
		{
			return _1680_2050;
		}
		[CompilerGenerated]
		set
		{
			_1680_2050 = value;
		}
	}

	public bool ShowPrepConfigSection
	{
		[CompilerGenerated]
		get
		{
			return _1680_2051;
		}
		[CompilerGenerated]
		set
		{
			_1680_2051 = value;
		}
	}

	public bool ShowSConfigSection
	{
		[CompilerGenerated]
		get
		{
			return _1680_2052;
		}
		[CompilerGenerated]
		set
		{
			_1680_2052 = value;
		}
	}

	public bool ShowMultiLineSection
	{
		[CompilerGenerated]
		get
		{
			return _1680_2053;
		}
		[CompilerGenerated]
		set
		{
			_1680_2053 = value;
		}
	}

	public bool ShowWorldTravelSection
	{
		[CompilerGenerated]
		get
		{
			return _1680_2054;
		}
		[CompilerGenerated]
		set
		{
			_1680_2054 = value;
		}
	}

	public bool ShowSimplified
	{
		[CompilerGenerated]
		get
		{
			return _1680_2055;
		}
		[CompilerGenerated]
		set
		{
			_1680_2055 = value;
		}
	}

	public bool DebugCountAInAllMaps
	{
		[CompilerGenerated]
		get
		{
			return _1680_2056;
		}
		[CompilerGenerated]
		set
		{
			_1680_2056 = value;
		}
	}

	public string ManualServerName
	{
		[CompilerGenerated]
		get
		{
			return _1680_2057;
		}
		[CompilerGenerated]
		set
		{
			_1680_2057 = value;
		}
	}

	public int ManualExVersion
	{
		[CompilerGenerated]
		get
		{
			return _1680_2058;
		}
		[CompilerGenerated]
		set
		{
			_1680_2058 = value;
		}
	}

	public uint ManualMapRowId
	{
		[CompilerGenerated]
		get
		{
			return _1680_2059;
		}
		[CompilerGenerated]
		set
		{
			_1680_2059 = value;
		}
	}

	public uint ManualAetheryteId
	{
		[CompilerGenerated]
		get
		{
			return _1680_205A;
		}
		[CompilerGenerated]
		set
		{
			_1680_205A = value;
		}
	}

	public WorldTravelPreset? SavedWorldTravel
	{
		[CompilerGenerated]
		get
		{
			return _1680_205B;
		}
		[CompilerGenerated]
		set
		{
			_1680_205B = value;
		}
	}

	public List<WorldTravelPreset> SavedWorldTravels
	{
		[CompilerGenerated]
		get
		{
			return _1680_205C;
		}
		[CompilerGenerated]
		set
		{
			_1680_205C = value;
		}
	}

	public bool ShowHuntChainSection
	{
		[CompilerGenerated]
		get
		{
			return _1680_205D;
		}
		[CompilerGenerated]
		set
		{
			_1680_205D = value;
		}
	}

	public HuntChainConfig HuntChain
	{
		[CompilerGenerated]
		get
		{
			return _1680_205E;
		}
		[CompilerGenerated]
		set
		{
			_1680_205E = value;
		}
	}

	public List<HuntChainPreset> SavedHuntChains
	{
		[CompilerGenerated]
		get
		{
			return _1680_205F;
		}
		[CompilerGenerated]
		set
		{
			_1680_205F = value;
		}
	}

	public bool EnableContextMenu
	{
		[CompilerGenerated]
		get
		{
			return _1680_2060;
		}
		[CompilerGenerated]
		set
		{
			_1680_2060 = value;
		}
	}

	public string CurrentInstanceFingerprint
	{
		[CompilerGenerated]
		get
		{
			return _1680_2061;
		}
		[CompilerGenerated]
		set
		{
			_1680_2061 = value;
		}
	}

	public int CurrentInstanceNumber
	{
		[CompilerGenerated]
		get
		{
			return _1680_2062;
		}
		[CompilerGenerated]
		set
		{
			_1680_2062 = value;
		}
	}

	public bool SwitchRetryAtAetheryte
	{
		[CompilerGenerated]
		get
		{
			return _1680_2063;
		}
		[CompilerGenerated]
		set
		{
			_1680_2063 = value;
		}
	}

	public bool LandingMode
	{
		[CompilerGenerated]
		get
		{
			return _1680_2064;
		}
		[CompilerGenerated]
		set
		{
			_1680_2064 = value;
		}
	}

	public bool AutoBmrAi
	{
		[CompilerGenerated]
		get
		{
			return _1680_206A;
		}
		[CompilerGenerated]
		set
		{
			_1680_206A = value;
		}
	}

	public bool SEnabled
	{
		[CompilerGenerated]
		get
		{
			return _1680_206B;
		}
		[CompilerGenerated]
		set
		{
			_1680_206B = value;
		}
	}

	public float SHpThreshold
	{
		[CompilerGenerated]
		get
		{
			return _1680_206C;
		}
		[CompilerGenerated]
		set
		{
			_1680_206C = value;
		}
	}

	public float SHoverDistance
	{
		[CompilerGenerated]
		get
		{
			return _1680_206D;
		}
		[CompilerGenerated]
		set
		{
			_1680_206D = value;
		}
	}

	public float SHoverHeight
	{
		[CompilerGenerated]
		get
		{
			return _1680_206E;
		}
		[CompilerGenerated]
		set
		{
			_1680_206E = value;
		}
	}

	public string SDashCommand
	{
		[CompilerGenerated]
		get
		{
			return _1680_206F;
		}
		[CompilerGenerated]
		set
		{
			_1680_206F = value;
		}
	}

	public bool SPathToEngage
	{
		[CompilerGenerated]
		get
		{
			return _1680_3000;
		}
		[CompilerGenerated]
		set
		{
			_1680_3000 = value;
		}
	}

	public string CrossWorldLeader
	{
		[CompilerGenerated]
		get
		{
			return _2000_00A0;
		}
		[CompilerGenerated]
		set
		{
			_2000_00A0 = value;
		}
	}

	public string PfComment
	{
		[CompilerGenerated]
		get
		{
			return _2000_1680;
		}
		[CompilerGenerated]
		set
		{
			_2000_1680 = value;
		}
	}

	public List<MultiLineMapConfig> MultiLineMaps
	{
		[CompilerGenerated]
		get
		{
			return _2000_2000;
		}
		[CompilerGenerated]
		set
		{
			_2000_2000 = value;
		}
	}

	public bool LeaderYellOnly
	{
		[CompilerGenerated]
		get
		{
			return _2000_2001;
		}
		[CompilerGenerated]
		set
		{
			_2000_2001 = value;
		}
	}

	public HashSet<uint> BlacklistedAetheryteIds
	{
		[CompilerGenerated]
		get
		{
			return _2000_2002;
		}
		[CompilerGenerated]
		set
		{
			_2000_2002 = value;
		}
	}

	public Dictionary<uint, uint> BlacklistReplacements
	{
		[CompilerGenerated]
		get
		{
			return _2000_2003;
		}
		[CompilerGenerated]
		set
		{
			_2000_2003 = value;
		}
	}

	public uint SpecialSmallAetheryteId
	{
		[CompilerGenerated]
		get
		{
			return _2000_2004;
		}
		[CompilerGenerated]
		set
		{
			_2000_2004 = value;
		}
	}

	public float SpecialNearbyRadius
	{
		[CompilerGenerated]
		get
		{
			return _2000_2005;
		}
		[CompilerGenerated]
		set
		{
			_2000_2005 = value;
		}
	}

	public Dictionary<string, string[]> ARealmMarks
	{
		[CompilerGenerated]
		get
		{
			return _2000_2006;
		}
		[CompilerGenerated]
		set
		{
			_2000_2006 = value;
		}
	}

	public Dictionary<string, string[]> SRealmMarks
	{
		[CompilerGenerated]
		get
		{
			return _2000_2007;
		}
		[CompilerGenerated]
		set
		{
			_2000_2007 = value;
		}
	}

	public bool HudVisible
	{
		[CompilerGenerated]
		get
		{
			return _2000_2008;
		}
		[CompilerGenerated]
		set
		{
			_2000_2008 = value;
		}
	}

	public float HudPosX
	{
		[CompilerGenerated]
		get
		{
			return _2000_2009;
		}
		[CompilerGenerated]
		set
		{
			_2000_2009 = value;
		}
	}

	public float HudPosY
	{
		[CompilerGenerated]
		get
		{
			return _2000_200A;
		}
		[CompilerGenerated]
		set
		{
			_2000_200A = value;
		}
	}

	public float SettingsWidth
	{
		[CompilerGenerated]
		get
		{
			return _2000_200B;
		}
		[CompilerGenerated]
		set
		{
			_2000_200B = value;
		}
	}

	public float SettingsHeight
	{
		[CompilerGenerated]
		get
		{
			return _2000_2010;
		}
		[CompilerGenerated]
		set
		{
			_2000_2010 = value;
		}
	}

	public int SettingsTab
	{
		[CompilerGenerated]
		get
		{
			return _2000_2011;
		}
		[CompilerGenerated]
		set
		{
			_2000_2011 = value;
		}
	}

	public bool DisclaimerAccepted
	{
		[CompilerGenerated]
		get
		{
			return _2000_2012;
		}
		[CompilerGenerated]
		set
		{
			_2000_2012 = value;
		}
	}

	public long DisclaimerAcceptedUtc
	{
		[CompilerGenerated]
		get
		{
			return _2000_2013;
		}
		[CompilerGenerated]
		set
		{
			_2000_2013 = value;
		}
	}

	public bool AutomationEnabled
	{
		get
		{
			if (Enabled)
			{
				return DisclaimerAccepted;
			}
			return false;
		}
	}

	public float HudAlpha
	{
		[CompilerGenerated]
		get
		{
			return _2000_2014;
		}
		[CompilerGenerated]
		set
		{
			_2000_2014 = value;
		}
	}

	public bool HudCompact
	{
		[CompilerGenerated]
		get
		{
			return _2000_2015;
		}
		[CompilerGenerated]
		set
		{
			_2000_2015 = value;
		}
	}

	public bool HudLocked
	{
		[CompilerGenerated]
		get
		{
			return _2000_2022;
		}
		[CompilerGenerated]
		set
		{
			_2000_2022 = value;
		}
	}

	public bool OnboardingCompleted
	{
		[CompilerGenerated]
		get
		{
			return _2000_2024;
		}
		[CompilerGenerated]
		set
		{
			_2000_2024 = value;
		}
	}

	public int OnboardingStep
	{
		[CompilerGenerated]
		get
		{
			return _2000_2025;
		}
		[CompilerGenerated]
		set
		{
			_2000_2025 = value;
		}
	}

	public bool DataClearAcknowledged
	{
		[CompilerGenerated]
		get
		{
			return _2000_2027;
		}
		[CompilerGenerated]
		set
		{
			_2000_2027 = value;
		}
	}
}
