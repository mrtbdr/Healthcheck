using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace LoadBalancer
{
    public class CpuStat
    {
        public double LoadPercentage { get; set; }
        public string Health { get; set; }
    }

    public class CpuHealthCheck
    {
        public static CpuStat DoHealthCheck()
        {
            double loadPercentage = CpuMetrics.CpuValues();


            CpuStat cpuStat = new CpuStat();

            if (loadPercentage < 80)
                cpuStat.Health = "Healthy";
            else if (loadPercentage >= 80 && loadPercentage < 90)
                cpuStat.Health = "Degraded";
            else
                cpuStat.Health = "Unhealthy";

            cpuStat.LoadPercentage = loadPercentage;


            return cpuStat;
           
        }

    }


    public class CpuMetrics
    {

        public static double CpuValues()
        {
            System.Console.WriteLine("\n" + "CPUVALUES METHODU BASLANGIC");
            CpuMetrics cpuMetrics = new CpuMetrics();
            var cpuLines = cpuMetrics.GetWmicOutput("CPU get Name,LoadPercentage /Value").Split("\n");

            var CpuUse = cpuLines[0].Split("=", StringSplitOptions.RemoveEmptyEntries)[1];
            var CpuName = cpuLines[1].Split("=", StringSplitOptions.RemoveEmptyEntries)[1];

            System.Console.WriteLine("\n" + "CPUVALUES METHODU SON");

            return Convert.ToDouble(CpuUse);
        }

        public string GetWmicOutput(string query, bool redirectStandardOutput = true)
        {
            System.Console.WriteLine("\n" + "GETWMIC METHODU BASLANGIC");

            var info = new ProcessStartInfo("wmic");
            info.Arguments = query;
            info.RedirectStandardOutput = redirectStandardOutput;
            var output = "";

            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
            }
            System.Console.WriteLine("\n" + "GETWMIC METHODU SON");

            return output.Trim();
        }


    }
}




