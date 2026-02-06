// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Aspire.Hosting.ApplicationModel;

/// <summary>
/// Provides constants for Devicon icons shipped with Aspire.
/// </summary>
/// <remarks>
/// See https://devicon.dev for the full list of available icons.
/// </remarks>
public static class DeviconIcons
{
    /// <summary>Gets the Azure icon.</summary>
    public static DeviconResourceIcon Azure => new("azure");

    /// <summary>Gets the C# icon.</summary>
    public static DeviconResourceIcon CSharp => new("csharp");

    /// <summary>Gets the Docker icon.</summary>
    public static DeviconResourceIcon Docker => new("docker");

    /// <summary>Gets the .NET Core icon.</summary>
    public static DeviconResourceIcon DotNetCore => new("dotnetcore");

    /// <summary>Gets the Elasticsearch icon.</summary>
    public static DeviconResourceIcon Elasticsearch => new("elasticsearch");

    /// <summary>Gets the Kubernetes icon.</summary>
    public static DeviconResourceIcon Kubernetes => new("kubernetes");

    /// <summary>Gets the MongoDB icon.</summary>
    public static DeviconResourceIcon MongoDb => new("mongodb");

    /// <summary>Gets the MySQL icon.</summary>
    public static DeviconResourceIcon MySql => new("mysql");

    /// <summary>Gets the Nginx icon.</summary>
    public static DeviconResourceIcon Nginx => new("nginx");

    /// <summary>Gets the Node.js icon.</summary>
    public static DeviconResourceIcon NodeJs => new("nodejs");

    /// <summary>Gets the PostgreSQL icon.</summary>
    public static DeviconResourceIcon PostgreSql => new("postgresql");

    /// <summary>Gets the RabbitMQ icon.</summary>
    public static DeviconResourceIcon RabbitMq => new("rabbitmq");

    /// <summary>Gets the Redis icon.</summary>
    public static DeviconResourceIcon Redis => new("redis");
}
