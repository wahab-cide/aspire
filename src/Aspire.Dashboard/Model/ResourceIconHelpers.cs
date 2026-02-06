// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Aspire.Dashboard.Model;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.FluentUI.AspNetCore.Components;
using Icons = Microsoft.FluentUI.AspNetCore.Components.Icons;

internal static class ResourceIconHelpers
{
    /// <summary>
    /// Maps a resource to an icon, checking for custom icons first, then falling back to default icons.
    /// </summary>
    public static Icon GetIconForResource(IconResolver iconResolver, ResourceViewModel resource, IconSize desiredSize, IconVariant desiredVariant = IconVariant.Filled)
    {
        // Check if the resource has a custom FluentUI icon specified
        if (!string.IsNullOrWhiteSpace(resource.IconName) && resource.IconSource != IconSource.Devicon)
        {
            var customIcon = iconResolver.ResolveIconName(resource.IconName, desiredSize, resource.IconVariant ?? IconVariant.Filled);
            if (customIcon != null)
            {
                return customIcon;
            }
        }

        // Fall back to default icons based on resource type
        var icon = resource.ResourceType switch
        {
            KnownResourceTypes.Executable => iconResolver.ResolveIconName("Apps", desiredSize, desiredVariant),
            KnownResourceTypes.Project => ResolveProjectIcon(iconResolver, resource, desiredSize, desiredVariant),
            KnownResourceTypes.Container => iconResolver.ResolveIconName("Box", desiredSize, desiredVariant),
            KnownResourceTypes.Parameter => iconResolver.ResolveIconName("Key", desiredSize, desiredVariant),
            KnownResourceTypes.ConnectionString => iconResolver.ResolveIconName("PlugConnectedSettings", desiredSize, desiredVariant),
            KnownResourceTypes.ExternalService => iconResolver.ResolveIconName("GlobeArrowForward", desiredSize, desiredVariant),
            string t when t.Contains("database", StringComparison.OrdinalIgnoreCase) => iconResolver.ResolveIconName("Database", desiredSize, desiredVariant),
            _ => iconResolver.ResolveIconName("SettingsCogMultiple", desiredSize, desiredVariant),
        };

        if (icon == null)
        {
            throw new InvalidOperationException($"Couldn't resolve resource icon for {resource.Name}.");
        }

        return icon;

        static Icon? ResolveProjectIcon(IconResolver iconResolver, ResourceViewModel resource, IconSize desiredSize, IconVariant desiredVariant = IconVariant.Filled)
        {
            if (resource.TryGetProjectPath(out var projectPath) && !string.IsNullOrEmpty(projectPath))
            {
                var extension = Path.GetExtension(projectPath);
                return extension?.ToLowerInvariant() switch
                {
                    ".csproj" or ".cs" => iconResolver.ResolveIconName("CodeCsRectangle", desiredSize, desiredVariant),
                    ".fsproj" => iconResolver.ResolveIconName("CodeFsRectangle", desiredSize, desiredVariant),
                    ".vbproj" => iconResolver.ResolveIconName("CodeVbRectangle", desiredSize, desiredVariant),
                    _ => iconResolver.ResolveIconName("CodeCircle", desiredSize, desiredVariant)
                };
            }

            return iconResolver.ResolveIconName("CodeCircle", desiredSize, desiredVariant);
        }
    }

    /// <summary>
    /// Gets the icon path data for a resource, checking for Devicon icons first if specified.
    /// </summary>
    /// <param name="iconResolver">The FluentUI icon resolver.</param>
    /// <param name="deviconResolver">The Devicon resolver.</param>
    /// <param name="resource">The resource to get the icon for.</param>
    /// <param name="desiredSize">The desired icon size.</param>
    /// <param name="desiredVariant">The desired icon variant (for FluentUI icons).</param>
    /// <returns>The SVG path data string for the icon.</returns>
    public static string GetIconPathDataForResource(
        IconResolver iconResolver,
        DeviconResolver deviconResolver,
        ResourceViewModel resource,
        IconSize desiredSize,
        IconVariant desiredVariant = IconVariant.Filled)
    {
        // Check if the resource has a custom Devicon specified
        if (!string.IsNullOrWhiteSpace(resource.IconName) && resource.IconSource == IconSource.Devicon)
        {
            var deviconPath = deviconResolver.GetIconPathData(resource.IconName);
            if (deviconPath is not null)
            {
                return deviconPath;
            }
            // Fall through to FluentUI icons if Devicon not found
        }

        // Use FluentUI icon
        var fluentIcon = GetIconForResource(iconResolver, resource, desiredSize, desiredVariant);
        return ExtractPathData(fluentIcon);
    }

    /// <summary>
    /// Extracts the path data from a FluentUI icon.
    /// </summary>
    public static string ExtractPathData(Icon icon)
    {
        var p = icon.Content;
        var e = System.Xml.Linq.XElement.Parse(p);
        return e.Attribute("d")!.Value;
    }

    public static (Icon? icon, Color color) GetHealthStatusIcon(HealthStatus? healthStatus)
    {
        return healthStatus switch
        {
            HealthStatus.Healthy => (new Icons.Filled.Size16.Heart(), Color.Success),
            HealthStatus.Degraded => (new Icons.Filled.Size16.HeartBroken(), Color.Warning),
            HealthStatus.Unhealthy => (new Icons.Filled.Size16.HeartBroken(), Color.Error),
            _ => (new Icons.Regular.Size16.CircleHint(), Color.Info)
        };
    }
}
