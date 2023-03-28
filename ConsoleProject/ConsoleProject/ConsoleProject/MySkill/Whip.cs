using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Whip : AttackSkill
    {
        public Whip(int damage, int skillCount, int skillDuration, char entity, ConsoleColor entityColor) : base(damage, skillCount, skillDuration, entityColor)
        {
            _entity = entity;
        }


        public override void SetRange()
        {
            range.Clear();
            int offsetX = Game.Instance.Player.PosX;
            int offsetY = Game.Instance.Player.PosY;
            int[,] map = Game.Instance.map;

            for(int j = offsetY - 1; j < offsetY + 2; j++)
            {
                for (int i = offsetX + 2; i <= offsetX + 10; i++)
                {
                    if (map.GetLength(1) <= i || i < 1 || map.GetLength(0) <= j || j < 1)
                        continue;

                    range.Add(new Utility.Pair<int, int>(i, j));
                }

                for (int i = offsetX - 2; i >= offsetX - 10; i--)
                {
                    if (map.GetLength(1) <= i || i < 1 || map.GetLength(0) <= j || j < 1)
                        continue;

                    range.Add(new Utility.Pair<int, int>(i, j));
                }
            }
        }
    }
}
