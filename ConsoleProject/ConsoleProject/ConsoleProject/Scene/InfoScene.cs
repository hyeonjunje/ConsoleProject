using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.Scene
{
    class InfoScene : BaseScene
    {
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
            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 - 8, GameManager.ConsoleSizeHeight / 2 - 10);
            Console.Write("경일 서바이벌");
            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 - 30, GameManager.ConsoleSizeHeight / 2 - 9);
            Console.Write("이 게임은 다가오는 적으로부터 오랫동안 살아남는 게임입니다.");
            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 - 20, GameManager.ConsoleSizeHeight / 2 - 8);
            Console.Write("당신은 적을 쓰러뜨리며 성장할 수 있습니다.");
            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 - 21, GameManager.ConsoleSizeHeight / 2 - 7);
            Console.Write("성장할 때마다 새로운 능력을 얻을 수 있습니다.");
            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 - 27, GameManager.ConsoleSizeHeight / 2 - 6);
            Console.Write("여러 능력을 얻으면서 다가오는 적으로부터 맞서 싸우세요!!");

            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 - 12, GameManager.ConsoleSizeHeight / 2 - 2);
            Console.Write("이동 - W / A / S / D");
            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 - 8, GameManager.ConsoleSizeHeight / 2 - 1);
            Console.Write("공격 - 자동");

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
