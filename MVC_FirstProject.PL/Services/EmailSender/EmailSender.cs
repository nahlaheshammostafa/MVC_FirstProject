using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MVC_FirstProject.PL.Services.EmailSender
{
	public class EmailSender : IEmailSender
	{
		private readonly IConfiguration _configuration;

		public EmailSender(IConfiguration configuration) 
		{
			_configuration = configuration;
		}
		public async Task SendAsync(string form, string recipients, string subject, string body)
		{
			var senderEmail = _configuration["EmailSettings:SenderEmail"];
			var senderPassword = _configuration["EmailSettings:senderPassword"];

			var emailMessage = new MailMessage();
			emailMessage.From = new MailAddress(form);
			emailMessage.Subject = subject;
			emailMessage.Body = $"<html><body>{body}</body></html>";
			emailMessage.To.Add(recipients);
			emailMessage.IsBodyHtml = true;

			var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpClientServer"],int.Parse(_configuration["EmailSettings:SmtpClientPort"]))
			{
				Credentials = new NetworkCredential(senderEmail,senderPassword),
				EnableSsl = true
			};
			await smtpClient.SendMailAsync(emailMessage);
		}
	}
}
