using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probability
{
    /// <summary>
    /// Probability Tool
    /// </summary>
    public class ProbabilityTool
    {
        public static readonly int RandomStsrtNum = 10000;
        public static readonly int RandomLength = 100000;
        public static readonly int DecaimalPointSize = 4;


        private static int MakeRandom(int min, int max)
        {
            int code = Guid.NewGuid().GetHashCode();
            Random ran = new Random(code);
            int val = ran.Next(min, max);
            return val;
        }

        public static bool IsHit(double probability)
        {
            if (probability >= 1)
            {
                return true;
            }
            else if (probability <= 0)
            {
                return false;
            }
            else
            {
                double probability2 = Math.Round(probability, DecaimalPointSize);
                int num = Convert.ToInt32(RandomLength * probability2);
                int random = MakeRandom(RandomStsrtNum, RandomStsrtNum + RandomLength - 1);
                if (random <= num)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
