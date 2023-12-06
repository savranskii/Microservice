﻿using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Models.Settings;

namespace SampleApp.Infrastructure.ExternalServices;

public class KafkaProducer
{
    private readonly ILogger<KafkaProducer> _logger;
    private readonly ProducerConfig _producerConfig;

    public KafkaProducer(ILogger<KafkaProducer> logger, IOptions<KafkaSettings> options)
    {
        _logger = logger;
        _producerConfig = new ProducerConfig { BootstrapServers = options.Value.BootstrapServers };
    }

    public async Task<string> SendMessageAsync<T>(string topic, T message)
    {
        var producer = new ProducerBuilder<string, string>(_producerConfig).Build();
        var result = await producer.ProduceAsync(topic, new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(message)
        });

        _logger.LogInformation(LogCategory.KafkaProduce, "Message sent to {topic}", topic);

        return result.Status.ToString();
    }
}
