using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    abstract class MeleeAttackSkill : ActiveSkill
    {
        protected MeleeAttackSkill(char shape, string name, int maxLevel, ConsoleColor entityColor, int damage, int skillCount, int skillDuration) : base(shape, name, maxLevel, entityColor, damage, skillCount, skillDuration)
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
