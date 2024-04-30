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

        public ParkingClearenceWorker(ILogger<ParkingClearenceWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("ParkingClearanceWorker running at: {time}", DateTimeOffset.Now);

                ParkingClearanceRepository parking = new ParkingClearanceRepository();
                parking.ClearParking();

                await Task.Delay(600000, stoppingToken);
            }
        }
    }
}

