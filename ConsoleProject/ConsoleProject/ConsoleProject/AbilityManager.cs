using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleProject.MySkill;
using System.Runtime.InteropServices;


namespace ConsoleProject
{
    class AbilityManager
    {
        private const int AbilityCardWidth = 15;
        private const int AbilityCardHeight = 12;

        private const int AbilityCardOffsetX = -8;

        private const int PaddingX = 5;

        public List<Skill> allSkills;

        private List<Skill> selectedSkills = new List<Skill>();

        private int _selectedNumber = 0;

        private int SelectedNumber
        {
            get { return _selectedNumber; }
            set
            {
                _selectedNumber = value;

                _selectedNumber = Utility.MyUtility.Clamp(_selectedNumber, 0, selectedSkills.Count - 1);
            }
        }

        public AbilityManager()
        {
            allSkills = new List<Skill>();

            allSkills.Add(new Whip('∫', "채찍", 5, ConsoleColor.Magenta,  5, 20, 20));
            allSkills.Add(new Rasor('=', "레이저", 5, ConsoleColor.DarkBlue, 2, 50, 20));
            allSkills.Add(new Fireball('@', "파이어볼", 5, ConsoleColor.DarkRed, 3, 30, 100));
            allSkills.Add(new HolyWater('#', "성수", 5, ConsoleColor.Blue, 1, 20, 100));
            allSkills.Add(new Rest('♨', "휴식~", 99, ConsoleColor.Cyan));
            allSkills.Add(new Speed('》', "이속증가", 2, ConsoleColor.Green));
            allSkills.Add(new ExpAdder('△', "exp량 증가", 2, ConsoleColor.Green));
            allSkills.Add(new Cross('†', "십자가", 5, ConsoleColor.Yellow, 5, 50, 1));
            allSkills.Add(new Lightning('/', "번개!!", 5, ConsoleColor.DarkYellow, 10, 40, 5));
        }

        public void InitAbility()
        {
            GameManager.Instance.Player.AddSkill(allSkills[0]);
            //GameManager.Instance.Player.AddSkill(allSkills[allSkills.Count - 1]);
        }

        public void ShowLevelUpUI()
        {
            // 화면 보여줌
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 5; i < GameManager.ConsoleSizeHeight - 5; i++)
            {
                for (int j = 10; j < GameManager.ConsoleSizeWidth - 4; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }

            SelectedNumber = 0;

            // 스킬 무작위 선택
            SelectRandomSkill();

            for(int i = 0; i < selectedSkills.Count; i++)
            {
                ShowAbility(GameManager.ConsoleSizeWidth / 2 - AbilityCardWidth / 2 + (PaddingX + AbilityCardWidth) * (i -  1), GameManager.ConsoleSizeHeight / 2 - AbilityCardHeight / 2, i);
            }

            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 - 2, (GameManager.ConsoleSizeHeight / 2) - (AbilityCardHeight / 2) - 6);
            Console.Write("레벨업!!!");
            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 - 15, (GameManager.ConsoleSizeHeight / 2) - (AbilityCardHeight / 2) - 5);
            Console.Write("A, D  :   선택,      SpaceBar   :    결정");
            while(true)
            {
                if (!SelectInput())
                {
                    Console.Clear();
                    return;
                }
            }
        }

        public void SelectRandomSkill()
        {
            Random rand = new Random();

            selectedSkills = new List<Skill>();
            List<Skill> temp = new List<Skill>();

            for(int i = 0; i < allSkills.Count; i++)
            {
                if(allSkills[i].level <= allSkills[i].maxLevel)
                {
                    temp.Add(allSkills[i]);
                }
            }
            allSkills = temp;

            allSkills = allSkills.OrderBy(_ => rand.Next()).ToList();


            for (int i = 0; i < 3; i++)
            {
                if(i < allSkills.Count)
                {
                    selectedSkills.Add(allSkills[i]);
                }
            }
        }

        private void ShowAbility(int cursorX, int cursorY, int index)
        {
            cursorX += AbilityCardOffsetX;

            Skill selectedSkill = selectedSkills[index];

            Console.SetCursorPosition(cursorX, cursorY);
            for(int i = 0; i <= AbilityCardWidth; i++)
            {
                Console.Write("=");
            }

            for(int i = 0; i < AbilityCardHeight - 2; i++)
            {
                cursorY++;
                Console.SetCursorPosition(cursorX, cursorY);
                Console.Write("|");

                Console.SetCursorPosition(cursorX + AbilityCardWidth, cursorY);
                Console.Write("|");

                // 이미지
                if (i == 3)
                {
                    Console.SetCursorPosition(cursorX + AbilityCardWidth / 2, cursorY);
                    Console.ForegroundColor = selectedSkill.entityColor;
                    Console.Write(selectedSkill.shape);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // 이름
                if (i == 5)
                {
                    Console.SetCursorPosition(cursorX + AbilityCardWidth / 2 - 3, cursorY);
                    Console.Write(selectedSkill.name);
                }

                // 설명
                if (i == 8)
                {
                    Console.SetCursorPosition(cursorX + AbilityCardWidth / 2 - 3, cursorY);
                    Console.Write($"레벨 : {selectedSkill.level}");
                }
            }

            Console.SetCursorPosition(cursorX, cursorY + 1);
            for (int i = 0; i <= AbilityCardWidth; i++)
            {
                Console.Write("=");
            }
        }


        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);


        private bool SelectInput()
        {
            ConsoleKeyInfo cki;

            cki = Console.ReadKey(true);

            if (cki.Key == ConsoleKey.A)
            {
                Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 + (PaddingX + AbilityCardWidth) * (SelectedNumber - 1) + AbilityCardOffsetX, GameManager.ConsoleSizeHeight / 2 + AbilityCardHeight / 2 + 2);
                Console.Write(' ');

                SelectedNumber--;
            }
            if (cki.Key == ConsoleKey.D)
            {
                Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 + (PaddingX + AbilityCardWidth) * (SelectedNumber - 1) + AbilityCardOffsetX, GameManager.ConsoleSizeHeight / 2 + AbilityCardHeight / 2 + 2);
                Console.Write(' ');

                SelectedNumber++;
            }
            if(cki.Key == ConsoleKey.Spacebar)
            {
                // 선택
                GameManager.Instance.Player.AddSkill(allSkills[SelectedNumber]);

                return false;
            }


            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 + (PaddingX + AbilityCardWidth) * (SelectedNumber - 1) + AbilityCardOffsetX, GameManager.ConsoleSizeHeight / 2 + AbilityCardHeight / 2 + 2);
            Console.Write('▲');

            // 스킬 설명창
            for (int i = GameManager.ConsoleSizeWidth / 2 + 22; i < GameManager.ConsoleSizeWidth - 6; i++)
            {
                Console.SetCursorPosition(i, GameManager.ConsoleSizeHeight / 2 - 5);
                Console.Write(' ');

                Console.SetCursorPosition(i, GameManager.ConsoleSizeHeight / 2 - 4);
                Console.Write(' ');
            }    


            Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 + 22, GameManager.ConsoleSizeHeight / 2 - 5);
            Console.Write(selectedSkills[SelectedNumber].explanation);

            if(selectedSkills[SelectedNumber].level >= 1)
            {
                Console.SetCursorPosition(GameManager.ConsoleSizeWidth / 2 + 22, GameManager.ConsoleSizeHeight / 2 - 4);
                Console.Write(selectedSkills[SelectedNumber].levelUpexplanation);
            }



            return true;
        }
    }
}
