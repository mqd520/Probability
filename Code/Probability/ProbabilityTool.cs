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
        public static readonly int RandomMinVal = 10000;
        public static readonly int RandomMaxVal = 100000;
        public static readonly int DecaimalPointSize = 4;


        private static int MakeRandom()
        {
            int code = Guid.NewGuid().GetHashCode();
            Random ran = new Random(code);
            int val = ran.Next(RandomMinVal, RandomMaxVal);
            return val;
        }

        public static bool IsHit(double probability)
        {
            double probability2 = Math.Round(probability, DecaimalPointSize);
            if (probability > 0.5)
            {
                probability2 = Math.Round(1 - probability2, DecaimalPointSize);
            }
            int ran = MakeRandom();
            int val = (int)(probability2 * RandomMaxVal);
            double opportunity = ((double)(RandomMaxVal - RandomMinVal)) / val;
            int opportunity2 = (int)Math.Round(opportunity);
            int remainder = (ran - RandomMinVal) % opportunity2;
            if (probability < 0.5)
            {
                return remainder == 0;
            }
            else
            {
                return !(remainder == 0);
            }
        }
    }
}
