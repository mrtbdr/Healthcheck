using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheck.HealthChecks
{
    public class SystemCpuHealthCheck : IHealthCheck

    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var client = CpuMetrics.DoIt();

            double doubleClient = Convert.ToDouble(client);


            var status = HealthStatus.Healthy;

            status = HealthStatus.Healthy;
            if (doubleClient < 10)
            {
                return Task.FromResult(HealthCheckResult.Healthy($"The respond time is good({doubleClient }.ms")
                    );
            }
            else if (doubleClient >= 10 && doubleClient < 20)
            {
                return Task.FromResult(HealthCheckResult.Degraded($"The respond time is a bit slow({doubleClient}.ms")
                    );
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy($"The respond time is a unacceptable({doubleClient}.ms")
                    );
            }
        }
    }
}
    
