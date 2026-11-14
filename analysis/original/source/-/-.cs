using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace _00A0;

internal static class _00A0
{
	internal static readonly (string Name, string[] Servers)[] _2000_2028 = new (string, string[])[4]
	{
		("陆行鸟", new string[8] { "红玉海", "神意之地", "拉诺西亚", "幻影群岛", "萌芽池", "宇宙和音", "沃仙曦染", "晨曦王座" }),
		("莫古力", new string[8] { "白金幻象", "白银乡", "潮风亭", "拂晓之间", "龙巢神殿", "旅人栈桥", "梦羽宝境", "神拳痕" }),
		("猫小胖", new string[7] { "紫水栈桥", "延夏", "静语庄园", "摩杜纳", "海猫茶屋", "柔风海湾", "琥珀原" }),
		("豆豆柴", new string[5] { "水晶塔", "银泪湖", "太阳海岸", "伊修加德", "红茶川" })
	};

	internal static readonly Dictionary<int, string[]> _2000_2029 = new Dictionary<int, string[]>
	{
		[3] = new string[6] { "雷克兰德", "珂露西亚岛", "安穆·艾兰", "伊尔美格", "拉凯提卡大森林", "黑风海" },
		[4] = new string[6] { "迷津", "萨维奈岛", "加雷马", "叹息海", "天外天垓", "厄尔庇斯" },
		[5] = new string[6] { "奥阔帕恰山", "克扎玛乌卡湿地", "亚克特尔树海", "夏劳尼荒野", "遗产之地", "活着的记忆" }
	};

	internal static readonly Dictionary<string, string[]> _2000_202A = new Dictionary<string, string[]>
	{
		["雷克兰德"] = new string[2] { "乔布要塞", "奥斯塔尔严命城" },
		["珂露西亚岛"] = new string[3] { "滞潮村", "工匠村", "图姆拉村" },
		["安穆·艾兰"] = new string[3] { "鼹灵集市", "上路客店", "络尾聚落" },
		["伊尔美格"] = new string[3] { "群花馆", "普拉恩尼茸洞", "云村" },
		["拉凯提卡大森林"] = new string[2] { "蛇行枝", "法诺村" },
		["黑风海"] = new string[2] { "鳍人潮池", "马克连萨斯广场" },
		["迷津"] = new string[3] { "公堂保管院", "小萨雷安", "无路总部" },
		["萨维奈岛"] = new string[3] { "新港", "代米尔遗烈乡", "波洛伽护法村" },
		["加雷马"] = new string[2] { "碎璃营地", "第三站" },
		["叹息海"] = new string[2] { "泪湾", "最佳威兔洞" },
		["天外天垓"] = new string[3] { "半途终旅", "异亚村落", "奥密克戎基地" },
		["厄尔庇斯"] = new string[3] { "醒悟天测园", "十二奇园", "创作者之家" },
		["奥阔帕恰山"] = new string[2] { "瓦丘恩佩洛", "沃拉的回响" },
		["克扎玛乌卡湿地"] = new string[4] { "哈努聚落", "朋友的灯火", "土陶郡", "水果码头" },
		["亚克特尔树海"] = new string[2] { "红豹村", "玛穆克" },
		["夏劳尼荒野"] = new string[3] { "胡萨塔伊驿镇", "谢申内青磷泉", "美花黑泽恩" },
		["遗产之地"] = new string[3] { "亚斯拉尼站", "边郊镇", "雷转质矿场" },
		["活着的记忆"] = new string[3] { "地场节点·忆", "地场节点·火", "地场节点·风" }
	};
}
[CompilerGenerated]
internal sealed class _1680
{
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 12)]
	internal struct _00A0
	{
	}

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 14)]
	internal struct _1680
	{
	}

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 32)]
	internal struct _2000
	{
	}

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 36)]
	internal struct _2001
	{
	}

	internal static readonly _1680 _2002_1680/* Not supported: data(30 00 48 00 45 00 4C 00 49 00 54 00 31 00) */;

	internal static readonly _2001 _2002_2000/* Not supported: data(0C FF 02 30 01 30 1B FF 1A FF 1F FF 01 FF 09 FF 0B 30 0D 30 0F 30 11 30 1D 20 19 20 26 20 B7 00 25 00 03 21) */;

	internal static readonly _00A0 _2002_2001/* Not supported: data(00 00 80 3F 00 00 80 3F 00 00 00 40) */;

	internal static readonly _2000 _2002_2002/* Not supported: data(2D 00 00 00 33 00 00 00 19 00 00 00 1E 00 00 00 21 00 00 00 26 00 00 00 27 00 00 00 1A 00 00 00) */;

	internal static readonly _00A0 _2002_2003/* Not supported: data(03 00 00 00 04 00 00 00 05 00 00 00) */;

	internal static ReadOnlySpan<_1680> _00A0<_00A0, _1680>(in _00A0 buffer, int P_1)
	{
		return MemoryMarshal.CreateReadOnlySpan(in Unsafe.As<_00A0, _1680>(ref Unsafe.AsRef(in buffer)), P_1);
	}

	internal static ref _1680 _1680<_00A0, _1680>(ref _00A0 P_0, int P_1)
	{
		return ref Unsafe.Add(ref Unsafe.As<_00A0, _1680>(ref P_0), P_1);
	}
}
