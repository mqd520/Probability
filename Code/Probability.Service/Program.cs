using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Common;
using Probability.Service._03_Service;

namespace Probability.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ConsoleHelper.WriteLine(
                ELogCategory.Info,
                string.Format("Probability Service Start"),
                true
            );
            SqlSugarTool.Init();
            MyService.Start();

            while (true)
            {
                string line = Console.ReadLine();
                if (line.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else
                {
                    ConsoleHelper.WriteLine(
                        ELogCategory.Warn,
                        string.Format("Invalid Exit Command"),
                        true
                    );
                }
            }
        }
    }
}
