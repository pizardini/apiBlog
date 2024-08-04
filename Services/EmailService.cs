// using Microsoft.Extensions.Configuration;
// using SendGrid;
// using SendGrid.Helpers.Mail;
// using System.Threading.Tasks;

// public interface IEmailService
// {
//     Task SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent);
// }

// public class SendGridEmailService : IEmailService
// {
//     private readonly string _apiKey;

//     public SendGridEmailService(IConfiguration configuration)
//     {
//         _apiKey = configuration["SendGrid:ApiKey"];
//     }

//     public async Task SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent)
//     {
//         var client = new SendGridClient(_apiKey);
//         var from = new EmailAddress("test@example.com", "Example User");
//         var to = new EmailAddress(toEmail);
//         var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//         var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

//         // Optionally handle the response or log it
//     }
// }
