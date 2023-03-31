using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Galic : MeleeAttackSkill
    {
        int width = 0;
        public Galic(char shape, string name, int maxLevel, ConsoleColor entityColor, int damage, int skillCount, int skillDuration) : base(shape, name, maxLevel, entityColor, damage, skillCount, skillDuration)
        {
            width = 2;
            explanation = "제자리에 마늘냄새를 뿌립니다.";
            levelUpexplanation = "범위, 데미지 상승";
        }

        public override void LevelUp()
        {
            width++;
            damage++;
        }

        public override void SetRange()
        {
            range.Clear();
            int offsetX = GameManager.Instance.Player.PosX;
            int offsetY = GameManager.Instance.Player.PosY;
            int[,] map = GameManager.Instance.map;

            for(int i = offsetY - width; i <= offsetY + width; i++)
            {
                for(int j = offsetX - width; j <= offsetX + width; j++)
                {
                    if (map.GetLength(0) <= i || i < Utility.MyUtility.ConsoleYMin || map.GetLength(1) <= j || j < 0)
                        continue;

                    range.Add(new Utility.Pair<int, int>(j, i));
                }
            }
        }
    }
}
