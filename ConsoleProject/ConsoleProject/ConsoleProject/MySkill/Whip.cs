using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Whip : AttackMeleeSkill
    {
        public Whip(int damage, int skillCount, int skillDuration, char entity, ConsoleColor entityColor) : base(damage, skillCount, skillDuration, entityColor)
        {
            _entity = entity;

            _unit = EUnit.Whip;
        }


        public override void SetRange()
        {
            range.Clear();
            int offsetX = Game.Instance.Player.PosX;
            int offsetY = Game.Instance.Player.PosY;
            int[,] map = Game.Instance.map;

            for(int i = offsetX - 1; i <= offsetX + 1; i++)
            {
                for(int j = offsetY + 1; j <= offsetY + 4; j++)
                {
                    if (map.GetLength(0) <= j || j < 1 || map.GetLength(1) <= i || i < 0)
                        continue;

                    range.Add(new Utility.Pair<int, int>(i, j));
                }

                for(int j = offsetY - 1; j >= offsetY - 4; j--)
                {
                    if (map.GetLength(0) <= j || j < 1 || map.GetLength(1) <= i || i < 0)
                        continue;

                    range.Add(new Utility.Pair<int, int>(i, j));
                }
            }
        }
    }
}
