using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class MyUtility
    {
        public const int ConsoleYMin = 2;

        public static int Clamp(int value, int min, int max)
        {
            if (value <= min)
                return min;

            if (value >= max)
                return max;

            return value;
        }
    }

    public class Pair<T, U>
    {
        public T first;
        public U second;

        public Pair(T first, U second)
        {
            this.first = first;
            this.second = second;
        }
    }
}
