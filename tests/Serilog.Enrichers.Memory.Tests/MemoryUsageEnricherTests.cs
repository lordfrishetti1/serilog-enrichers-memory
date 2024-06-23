using System.Diagnostics.CodeAnalysis;
using Moq;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Enrichers.Memory.Tests;

[ExcludeFromCodeCoverage]
public class MemoryUsageEnricherTests
{
    [Fact]
    public void Enrich_ShouldAddMemoryUsageProperty()
    {
        // Arrange
        var enricher = new MemoryUsageEnricher();
        var logEvent = new LogEvent(DateTimeOffset.Now, LogEventLevel.Information, null, new MessageTemplate(new List<MessageTemplateToken>()), new List<LogEventProperty>());
        var propertyFactoryMock = new Mock<ILogEventPropertyFactory>();
        propertyFactoryMock.Setup(x => x.CreateProperty(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()))
            .Returns((string name, object value, bool destructureObjects) => new LogEventProperty(name, new ScalarValue(value)));

        // Act
        enricher.Enrich(logEvent, propertyFactoryMock.Object);

        // Assert
        var memoryUsageProperty = logEvent.Properties.FirstOrDefault(p => p.Key == MemoryUsageEnricher.MemoryUsagePropertyName);
        Assert.NotNull(memoryUsageProperty.Value);
    }

    [Fact]
    public void Enrich_ShouldAddMemoryUsageWithPositiveValue()
    {
        // Arrange
        var enricher = new MemoryUsageEnricher();
        var logEvent = new LogEvent(DateTimeOffset.Now, LogEventLevel.Information, null, new MessageTemplate(new List<MessageTemplateToken>()), new List<LogEventProperty>());
        var propertyFactoryMock = new Mock<ILogEventPropertyFactory>();
        propertyFactoryMock.Setup(x => x.CreateProperty(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()))
            .Returns((string name, object value, bool destructureObjects) => new LogEventProperty(name, new ScalarValue(value)));

        // Act
        enricher.Enrich(logEvent, propertyFactoryMock.Object);

        // Assert
        var memoryUsageProperty = logEvent.Properties.FirstOrDefault(p => p.Key == MemoryUsageEnricher.MemoryUsagePropertyName);
        Assert.True(memoryUsageProperty.Value is ScalarValue scalarValue && scalarValue.Value is long && (long)scalarValue.Value > 0);
    }

    [Fact]
    public void Enrich_ShouldNotAddMemoryUsagePropertyTwice()
    {
        // Arrange
        var enricher = new MemoryUsageEnricher();
        var logEvent = new LogEvent(DateTimeOffset.Now, LogEventLevel.Information, null, new MessageTemplate(new List<MessageTemplateToken>()), new List<LogEventProperty>());
        var propertyFactoryMock = new Mock<ILogEventPropertyFactory>();
        propertyFactoryMock.Setup(x => x.CreateProperty(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()))
            .Returns((string name, object value, bool destructureObjects) => new LogEventProperty(name, new ScalarValue(value)));

        // Act
        enricher.Enrich(logEvent, propertyFactoryMock.Object);
        enricher.Enrich(logEvent, propertyFactoryMock.Object); // Enrich a second time

        // Assert
        var memoryUsageProperties = logEvent.Properties.Where(p => p.Key == MemoryUsageEnricher.MemoryUsagePropertyName);
        Assert.Single(memoryUsageProperties);
    }
}