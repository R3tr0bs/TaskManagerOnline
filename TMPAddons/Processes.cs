using System;
using System.Diagnostics;
namespace TMPAddons
{
    /// <summary>
    /// this class will handle the processes.
    /// GetAllRunningProcesses() - will return all the running processes in the pc. returns string.
    /// KillProcessById(id) - will kill the process with the id id. returns bool.
    /// KillProcessesByName(name) - will kill the processes with the same name as name. returns bool.
    /// </summary>
    public static class Processes
    {
        public static string GetAllRunningProcesses()
        {
            //this function will return all of the running processes.
            //returns "process \nproccess..."
            //returns string
            string ret = "";
            try
            {
                Process[] all = Process.GetProcesses();
                foreach (Process prc in all)
                {
                    ret += prc.ProcessName + ":" + prc.Id.ToString() + "\n";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return ret;
        }
        public static bool KillProcessById(int id)
        {
            //this function will kill the proccess with the id given
            //returns true if everything went well,
            //otherwise returns false and write the exception(console)
            bool ret = false;
            try
            {
                Process k = Process.GetProcessById(id);
                k.Kill();
                ret = true;
            }
            catch (Exception err)
            {
                ret = false;
                Console.WriteLine(err);
            }
            return ret;
        }
        public static bool KillProcessesByName(string name)
        {
            //this function will kill all the proccesses with the same name given
            //returns true if everything went well,
            //otherwise returns false and write the exception(console)
            bool ret = false;
            try
            {
                Process[] k = Process.GetProcessesByName(name);
                foreach (Process p in k)
                {
                    p.Kill();
                }
                ret = true;
            }
            catch (Exception err)
            {
                ret = false;
                Console.WriteLine(err);
            }
            return ret;
        }
    }
}
