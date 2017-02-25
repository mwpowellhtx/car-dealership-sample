using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Powell.Vehicles.Mvc.Services
{
    using MailKit.Net.Smtp;
    using MimeKit;

    public interface IMailKitMessageService : IIdentityMessageService
    {
    }

    public class MailKitMessageService : IMailKitMessageService
    {
        private static MimeMessage CreateMessage(IdentityMessage message)
        {
            var result = new MimeMessage
            {
                Subject = message.Subject,
                To = {new MailboxAddress(message.Destination, message.Destination)},
                From = {new MailboxAddress(@"mwpowellhtx@gmail.com", @"mwpowellhtx@gmail.com")},
                Body = new TextPart("html") {Text = message.Body}
            };

            return result;
        }

        public Task SendAsync(IdentityMessage message)
        {
            return Task.Run(() =>
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(@"smtp.gmail.com", 465);

                    // Remove since we do not have an OAuth token.
                    client.AuthenticationMechanisms.Remove(@"XOAUTH2");

                    // TODO: TBD: I don't advise this for even demo code...
                    client.Authenticate("mwpowellhtx", @"#Pass16wd@");

                    client.Send(CreateMessage(message));

                    client.Disconnect(true);
                }
            });
        }
    }
}
