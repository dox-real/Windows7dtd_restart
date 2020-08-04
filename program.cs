using System;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Transactions;


// This code is currently working
// Generates VBS script if Configs.Broadcaston = 1, should also generate batch at this time
// VBS Timer needs refinement
// Broadcast off further testing needed
// Player restart input needs to be expanded and tested, more regex needed - going to try and include minutes
// Other than that I think its golden and almost ready for running on server.
// Code needs cleaning - extra thread.sleeps removed, extra Console.Writes removed


namespace Program
{
    class Program


{

        public class VBS
        {
            public static string line01;
            public static string line02;
            public static string line03;
            public static string line04;
            public static string line05;
            public static string line06;
            public static string line07;
            public static string line08;
            public static string line09;
            public static string line10;
        }

        public class Math
        {
            public static int telbroadcast01;
            public static int onehour = 3600000;
        }
        public class Configs
        {
            public static string broadcastson = "0";
            public static string broadcastfile = @"broadcast.vbs";
            public static string telport;
            public static string telpass;
            public static string serverdirectory;
            public static string cfgserverpath;
            public static int cfgrestarttimer;
            public static int convertedtimer1;
            public static int finalizedtimer1;
        }

        private static class PathServ
        {
            public static string pathserv2;
        }

        public class Counter
        {
            public static int restartcounter = 0;
        }
        static void Main(string[] args)
    {
            
            var serverpath = (String)null;
            var mepath = (String)null;
            
            string cfgfile = @"config.txt";


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
                    // loading values from config - so can add serverpath later
                    // need to make it find cfg
                    Configs.broadcastson = File.ReadLines(cfgfile).Skip(4).Take(1).First();
                    Configs.telpass = File.ReadLines(cfgfile).Skip(2).Take(1).First();
                    Configs.telport = File.ReadLines(cfgfile).Skip(1).Take(1).First();
                    Configs.finalizedtimer1 = int.Parse(File.ReadLines(cfgfile).Skip(3).Take(1).First());
                try
                {
                    Configs.cfgserverpath = File.ReadLines(cfgfile).Skip(5).Take(1).First();
                    Configs.serverdirectory = File.ReadLines(cfgfile).Skip(6).Take(1).First();
                    Math.telbroadcast01 = int.Parse(File.ReadLines(cfgfile).Skip(3).Take(1).First());
                    VBS.line01 = File.ReadLines(cfgfile).Skip(7).Take(1).First();
                    VBS.line02 = File.ReadLines(cfgfile).Skip(8).Take(1).First();
                    VBS.line03 = File.ReadLines(cfgfile).Skip(9).Take(1).First();
                    VBS.line04 = File.ReadLines(cfgfile).Skip(10).Take(1).First();
                    VBS.line05 = File.ReadLines(cfgfile).Skip(11).Take(1).First();
                    VBS.line06 = File.ReadLines(cfgfile).Skip(12).Take(1).First();
                    VBS.line07 = File.ReadLines(cfgfile).Skip(13).Take(1).First();
                    VBS.line08 = File.ReadLines(cfgfile).Skip(14).Take(1).First();
                    VBS.line09 = File.ReadLines(cfgfile).Skip(15).Take(1).First();
                    VBS.line09 = File.ReadLines(cfgfile).Skip(16).Take(1).First();
                    if (Configs.broadcastson == "Y")
                    {
                        
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Server path not found.");
                }
                  //  Thread.Sleep(3000);
                   }
                else
                    {
                    Console.WriteLine("Config not found.");
               //     Thread.Sleep(2000);
                    Console.WriteLine("generating..");
                Console.Clear();
                Console.WriteLine("Enable broadcasts? (Y/n): ");
                string broadcaston = Console.ReadLine();
                if (broadcaston == "Y")
                {
                    Configs.broadcastson = "1";
                    Console.WriteLine("Please enter telnet port for broadcasts: ");
                    Configs.telport = Console.ReadLine();
                    Console.WriteLine("Please input telnet password: ");
                    Configs.telpass = Console.ReadLine();
                }
                    Console.Clear();
                    Console.WriteLine(Environment.NewLine + "Please input desired restart timer. Ex: 8h, 6h, 1h etc");
                    string userinputrestart = Console.ReadLine();
                    string numberOnly = Regex.Replace(userinputrestart, "[^0-9.]", "");
                try
                    {
                    Configs.convertedtimer1 = Int32.Parse(numberOnly);
                    }
                catch (FormatException)
                {
                    Console.WriteLine("Input not valid. Defaulting.");
                    Configs.convertedtimer1 = 8;
                    
                }
                    Console.Clear();
                    Console.WriteLine("Timer successfully set.");
                    // Convert hour to milliseconds
                    Configs.finalizedtimer1 = Configs.convertedtimer1 * 60 * 60 * 1000;
                    // Make it not break on one hour timer
                    Configs.finalizedtimer1 = Configs.finalizedtimer1 + 1;
                    // Remove one hour
                    Math.telbroadcast01 = Configs.finalizedtimer1 - Math.onehour;
                var me = System.Diagnostics.Process.GetProcessesByName("7dtd_SimpleStart")[0];
                mepath = me.MainModule.FileName;
                medirpath = Path.GetDirectoryName(mepath);
                string[] configlines = { "Matt's 7DTD Simple Start", Configs.telport, Configs.telpass, Configs.finalizedtimer1.ToString(), Configs.broadcastson.ToString() };
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(medirpath, "config" +
                    ".txt")))
                {
                    foreach (string line in configlines)
                        outputFile.WriteLine(line);
                }
                // need to generate bat file with telnet localhost and Configs.telport
                // Generate vb file here using saved port, password, and timers. VBS waits time minus hour then sends broadcast, then waits hour, then ends

