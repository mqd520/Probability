using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using Common;
using Probability.Service._00_Def;
using Probability.Service._01_Config;

namespace Probability.Service._03_Service
{
    public class MyService
    {
        private static Timer _t = new Timer();

        public static void Start()
        {
            _t.Interval = MyConfig.TaskInterval;
            _t.Elapsed += _t_Elapsed;
            _t.AutoReset = false;

            ConsoleHelper.WriteLine(
                ELogCategory.Info,
                string.Format("Probability Task Start"),
                true
            );
            _t.Start();
        }

        private static void _t_Elapsed(object sender, ElapsedEventArgs e)
        {
            _t.Stop();

            try
            {
                ConsoleHelper.WriteLine(
                    ELogCategory.Info,
                    string.Format("Start Test Probability"),
                    true
                );
                //Console.WriteLine(string.Format("Progress: 0/{0}", MyConfig.TaskSize));
                int n = 0;
                var probability = GetRandomProbability();
                long timestamp = DateTime.Now.GetMillTimeStamp();
                for (int i = 0; i < MyConfig.TaskSize; i++)
                {
                    var b = ProbabilityTool.IsHit(probability);
                    if (b)
                    {
                        n++;
                    }
                    //Console.SetCursorPosition(0, Console.CursorTop - 1);
                    //Console.WriteLine(string.Format("Progress: {0}/{0}", i + 1, MyConfig.TaskSize));
                }
                long timestamp2 = DateTime.Now.GetMillTimeStamp();
                ConsoleHelper.WriteLine(
                    ELogCategory.Info,
                    string.Format("Test Probability Over"),
                    true
                );

                #region Wirte Db
                double probability2 = (double)n / MyConfig.TaskSize;
                var entity = new ProbabilityRecords
                {
                    ActualProbability = probability2,
                    Count = MyConfig.TaskSize,
                    EndTime = timestamp2,
                    ExpectedProbability = probability,
                    ProbabilityDifference = probability2 - probability,
                    StartTime = timestamp
                };
                ConsoleHelper.WriteLine(
                    ELogCategory.Info,
                    string.Format("Start Write Db"),
                    true
                );
                int n2 = 0;
                bool bException = false;
                try
                {
                    using (var db = SqlSugarTool.GetDb())
                    {
                        n2 = db.Insertable<ProbabilityRecords>(entity).ExecuteCommand();
                    }
                }
                catch (Exception ex)
                {
                    bException = true;
                    ConsoleHelper.WriteLine(
                        ELogCategory.Fatal,
                        string.Format("Write Db Exception: {0}", ex.Message),
                        true,
                        e: ex
                    );
                }
                if (!bException)
                {
                    ConsoleHelper.WriteLine(
                        n2 > 0 ? ELogCategory.Info : ELogCategory.Warn,
                        string.Format("Write Db Over: {0}", n2 > 0 ? "Success" : "Failed"),
                        true
                    );
                }
                #endregion
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteLine(
                    ELogCategory.Fatal,
                    string.Format("MyService.TaskStart Exception: {0}", ex.Message),
                    true,
                    e: ex
                );
            }
            finally
            {
                Console.WriteLine("");
                _t.Start();
            }
        }

        private static double GetRandomProbability()
        {
            int code = Guid.NewGuid().GetHashCode();
            Random ran = new Random(code);
            int val = ran.Next(1000, 9999);
            int val2 = val - 1000;
            double d = (double)val2 / 10000;
            return d;
        }
    }
}
