// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Aspire.Hosting.ApplicationModel;

/// <summary>
/// Specifies the source library for an icon.
/// </summary>
public enum IconSource
{
    /// <summary>
    /// FluentUI icon library. See https://aka.ms/fluentui-system-icons for available icons.
    /// </summary>
    FluentUi,

    /// <summary>
    /// Devicon icon library. See https://devicon.dev for available icons.
    /// </summary>
    Devicon
}
