using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    abstract class Entity
    {
        public int maxHp = 100;
        public bool isDead = false;

        private int _currentHp;

        private int _posX;
        private int _posY;

        private EUnit prevUnit = EUnit.None;

        protected char _entity;
        protected EUnit _unit;
        protected ConsoleColor _entityColor;

        public int PosX
        {
            get { return _posX; }
            set
            {
                _posX = value;
                _posX = Utility.MyUtility.Clamp(_posX, 0, Console.WindowWidth - 1);
            }
        }
        public int PosY
        {
            get { return _posY; }
            set
            {
                _posY = value;
                _posY = Utility.MyUtility.Clamp(_posY, 1, Console.WindowHeight - 1);
            }
        }

        public int CurrentHp
        {
            get { return _currentHp; }
            set
            {
                _currentHp = value;
                _currentHp = Utility.MyUtility.Clamp(_currentHp, 0, maxHp);

                if(_currentHp <= 0)
                {
                    isDead = true;
                    Dead();
                }
            }
        }

        public virtual void ShowEntity()
        {
            Console.SetCursorPosition(PosX, PosY);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write(' ');
            Game.Instance.map[PosY, PosX] = (int)EUnit.None;

            Move();

            Console.SetCursorPosition(PosX, PosY);
            Console.ForegroundColor = _entityColor;
            Console.Write(_entity);
            Game.Instance.map[PosY, PosX] = (int)_unit;
            Console.ResetColor();
        }


        protected abstract void Move();

        public abstract void Dead();

        public abstract void HitCheck();
    }
}
