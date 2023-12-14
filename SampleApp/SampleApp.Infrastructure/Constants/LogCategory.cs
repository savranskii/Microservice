namespace SampleApp.Infrastructure.Constants;

public static class LogCategory
{
    public const int KafkaProduce = 1000;
    public const int KafkaConsume = 1001;
    public const int DomainEventHandler = 2000;
    public const int IntegrationEventHandler = 3000;
    public const int CommandHandler = 4000;
    public const int QueryHandler = 5000;
    public const int CustomerEndpoint = 6000;
    public const int ExceptionHandler = 9000;
}
