using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Dalamud.Configuration;

namespace AutomaticHunting;

[Serializable]
public sealed class PluginConfiguration : IPluginConfiguration
{
	[CompilerGenerated]
	private int field_578 = 3;

	[CompilerGenerated]
	private bool field_579 = true;

	[CompilerGenerated]
	private string field_580 = "";

	[CompilerGenerated]
	private List<LeaderEntry> field_581 = new List<LeaderEntry>();

	[CompilerGenerated]
	private float field_582 = 300f;

	[CompilerGenerated]
	private float field_583 = 1f;

	[CompilerGenerated]
	private bool field_584 = true;

	[CompilerGenerated]
	private bool field_585 = true;

	[CompilerGenerated]
	private bool field_586 = true;

	[CompilerGenerated]
	private bool field_587 = true;

	[CompilerGenerated]
	private bool field_588;

	[CompilerGenerated]
	private uint field_589;

	[CompilerGenerated]
	private string field_590 = GlobalGameData.DefaultMountCommand;

	[CompilerGenerated]
	private bool field_591;

	[CompilerGenerated]
	private bool field_592;

	[CompilerGenerated]
	private bool field_593;

	[CompilerGenerated]
	private bool field_594;

	[CompilerGenerated]
	private bool field_595;

	[CompilerGenerated]
	private bool field_596;

	[CompilerGenerated]
	private bool field_597;

	[CompilerGenerated]
	private string field_598 = "";

	[CompilerGenerated]
	private int field_599 = 3;

	[CompilerGenerated]
	private uint field_600;

	[CompilerGenerated]
	private uint field_601;

	[CompilerGenerated]
	private WorldTravelPreset? field_602;

	[CompilerGenerated]
	private List<WorldTravelPreset> field_603 = new List<WorldTravelPreset>();

	[CompilerGenerated]
	private bool field_604;

	[CompilerGenerated]
	private HuntChainConfig field_605 = new HuntChainConfig();

	[CompilerGenerated]
	private List<HuntChainPreset> field_606 = new List<HuntChainPreset>();

	[CompilerGenerated]
	private bool field_607 = true;

	[CompilerGenerated]
	private string field_608 = string.Empty;

	[CompilerGenerated]
	private int field_609 = 1;

	[CompilerGenerated]
	private bool field_610 = true;

	[CompilerGenerated]
	private bool field_611 = true;

	[CompilerGenerated]
	private bool field_612 = true;

	[CompilerGenerated]
	private bool field_613;

	[CompilerGenerated]
	private float field_614 = 80f;

	[CompilerGenerated]
	private float field_615 = 50f;

	[CompilerGenerated]
	private float field_616 = 50f;

	[CompilerGenerated]
	private string field_617 = GlobalGameData.DefaultDashCommand;

	[CompilerGenerated]
	private bool field_618;

	[CompilerGenerated]
	private string field_619 = "";

	[CompilerGenerated]
	private string field_620 = "";

	[CompilerGenerated]
	private List<MultiLineMapConfig> field_621 = new List<MultiLineMapConfig>();

	[CompilerGenerated]
	private bool field_622 = true;

	[CompilerGenerated]
	private HashSet<uint> field_623 = new HashSet<uint> { 148u, 175u };

	[CompilerGenerated]
	private Dictionary<uint, uint> field_624 = new Dictionary<uint, uint>
	{
		[148u] = 147u,
		[175u] = 174u
	};

	[CompilerGenerated]
	private uint field_625 = 173u;

	[CompilerGenerated]
	private float field_626 = 10f;

	[CompilerGenerated]
	private Dictionary<string, string[]> field_627 = GlobalGameData.BuildMarks(2);

	[CompilerGenerated]
	private Dictionary<string, string[]> field_628 = GlobalGameData.BuildMarks(3);

	[CompilerGenerated]
	private bool field_629 = true;

	[CompilerGenerated]
	private float field_630 = -1f;

	[CompilerGenerated]
	private float field_631 = -1f;

	[CompilerGenerated]
	private float field_632 = 540f;

	[CompilerGenerated]
	private float field_633 = 660f;

	[CompilerGenerated]
	private int field_634;

	[CompilerGenerated]
	private bool field_635;

	[CompilerGenerated]
	private long field_636;

	[CompilerGenerated]
	private float field_637 = 0.88f;

	[CompilerGenerated]
	private bool field_638;

