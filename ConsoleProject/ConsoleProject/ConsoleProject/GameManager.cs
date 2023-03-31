using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleProject.Scene;
using System.Runtime.InteropServices;

namespace ConsoleProject
{
    enum EUnit
    {
        // 렌더링되는 순서
        None,    // 0
        Player,  // 1
        Enemy,   // 2
    }

    class GameManager : Singleton<GameManager>
    {
        public Player Player => ((MainScene)SceneManager.Instance.GetScene(EScene.Main)).Player;

        public List<Enemy> enemies = ((MainScene)SceneManager.Instance.GetScene(EScene.Main)).enemies;

        public Enemy CloestEnemy => ((MainScene)SceneManager.Instance.GetScene(EScene.Main)).CloestEnemy;

        // 나와 적
        public int[,] map => ((MainScene)SceneManager.Instance.GetScene(EScene.Main)).map;

        // 스킬 데미지
        public int[,] attackMap => ((MainScene)SceneManager.Instance.GetScene(EScene.Main)).attackMap;

        // 스킬 문자
        public char[,] charMap => ((MainScene)SceneManager.Instance.GetScene(EScene.Main)).charMap;

        public const int ConsoleSizeWidth = 120;
        public const int ConsoleSizeHeight = 30;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        public GameManager()
        {
            // 콘솔창 이름
            Console.Title = "경일 서바이벌";

            // 콘솔 커서 안보이게 하기
            Console.CursorVisible = false;

            Console.SetWindowSize(120, 30);
            Console.SetBufferSize(120, 30);

            //Console.SetWindowSize()
        }

        public void Start()
        {
            // 시작은 인트로 씬으로 설정
            SceneManager.Instance.ChangeScene(EScene.Intro);
        }


        public void Update()
        {
            while(true)
            {
                // 각 씬들은 상태로 구현되어 있어 현재씬을 가져와 계속 업데이트
                SceneManager.Instance.CurrentScene.Update();
            }
        }
    }
}
