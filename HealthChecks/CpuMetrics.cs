using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheck.HealthChecks
{
    public class CpuMetrics
    {
        

        public static string DoIt()
        {
                CpuMetrics cpuMetrics = new CpuMetrics();
                var cpuLines = cpuMetrics.GetWmicOutput("CPU get Name,LoadPercentage /Value").Split("\n");

                var CpuUse = cpuLines[0].Split("=", StringSplitOptions.RemoveEmptyEntries)[1];
                var CpuName = cpuLines[1].Split("=", StringSplitOptions.RemoveEmptyEntries)[1];

                return CpuUse;
        }

        public string GetWmicOutput(string query, bool redirectStandardOutput = true)
        {

            var info = new ProcessStartInfo("wmic");
            info.Arguments = query;
            info.RedirectStandardOutput = redirectStandardOutput;
            var output = "";
            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
            }
            return output.Trim();
        }


    }
}





        

    

