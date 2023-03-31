using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ConsoleProject.Scene
{
    class CreditScene : BaseScene
    {
        private string[] _creditString = {"Producer / Game Design : 제현준", "Main Programming : 제현준",
            "Artificial Intelligence Programming : 제현준", "Character Arts : 제현준", "Background Arts : 제현준",
            "Special Effects Art : 제현준", "User Interface Design : 제현준", "Scenario : 제현준",
            "Music : 없음", "Sound Effects : 없음", "Special Thanks : 경일 게임아카데미 교수님, 플밍 40기 모든 사람들"};

        public override void End()
        {
            Console.Clear();

            for (int i = 0; i < GameManager.ConsoleSizeHeight; i++)
            {
                for (int j = 0; j < GameManager.ConsoleSizeWidth; j++)
                {
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }

        public override void Start()
        {
            for(int i = 0; i < _creditString.Length; i++)
            {
                Console.SetCursorPosition(30, i + 5);
                Console.Write(_creditString[i]);
            }

            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 - 5, GameManager.ConsoleSizeHeight / 2 + 6);
            Console.WriteLine("돌아가기");

            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 - 7, GameManager.ConsoleSizeHeight / 2 + 6);
            Console.Write('▶');
        }

        public override void Update()
        {
            ConsoleKeyInfo cki;

            cki = Console.ReadKey(true);

            if (cki.Key == ConsoleKey.Spacebar)
            {
                SceneManager.Instance.ChangeScene(EScene.Intro);
            }
        }
    }
}
