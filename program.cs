using System;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Text;

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
            var mepath = (String)null;
            int configstart = 1;
            string cfgfile = @"config.txt";
            string cfgfile2 = @"config.txt";
            string linetoWrite = null;
            string direcname;
            string medirpath;
            string pathcomplete;
            string pathtelnet = "cfg/telnet.bat";
            int on = 1;
            int restarttimer = 28800000;

            Console.WriteLine(File.Exists(cfgfile) ? "File exists." : "File does not exist.");
            if (File.Exists(cfgfile))
            {
                Console.WriteLine("Config found.");
            }
            else
            {
                Console.WriteLine("Config not found.");
                Thread.Sleep(2000);
                Console.WriteLine("generating..");
                using FileStream fs = File.Create(cfgfile);
                Byte[] title = new UTF8Encoding(true).GetBytes("Dox's Windows Simple Restart." + Environment.NewLine);
                fs.Write(title, 0, title.Length);

            }
            while (on == 1)
            {
                var me = System.Diagnostics.Process.GetProcessesByName("7dtd_SimpleStart")[0];
                mepath = me.MainModule.FileName;
                medirpath = Path.GetDirectoryName(mepath);


                Console.Clear();
                try
                {
                    using (StreamReader sr = File.OpenText(cfgfile))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                            Console.WriteLine(s);
                    }
                }

                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }
                
                Thread.Sleep(4000);
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
                        using FileStream fs = File.OpenWrite(cfgfile);
                        Byte[] title = new UTF8Encoding(true).GetBytes("Dox's Windows Simple Start" + Environment.NewLine + "Serverpath:" + serverpath + Environment.NewLine + "RestartTimer(Milliseconds): " + restarttimer +Environment.NewLine);
                        fs.Write(title, 0, title.Length);
                        Console.Clear();
                        Console.Write("Path set to var, waiting..");

                        Console.Clear();
                        Console.Write(server +Environment.NewLine);
                        Console.Write("Restart in 5 seconds.");
                        Thread.Sleep(5000);
                        Directory.SetCurrentDirectory(medirpath);
                        Process.Start(pathtelnet);
                        Thread.Sleep(28800000);
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
                    
                    Console.Clear();
                    Console.WriteLine(pathcomplete);
                    Console.WriteLine("Server located.");
                    Thread.Sleep(2000);
                    PathServ.pathserv2 = pathcomplete;
                    
                    Console.Clear();
                    
                    Process.Start(pathcomplete);
                    Thread.Sleep(10000);
                }
                catch (System.IndexOutOfRangeException e)
                {
                    Console.Clear();
                    
                    try
                    {
                        Process.Start(PathServ.pathserv2);
                    }
                    catch { }
                    Console.WriteLine(" For first time use please start server. This program uses the process to track the working directory.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();

                }
                
            }
        }

    }

   
}

