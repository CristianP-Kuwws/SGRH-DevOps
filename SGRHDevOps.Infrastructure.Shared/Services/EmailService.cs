using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SGRHDevOps.Core.Domain.Settings;

namespace SGRHDevOps.Infrastructure.Shared.Services
{
    public class EmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync()
        {
            throw new NotImplementedException();
        }

    }
}
