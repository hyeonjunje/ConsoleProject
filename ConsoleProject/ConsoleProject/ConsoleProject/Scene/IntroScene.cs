using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ConsoleProject.Scene
{
    class IntroScene : BaseScene
    {
        private int _index = 0;
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;

                _index = Utility.MyUtility.Clamp(_index, 0, 3);
            }
        }

        public override void End()
        {
            Console.Clear();

            for(int i = 0; i < Console.WindowHeight; i++)
            {
                for(int j = 0; j < Console.WindowWidth; j++)
                {
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }

        public override void Start()
        {
            Index = 0;

            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 10);
            Console.Write("~~경일 서바이벌~~");

            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
            Console.WriteLine("Start");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 2);
            Console.WriteLine("Info");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 4);
            Console.WriteLine("Credit");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 6);
            Console.WriteLine("Exit");

            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2);
            Console.Write('▶');
        }

        public override void Update()
        {
            ConsoleKeyInfo cki;

            cki = Console.ReadKey(true);

            if (cki.Key == ConsoleKey.W)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + Index * 2);
                Console.Write(' ');

                Index--;
            }
            if (cki.Key == ConsoleKey.S)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + Index * 2);
                Console.Write(' ');

                Index++;
            }

            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + Index * 2);
            Console.Write('▶');

            Console.SetCursorPosition(0, 0);
            Console.Write(Index);

            if (cki.Key == ConsoleKey.Spacebar)
            {
                // 선택

                switch(Index)
                {
                    case 0:
                        SceneManager.Instance.ChangeScene(EScene.Main);
                        break;
                    case 1:
                        SceneManager.Instance.ChangeScene(EScene.Info);
                        break;
                    case 2:
                        SceneManager.Instance.ChangeScene(EScene.Credit);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
