using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Whip : MeleeAttackSkill
    {
        int rangeX;
        int rangeY;

        public Whip(char shape, string name, int damage, int skillCount, int skillDuration, ConsoleColor entityColor) : base(shape, name, damage, skillCount, skillDuration, entityColor)
        {
            rangeX = 3;
            rangeY = 3;
        }

        public override void LevelUp()
        {
            if(level % 2 == 1)
            {
                rangeX++;
            }
            rangeY++;
        }

        public override void SetRange()
        {
            range.Clear();
            int offsetX = Game.Instance.Player.PosX;
            int offsetY = Game.Instance.Player.PosY;
            int[,] map = Game.Instance.map;

            for(int j = offsetY + 1; j < offsetY + 4; j++)
            {
                for (int i = offsetX + -1; i <= offsetX + 1; i++)
                {
                    if (map.GetLength(1) <= i || i < 1 || map.GetLength(0) <= j || j < 1)
                        continue;

                    range.Add(new Utility.Pair<int, int>(i, j));
                }
            }

            for (int i = offsetX -1; i <= offsetX + 1; i++)
            {
                for(int j = offsetY + 1; j < offsetY + 4; j++)
                {
                    if (map.GetLength(1) <= i || i < 1 || map.GetLength(0) <= j || j < 1)
                        continue;

                    range.Add(new Utility.Pair<int, int>(i, j));
                }

                for(int j = offsetY - 1; j > offsetY - 4; j--)
                {
                    if (map.GetLength(1) <= i || i < 1 || map.GetLength(0) <= j || j < 1)
                        continue;

                    range.Add(new Utility.Pair<int, int>(i, j));
                }
            }
        }
    }
}
