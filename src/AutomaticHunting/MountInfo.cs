using System.Runtime.CompilerServices;

namespace AutomaticHunting;

public sealed class MountInfo
{
	[CompilerGenerated]
	private uint field_542;

	[CompilerGenerated]
	private string field_543 = string.Empty;

	[CompilerGenerated]
	private uint field_544;

	public uint RowId
	{
		[CompilerGenerated]
		get
		{
			return field_542;
		}
		[CompilerGenerated]
		set
		{
			field_542 = value;
		}
	}

	public string Name
	{
		[CompilerGenerated]
		get
		{
			return field_543;
		}
		[CompilerGenerated]
		set
		{
			field_543 = value;
		}
	}

	public uint IconId
	{
		[CompilerGenerated]
		get
		{
			return field_544;
		}
		[CompilerGenerated]
		set
		{
			field_544 = value;
		}
	}
}
