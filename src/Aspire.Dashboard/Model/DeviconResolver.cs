// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Concurrent;
using System.Reflection;
using System.Xml.Linq;

namespace Aspire.Dashboard.Model;

/// <summary>
/// Resolves Devicon icons from embedded resources and extracts their SVG path data.
/// </summary>
public sealed class DeviconResolver
{
    private const string ResourcePrefix = "Aspire.Dashboard.Resources.Icons.Devicon.";
    private static readonly Assembly s_assembly = typeof(DeviconResolver).Assembly;
    private readonly ConcurrentDictionary<string, string?> _cache = new(StringComparer.OrdinalIgnoreCase);
    private readonly ILogger<DeviconResolver> _logger;

    public DeviconResolver(ILogger<DeviconResolver> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets the SVG path data for a Devicon icon.
    /// </summary>
    /// <param name="iconName">The name of the Devicon icon (e.g., "redis", "postgresql").</param>
    /// <returns>The SVG path data string, or null if the icon is not found.</returns>
    public string? GetIconPathData(string iconName)
    {
        if (string.IsNullOrWhiteSpace(iconName))
        {
            return null;
        }

        return _cache.GetOrAdd(iconName, LoadAndExtractPath);
    }

    private string? LoadAndExtractPath(string iconName)
    {
        var resourceName = $"{ResourcePrefix}{iconName.ToLowerInvariant()}.svg";

        try
        {
            using var stream = s_assembly.GetManifestResourceStream(resourceName);
            if (stream is null)
            {
                _logger.LogDebug("Devicon '{IconName}' not found as embedded resource (resource name: {ResourceName})", iconName, resourceName);
                return null;
            }

            using var reader = new StreamReader(stream);
            var svgContent = reader.ReadToEnd();

            return ExtractPathData(svgContent, iconName);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load Devicon '{IconName}'", iconName);
            return null;
        }
    }

    private string? ExtractPathData(string svgContent, string iconName)
    {
        try
        {
            var doc = XDocument.Parse(svgContent);
            var svgNs = doc.Root?.GetDefaultNamespace();

            // Try with namespace first (if present), then without
            var pathElement = (svgNs is not null
                    ? doc.Descendants(svgNs + "path").FirstOrDefault(p => p.Attribute("d") is not null)
                    : null)
                ?? doc.Descendants("path").FirstOrDefault(p => p.Attribute("d") is not null);

            if (pathElement is not null)
            {
                return pathElement.Attribute("d")?.Value;
            }

            _logger.LogWarning("Devicon '{IconName}' SVG does not contain a valid path element with 'd' attribute", iconName);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to parse Devicon '{IconName}' SVG content", iconName);
            return null;
        }
    }
}
