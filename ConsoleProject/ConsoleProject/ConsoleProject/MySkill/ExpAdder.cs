using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class ExpAdder : ItemSkill
    {
        public ExpAdder(char shape, string name, int maxLevel, ConsoleColor entityColor) : base(shape, name, maxLevel, entityColor)
        {
            explanation = "얻는 경험지가 증가합니다.";
            levelUpexplanation = "경험치량 상승";
        }

        public override void Use()
        {
            level++;
            GameManager.Instance.Player.expAmount++;
        }
    }
}
