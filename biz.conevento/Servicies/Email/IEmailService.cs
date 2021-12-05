using biz.conevento.Models.Email;

namespace biz.conevento.Servicies
{
    public interface IEmailService
    {
        string SendEmail(EmailModel email);
        string SendEmailAttach(EmailModelAttach email);
    }
}
