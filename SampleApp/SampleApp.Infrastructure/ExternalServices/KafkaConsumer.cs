using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SampleApp.Infrastructure.Constants;
using SampleApp.Infrastructure.Models;
using SampleApp.Infrastructure.Models.Options;

namespace SampleApp.Infrastructure.ExternalServices;

public class KafkaConsumer
{
    private readonly string _topic;
    private readonly ILogger<KafkaConsumer> _logger;
    private readonly ConsumerConfig _consumerConfig;

    public KafkaConsumer(ILogger<KafkaConsumer> logger, IOptions<KafkaOptions> options)
    {
        _logger = logger;
        _topic = options.Value.Topic;
        _consumerConfig = new ConsumerConfig
        {
            GroupId = options.Value.GroupId,
            BootstrapServers = options.Value.BootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var consumerBuilder = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
            consumerBuilder.Subscribe(_topic);
            var cancelToken = new CancellationTokenSource();

            try
            {
                while (true)
                {
                    var consumer = consumerBuilder.Consume(cancelToken.Token);
                    var request = JsonSerializer.Deserialize<ProcessingRequest>(consumer.Message.Value);
                    _logger.LogInformation(LogCategory.KafkaConsume, "Processing Id: {id}", request?.Id);
                }
            }
            catch (OperationCanceledException)
            {
                consumerBuilder.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            _logger.LogError(LogCategory.KafkaConsume, "Consumer error. Reason: {message}", ex.Message);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
