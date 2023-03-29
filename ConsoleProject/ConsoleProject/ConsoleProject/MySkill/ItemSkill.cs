using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    abstract class ItemSkill : Skill
    {
        public ItemSkill(char shape, string name, ConsoleColor entityColor) : base(shape, name, entityColor)
        {
            skillType = ESkillType.Item;
        }

        public abstract void Use();
    }
}