	[CompilerGenerated]
	private bool field_639;

	[CompilerGenerated]
	private bool field_640;

	[CompilerGenerated]
	private int field_641;

	[CompilerGenerated]
	private bool field_642;

	public int Version
	{
		[CompilerGenerated]
		get
		{
			return field_578;
		}
		[CompilerGenerated]
		set
		{
			field_578 = value;
		}
	}

	public bool Enabled
	{
		[CompilerGenerated]
		get
		{
			return field_579;
		}
		[CompilerGenerated]
		set
		{
			field_579 = value;
		}
	}

	public string LeaderName
	{
		[CompilerGenerated]
		get
		{
			return field_580;
		}
		[CompilerGenerated]
		set
		{
			field_580 = value;
		}
	}

	public List<LeaderEntry> LeaderEntries
	{
		[CompilerGenerated]
		get
		{
			return field_581;
		}
		[CompilerGenerated]
		set
		{
			field_581 = value;
		}
	}

	public bool AutoTeleportToFlag { get; set; } = true;

	public float AMarkSearchRadius { get; set; } = AMarkSearchPolicy.DefaultRadius;

	public float TeleportThreshold
	{
		[CompilerGenerated]
		get
		{
			return field_582;
		}
		[CompilerGenerated]
		set
		{
			field_582 = value;
		}
	}

	public float WaitAfterTeleport
	{
		[CompilerGenerated]
		get
		{
			return field_583;
		}
		[CompilerGenerated]
		set
		{
			field_583 = value;
		}
	}

	public bool UseFlightNavigation
	{
		[CompilerGenerated]
		get
		{
			return field_584;
		}
		[CompilerGenerated]
		set
		{
			field_584 = value;
		}
	}

	public bool EchoMessages
	{
		[CompilerGenerated]
		get
		{
			return field_585;
		}
		[CompilerGenerated]
		set
		{
			field_585 = value;
		}
	}

	public bool SetFlagOnNavigation
	{
		[CompilerGenerated]
		get
		{
			return field_586;
		}
		[CompilerGenerated]
		set
		{
			field_586 = value;
		}
	}

	public bool ShowOverlay
	{
		[CompilerGenerated]
		get
		{
			return field_587;
		}
		[CompilerGenerated]
		set
		{
			field_587 = value;
		}
	}

	public bool ShowDistances
	{
		[CompilerGenerated]
		get
		{
			return field_588;
		}
		[CompilerGenerated]
		set
		{
			field_588 = value;
		}
	}

	public uint SelectedMountId
	{
		[CompilerGenerated]
		get
		{
			return field_589;
		}
		[CompilerGenerated]
		set
		{
			field_589 = value;
		}
	}

	public string MountCommand
	{
		[CompilerGenerated]
		get
		{
			return field_590;
		}
		[CompilerGenerated]
		set
		{
			field_590 = value;
		}
	}

	public bool ShowDebugSection
	{
		[CompilerGenerated]
		get
		{
			return field_591;
		}
		[CompilerGenerated]
		set
		{
			field_591 = value;
		}
	}

	public bool ShowPrepConfigSection
	{
		[CompilerGenerated]
		get
		{
			return field_592;
		}
		[CompilerGenerated]
		set
		{
			field_592 = value;
		}
	}

	public bool ShowSConfigSection
	{
		[CompilerGenerated]
		get
		{
			return field_593;
		}
		[CompilerGenerated]
		set
		{
			field_593 = value;
		}
	}

	public bool ShowMultiLineSection
	{
		[CompilerGenerated]
		get
		{
			return field_594;
		}
		[CompilerGenerated]
		set
		{
			field_594 = value;
		}
	}

	public bool ShowWorldTravelSection
	{
		[CompilerGenerated]
		get
		{
			return field_595;
		}
		[CompilerGenerated]
		set
		{
			field_595 = value;
		}
	}

	public bool ShowSimplified
	{
		[CompilerGenerated]
		get
		{
			return field_596;
		}
		[CompilerGenerated]
		set
		{
			field_596 = value;
		}
	}

	public bool DebugCountAInAllMaps
	{
		[CompilerGenerated]
		get
		{
			return field_597;
		}
		[CompilerGenerated]
		set
		{
			field_597 = value;
		}
	}

