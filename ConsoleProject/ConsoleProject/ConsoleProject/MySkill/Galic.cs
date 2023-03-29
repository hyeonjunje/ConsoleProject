using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Galic : MeleeAttackSkill
    {
        public Galic(char shape, string name, int damage, int skillCount, int skillDuration, ConsoleColor entityColor) : base(shape, name, damage, skillCount, skillDuration, entityColor)
        {
        }

        public override void LevelUp()
        {
            throw new NotImplementedException();
        }

        public override void SetRange()
        {
            range.Clear();
            int radius = 4;
            int offsetX = Game.Instance.Player.PosX;
            int offsetY = Game.Instance.Player.PosY;
            int[,] map = Game.Instance.map;

            for(int i = offsetY - radius / 2; i <= offsetY + radius / 2; i++)
            {
                for(int j = offsetX - radius; j <= offsetX + radius; j++)
                {
                    if (map.GetLength(0) <= i || i < 1 || map.GetLength(1) <= j || j < 1)
                        continue;

                    range.Add(new Utility.Pair<int, int>(j, i));
                }
            }
        }
    }
}
