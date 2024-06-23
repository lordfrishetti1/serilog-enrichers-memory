using System.Diagnostics.CodeAnalysis;
using Moq;
using Serilog.Configuration;

namespace Serilog.Enrichers.Memory.Tests;

[ExcludeFromCodeCoverage]
public class ProcessLoggerConfigurationExtensionsTests
{
    [Fact]
    public void WithMemoryUsage_ThrowsArgumentNullException_WhenEnrichmentConfigurationIsNull()
    {
        // Arrange
        LoggerEnrichmentConfiguration enrichmentConfiguration = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => ProcessLoggerConfigurationExtensions.WithMemoryUsage(enrichmentConfiguration));
    }
}