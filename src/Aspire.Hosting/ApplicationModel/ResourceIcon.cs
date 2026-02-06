// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Aspire.Hosting.ApplicationModel;

/// <summary>
/// Represents a Devicon resource icon.
/// </summary>
/// <param name="Name">The name of the Devicon icon (e.g., "redis", "postgresql").</param>
public sealed record DeviconResourceIcon(string Name);
