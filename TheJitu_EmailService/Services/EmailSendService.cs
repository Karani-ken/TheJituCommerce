using MailKit.Net.Smtp;
using MimeKit;
using TheJitu_EmailService.Models.Dto;

namespace TheJitu_EmailService.Services
{
    public class EmailSendService
    {
        public async Task SendMail(UserMessageDto messageDto, string message)
        {
            MimeMessage message1= new MimeMessage();
            message1.From.Add(new MailboxAddress("Everything E-Commerce", "my.test.email.01@gmail.com"));

            //send the recipient address
            message1.To.Add(new MailboxAddress(messageDto.Name, messageDto.Email));

            message1.Subject = "Welcome to Everything Shopping Site";
            var body = new TextPart("html")
            {
                Text = message.ToString()
            };
            message1.Body = body;

            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);

            client.Authenticate("my.test.email.01@gmail.com", "ubqyzbodumwjlkvn");

            await client.SendAsync(message1);
            await client.DisconnectAsync(true);
        }
    }
}
