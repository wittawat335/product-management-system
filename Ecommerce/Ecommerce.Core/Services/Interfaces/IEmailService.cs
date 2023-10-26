using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO request, string host, string username, string password);
        Task<Response<MailRequest>> SendEmailAsync(MailRequest request);
        string GetHtmlContent();
    }
}
