namespace TheJitu_EmailService.Messaging
{
    public interface IAzureMessageBusConsumer
    {
        Task Start();

        Task Stop();
    }
}
