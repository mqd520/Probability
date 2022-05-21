using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Probability.Winform
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Init();
            Application.Run(new Form1());
            Exit();
        }

        static void Init()
        {
            int n = 182778378;
            string str = CommonTool.FormatNumber(n);

            SqlSugarTool.Init();
        }

        static void Exit()
        {
            // ...
        }
    }
}
