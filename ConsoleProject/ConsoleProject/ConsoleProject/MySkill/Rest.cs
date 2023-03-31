using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Rest : ItemSkill
    {
        int healAmount;

        public Rest(char shape, string name, int maxLevel, ConsoleColor entityColor) : base(shape, name, maxLevel, entityColor)
        {
            healAmount = 30;
            explanation = "체력을 30회복합니다.";
        }

        public override void Use()
        {
            GameManager.Instance.Player.CurrentHp += healAmount;
        }
    }
}
