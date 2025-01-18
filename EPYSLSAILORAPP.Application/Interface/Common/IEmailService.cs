using EPYSLSAILORAPP.Application.DTO.General;
using EPYSLSAILORAPP.Application.DTO;

namespace EPYSLSAILORAPP.Application.Interface.Common
{
    public interface IEmailService
    {
        Task<MailServerConfigurationDTO> GetActiveMailServerConfigurationAsync();
        Task<UserEmailInfoDTO> GetUserEmailInfoAsync(int userId);


        Task<EmployeeMailSetupDTO> GetEmployeeMailSetupAsync(int userCode, string setupFor);

        Task<string> GetSupplierEmailInfoAsync(int contactId);

        Task SendAutoEmailAsync(string username, string toEmail, string subject, string messageBody);

        Task SendAutoEmailAsync(string username, string toEmail, string subject, string messageBody, string fileName, byte[] attachment);

        Task SendAutoEmailAsync(string fromMail, string password, string toEmail, string cc, string bcc, string subject, string messageBody, string fileName, byte[] attachment);

        Task SendEmailAsync(string senderName, string senderEmail, string senderPass, string toEmail, string cc, string bcc, string subject, string messageBody, string fileName, byte[] attachment);
    }
}
