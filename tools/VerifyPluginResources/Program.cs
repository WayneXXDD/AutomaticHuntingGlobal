using System.IO.Compression;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text.Json;

const string namesResource = "AutomaticHunting.TraditionalNames.zh-TW.json";
const string helpResource = "AutomaticHunting.Help.help.json";
if (args.Length is < 2 or > 3) throw new ArgumentException("Expected a DLL or ZIP path, source directory, and optional --expect-missing-name-resource.");
using var payload = ReadPlugin(args[0]);
using var pe = new PEReader(payload);
var metadata = pe.GetMetadataReader();
var resources = metadata.ManifestResources.ToDictionary(h => metadata.GetString(metadata.GetManifestResource(h).Name), h => metadata.GetManifestResource(h), StringComparer.Ordinal);
if (args.Length == 3 && args[2] == "--expect-missing-name-resource") {
    if (resources.TryGetValue(namesResource, out var previous) && previous.Implementation.IsNil)
        throw new InvalidOperationException("The regression package unexpectedly contains the main-assembly name resource.");
    Console.WriteLine("Regression confirmed: the previous release is missing the main-assembly Traditional Chinese name resource.");
    return;
}
foreach (var (logicalName, fileName) in new[] {
    (namesResource, "TraditionalNames.zh-TW.json"), (helpResource, "AutomaticHunting.Help.help.json")
}) {
    if (!resources.TryGetValue(logicalName, out var resource) || !resource.Implementation.IsNil)
        throw new InvalidOperationException("The packaged main assembly is missing embedded resource: " + logicalName);
    var directory = pe.PEHeaders.CorHeader?.ResourcesDirectory ?? throw new InvalidOperationException("The DLL has no CLR resource directory.");
    var offset = checked((int)resource.Offset);
    if (offset < 0 || directory.Size < 4 || offset > directory.Size - 4) throw new InvalidDataException("Invalid resource offset.");
    var block = pe.GetSectionData(directory.RelativeVirtualAddress);
    var length = block.GetReader(offset, 4).ReadInt32();
    if (length <= 0 || length > directory.Size - offset - 4) throw new InvalidDataException("Invalid resource size.");
    var bytes = block.GetReader(offset + 4, length).ReadBytes(length);
    var source = File.ReadAllBytes(Path.Combine(args[1], fileName));
    if (!bytes.AsSpan().SequenceEqual(source)) throw new InvalidDataException("Packaged resource differs from its source: " + logicalName);
    using var json = JsonDocument.Parse(bytes);
    if (logicalName == namesResource) {
        foreach (var sheet in new[] { "PlaceName", "BNpcName", "Mount" }) {
            var entries = json.RootElement.GetProperty(sheet);
            if (entries.ValueKind != JsonValueKind.Object || !entries.EnumerateObject().Any())
                throw new InvalidDataException("Empty Traditional Chinese name table: " + sheet);
            Console.WriteLine($"Verified packaged {sheet} translations: {entries.EnumerateObject().Count()} entries.");
        }
    } else if (json.RootElement.GetProperty("groups").GetArrayLength() == 0) {
        throw new InvalidDataException("The packaged help document is empty.");
    }
    Console.WriteLine("Verified main-assembly resource: " + logicalName);
}
Console.WriteLine("Packaged DLL resource verification passed without executing plugin code.");

static MemoryStream ReadPlugin(string path)
{
    var result = new MemoryStream();
    if (Path.GetExtension(path).Equals(".zip", StringComparison.OrdinalIgnoreCase)) {
        using var archive = ZipFile.OpenRead(path);
        var entries = archive.Entries.Where(e => e.FullName == "AutomaticHunting.dll").ToArray();
        if (entries.Length != 1) throw new InvalidDataException("Expected exactly one root plugin DLL.");
        using var input = entries[0].Open();
        input.CopyTo(result);
    } else {
        using var input = File.OpenRead(path);
        input.CopyTo(result);
    }
    result.Position = 0;
    return result;
}
