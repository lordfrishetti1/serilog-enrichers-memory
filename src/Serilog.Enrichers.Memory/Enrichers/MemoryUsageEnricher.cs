﻿// Copyright 2017, 2024 Josh Schreuder
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Serilog.Core;
using Serilog.Events;

// ReSharper disable CheckNamespace
namespace Serilog.Enrichers;
// ReSharper restore CheckNamespace

/// <summary>
/// Enriches log events with a MemoryUsage property containing an estimation of the current process' memory usage in bytes.
/// </summary>
public class MemoryUsageEnricher : ILogEventEnricher
{
    /// <summary>
    /// The property name added to enriched log events.
    /// </summary>
    public const string MemoryUsagePropertyName = "MemoryUsage";

    /// <summary>
    /// Enrich the log event.
    /// </summary>
    /// <param name="logEvent">The log event to enrich.</param>
    /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(MemoryUsagePropertyName, GC.GetTotalMemory(false)));
    }
}