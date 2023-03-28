using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Fireball : AttackMeleeSkill
    {
        public Fireball(int damage, int skillCount, int skillDuration, char entity, ConsoleColor entityColor) : base(damage, skillCount, skillDuration, entityColor)
        {
            _entity = entity;

            _unit = EUnit.Fireball;
        }

        public override void SetRange()
        {
            range.Clear();

            int radius = 2;

            int offsetX = Game.Instance.Player.PosX;
            int offsetY = Game.Instance.Player.PosY;
            int[,] map = Game.Instance.map;

            for(int i = offsetY - radius; i <= offsetY + radius; i++)
            {
                for(int j = offsetX - radius; j <= offsetX + radius; j++)
                {
                    if (i == offsetY && j == offsetX)
                        continue;

                    if (map.GetLength(1) <= j || j < 1 || map.GetLength(0) <= i || i < 1)
                        continue;

                    range.Add(new Utility.Pair<int, int>(j, i));
                }
            }
        }
    }
}
