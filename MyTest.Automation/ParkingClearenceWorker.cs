using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyTest.Automation
{
    public class ParkingClearenceWorker : BackgroundService
    {
        private readonly ILogger<ParkingClearenceWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ParkingClearenceWorker(ILogger<ParkingClearenceWorker> logger, IServiceProvider serviceProvider)
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
                    _logger.LogInformation("ParkingClearanceWorker running at: {time}", DateTimeOffset.Now);
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        ParkingClearanceRepository parking = scope.ServiceProvider.GetRequiredService<ParkingClearanceRepository>();
                        parking.ClearParking();
                        await Task.Delay(600000, stoppingToken);

                    }

                }

            }
            catch(Exception ex)
            {
                _logger.LogError("Parking clearance -" + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}

