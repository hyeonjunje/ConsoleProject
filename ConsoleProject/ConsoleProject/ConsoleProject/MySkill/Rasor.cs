using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Rasor : MeleeAttackSkill
    {
        public Rasor(char shape, string name, int damage, int skillCount, int skillDuration, ConsoleColor entityColor) : base(shape, name, damage, skillCount, skillDuration, entityColor)
        {
        }

        public override void LevelUp()
        {
            throw new NotImplementedException();
        }

        public override void SetRange()
        {
            range.Clear();

            int offsetX = Game.Instance.Player.PosX;
            int offsetY = Game.Instance.Player.PosY;
            int[,] map = Game.Instance.map;

            if(Game.Instance.Player.isRight)
            {
                for(int i = offsetY - 1; i < offsetY + 2; i++)
                {
                    for(int j = offsetX + 1; j <= offsetX + 100; j++)
                    {
                        if (map.GetLength(1) <= j || j < 1 || map.GetLength(0) <= i || i < 1)
                            continue;

                        range.Add(new Utility.Pair<int, int>(j, i));
                    }
                }
            }
            else
            {
                for (int i = offsetY - 1; i < offsetY + 2; i++)
                {
                    for (int j = offsetX - 1; j >= offsetX - 100; j--)
                    {
                        if (map.GetLength(1) <= j || j < 1 || map.GetLength(0) <= i || i < 1)
                            continue;

                        range.Add(new Utility.Pair<int, int>(j, i));
                    }
                }
            }
        }
    }
}
