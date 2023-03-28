using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleProject
{
    enum EUnit
    {
        // 렌더링되는 순서
        None,    // 0
        Galic,   // 1
        Whip,    // 2
        Rasor,   // 3
        Fireball,
        Player,  // 4
        Enemy,   // 5
    }
    class Game
    {
        #region 싱글톤
        private static Game _instance = null;
        public static Game Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Game();
                }
                return _instance;
            }
        }
        #endregion

        private Player _player;
        public Player Player { get { return _player; } }

        public List<Enemy> enemies = new List<Enemy>();

        public int[,] map = new int[Console.WindowHeight, Console.WindowWidth];

        private Random random = new Random();


        // 적은 -1, 플레이어는 1, 무기는 2
        // public int[,] map = new int[Console.WindowHeight, Console.WindowWidth];

        private DateTime startTime;

        public Game()
        {
            // 콘솔창 이름
            Console.Title = "경일 서바이벌";
            // 콘솔 커서 안보이게 하기
            Console.CursorVisible = false;

            _player = new Player();
        }

        public void StartGame()
        {
            startTime = DateTime.Now;

            // 시작시 실행할 부분
            ShowBackground();

            // 계속 실행할 부분
            Update();
        }

        private void Update()
        {
            int enemySpawnCount = 0;

            while(true)
            {
                // map초기화
                ShowUI();

                // 일정시간마다 적 랜덤 생성
                if(enemySpawnCount++ == 50)
                {
                    enemySpawnCount = 0;
                    SpawnEnemy();
                }

                // 플레이어 보여줌
                _player.ShowEntity();

                // 적 보여줌
                foreach (Enemy enemy in enemies)
                {
                    enemy.ShowEntity();
                }

                // 플레이어 공격
                _player.Attack();

                // 플레이어 맞은지 확인
                _player.HitCheck();

                if(_player.isDead)
                {
                    break;
                }

                // 적 맞은지 확인
                foreach (Enemy enemy in enemies)
                {
                    enemy.HitCheck();
                }

                // 죽은 적이 있다면 enemies list 정리
                List<Enemy> temp = new List<Enemy>(); ;
                foreach(Enemy enemy in enemies)
                {
                    if(!enemy.isDead)
                    {
                        temp.Add(enemy);
                    }
                }
                enemies = temp;

                // 30프레임
                Thread.Sleep(33);
            }
        }

        private void ShowUI()
        {
            TimeSpan time = DateTime.Now - startTime;
            string playerInfoText = $" HP({ _player.CurrentHp}/{ _player.maxHp})  {time.ToString(@"hh\:mm\:ss"), 50}                                            Level : {_player.level}";

            // Hp Bar   
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                if (i <= Console.WindowWidth * _player.CurrentHp / _player.maxHp)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                if(playerInfoText.Length > i)
                {
                    Console.Write(playerInfoText[i]);
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.ResetColor();
        }

        private void ShowBackground()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            for(int i = 0; i < Console.WindowHeight; i++)
            {
                for(int j = 0; j < Console.WindowWidth; j++)
                {
                    if(_player.PosX != j && _player.PosY != i)
                        Console.WriteLine(' ');
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        private void SpawnEnemy()
        {
            int randomPosX = random.Next(Console.WindowWidth);
            int randomPosY = random.Next(Console.WindowHeight);

            enemies.Add(new Enemy(randomPosX, randomPosY, 10));
        }
    }
}
