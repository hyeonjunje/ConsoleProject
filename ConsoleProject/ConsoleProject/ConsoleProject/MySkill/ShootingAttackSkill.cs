using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    abstract class ShootingAttackSkill : ActiveSkill
    {
        protected int posX;
        protected int posY;

        protected int dirX;
        protected int dirY;

        public ShootingAttackSkill(char shape, string name, int damage, int skillCount, int skillDuration, ConsoleColor entityColor) : base(shape, name, damage, skillCount, skillDuration, entityColor)
        {
        }

        public override void Attack(int count)
        {
            if (!isUsing && count % skillCount == 0)
            {
                SetRange();

                Aim();

                Use();
            }
            else if (isUsing && count % skillDuration == 0)
            {
                UnShow();

                Finish();
            }
            else if(isUsing)
            {
                Shoot(dirX, dirY);
            }
        }

        private void Aim()
        {
            Enemy enemy = Game.Instance.CloestEnemy;

            if (enemy == null)
            {
                dirX = 1;
                dirY = 0;
            }
            else if (Game.Instance.Player.PosX != enemy.PosX || Game.Instance.Player.PosY != enemy.PosY)
            {
                if (Game.Instance.Player.PosX < enemy.PosX)
                {
                    dirX = 1;
                }
                else if (Game.Instance.Player.PosX > enemy.PosX)
                {
                    dirX = -1;
                }
                else
                {
                    dirX = 0;
                }

                if (Game.Instance.Player.PosY < enemy.PosY)
                {
                    dirY = 1;
                }
                else if (Game.Instance.Player.PosY > enemy.PosY)
                {
                    dirY = -1;
                }
                else
                {
                    dirY = 0;
                }
            }
            else
            {
                dirX = 1;
                dirY = 0;
            }
        }

        private void Shoot(int x, int y)
        {
            UnShow();

            range.Clear();

            posX += x;
            posY += y;

            int radius = 4;
            int offsetX = posX;
            int offsetY = posY;
            int[,] map = Game.Instance.map;

            for (int i = offsetY - radius / 2 + y; i <= offsetY + radius / 2 + y; i++)
            {
                for (int j = offsetX - radius + x; j <= offsetX + radius + x; j++)
                {
                    if (map.GetLength(0) <= i || i < 1 || map.GetLength(1) <= j || j < 1)
                        continue;

                    range.Add(new Utility.Pair<int, int>(j, i));
                }
            }

            Show();
        }
    }
}
