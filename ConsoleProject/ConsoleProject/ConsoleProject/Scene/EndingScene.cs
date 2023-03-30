using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.Scene
{
    class EndingScene : BaseScene
    {
        public override void End()
        {
            Console.Clear();

            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }

        public override void Start()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 11, Console.WindowHeight / 2 - 10);
            Console.Write("당신은 죽어버렸습니다.");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 9);
            Console.Write($"살아남은 시간 : {((Scene.MainScene)Scene.SceneManager.Instance.GetScene(Scene.EScene.Main)).enemiesCount}");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 8);
            Console.Write($"쓰러뜨린 적 : {((Scene.MainScene)Scene.SceneManager.Instance.GetScene(Scene.EScene.Main)).time.ToString(@"hh\:mm\:ss")}");

            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 6);
            Console.WriteLine("처음으로");

            Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 + 6);
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
