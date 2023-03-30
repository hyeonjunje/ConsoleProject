using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Electronic : MeleeAttackSkill
    {
        int width = 3;

        public Electronic(char shape, string name, int damage, int skillCount, int skillDuration, ConsoleColor entityColor) : base(shape, name, damage, skillCount, skillDuration, entityColor)
        {
            width = 3;
        }

        public override void LevelUp()
        {
            width++;
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
