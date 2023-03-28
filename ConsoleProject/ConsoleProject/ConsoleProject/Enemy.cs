using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    class Enemy : Entity
    {
        private int _moveCount = 0;

        public Enemy(int x, int y, int maxHp)
        {
            PosX = x;
            PosY = y;

            this.maxHp = maxHp;

            CurrentHp = maxHp;

            _entity = '■';
            _unit = EUnit.Enemy;
            _entityColor = ConsoleColor.Red;

            isDead = false;
        }


        protected override void Move()
        {
            if (_moveCount >= 1)
            {
                _moveCount = 0;
                return;
            }
            _moveCount++;

            int targetPosX = Game.Instance.Player.PosX;
            int targetPosY = Game.Instance.Player.PosY;

            int goalPosX = PosX;
            int goalPosY = PosY;

            if (targetPosX > PosX)
            {
                goalPosX++;
            }

            if(targetPosX < PosX)
            {
                goalPosX--;
            }

            if(targetPosY > PosY)
            {
                goalPosY++;
            }

            if(targetPosY < PosY)
            {
                goalPosY--;
            }

            // 이동하려는 곳에 이미 다른 적이 있다면 이동 불가
            if (Game.Instance.map[goalPosY, goalPosX] == (int)EUnit.Enemy)
            {
                return;
            }
            PosX = goalPosX;
            PosY = goalPosY;
        }

        public override void Dead()
        {
            
        }

        public override void HitCheck()
        {
            if (Game.Instance.map[PosY, PosX] >= (int)EUnit.Galic && Game.Instance.map[PosY, PosX] < (int)EUnit.Player)
            {
                CurrentHp = 0;
                // CurrentHp--;
            }
        }
    }
}
