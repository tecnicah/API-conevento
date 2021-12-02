using biz.conevento.Model.Email;

namespace biz.conevento.Servicies
{
    public interface IEmailService
    {
        string SendEmail(EmailModel email);
        string SendEmailAttach(EmailModelAttach email);
    }
}
