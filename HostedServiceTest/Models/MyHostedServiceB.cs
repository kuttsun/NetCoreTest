using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceTest.Models
{
    public class MyHostedServiceB : BackgroundService
    {
        readonly ILogger<MyHostedServiceA> _logger;

        public MyHostedServiceB(ILogger<MyHostedServiceA> logger)
        {
            _logger = logger;
            //Constructor’s parameters validations...       
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"MyHostedServiceB is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug($" MyHostedServiceB background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"MyHostedServiceB task doing background work.");

                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogDebug($"MyHostedServiceB background task is stopping.");
        }
    }
}
