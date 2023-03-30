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

        public Rest(char shape, string name, ConsoleColor entityColor) : base(shape, name, entityColor)
        {
            healAmount = 30;
        }

        public override void Use()
        {
            GameManager.Instance.Player.CurrentHp += healAmount;
        }
    }
}
