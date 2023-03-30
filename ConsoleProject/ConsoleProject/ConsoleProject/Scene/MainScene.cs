using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleProject.Scene
{
    class MainScene : BaseScene
    {
        private int _count = 1;
        private DateTime _startTime;
        private Player _player;
        private int _spawnCount;

        public Player Player { get { return _player; } }

        public List<Enemy> enemies = new List<Enemy>();

        public int enemiesCount = 0;
        public TimeSpan time;

        public Enemy CloestEnemy
        {
            get
            {
                double distance = 10000;
                Enemy enemy = null;

                for (int i = 0; i < enemies.Count; i++)
                {
                    double dis = Math.Sqrt(Math.Pow(enemies[i].PosX - _player.PosX, 2) + Math.Pow(enemies[i].PosY - _player.PosY, 2));
                    if (distance > dis)
                    {
                        distance = dis;
                        enemy = enemies[i];
                    }
                }

                return enemy;
            }
        }

        public int SpawnCount
        {
            get 
            {
                if (Player.level % 3 == 0)
                {
                    _spawnCount--;
                    _spawnCount = Utility.MyUtility.Clamp(_spawnCount, 4, 10);
                }

                return _spawnCount; 
            }
        }

        // 나와 적
        public int[,] map = new int[Console.WindowHeight, Console.WindowWidth];

        // 스킬 데미지
        public int[,] attackMap = new int[Console.WindowHeight, Console.WindowWidth];

        // 스킬 문자
        public char[,] charMap = new char[Console.WindowHeight, Console.WindowWidth];

        private AbilityManager abilityManager;

        private Random random = new Random();


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

            enemies.Clear();
        }

        public override void Start()
        {
            enemiesCount = 0;

            _spawnCount = 10;

            _count = 1;

            _startTime = DateTime.Now;

            _player = new Player();

            abilityManager = new AbilityManager();

            // 플레이어 능력 초기화
            abilityManager.InitAbility();

            // 시작시 실행할 부분
            InitmapData();
        }

        public override void Update()
        {
            _count++;

            // CheckLevelUp(ref _count);

            // map초기화
            map = new int[Console.WindowHeight, Console.WindowWidth];

            ShowUI();

            // 일정시간마다 적 랜덤 생성
            SpawnEnemy(_count);

            // 플레이어 이동, 공격, 피격
            _player.Update(_count);

            // 적 이동, 공격, 피격
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(_count);
            }

            _player.HitCheck();

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

        private void ShowUI()
        {
            time = DateTime.Now - _startTime;
            string playerInfoText = $" HP({ _player.CurrentHp}/{ _player.maxHp})  {time.ToString(@"hh\:mm\:ss"),50}                                            Level : {_player.level}";

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

                if (playerInfoText.Length > i)
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
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    if (_player.PosX != j && _player.PosY != i)
                        Console.WriteLine(' ');
                    charMap[i, j] = ' ';
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        private void SpawnEnemy(int count)
        {
            if (count % SpawnCount == 0)
            {
                int randomPosX = random.Next(Console.WindowWidth);
                int randomPosY = random.Next(Console.WindowHeight);

                enemies.Add(new Enemy(randomPosX, randomPosY, 10));
            }
        }

        public void LevelUp()
        {
            _player.LevelUp();

            // 스킬 선택 창 보여줌(무한반복)  =>  선택하면 return해서 나올 수 있음
            abilityManager.ShowLevelUpUI();

            InitmapData();
        }

        private void InitmapData()
        {
            map = new int[Console.WindowHeight, Console.WindowWidth];
            attackMap = new int[Console.WindowHeight, Console.WindowWidth];
            charMap = new char[Console.WindowHeight, Console.WindowWidth];

            ShowBackground();
        }
    }
}
