using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceTest.Models
{
    public class MyHostedServiceA : BackgroundService
    {
        readonly ILogger<MyHostedServiceA> _logger;

        public MyHostedServiceA(ILogger<MyHostedServiceA> logger)
        {
            _logger = logger;
            //Constructor’s parameters validations...       
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"MyHostedServiceA is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug($" MyHostedServiceA background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"MyHostedServiceA task doing background work.");

                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogDebug($"MyHostedServiceA background task is stopping.");
        }
    }
}
