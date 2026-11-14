using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutomaticHunting;

/// <summary>Changes display text only; original names, commands and hidden widget IDs stay intact.</summary>
internal sealed class TraditionalNameFormatter
{
    private static readonly Regex NameRun = new(@"[A-Za-zÀ-ÖØ-öø-ÿ][A-Za-zÀ-ÖØ-öø-ÿ0-9'’ .\-]*", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private readonly IReadOnlyDictionary<string, string> names;
    private readonly ConcurrentDictionary<string, byte> protectedNames = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, string> cache = new(StringComparer.Ordinal);

    internal TraditionalNameFormatter(IReadOnlyDictionary<string, string> names, ISet<string> protectedNames)
    {
        this.names = names;
        foreach (var name in protectedNames) this.protectedNames.TryAdd(name, 0);
    }

    internal string Translate(string? text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        if (text.StartsWith("/", StringComparison.Ordinal)) return text;
        if (cache.TryGetValue(text, out var cached)) return cached;
        var idStart = text.IndexOf("##", StringComparison.Ordinal);
        var visible = idStart < 0 ? text : text[..idStart];
        visible = CleanDisplay(visible);
        const string oldPresetPrefix = "\u8fde\u8f66\u914d\u7f6e ";
        if (visible.StartsWith(oldPresetPrefix, StringComparison.Ordinal))
            visible = "連車設定 " + visible[oldPresetPrefix.Length..];
        var suffix = idStart < 0 ? string.Empty : text[idStart..];
        var translated = Name(visible);
        if (translated == visible) translated = NameRun.Replace(visible, match => {
            var token = match.Value.Trim();
            var display = Name(token);
            if (display == token) return match.Value;
            var offset = match.Value.IndexOf(token, StringComparison.Ordinal);
            return match.Value[..offset] + display + match.Value[(offset + token.Length)..];
        });
        var result = translated + suffix;
        if (text.Length <= 1024) {
            if (cache.Count >= 1024) cache.Clear();
            cache.TryAdd(text, result);
        }
        return result;
    }

    internal void PreserveName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name) && protectedNames.TryAdd(name, 0)) cache.Clear();
    }

    internal bool Matches(string native, string query) =>
        string.IsNullOrWhiteSpace(query) || native.Contains(query, StringComparison.OrdinalIgnoreCase)
        || Translate(native).Contains(query, StringComparison.OrdinalIgnoreCase);

    private static string CleanDisplay(string text)
    {
        var clean = new System.Text.StringBuilder(text.Length);
        foreach (var c in text)
            if (c != '\uFFFD' && !(c >= '\uE000' && c <= '\uF8FF')) clean.Append(c);
        return clean.ToString();
    }

    private string Name(string text) => !protectedNames.ContainsKey(text) && names.TryGetValue(text, out var name) ? name : text;
}