	public string ManualServerName
	{
		[CompilerGenerated]
		get
		{
			return field_598;
		}
		[CompilerGenerated]
		set
		{
			field_598 = value;
		}
	}

	public int ManualExVersion
	{
		[CompilerGenerated]
		get
		{
			return field_599;
		}
		[CompilerGenerated]
		set
		{
			field_599 = value;
		}
	}

	public uint ManualMapRowId
	{
		[CompilerGenerated]
		get
		{
			return field_600;
		}
		[CompilerGenerated]
		set
		{
			field_600 = value;
		}
	}

	public uint ManualAetheryteId
	{
		[CompilerGenerated]
		get
		{
			return field_601;
		}
		[CompilerGenerated]
		set
		{
			field_601 = value;
		}
	}

	public WorldTravelPreset? SavedWorldTravel
	{
		[CompilerGenerated]
		get
		{
			return field_602;
		}
		[CompilerGenerated]
		set
		{
			field_602 = value;
		}
	}

	public List<WorldTravelPreset> SavedWorldTravels
	{
		[CompilerGenerated]
		get
		{
			return field_603;
		}
		[CompilerGenerated]
		set
		{
			field_603 = value;
		}
	}

	public bool ShowHuntChainSection
	{
		[CompilerGenerated]
		get
		{
			return field_604;
		}
		[CompilerGenerated]
		set
		{
			field_604 = value;
		}
	}

	public HuntChainConfig HuntChain
	{
		[CompilerGenerated]
		get
		{
			return field_605;
		}
		[CompilerGenerated]
		set
		{
			field_605 = value;
		}
	}

	public List<HuntChainPreset> SavedHuntChains
	{
		[CompilerGenerated]
		get
		{
			return field_606;
		}
		[CompilerGenerated]
		set
		{
			field_606 = value;
		}
	}

	public bool EnableContextMenu
	{
		[CompilerGenerated]
		get
		{
			return field_607;
		}
		[CompilerGenerated]
		set
		{
			field_607 = value;
		}
	}

	public string CurrentInstanceFingerprint
	{
		[CompilerGenerated]
		get
		{
			return field_608;
		}
		[CompilerGenerated]
		set
		{
			field_608 = value;
		}
	}

	public int CurrentInstanceNumber
	{
		[CompilerGenerated]
		get
		{
			return field_609;
		}
		[CompilerGenerated]
		set
		{
			field_609 = value;
		}
	}

	public bool SwitchRetryAtAetheryte
	{
		[CompilerGenerated]
		get
		{
			return field_610;
		}
		[CompilerGenerated]
		set
		{
			field_610 = value;
		}
	}

	public bool LandingMode
	{
		[CompilerGenerated]
		get
		{
			return field_611;
		}
		[CompilerGenerated]
		set
		{
			field_611 = value;
		}
	}

	public bool AutoBmrAi
	{
		[CompilerGenerated]
		get
		{
			return field_612;
		}
		[CompilerGenerated]
		set
		{
			field_612 = value;
		}
	}

	public bool SEnabled
	{
		[CompilerGenerated]
		get
		{
			return field_613;
		}
		[CompilerGenerated]
		set
		{
			field_613 = value;
		}
	}

	public float SHpThreshold
	{
		[CompilerGenerated]
		get
		{
			return field_614;
		}
		[CompilerGenerated]
		set
		{
			field_614 = value;
		}
	}

	public float SHoverDistance
	{
		[CompilerGenerated]
		get
		{
			return field_615;
		}
		[CompilerGenerated]
		set
		{
			field_615 = value;
		}
	}

	public float SHoverHeight
	{
		[CompilerGenerated]
		get
		{
			return field_616;
		}
		[CompilerGenerated]
		set
		{
			field_616 = value;
		}
	}

	public string SDashCommand
	{
		[CompilerGenerated]
		get
		{
			return field_617;
		}
		[CompilerGenerated]
		set
		{
			field_617 = value;
		}
	}

	public bool SPathToEngage
	{
		[CompilerGenerated]
		get
		{
			return field_618;
		}
		[CompilerGenerated]
		set
		{
			field_618 = value;
		}
	}

	public string CrossWorldLeader
	{
		[CompilerGenerated]
		get
		{
			return field_619;
		}
		[CompilerGenerated]
		set
		{
			field_619 = value;
		}
	}

	public string PfComment
	{
		[CompilerGenerated]
		get
		{
			return field_620;
		}
		[CompilerGenerated]
		set
		{
			field_620 = value;
		}
	}

