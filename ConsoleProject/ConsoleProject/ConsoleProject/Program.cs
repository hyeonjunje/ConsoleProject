﻿using System;
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
            GameManager.Instance.Start();

            GameManager.Instance.Update();
        }
    }
}
