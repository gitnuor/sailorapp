using EPYSLSAILORAPP.Application.DTO.General;

using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.General;
using EPYSLSAILORAPP.Domain.Statics;
using MailKit.Net.Smtp;
using MimeKit;

namespace EPYSLSAILORAPP.Infrastructure.Services.Common
{
    public class EmailService : IEmailService
    {
        //private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IDapperCRUDService<DapperBaseEntity> _dapperCRUDService;

        public EmailService(IDapperCRUDService<DapperBaseEntity> dapperCRUDService)
        {
            _dapperCRUDService = dapperCRUDService;
        }

       
        public async Task<MailServerConfigurationDTO> GetActiveMailServerConfigurationAsync()
        {
            var query = $@"
                    Select Top 1 *
					From MailServerConfiguration
					Where IsActive = 1";

            return await _dapperCRUDService.GetFirstOrDefaultAsync<MailServerConfigurationDTO>(query);
        }
        public async Task<EmployeeMailSetupDTO> GetEmployeeMailSetupAsync(int userCode, string setupFor)
        {
            var query = $@"
                Select EMS.ToMailID, CCMailID, BCCMailID
                From EmployeeMailSetup EMS
                Inner Join Employee E On E.EmployeeCode = EMS.EmployeeCode
                Inner Join owin_user LU On LU.EmployeeCode = EMS.EmployeeCode
                Inner Join MailSetupFor MSF on MSF.SetupForID = EMS.SetupForID
                Where LU.UserCode = {userCode} And MSF.SetupForName in (Select _ID From dbo.fnReturnStringArray('{setupFor}',','))";

            return await _dapperCRUDService.GetFirstOrDefaultAsync<EmployeeMailSetupDTO>(query);
        }

      
        public async Task<UserEmailInfoDTO> GetUserEmailInfoAsync(int userCode)
        {
            var query = $@"
                Select E.EmployeeName Name, E.EmailID Email, ISNULL(D.Designation, '') Designation, ISNULL(ED.DepertmentDescription, '') Department, U.EmailPassword
                From LoginUser U
                Inner Join Employee E On E.EmployeeCode = U.EmployeeCode
                Inner Join EmployeeDepartment ED On ED.DepertmentID = E.DepertmentID
                Inner Join EmployeeDesignation D On D.DesigID = E.DesigID
                Where E.IsRegular = 1 And U.IsActive = 1 and U.UserCode in ({userCode})";

            return await _dapperCRUDService.GetFirstOrDefaultAsync<UserEmailInfoDTO>(query);
        }

        public async Task<string> GetSupplierEmailInfoAsync(int contactId)
        {
            var query = $"Select EmailNo From ContactManagementInfo Where ContactID = {contactId}";
            return await _dapperCRUDService.GetFirstOrDefaultAsync<string>(query);
        }

        public async Task SendAutoEmailAsync(string username, string toEmail, string subject, string messageBody)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("ERP Auto Mail", "erpnoreply@epylliongroup.com"));

                var receivers = toEmail.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                InternetAddressList receiverList = new InternetAddressList();
                receivers.ToList().ForEach(x => receiverList.Add(new MailboxAddress(x, x)));
                message.To.AddRange(receiverList);

                //message.Cc.Add();

                message.Subject = subject;

                var bodyBuilder = new BodyBuilder
                {
                    TextBody = $@"Greetings {username},",
                    HtmlBody = messageBody
                };

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("mail.epylliongroup.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("erpnoreply@epylliongroup.com", "Ugr7jT5d");

                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                //_logger.Error(ex.Message);
                throw ex;
            }
        }

        public async Task SendAutoEmailAsync(string username, string toEmail, string subject, string messageBody, string fileName, byte[] attachment)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("ERP Auto Mail", "erpnoreply@epylliongroup.com"));

                var receivers = toEmail.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                InternetAddressList receiverList = new InternetAddressList();
                receivers.ToList().ForEach(x => receiverList.Add(new MailboxAddress(x, x)));
                message.To.AddRange(receiverList);

                //message.Cc.Add();

                message.Subject = subject;

                var bodyBuilder = new BodyBuilder
                {
                    TextBody = $@"Greetings {username},",
                    HtmlBody = messageBody
                };

                bodyBuilder.Attachments.Add(fileName, attachment);

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("mail.epylliongroup.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("erpnoreply@epylliongroup.com", "Ugr7jT5d");

                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                //_logger.Error(ex.Message);
                throw ex;
            }
        }

        public async Task SendAutoEmailAsync(string fromemail, string password, string toEmail, string cc, string bcc, string subject, string messageBody, string fileName, byte[] attachment)
        {
            try
            {
                //AppUser = UserService

                var mailConfiguration = await GetActiveMailServerConfigurationAsync();
                if (mailConfiguration.IsNotNull())
                {
                    if (fromemail == "Erp No Reply")
                    {
                        fromemail = mailConfiguration.SMTPMailID;
                        password = mailConfiguration.SMTPMailPassword;
                    }
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("", fromemail));

                    var receivers = toEmail.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    InternetAddressList receiverList = new InternetAddressList();
                    receivers.ToList().ForEach(x => receiverList.Add(new MailboxAddress(x, x)));
                    message.To.AddRange(receiverList);

                    if (cc.NotNullOrEmpty())
                    {
                        var addresses = cc.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => MailboxAddress.Parse(x));
                        message.Cc.AddRange(addresses);
                    }

                    if (bcc.NotNullOrEmpty())
                    {
                        var addresses = bcc.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => MailboxAddress.Parse(x));
                        message.Bcc.AddRange(addresses);
                    }

                    message.Subject = subject;

                    var bodyBuilder = new BodyBuilder
                    {
                        TextBody = $@"Greetings,",
                        HtmlBody = messageBody
                    };

                    if (fileName.IsNotNullOrEmpty())
                    {
                        bodyBuilder.Attachments.Add(fileName, attachment);
                    }
                    message.Body = bodyBuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        client.Connect(mailConfiguration.SMTPServerIP, mailConfiguration.SMTPServerPort, false);

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate(fromemail, password);

                        await client.SendAsync(message);
                        client.Disconnect(true);
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.Error(ex.Message);
                throw ex;
            }
        }
        public async Task SendEmailAsync(string senderName, string senderEmail, string senderPass, string toEmail, string cc, string bcc, string subject, string messageBody, string fileName, byte[] attachment)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderEmail));

                var receivers = toEmail.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                InternetAddressList receiverList = new InternetAddressList();
                receivers.ToList().ForEach(x => receiverList.Add(new MailboxAddress(x, x)));
                message.To.AddRange(receiverList);

                if (cc.NotNullOrEmpty())
                {
                    var addresses = cc.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => MailboxAddress.Parse(x));
                    message.Cc.AddRange(addresses);
                }

                if (bcc.NotNullOrEmpty())
                {
                    var addresses = bcc.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => MailboxAddress.Parse(x));
                    message.Bcc.AddRange(addresses);
                }

                message.Subject = subject;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = messageBody
                };

                if (fileName.NotNullOrEmpty() || attachment.Length > 0) bodyBuilder.Attachments.Add(fileName, attachment);

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("mail.epylliongroup.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(senderEmail, senderPass);

                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                //_logger.Error(ex.Message);
                throw ex;
            }
        }
    }
}