	public List<MultiLineMapConfig> MultiLineMaps
	{
		[CompilerGenerated]
		get
		{
			return field_621;
		}
		[CompilerGenerated]
		set
		{
			field_621 = value;
		}
	}

	public bool LeaderYellOnly
	{
		[CompilerGenerated]
		get
		{
			return field_622;
		}
		[CompilerGenerated]
		set
		{
			field_622 = value;
		}
	}

	public HashSet<uint> BlacklistedAetheryteIds
	{
		[CompilerGenerated]
		get
		{
			return field_623;
		}
		[CompilerGenerated]
		set
		{
			field_623 = value;
		}
	}

	public Dictionary<uint, uint> BlacklistReplacements
	{
		[CompilerGenerated]
		get
		{
			return field_624;
		}
		[CompilerGenerated]
		set
		{
			field_624 = value;
		}
	}

	public uint SpecialSmallAetheryteId
	{
		[CompilerGenerated]
		get
		{
			return field_625;
		}
		[CompilerGenerated]
		set
		{
			field_625 = value;
		}
	}

	public float SpecialNearbyRadius
	{
		[CompilerGenerated]
		get
		{
			return field_626;
		}
		[CompilerGenerated]
		set
		{
			field_626 = value;
		}
	}

	public Dictionary<string, string[]> ARealmMarks
	{
		[CompilerGenerated]
		get
		{
			return field_627;
		}
		[CompilerGenerated]
		set
		{
			field_627 = value;
		}
	}

	public Dictionary<string, string[]> SRealmMarks
	{
		[CompilerGenerated]
		get
		{
			return field_628;
		}
		[CompilerGenerated]
		set
		{
			field_628 = value;
		}
	}

	public bool HudVisible
	{
		[CompilerGenerated]
		get
		{
			return field_629;
		}
		[CompilerGenerated]
		set
		{
			field_629 = value;
		}
	}

	public float HudPosX
	{
		[CompilerGenerated]
		get
		{
			return field_630;
		}
		[CompilerGenerated]
		set
		{
			field_630 = value;
		}
	}

	public float HudPosY
	{
		[CompilerGenerated]
		get
		{
			return field_631;
		}
		[CompilerGenerated]
		set
		{
			field_631 = value;
		}
	}

	public float SettingsWidth
	{
		[CompilerGenerated]
		get
		{
			return field_632;
		}
		[CompilerGenerated]
		set
		{
			field_632 = value;
		}
	}

	public float SettingsHeight
	{
		[CompilerGenerated]
		get
		{
			return field_633;
		}
		[CompilerGenerated]
		set
		{
			field_633 = value;
		}
	}

	public int SettingsTab
	{
		[CompilerGenerated]
		get
		{
			return field_634;
		}
		[CompilerGenerated]
		set
		{
			field_634 = value;
		}
	}

	public bool DisclaimerAccepted
	{
		[CompilerGenerated]
		get
		{
			return field_635;
		}
		[CompilerGenerated]
		set
		{
			field_635 = value;
		}
	}

	public long DisclaimerAcceptedUtc
	{
		[CompilerGenerated]
		get
		{
			return field_636;
		}
		[CompilerGenerated]
		set
		{
			field_636 = value;
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
			return field_637;
		}
		[CompilerGenerated]
		set
		{
			field_637 = value;
		}
	}

	public bool HudCompact
	{
		[CompilerGenerated]
		get
		{
			return field_638;
		}
		[CompilerGenerated]
		set
		{
			field_638 = value;
		}
	}

	public bool HudLocked
	{
		[CompilerGenerated]
		get
		{
			return field_639;
		}
		[CompilerGenerated]
		set
		{
			field_639 = value;
		}
	}

	public bool OnboardingCompleted
	{
		[CompilerGenerated]
		get
		{
			return field_640;
		}
		[CompilerGenerated]
		set
		{
			field_640 = value;
		}
	}

	public int OnboardingStep
	{
		[CompilerGenerated]
		get
		{
			return field_641;
		}
		[CompilerGenerated]
		set
		{
			field_641 = value;
		}
	}

	public bool DataClearAcknowledged
	{
		[CompilerGenerated]
		get
		{
			return field_642;
		}
		[CompilerGenerated]
		set
		{
			field_642 = value;
		}
	}
}
