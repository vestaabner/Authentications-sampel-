using Domain.Settings;
using System.Threading.Tasks;

namespace Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
