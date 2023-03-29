using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    abstract class ActiveSkill : Skill
    {
        public int damage;
        public int skillCount;
        public int skillDuration;
        public bool isUsing;

        protected char _entity;
        protected ConsoleColor _entityColor;

        protected List<Utility.Pair<int, int>> range;

        public ActiveSkill(int damage, int skillCount, int skillDuration, ConsoleColor entityColor)
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
                Game.Instance.charMap[range[i].second, range[i].first] = _entity;
                Game.Instance.attackMap[range[i].second, range[i].first] = damage;
            }
            Console.ResetColor();
        }

        public virtual void UnShow()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            for (int i = 0; i < range.Count; i++)
            {
                Console.SetCursorPosition(range[i].first, range[i].second);
                Console.Write(' ');
                Game.Instance.charMap[range[i].second, range[i].first] = ' ';
                Game.Instance.attackMap[range[i].second, range[i].first] = 0;
            }
            Console.ResetColor();
        }

        public virtual void Use()
        {
            isUsing = true;
        }

        public virtual void Finish()
        {
            isUsing = false;
        }

        public abstract void SetRange();


        public abstract void Attack(int count);
    }
}
