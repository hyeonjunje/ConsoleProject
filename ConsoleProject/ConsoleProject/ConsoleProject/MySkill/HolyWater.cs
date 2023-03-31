using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class HolyWater : MeleeAttackSkill
    {
        int width = 3;

        public HolyWater(char shape, string name, int maxLevel, ConsoleColor entityColor, int damage, int skillCount, int skillDuration) : base(shape, name, maxLevel, entityColor, damage, skillCount, skillDuration)
        {
            width = 3;
            explanation = "랜덤한 곳에 성수를 뿌립니다.";
            levelUpexplanation = "범위, 데미지 상승";
        }

        public override void LevelUp()
        {
            width++;
            damage += 1;
        }

        public override void SetRange()
        {
            range.Clear();

            Random random = new Random();

            int offsetY = random.Next(Console.WindowHeight - 1);
            int offsetX = random.Next(Console.WindowWidth - 1);
            int[,] map = GameManager.Instance.map;

            for (int i = offsetY - width; i <= offsetY + width; i++)
            {
                for (int j = offsetX - width; j <= offsetX + width; j++)
                {
                    if (map.GetLength(1) <= j || j < 0 || map.GetLength(0) <= i || i < Utility.MyUtility.ConsoleYMin)
                        continue;

                    range.Add(new Utility.Pair<int, int>(j, i));
                }
            }
        }
    }
}
