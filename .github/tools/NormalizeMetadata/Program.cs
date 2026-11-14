using Mono.Cecil;
using System.Text.Json;

if (args.Length != 2) throw new ArgumentException("Expected input DLL and output DLL.");
using var assembly = AssemblyDefinition.ReadAssembly(args[0], new ReaderParameters { ReadSymbols = false, InMemory = true });
var module = assembly.MainModule;
var types = Flatten(module.Types).ToList();
var mappings = new List<object>();
var linkedTypes = new List<(TypeReference Reference, TypeDefinition Definition)>();
foreach (var reference in module.GetTypeReferences()) LinkType(reference);
foreach (var type in types) {
    LinkAttributes(type);
    foreach (var method in type.Methods) {
        LinkAttributes(method);
        LinkAttributes(method.MethodReturnType);
        foreach (var p in method.Parameters) LinkAttributes(p);
    }
    foreach (var field in type.Fields) LinkAttributes(field);
    foreach (var property in type.Properties) LinkAttributes(property);
    foreach (var ev in type.Events) LinkAttributes(ev);
}
LinkAttributes(module);
LinkAttributes(assembly);
var linkedMembers = new List<(MemberReference Reference, IMemberDefinition Definition)>();
foreach (var reference in module.GetMemberReferences()) {
    if (reference.DeclaringType?.GetElementType().Scope != module) continue;
    try {
        IMemberDefinition? definition = reference switch { MethodReference m => m.Resolve(), FieldReference f => f.Resolve(), _ => null };
        if (definition != null) linkedMembers.Add((reference, definition));
    } catch { }
}
foreach (var type in types) {
    if (HasHiddenName(type.Name)) {
        if (type.IsNested) { type.IsNestedPublic = true; } else { type.IsPublic = true; }
    }
    Rename(type, "RecoveredType_" + type.MetadataToken.RID);
    if (HasHiddenName(type.Namespace)) type.Namespace = "Recovered";
    foreach (var p in type.GenericParameters) p.Name = "T" + p.Position;
    foreach (var field in type.Fields) Rename(field, "field_" + field.MetadataToken.RID);
    foreach (var property in type.Properties) Rename(property, "Property_" + property.MetadataToken.RID);
    foreach (var ev in type.Events) Rename(ev, "Event_" + ev.MetadataToken.RID);
    foreach (var method in type.Methods) {
        if (!method.IsConstructor) Rename(method, "Method_" + method.MetadataToken.RID);
        foreach (var p in method.GenericParameters) p.Name = "TMethod" + p.Position;
        foreach (var p in method.Parameters) p.Name = "arg" + p.Index;
    }
    foreach (var property in type.Properties) {
        if (property.SetMethod?.Parameters.Count > 0) property.SetMethod.Parameters[^1].Name = "value";
    }
}
foreach (var (reference, definition) in linkedMembers) reference.Name = definition.Name;
foreach (var (reference, definition) in linkedTypes) {
    reference.Name = definition.Name;
    reference.Namespace = definition.Namespace;
}
Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(args[1]))!);
assembly.Write(args[1]);
File.WriteAllText(Path.ChangeExtension(args[1], ".names.json"), JsonSerializer.Serialize(mappings, new JsonSerializerOptions { WriteIndented = true }));
Console.WriteLine("Normalized " + mappings.Count + " metadata identifiers without changing method bodies.");

bool HasHiddenName(string name) => name.Any(c => c > 127 || char.IsWhiteSpace(c) || char.IsControl(c));
void LinkType(TypeReference reference) {
    var element = reference.GetElementType();
    if (element.Scope != module && (element.Scope is not AssemblyNameReference scope || scope.Name != assembly.Name.Name)) return;
    try { if (element.Resolve() is { } definition) linkedTypes.Add((element, definition)); } catch { }
}
void LinkAttributes(ICustomAttributeProvider provider) {
    foreach (var attribute in provider.CustomAttributes) {
        foreach (var argument in attribute.ConstructorArguments) LinkArgument(argument);
        foreach (var property in attribute.Properties) LinkArgument(property.Argument);
        foreach (var field in attribute.Fields) LinkArgument(field.Argument);
    }
}
void LinkArgument(CustomAttributeArgument argument) {
    if (argument.Value is TypeReference reference) LinkType(reference);
    if (argument.Value is CustomAttributeArgument[] values) foreach (var value in values) LinkArgument(value);
}
void Rename(IMemberDefinition member, string replacement) {
    if (!HasHiddenName(member.Name)) return;
    mappings.Add(new { token = member.MetadataToken.ToUInt32().ToString("X8"), originalName = member.Name, restoredName = replacement });
    member.Name = replacement;
}
static IEnumerable<TypeDefinition> Flatten(IEnumerable<TypeDefinition> source) {
    foreach (var type in source) { yield return type; foreach (var nested in Flatten(type.NestedTypes)) yield return nested; }
}
