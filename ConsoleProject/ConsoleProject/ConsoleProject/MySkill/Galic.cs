using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Galic : AttackSkill
    {
        public Galic(int damage, int skillCount, int skillDuration, char entity, ConsoleColor entityColor) : base(damage, skillCount, skillDuration, entityColor)
        {
            _entity = entity;
        }

        public override void SetRange()
        {
            range.Clear();
            int radius = 4;
            int offsetX = Game.Instance.Player.PosX;
            int offsetY = Game.Instance.Player.PosY;
            int[,] map = Game.Instance.map;

            for(int i = offsetY - radius; i <= offsetY + radius; i++)
            {
                for(int j = offsetX - radius; j <= offsetX + radius; j++)
                {
                    if (map.GetLength(0) <= i || i < 1 || map.GetLength(1) <= j || j < 1)
                        continue;

                    if (i == offsetY && j == offsetX)
                        continue;

                    range.Add(new Utility.Pair<int, int>(j, i));
                }
            }
        }
    }
}
