using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    abstract class ItemSkill : Skill
    {
        protected ItemSkill(char shape, string name, int maxLevel, ConsoleColor entityColor) : base(shape, name, maxLevel, entityColor)
        {
            skillType = ESkillType.Item;
        }

        public abstract void Use();
    }
}
