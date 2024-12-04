using System.Net.Mail;

namespace TempoMapRepository.Services.Smtp
{
    public interface ITransporterService
    {
        SmtpClient Client { get; }
    }
}
