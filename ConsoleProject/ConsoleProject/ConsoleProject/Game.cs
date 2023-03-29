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
        Player,  // 1
        Enemy,   // 2
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

        public Enemy CloestEnemy
        {
            get
            {
                double distance = 10000;
                Enemy enemy = null;

                for(int i = 0; i < enemies.Count; i++)
                {
                    double dis = Math.Sqrt(Math.Pow(enemies[i].PosX - _player.PosX, 2) + Math.Pow(enemies[i].PosY - _player.PosY, 2));
                    if(distance > dis)
                    {
                        distance = dis;
                        enemy = enemies[i];
                    }
                }

                return enemy;
            }
        }

        // 나와 적
        public int[,] map = new int[Console.WindowHeight, Console.WindowWidth];

        // 스킬 데미지
        public int[,] attackMap = new int[Console.WindowHeight, Console.WindowWidth];

        // 스킬 문자
        public char[,] charMap = new char[Console.WindowHeight, Console.WindowWidth];

        private Random random = new Random();

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
            int count = 1;

            while(true)
            {
                count++;

                // map초기화
                map = new int[Console.WindowHeight, Console.WindowWidth];

                ShowUI();

                // 일정시간마다 적 랜덤 생성
                SpawnEnemy(count);

                // 플레이어 이동, 공격, 피격
                _player.Update(count);

                // 적 이동, 공격, 피격
                foreach (Enemy enemy in enemies)
                {
                    enemy.Update(count);
                }

                _player.HitCheck();


                // 죽음 확인
                if (_player.isDead)
                {
                    break;
                }

                // 죽은 적이 있다면 enemies list 정리
                List<Enemy> temp = new List<Enemy>();
                foreach (Enemy enemy in enemies)
                {
                    if (!enemy.isDead)
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
                    charMap[i, j] = ' ';
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        private void SpawnEnemy(int count)
        {
            if(count % 10 == 0)
            {
                int randomPosX = random.Next(Console.WindowWidth);
                int randomPosY = random.Next(Console.WindowHeight);

                enemies.Add(new Enemy(randomPosX, randomPosY, 10));
            }
        }
    }
}