                //                  using FileStream fs = File.Create(cfgfile);
                //                  Byte[] title = new UTF8Encoding(true).GetBytes(set OBJECT = WScript.CreateObject("WScript.Shell")
                //WScript.sleep 50
                //OBJECT.SendKeys "+ Configs.telpass +"{ENTER}"
                //WScript.sleep " + Math.telbroadcast01 + "
                //OBJECT.SendKeys "say ""Server Reboot in 1 hour.""{ENTER}"
                //WScript.sleep 180000
                //OBJECT.SendKeys "say ""Server Reboot in 30 minutes.""{ENTER}"
                //WScript.sleep etc
                //OBJECT.SendKeys "say ""Server Reboot in 10 minutes.""{ENTER}"
                //WScript.sleep 10000
                //OBJECT.SendKeys "exit{ENTER}"
                //WScript.sleep 50);
                //    fs.Write(title, 0, title.Length);
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
                
                Thread.Sleep(10000);
                try
                {
                    var server = System.Diagnostics.Process.GetProcessesByName("7DaysToDieServer")[0];
                    if (server != null)
                    {
                        serverpath = server.MainModule.FileName;
                        direcname = Path.GetDirectoryName(serverpath);
                        pathcomplete = direcname + "\\startdedicated.bat";
                        Configs.serverdirectory = direcname;
                        Configs.cfgserverpath = pathcomplete;
                        var me2 = System.Diagnostics.Process.GetProcessesByName("7dtd_SimpleStart")[0];
                        mepath = me2.MainModule.FileName;
                        medirpath = Path.GetDirectoryName(mepath);
                        string[] configlines = { "Matt's 7DTD Simple Start", Configs.telport, Configs.telpass, Configs.finalizedtimer1.ToString(), Configs.broadcastson.ToString(), Configs.cfgserverpath, Configs.serverdirectory, "set OBJECT = WScript.CreateObject(\"WScript.Shell\")", "WScript.sleep 50",
                        "OBJECT.SendKeys \"" + Configs.telpass + "{ENTER}\"" , "WScript.Sleep " + Math.telbroadcast01, "OBJECT.SendKeys \"say \"\"Server Reboot in 1 hour.\"\"{ENTER}\"", "WScript.Sleep 3000000", "OBJECT.SendKeys \"say \"\"Server Reboot in 30 minutes.\"\"{ENTER}\"", "WScript.Sleep 1200000", "OBJECT.SendKeys \"say \"\"Server Reboot in 10 minute.\"\"{ENTER}\"", "Object.SendKeys \"exit{ENTER}\"", };
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(medirpath, "config" +
                            ".txt")))
                        {
                            foreach (string line in configlines)
                                outputFile.WriteLine(line);
                        }
                        

