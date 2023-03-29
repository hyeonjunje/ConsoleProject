using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    enum ESkillType
    {
        None,
        Active,
        Item
    }

    abstract class Skill
    {
        public char shape;
        public string name;
        public int level;
        public ConsoleColor entityColor;
        public ESkillType skillType = ESkillType.None;

        public Skill(char shape, string name, ConsoleColor entityColor)
        {
            this.shape = shape;
            this.name = name;
            this.entityColor = entityColor;
            level = 0;
        }
    }
}
