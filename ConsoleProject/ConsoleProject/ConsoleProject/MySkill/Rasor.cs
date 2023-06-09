﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Rasor : MeleeAttackSkill
    {
        int width = 0;

        public Rasor(char shape, string name, int maxLevel, ConsoleColor entityColor, int damage, int skillCount, int skillDuration) : base(shape, name, maxLevel, entityColor, damage, skillCount, skillDuration)
        {
            width = 0;
            explanation = "전방으로 레이저를 쏩니다.";
            levelUpexplanation = "범위, 데미지 상승";
        }

        public override void LevelUp()
        {
            width++;
            damage += 1;
        }

        public override void SetRange()
        {
            range.Clear();

            int offsetX = GameManager.Instance.Player.PosX;
            int offsetY = GameManager.Instance.Player.PosY;
            int[,] map = GameManager.Instance.map;

            if(GameManager.Instance.Player.isRight)
            {
                for(int i = offsetY - width; i <= offsetY + width; i++)
                {
                    for(int j = offsetX + 1; j <= offsetX + 100; j++)
                    {
                        if (map.GetLength(1) <= j || j < 0 || map.GetLength(0) <= i || i < 1)
                            continue;

                        range.Add(new Utility.Pair<int, int>(j, i));
                    }
                }
            }
            else
            {
                for (int i = offsetY - width; i <= offsetY + width; i++)
                {
                    for (int j = offsetX - 1; j >= offsetX - 100; j--)
                    {
                        if (map.GetLength(1) <= j || j < 0 || map.GetLength(0) <= i || i < 1)
                            continue;

                        range.Add(new Utility.Pair<int, int>(j, i));
                    }
                }
            }
        }
    }
}
