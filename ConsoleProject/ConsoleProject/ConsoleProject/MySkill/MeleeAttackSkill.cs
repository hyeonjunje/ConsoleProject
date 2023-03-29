using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    abstract class MeleeAttackSkill : ActiveSkill
    {
        public MeleeAttackSkill(char shape, string name, int damage, int skillCount, int skillDuration, ConsoleColor entityColor) : base(shape, name, damage, skillCount, skillDuration, entityColor)
        {
        }

        public override void Attack(int count)
        {
            if(!isUsing && count % skillCount == 0)
            {
                SetRange();

                Use();

                Show();
            }
            else if(isUsing && count % skillDuration == 0)
            {
                UnShow();

                Finish();
            }
        }
    }
}
