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

        protected List<Utility.Pair<int, int>> range;

        public ActiveSkill(char shape, string name, int damage, int skillCount, int skillDuration, ConsoleColor entityColor) : base(shape, name, entityColor)
        {
            this.damage = damage;
            this.skillCount = skillCount;
            this.skillDuration = skillDuration;
            isUsing = false;

            range = new List<Utility.Pair<int, int>>();

            skillType = ESkillType.Active;
        }

        public void Show()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = entityColor;
            for (int i = 0; i < range.Count; i++)
            {
                Console.SetCursorPosition(range[i].first, range[i].second);
                Console.Write(shape);
                GameManager.Instance.charMap[range[i].second, range[i].first] = shape;
                GameManager.Instance.attackMap[range[i].second, range[i].first] = damage;
            }
            Console.ResetColor();
        }

        public void UnShow()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            for (int i = 0; i < range.Count; i++)
            {
                Console.SetCursorPosition(range[i].first, range[i].second);
                Console.Write(' ');
                GameManager.Instance.charMap[range[i].second, range[i].first] = ' ';
                GameManager.Instance.attackMap[range[i].second, range[i].first] = 0;
            }
            Console.ResetColor();
        }

        public void Use()
        {
            isUsing = true;
        }

        public void Finish()
        {
            isUsing = false;
        }

        public abstract void SetRange();

        public abstract void Attack(int count);

        public abstract void LevelUp();
    }
}
