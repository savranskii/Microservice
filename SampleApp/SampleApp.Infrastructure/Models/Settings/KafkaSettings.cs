namespace SampleApp.Infrastructure.Models.Settings;

public class KafkaSettings
{
    public required string BootstrapServers { get; init; } = string.Empty;
    public required string GroupId { get; init; } = string.Empty;
    public required string Topic { get; init; } = string.Empty;
}
