using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine(Console.WindowHeight + "  " + Console.WindowWidth);
            // Console.WriteLine(Console.BufferHeight + " " + Console.BufferWidth);
            GameManager.Instance.Start();

            GameManager.Instance.Update();
        }
    }
}
