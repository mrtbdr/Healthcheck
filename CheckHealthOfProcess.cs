using System;
using System.Diagnostics;


namespace LoadBalancer
{ 
    public class CheckHealthOfProcess
    {

        public static bool IsAppResponding(string processName)
        {
            Process myProcess = FindSpecificProcess(processName);

            if (myProcess == null)
                throw new InvalidProgramException("There is no application that's name is: " + processName);
            else
                return myProcess.Responding;
        }

        public static Process FindSpecificProcess(string processName)
        {
            Process[] processesList = GetProcesses();

            foreach (Process process in processesList)
                if (String.Equals(processName, process.ProcessName))
                    return process;

            return null;
        }

        public static void PrintAllProcess()
        {
            Process[] processes = GetProcesses();

            foreach (Process process in processes)
                Console.WriteLine("Process Name:" + process.ProcessName + " Process Stat:" + process.Responding);
        }

        public static Process[] GetProcesses() { return Process.GetProcesses(); }
    }
}