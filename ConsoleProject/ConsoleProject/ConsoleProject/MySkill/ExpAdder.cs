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
        }

        public override void Use()
        {
            level++;
            GameManager.Instance.Player.expAmount++;
        }
    }
}
