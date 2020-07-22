using System;
using System.Threading;
using System.Diagnostics;


namespace Program
{
    class Program
{
    static void Main(string[] args)
    {
            var serverpath = (String)null;
        int on = 1;
            while (on == 1)
            {
                Console.Clear();
                Console.Write("Identify Server Process");
                var server = System.Diagnostics.Process.GetProcessesByName("notepad")[0];
                if (server != null)
                {
                    serverpath = server.MainModule.FileName;
                    Console.Write("Path set to var, waiting..");
                    
                    Console.Clear();
                    Console.Write(server);
                    Thread.Sleep(10000);
                    server.Kill();
                    Console.Write("attempted to kill server, waiting..");
                    Thread.Sleep(5000);
                }
                else
                { }
                Console.WriteLine("server = null");
            Process.Start(serverpath);
            }
        }

    }
}
