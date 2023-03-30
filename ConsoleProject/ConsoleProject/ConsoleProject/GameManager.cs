using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleProject.Scene;

namespace ConsoleProject
{
    enum EUnit
    {
        // 렌더링되는 순서
        None,    // 0
        Player,  // 1
        Enemy,   // 2
    }

    class GameManager
    {
        #region 싱글톤
        private static GameManager _instance = null;
        public static GameManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }
        #endregion

        public Player Player => ((MainScene)SceneManager.Instance.GetScene(EScene.Main)).Player;

        public List<Enemy> enemies = ((MainScene)SceneManager.Instance.GetScene(EScene.Main)).enemies;

        public Enemy CloestEnemy => ((MainScene)SceneManager.Instance.GetScene(EScene.Main)).CloestEnemy;

        // 나와 적
        public int[,] map => ((MainScene)SceneManager.Instance.GetScene(EScene.Main)).map;

        // 스킬 데미지
        public int[,] attackMap => ((MainScene)SceneManager.Instance.GetScene(EScene.Main)).attackMap;

        // 스킬 문자
        public char[,] charMap => ((MainScene)SceneManager.Instance.GetScene(EScene.Main)).charMap;

        public GameManager()
        {
            // 콘솔창 이름
            Console.Title = "경일 서바이벌";

            // 콘솔 커서 안보이게 하기
            Console.CursorVisible = false;
        }

        public void Start()
        {
            SceneManager.Instance.ChangeScene(EScene.Intro);
        }


        public void Update()
        {
            while(true)
            {
                SceneManager.Instance.CurrentScene.Update();
            }
        }
    }
}
