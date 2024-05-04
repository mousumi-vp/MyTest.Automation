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
    public sealed class DailyScheduleStatusMailWorker : BackgroundService
    {
        private readonly ILogger<InterviewReminderMailWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        public DailyScheduleStatusMailWorker(
            ILogger<InterviewReminderMailWorker> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {

                    _logger.LogInformation("DailyScheduleStatusMailWorker running at: {time}", DateTimeOffset.Now);

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var scheduleStatusMailRepository = scope.ServiceProvider.GetRequiredService<DailyScheduleStatusMailRepository>();
                        scheduleStatusMailRepository.TriggerEmailToCandidateOwners();
                        await Task.Delay(600000, cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Schedule status email -" + ex.Message + "\n" + ex.StackTrace);
            }

        }
    }
}
