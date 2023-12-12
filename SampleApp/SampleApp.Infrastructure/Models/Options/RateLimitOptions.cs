using System.ComponentModel.DataAnnotations;

namespace SampleApp.Infrastructure.Models.Options;

public class RateLimitOptions
{
    [Required]
    public int PermitLimit { get; set; } = 100;
    [Required]
    public int Window { get; set; } = 10;
    [Required]
    public int ReplenishmentPeriod { get; set; } = 2;
    [Required]
    public int QueueLimit { get; set; } = 2;
    [Required]
    public int SegmentsPerWindow { get; set; } = 8;
    [Required]
    public int TokenLimit { get; set; } = 10;
    [Required]
    public int TokenLimit2 { get; set; } = 20;
    [Required]
    public int TokensPerPeriod { get; set; } = 4;
    [Required]
    public bool AutoReplenishment { get; set; } = false;
}
