using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace Probability.Service._01_Config
{
    public static class MyConfig
    {
        static MyConfig()
        {
            var nvc = ConfigurationManager.AppSettings;

            {
                int n = 0;
                bool b = int.TryParse(nvc["TaskInterval"], out n);
                if (b)
                {
                    TaskInterval = n;
                }
            }

            {
                int n = 0;
                bool b = int.TryParse(nvc["TaskSize"], out n);
                if (b)
                {
                    TaskSize = n;
                }
            }
        }

        public static int TaskInterval { get; private set; } = 10;

        public static int TaskSize { get; private set; } = 100;
    }
}
