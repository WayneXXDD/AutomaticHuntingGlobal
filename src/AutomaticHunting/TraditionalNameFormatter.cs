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
    private readonly ISet<string> protectedNames;
    private readonly ConcurrentDictionary<string, string> cache = new(StringComparer.Ordinal);

    internal TraditionalNameFormatter(IReadOnlyDictionary<string, string> names, ISet<string> protectedNames)
    {
        this.names = names;
        this.protectedNames = protectedNames;
    }

    internal string Translate(string? text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        if (text.StartsWith("/", StringComparison.Ordinal)) return text;
        if (cache.TryGetValue(text, out var cached)) return cached;
        var idStart = text.IndexOf("##", StringComparison.Ordinal);
        var visible = idStart < 0 ? text : text[..idStart];
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

    private string Name(string text) => !protectedNames.Contains(text) && names.TryGetValue(text, out var name) ? name : text;
}
