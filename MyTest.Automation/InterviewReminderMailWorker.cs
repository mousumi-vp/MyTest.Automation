using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Repository;

namespace MyTest.Automation
{
    public sealed class InterviewReminderMailWorker : BackgroundService
    {
        private readonly ILogger<InterviewReminderMailWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        public InterviewReminderMailWorker(
            ILogger<InterviewReminderMailWorker> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {

                    _logger.LogInformation("InterviewReminderMailWorker running at: {time}", DateTimeOffset.Now);

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var reminderMailRepository = scope.ServiceProvider.GetRequiredService<InterviewReminderMailRepository>();
                        reminderMailRepository.TriggerEmailAll();
                        await Task.Delay(600000, stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Reminder Email -" + ex.Message + "\n" + ex.StackTrace);
            }

        }
    }
}
