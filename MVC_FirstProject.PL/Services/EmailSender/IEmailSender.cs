using System.Threading.Tasks;

namespace MVC_FirstProject.PL.Services.EmailSender
{
	public interface IEmailSender
	{
		Task SendAsync(string from, string recipients, string subject, string body);
	}
}
