namespace SampleApp.Infrastructure.Constants;

public static class LogCategory
{
    public const int KafkaProduce = 1000;
    public const int KafkaConsume = 1001;
    public const int DomainEventHandler = 2000;
    public const int CommandHandler = 3000;
    public const int QueryHandler = 4000;
    public const int ExceptionHandler = 9000;
}
