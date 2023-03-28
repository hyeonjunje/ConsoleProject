using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    abstract class AttackLongLangeSkill : Skill
    {
        public int damage;
        public int skillCount;
        public int skillDuration;
        public bool isUsing;

        protected char _entity;
        protected EUnit _unit;
        protected ConsoleColor _entityColor;

        protected List<Utility.Pair<int, int>> range;

        public AttackLongLangeSkill(int damage, int skillCount, int skillDuration, ConsoleColor entityColor)
        {
            this.damage = damage;
            this.skillCount = skillCount;
            this.skillDuration = skillDuration;
            isUsing = false;
            _entityColor = entityColor;

            range = new List<Utility.Pair<int, int>>();
        }

        public virtual void Show()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = _entityColor;
            for (int i = 0; i < range.Count; i++)
            {
                Console.SetCursorPosition(range[i].first, range[i].second);
                Console.Write(_entity);
            }
            Console.ResetColor();
        }

        public virtual void UnShow()
        {
            isUsing = false;

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            for (int i = 0; i < range.Count; i++)
            {
                Console.SetCursorPosition(range[i].first, range[i].second);
                Console.Write(' ');

                if (Game.Instance.map[range[i].second, range[i].first] == (int)_unit)
                    Game.Instance.map[range[i].second, range[i].first] = (int)EUnit.None;
            }
            Console.ResetColor();
        }

        public virtual void Use()
        {
            isUsing = true;

            for (int i = 0; i < range.Count; i++)
            {
                Game.Instance.map[range[i].second, range[i].first] = (int)_unit;
            }
        }

        public abstract void SetRange();

        // x, y 방향
        public virtual void Shoot(int x, int y)
        {
            int[,] map = Game.Instance.map;

            for (int i = 0; i < range.Count; i++)
            {
                if (map.GetLength(1) <= range[i].first + x || range[i].first + x < 1 || map.GetLength(0) <= range[i].second || range[i].second < 1)
                    continue;

                range[i].first += x;
                range[i].second += y;
            }
        }
    }
}
