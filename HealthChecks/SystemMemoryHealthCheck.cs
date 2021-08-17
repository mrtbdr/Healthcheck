using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheck.HealthChecks
{
    public class SystemMemoryHealthCheck : IHealthCheck

    {
        public  Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var client = new MemoryMetricsClient();
            var metrics = client.GetMetrics();
            var percentUsed = 100 * metrics.Used / metrics.Total;

            var status = HealthStatus.Healthy;
            if (percentUsed < 80)
            {
                status= HealthStatus.Healthy;
                return Task.FromResult(HealthCheckResult.Healthy ($"Kullanılan ram yüzdesi({percentUsed}- sağlıklı)"));

            }
            else if (percentUsed >= 80 && percentUsed < 90 )
            {
                status = HealthStatus.Degraded;
                return Task.FromResult(HealthCheckResult.Degraded($"Kullanılan ram yüzdesi({percentUsed})sağlıksız"));
            }
            else
            {
                status = HealthStatus.Unhealthy;
                return Task.FromResult(HealthCheckResult.Unhealthy($"Kullanılan ram yüzdesi({percentUsed}) fazla"));
            }

            var data = new Dictionary<string, object>();
            data.Add("Total", metrics.Total);
            data.Add("Used", metrics.Used);
            data.Add("Free", metrics.Free);

            var result = new HealthCheckResult(status, null, null, data);

            return  Task.FromResult(result);
        }
    }
}
