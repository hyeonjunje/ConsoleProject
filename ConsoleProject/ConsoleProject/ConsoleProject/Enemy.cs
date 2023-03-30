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

            int targetPosX = GameManager.Instance.Player.PosX;
            int targetPosY = GameManager.Instance.Player.PosY;

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
            if (GameManager.Instance.map[goalPosY, goalPosX] == (int)EUnit.Enemy)
            {
                return;
            }
            PosX = goalPosX;
            PosY = goalPosY;
        }

        public override void Dead()
        {
            GameManager.Instance.Player.CurrentExp += 20;
            //GameManager.Instance.Player.CurrentExp += GameManager.Instance.Player.expAmount;

            ((Scene.MainScene)Scene.SceneManager.Instance.GetScene(Scene.EScene.Main)).enemiesCount++;

            Console.SetCursorPosition(PosX, PosY);
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            Console.Write(GameManager.Instance.charMap[PosY, PosX]);

            Console.ResetColor();
        }

        public override void HitCheck()
        {
            if (GameManager.Instance.attackMap[PosY, PosX] > 0)
            {
                CurrentHp -= GameManager.Instance.attackMap[PosY, PosX];
            }
        }

        public override void Update(int count)
        {
            ShowEntity();

            HitCheck();
        }
    }
}
