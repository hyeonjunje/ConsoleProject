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

        private const int PaddingX = 5;

        public List<Skill> allSkills;

        private Skill[] selectedSkills = new Skill[3];

        private int _selectedNumber = 0;

        private int SelectedNumber
        {
            get { return _selectedNumber; }
            set
            {
                _selectedNumber = value;

                _selectedNumber = Utility.MyUtility.Clamp(_selectedNumber, 0, 2);
            }
        }

        public AbilityManager()
        {
            allSkills = new List<Skill>();

            allSkills.Add(new Whip('∫', "채찍", 5, 20, 20, ConsoleColor.Magenta));
            allSkills.Add(new Rasor('=', "레이저", 2, 50, 20, ConsoleColor.Blue));
            allSkills.Add(new Fireball('@', "파이어볼", 3, 30, 100, ConsoleColor.DarkRed));
            allSkills.Add(new Electronic('/', "번개!", 1, 20, 100, ConsoleColor.Yellow));
            allSkills.Add(new Rest('♨', "휴식~", ConsoleColor.Cyan));
        }

        public void ShowLevelUpUI()
        {
            // 화면 보여줌
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 5; i < Console.WindowHeight - 5; i++)
            {
                for (int j = 10; j < Console.WindowWidth - 10; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }

            SelectedNumber = 0;

            // 스킬 무작위 선택
            SelectRandomSkill();

            ShowAbility(Console.WindowWidth / 2 - AbilityCardWidth / 2 - PaddingX - AbilityCardWidth, Console.WindowHeight / 2 - AbilityCardHeight / 2, 0);
            ShowAbility(Console.WindowWidth / 2 - AbilityCardWidth / 2, Console.WindowHeight / 2 - AbilityCardHeight / 2, 1);
            ShowAbility(Console.WindowWidth / 2 - AbilityCardWidth / 2 + PaddingX + AbilityCardWidth, Console.WindowHeight / 2 - AbilityCardHeight / 2, 2);

            Console.SetCursorPosition(Console.WindowWidth / 2 - 2, (Console.WindowHeight / 2) - (AbilityCardHeight / 2) - 6);
            Console.Write("레벨업!!!");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 15, (Console.WindowHeight / 2) - (AbilityCardHeight / 2) - 5);
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
            allSkills = allSkills.OrderBy(_ => rand.Next()).ToList();


            for (int i = 0; i < selectedSkills.Length; i++)
            {
                selectedSkills[i] = allSkills[i];
            }
        }

        private void ShowAbility(int cursorX, int cursorY, int index)
        {
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
                Console.SetCursorPosition(Console.WindowWidth / 2 + (PaddingX + AbilityCardWidth) * (SelectedNumber - 1), Console.WindowHeight / 2 + AbilityCardHeight / 2 + 2);
                Console.Write(' ');

                SelectedNumber--;
            }
            if (cki.Key == ConsoleKey.D)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 + (PaddingX + AbilityCardWidth) * (SelectedNumber - 1), Console.WindowHeight / 2 + AbilityCardHeight / 2 + 2);
                Console.Write(' ');

                SelectedNumber++;
            }
            if(cki.Key == ConsoleKey.Spacebar)
            {
                // 선택
                Game.Instance.Player.AddSkill(allSkills[SelectedNumber]);

                return false;
            }


            Console.SetCursorPosition(Console.WindowWidth / 2 + (PaddingX + AbilityCardWidth) * (SelectedNumber - 1), Console.WindowHeight / 2 + AbilityCardHeight / 2 + 2);
            Console.Write('▲');

            return true;
        }
    }
}
