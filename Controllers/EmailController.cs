// using Microsoft.AspNetCore.Mvc;
// using System.Threading.Tasks;

// [ApiController]
// [Route("[controller]")]
// public class EmailController : ControllerBase
// {
//     private readonly IEmailService _emailService;

//     public EmailController(IEmailService emailService)
//     {
//         _emailService = emailService;
//     }

//     [HttpPost("send")]
//     public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
//     {
//         await _emailService.SendEmailAsync(emailRequest.ToEmail, emailRequest.Subject, emailRequest.PlainTextContent, emailRequest.HtmlContent);
//         return Ok("Email sent successfully.");
//     }
// }

// public class EmailRequest
// {
//     public string ToEmail { get; set; }
//     public string Subject { get; set; }
//     public string PlainTextContent { get; set; }
//     public string HtmlContent { get; set; }
// }
