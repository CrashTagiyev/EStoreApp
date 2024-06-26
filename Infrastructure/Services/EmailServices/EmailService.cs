﻿using Application.ServiceInterfaces.EmailServices;
using Domain.Entities.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Services.AuthServices;

namespace Infrastructure.Services.EmailServices
{
	public class EmailService(IConfiguration config) : IEmailService
	{
		private readonly IConfiguration _config = config;

		public Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML)
		{
			string MailServer = _config["EmailSettings:MailServer"]!;
			string FromEmail = _config["EmailSettings:FromEmail"]!;
			string Password = _config["EmailSettings:Password"]!;
			int Port = int.Parse(_config["EmailSettings:MailPort"]!);
			var client = new SmtpClient(MailServer, Port)
			{
				Credentials = new NetworkCredential(FromEmail, Password),
				EnableSsl = true,
			};
			MailMessage mailMessage = new MailMessage(FromEmail, toEmail, subject, body)
			{
				IsBodyHtml = isBodyHTML
			};
			return client.SendMailAsync(mailMessage);
		}

	}
}
