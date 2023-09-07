using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using TheJitu_EmailService.Models.Dto;
using TheJitu_EmailService.Services;

namespace TheJitu_EmailService.Messaging
{
    public class AzureMessagingBusConsumer : IAzureMessageBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        private readonly string QueueName;
        private readonly ServiceBusProcessor _registrationProcessor;
        private readonly EmailSendService emailSend;

        public AzureMessagingBusConsumer(IConfiguration configuration)
        {
                _configuration=configuration;
            ConnectionString = _configuration.GetSection("ServiceBus:ConnectionString").Get<string>();
            QueueName = _configuration.GetSection("QueuesandTopics:RegisterUser").Get<string>();

            //connect to the service bus client
            var serviceBusClient = new ServiceBusClient(ConnectionString);
            _registrationProcessor = serviceBusClient.CreateProcessor(QueueName);
            emailSend=new EmailSendService();
        }
        public async Task Start()
        {
            //start processing
            _registrationProcessor.ProcessMessageAsync += OnRegistration;
            _registrationProcessor.ProcessErrorAsync += ErrorHandler;

            await _registrationProcessor.StopProcessingAsync();
        }
        public async Task Stop()
        {
            //stop processing
            await _registrationProcessor.StopProcessingAsync();
            await _registrationProcessor.DisposeAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            throw new NotImplementedException();
        }

        private async Task OnRegistration(ProcessMessageEventArgs args)
        {
            //get th message from arguments
            var message = args.Message;
            //convert the data to a string before deserializing to an object
            var body = Encoding.UTF8.GetString(message.Body);
            //deserialize email to an object
            var userMessage = JsonConvert.DeserializeObject<UserMessageDto>(body);
            //send the email
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://cdn.pixabay.com/photo/2023/04/20/10/19/coding-7939372_1280.jpg\" width=\"1000\" height=\"600\">");
                stringBuilder.Append("<h1> Hello " + userMessage.Name + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to The Jitu Shopping Site ");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p> Start Shopping here</p>");
                await emailSend.SendMail(userMessage, stringBuilder.ToString());
                //delete the email from the queue
                await args.CompleteMessageAsync(message);
            }catch(Exception ex) {}
        }

       
       

       
    }
}
