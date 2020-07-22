using System;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Program
{
    class Program
{

        static class PathServ
        {
            public static string pathserv2;
        }
        static void Main(string[] args)
    {
            
            var serverpath = (String)null;
            string direcname;
            string pathcomplete;
            int on = 1;
            while (on == 1)
            {
                Console.Clear();
                Console.Write("Identify Server Process.");
                Thread.Sleep(1000);
                Console.Clear();
                Console.Write("Identify Server Process..");
                Thread.Sleep(1000);
                Console.Clear();
                foreach (Process pr in Process.GetProcesses())
                {
                    try
                    {
                        Console.WriteLine("App Name: {0}, Process Name: {1}", Path.GetFileName(pr.MainModule.FileName), pr.ProcessName);
                    }
                    catch { }
                }
                
                Thread.Sleep(2000);
                try
                {
                    var server = System.Diagnostics.Process.GetProcessesByName("7DaysToDieServer")[0];
                    if (server != null)
                    {
                        serverpath = server.MainModule.FileName;
                        Console.Clear();
                        Console.Write("Path set to var, waiting..");

                        Console.Clear();
                        Console.Write(server);
                        Thread.Sleep(5000);
                        server.Kill();
                        Console.Clear();
                        Console.Write("attempted to kill server, waiting..");

                        Thread.Sleep(10000);
                    }
                    else
                    { }
                    Console.Clear();
                    Console.Write(serverpath);
                    
                    Thread.Sleep(5000);
                    Console.WriteLine("server = null");
                    Console.Clear();
                    direcname = Path.GetDirectoryName(serverpath);
                    Console.WriteLine(direcname);
                    try
                    {
                        //Set the current directory.
                        Directory.SetCurrentDirectory(direcname);
                    }
                    catch (DirectoryNotFoundException e)
                    {
                        Console.WriteLine("The specified directory does not exist. {0}", e);
                    }
                    pathcomplete = direcname + "\\startdedicated.bat";
                    Thread.Sleep(5000);
                    Console.Clear();
                    Console.WriteLine(pathcomplete);
                    PathServ.pathserv2 = pathcomplete;
                    Thread.Sleep(5000);
                    Console.Clear();
                    
                    Process.Start(pathcomplete); 
                    Thread.Sleep(10000);
                }
                catch (System.IndexOutOfRangeException e)
                {
                    Console.Clear();
                    Console.WriteLine("Server does not appear to be running.");
                    try
                    {
                        Process.Start(PathServ.pathserv2);
                    }
                    catch { }
                    Console.WriteLine("This would be a good place to try and start the server using a defined path.");
                    Console.WriteLine("Please launch with server running. Trying again in 4 seconds.");
                    Thread.Sleep(4000);

                }
                
            }
        }

    }

   
}
