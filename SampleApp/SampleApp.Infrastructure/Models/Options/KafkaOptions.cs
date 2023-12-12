using System.ComponentModel.DataAnnotations;

namespace SampleApp.Infrastructure.Models.Options;

public class KafkaOptions
{
    [Required]
    public string BootstrapServers { get; set; } = string.Empty;
    [Required]
    public string GroupId { get; set; } = string.Empty;
    [Required]
    public string Topic { get; set; } = string.Empty;
}
