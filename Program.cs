using System;
using System.Threading;



namespace LoadBalancer
{
    class Program
    {
        static void Main(string[] args)
        {
                while(true)
                {
                    var cs = "Host=localhost;Username=postgres;Password=2365;Database=mealordering";
                    System.Console.WriteLine(SqlConnectionHealthCheck.ConnectDB("PostgreSQL",cs));


                    try
                    {
                       
                       
                        /*
                        var cpuStat = CpuHealthCheck.DoHealthCheck();
                        System.Console.WriteLine("CPU Percentage:" + cpuStat.LoadPercentage + " Health:" + cpuStat.Health);

                        var memoryStat = MemoryMetricsClient.MemoryHealthCheck();
                        System.Console.WriteLine("Memory Percentage:" + memoryStat.LoadPercentage + " Health:" + memoryStat.Health);
                        */
                        //DiscMetrics.DiscUsagePercentage();



                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.ToString());
                    }

                    Thread.Sleep(10000);
                }
                
         }
        

            
    }
}
