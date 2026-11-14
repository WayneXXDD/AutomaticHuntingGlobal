import json
import re
import sys
from pathlib import Path
from fontTools.ttLib import TTFont

root = Path(__file__).resolve().parents[2]
font = TTFont(sys.argv[1])
cmap = font.getBestCmap()
required = set()
for path in (root / "src").rglob("*.cs"):
    source = path.read_text(encoding="utf-8-sig", errors="strict")
    if "\ufffd" in source:
        raise SystemExit(f"Invalid replacement character in {path}")
    for literal in re.findall(r'"(?:\\.|[^"\\])*"', source):
        required.update(c for c in literal if ord(c) >= 0x20 and not c.isspace())
    if ".UI" in str(path):
        for phrase in ("设置", "显示", "加载", "狩猎", "导航", "开启", "关闭", "确认", "选择", "当前", "战斗", "队伍", "地图", "点击"):
            if phrase in source:
                raise SystemExit(f"Untranslated UI phrase in {path}: {phrase}")
for path in (root / "src").glob("*.json"):
    value = json.loads(path.read_text(encoding="utf-8-sig", errors="strict"))
    def visit(node):
        if isinstance(node, str):
            if any(ord(c) == 0xfffd or 0xe000 <= ord(c) <= 0xf8ff or ord(c) > 0xffff for c in node):
                raise SystemExit(f"Unsupported text in {path}")
            required.update(c for c in node if ord(c) >= 0x20 and not c.isspace())
        elif isinstance(node, dict):
            for item in node.values(): visit(item)
        elif isinstance(node, list):
            for item in node: visit(item)
    visit(value)
missing = sorted(ord(c) for c in required if ord(c) not in cmap or cmap[ord(c)] == ".notdef")
if missing:
    raise SystemExit("Font does not cover: " + ", ".join(f"U+{n:04X} {chr(n)}" for n in missing[:60]))
print(f"Traditional Chinese source, UTF-8 and font coverage passed: {len(required)} distinct glyphs.")
