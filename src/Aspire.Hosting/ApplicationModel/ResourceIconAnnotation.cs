// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;

namespace Aspire.Hosting.ApplicationModel;

/// <summary>
/// Specifies the icon to use when displaying a resource in the dashboard.
/// </summary>
/// <param name="iconName">The name of the icon to use.</param>
/// <param name="iconSource">The source library for the icon. Defaults to <see cref="IconSource.FluentUi"/>.</param>
/// <param name="iconVariant">The variant of the icon (Regular or Filled). Only applicable for FluentUI icons.</param>
[DebuggerDisplay("Type = {GetType().Name,nq}, IconName = {IconName}, IconSource = {IconSource}, IconVariant = {IconVariant}")]
public sealed class ResourceIconAnnotation(
    string iconName,
    IconSource iconSource = IconSource.FluentUi,
    IconVariant iconVariant = IconVariant.Filled) : IResourceAnnotation
{
    /// <summary>
    /// Gets the name of the icon to use for the resource.
    /// </summary>
    /// <remarks>
    /// For FluentUI icons, see https://aka.ms/fluentui-system-icons for available icons.
    /// For Devicon icons, see https://devicon.dev for available icons.
    /// </remarks>
    public string IconName { get; } = iconName ?? throw new ArgumentNullException(nameof(iconName));

    /// <summary>
    /// Gets the source library for the icon.
    /// </summary>
    public IconSource IconSource { get; } = iconSource;

    /// <summary>
    /// Gets the variant of the icon (Regular or Filled).
    /// </summary>
    /// <remarks>
    /// This property is only applicable for FluentUI icons. For Devicon icons, this value is ignored.
    /// </remarks>
    public IconVariant IconVariant { get; } = iconVariant;
}