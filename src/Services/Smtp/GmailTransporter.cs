using System.Net;
using System.Net.Mail;

namespace TempoMapRepository.Services.Smtp
{
    public class GmailTransporter : ITransporterService
    {
        private IServiceProvider _serviceProvider;
        public SmtpClient Client { get; }
        public GmailTransporter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            NetworkCredential networkCredential = new NetworkCredential(configuration["SMTP:Gmail:Username"], configuration["SMTP:Gmail:Password"]);
            Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.UseDefaultCredentials = false;
            Client.Credentials = networkCredential;
        }

    }
}
