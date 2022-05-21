using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace Probability.Winform
{
    public class CommonTool
    {
        public static string FormatNumber(int number)
        {
            string result = "";

            string str = number.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                int len = str.Length;
                int step = len / 4 + (len % 4 == 0 ? 0 : 1);
                string[] arr = new string[step];
                for (int i = 0; i < step; i++)
                {
                    int index = len - ((i + 1) * 4);
                    if (index < 0)
                    {
                        index = 0;
                    }
                    int size = 4;
                    if (i == step - 1)
                    {
                        if (len % 4 != 0)
                        {
                            size = len - i * 4;
                        }
                    }
                    if (size < 0)
                    {
                        size = 0;
                    }

                    arr[step - 1 - i] = str.Substring(index, size);
                }

                result = arr.ConcatElement(",");
            }

            return result;
        }
    }
}
