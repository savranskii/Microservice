namespace SampleApp.Infrastructure.ExternalServices;

public interface IIntegrationEventSender
{
    Task<string> SendMessageAsync<T>(string topic, T message);
}