                        try
                        {
                            Configs.cfgserverpath = File.ReadLines(cfgfile).Skip(5).Take(1).First();
                            Configs.serverdirectory = File.ReadLines(cfgfile).Skip(6).Take(1).First();
                            Math.telbroadcast01 = int.Parse(File.ReadLines(cfgfile).Skip(3).Take(1).First());
                            VBS.line01 = File.ReadLines(cfgfile).Skip(7).Take(1).First();
                            VBS.line02 = File.ReadLines(cfgfile).Skip(8).Take(1).First();
                            VBS.line03 = File.ReadLines(cfgfile).Skip(9).Take(1).First();
                            VBS.line04 = File.ReadLines(cfgfile).Skip(10).Take(1).First();
                            VBS.line05 = File.ReadLines(cfgfile).Skip(11).Take(1).First();
                            VBS.line06 = File.ReadLines(cfgfile).Skip(12).Take(1).First();
                            VBS.line07 = File.ReadLines(cfgfile).Skip(13).Take(1).First();
                            VBS.line08 = File.ReadLines(cfgfile).Skip(14).Take(1).First();
                            VBS.line09 = File.ReadLines(cfgfile).Skip(15).Take(1).First();
                            VBS.line09 = File.ReadLines(cfgfile).Skip(16).Take(1).First();
                            if (Configs.broadcastson == "Y")
                            {

                            }
                        }
                        catch (Exception Ex)
                        {
                            Console.WriteLine("Server path not found.");
                        }


                        string[] vbslines = { VBS.line01, VBS.line02, VBS.line03, VBS.line04, VBS.line05, VBS.line06, VBS.line07, VBS.line08, VBS.line09, VBS.line10, };
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(medirpath, "broadcast" +
                            ".vbs")))
                        {
                            foreach (string line in vbslines)
                                outputFile.WriteLine(line);
                        }
                        

                        Console.Clear();
                        Console.Write(server +Environment.NewLine);
                        
                      Thread.Sleep(7000);
                        Console.Clear();
                        Console.Write("Restart in " + Configs.finalizedtimer1 + " milliseconds." + Environment.NewLine);
                        Console.Write("Restart count: " + Counter.restartcounter);
                        Directory.SetCurrentDirectory(medirpath);
                        if (Configs.broadcastson == "Y")
                        {
                            Process.Start(pathtelnet);
                            Thread.Sleep(7000);
                        }
                        
                        Thread.Sleep(Configs.finalizedtimer1);
                        server.Kill();
                        Console.Clear();
                        Console.Write("attempted to kill server, waiting..");

                   //     Thread.Sleep(10000);
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
                    Thread.Sleep(20000);
                    PathServ.pathserv2 = pathcomplete;
                    
                    Console.Clear();
                    
                    Process.Start(pathcomplete);
                    Configs.cfgserverpath = pathcomplete;
                 //   Thread.Sleep(30000);
                    Console.Clear();

                    Counter.restartcounter = Counter.restartcounter + 1;
                  //  Thread.Sleep(10000);
                }
                catch (System.IndexOutOfRangeException e)
                {
                    Console.Clear();

                    try
                    {
                        Directory.SetCurrentDirectory(Configs.serverdirectory);
                        Process.Start(Configs.cfgserverpath);
                    }
                    catch
                    {
                        Console.WriteLine(" For first time use please start server. This program uses the process to track the working directory.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                    }

                }
                
            }
        }

    }
