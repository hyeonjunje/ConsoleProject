using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Speed : ItemSkill
    {
        public Speed(char shape, string name, int maxLevel, ConsoleColor entityColor) : base(shape, name, maxLevel, entityColor)
        {
            explanation = "이동속도가 증가합니다.";
            levelUpexplanation = "이동속도 상승";
        }

        public override void Use()
        {
            level++;
            GameManager.Instance.Player.moveSpeed++;
        }
    }
}
