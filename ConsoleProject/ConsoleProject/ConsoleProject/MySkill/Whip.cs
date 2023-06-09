﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Whip : MeleeAttackSkill
    {
        int rangeX;
        int rangeY;

        public Whip(char shape, string name, int maxLevel, ConsoleColor entityColor, int damage, int skillCount, int skillDuration) : base(shape, name, maxLevel, entityColor, damage, skillCount, skillDuration)
        {
            rangeX = 1;
            rangeY = 4;

            explanation = "상하로 채찍을 휘두릅니다.";
            levelUpexplanation = "범위, 데미지 상승";
        }

        public override void LevelUp()
        {
            if(level % 2 == 1)
            {
                rangeX++;
            }
            rangeY++;
            damage += 1;
        }

        public override void SetRange()
        {
            range.Clear();
            int offsetX = GameManager.Instance.Player.PosX;
            int offsetY = GameManager.Instance.Player.PosY;
            int[,] map = GameManager.Instance.map;

            for (int i = offsetX - rangeX; i <= offsetX + rangeX; i++)
            {
                for(int j = offsetY + 1; j < offsetY + rangeY; j++)
                {
                    if (map.GetLength(1) <= i || i < 0 || map.GetLength(0) <= j || j < 1)
                        continue;

                    range.Add(new Utility.Pair<int, int>(i, j));
                }

                for(int j = offsetY - 1; j > offsetY - rangeY; j--)
                {
                    if (map.GetLength(1) <= i || i < 0 || map.GetLength(0) <= j || j < 1)
                        continue;

                    range.Add(new Utility.Pair<int, int>(i, j));
                }
            }
        }
    }
}
