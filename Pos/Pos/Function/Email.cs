using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Pos.Models;

namespace Pos.Function
{
	internal class Email
	{
		public static void SendEmail(string toAddress, string subject, string body)
		{
			Models.Setting setting = Function.Helper.getSetting();

			if(setting != null)
			{
				var client = new SmtpClient(setting.MailHost, int.Parse(setting.MailPort.ToString()))
				{
					Credentials = new NetworkCredential(setting.MailUsername, setting.MailPassword),
					EnableSsl = true
				};

				var mailMessage = new MailMessage
				{
					From = new MailAddress(setting.MailMailer),
					Subject = subject,
					Body = body,
					IsBodyHtml = setting.MailAllowHtml == "Yes" ? true : false,
				};

				mailMessage.To.Add(toAddress);

				client.Send(mailMessage);
			}
		}
	}
}
