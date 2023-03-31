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
        public int maxLevel;
        public ConsoleColor entityColor;
        public ESkillType skillType = ESkillType.None;

        public string explanation = "안녕?";
        public string levelUpexplanation = "레벨이 오른다.";

        public Skill(char shape, string name, int maxLevel, ConsoleColor entityColor)
        {
            this.shape = shape;
            this.name = name;
            this.maxLevel = maxLevel;
            this.entityColor = entityColor;
            level = 0;
        }
    }
}
