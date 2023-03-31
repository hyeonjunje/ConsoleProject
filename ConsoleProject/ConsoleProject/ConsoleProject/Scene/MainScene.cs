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

        private bool _isEnemyEvent = false;

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
        public int[,] map = new int[GameManager.ConsoleSizeHeight, GameManager.ConsoleSizeWidth];

        // 스킬 데미지
        public int[,] attackMap = new int[GameManager.ConsoleSizeHeight, GameManager.ConsoleSizeWidth];

        // 스킬 문자
        public char[,] charMap = new char[GameManager.ConsoleSizeHeight, GameManager.ConsoleSizeWidth];

        private AbilityManager abilityManager;

        private Random random = new Random();


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
            enemies.Clear();
        }

        public override void Start()
        {
            enemiesCount = 0;

            _spawnCount = 10;

            _count = 1;

            _isEnemyEvent = false;

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
            map = new int[GameManager.ConsoleSizeHeight, GameManager.ConsoleSizeWidth];

            ShowUI();

            // 일정시간마다 적 랜덤 생성
            SpawnEnemy(_count);

            // 플레이어 이동, 공격, 피격
            _player.Update(_count);

            // 적 이동, 공격, 피격
            foreach (Enemy enemy in enemies)
            {
                if(!enemy.isDead)
                    enemy.Update(_count);
            }

            _player.HitCheck();

            // 죽은 적이 있다면 enemies list 정리
            List<Enemy> temp = new List<Enemy>();

            for(int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].isDead)
                {
                    temp.Add(enemies[i]);
                }
                else
                {
                    enemies[i] = null;
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
            for (int i = 0; i < GameManager.ConsoleSizeWidth; i++)
            {
                if (i <= GameManager.ConsoleSizeWidth * _player.CurrentHp / _player.maxHp)
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
            for (int i = 0; i < GameManager.ConsoleSizeHeight; i++)
            {
                for (int j = 0; j < GameManager.ConsoleSizeWidth; j++)
                {
                    Console.Write(' ');
                    charMap[i, j] = ' ';
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        private void SpawnEnemy(int count)
        {
            // 1분마다 사방에서 몬스터 덮쳐오기
            if (time.ToString(@"\:ss") == ":59" && !_isEnemyEvent)
            {
                for(int i = Utility.MyUtility.ConsoleYMin; i < GameManager.ConsoleSizeHeight; i+=3)
                {
                    enemies.Add(new Enemy(0, i, 5 + _player.level));
                    enemies.Add(new Enemy(GameManager.ConsoleSizeWidth - 1, i, 5 + _player.level));
                }

                for (int i = 1; i < GameManager.ConsoleSizeWidth; i+=3)
                {
                    enemies.Add(new Enemy(i, Utility.MyUtility.ConsoleYMin, 5 + _player.level));
                    enemies.Add(new Enemy(i, GameManager.ConsoleSizeHeight, 5 + _player.level));
                }

                _isEnemyEvent = true;
            }
            else if(time.ToString(@"\:ss") == ":00")
            {
                _isEnemyEvent = false;
            }


            if (count % SpawnCount == 0)
            {
                int randomPosX = random.Next(GameManager.ConsoleSizeWidth);
                int randomPosY = random.Next(GameManager.ConsoleSizeHeight);

                enemies.Add(new Enemy(randomPosX, randomPosY, 5 + _player.level));

                if(random.Next(10) != 0 && _player.level >= 5)
                {
                    enemies.Add(new Enemy(randomPosX, randomPosY, 5 + _player.level, 0));
                }
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
            map = new int[GameManager.ConsoleSizeHeight, GameManager.ConsoleSizeWidth];
            attackMap = new int[GameManager.ConsoleSizeHeight, GameManager.ConsoleSizeWidth];
            charMap = new char[GameManager.ConsoleSizeHeight, GameManager.ConsoleSizeWidth];

            ShowBackground();
        }
    }
}
