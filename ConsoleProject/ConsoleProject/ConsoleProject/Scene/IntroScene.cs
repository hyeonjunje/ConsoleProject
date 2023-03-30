using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.Scene
{
    class IntroScene : BaseScene
    {
        public override void End()
        {

        }

        public override void Start()
        {
            Console.WriteLine("~~경일 서바이벌~~");

            Console.WriteLine("Start");
            Console.WriteLine("Info");
            Console.WriteLine("Credit");
            Console.WriteLine("Exit");
        }

        public override void Update()
        {

        }

        private void ShowBackground()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
