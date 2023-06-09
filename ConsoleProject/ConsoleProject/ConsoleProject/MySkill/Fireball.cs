﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Fireball : ShootingAttackSkill
    {
        public Fireball(char shape, string name, int maxLevel, ConsoleColor entityColor, int damage, int skillCount, int skillDuration) : base(shape, name, maxLevel, entityColor, damage, skillCount, skillDuration)
        {
            explanation = "가장 가까운 적에게 불을 쏩니다.";
            levelUpexplanation = "데미지 상승";
        }

        public override void LevelUp()
        {
            damage += 1;
        }

        public override void SetRange()
        {
            range.Clear();
            int radius = 4;
            int offsetX = GameManager.Instance.Player.PosX;
            int offsetY = GameManager.Instance.Player.PosY;

            posX = offsetX;
            posY = offsetY;

            int[,] map = GameManager.Instance.map;

            for (int i = offsetY - radius / 2; i <= offsetY + radius / 2; i++)
            {
                for (int j = offsetX - radius; j <= offsetX + radius; j++)
                {
                    if (map.GetLength(0) <= i || i < Utility.MyUtility.ConsoleYMin || map.GetLength(1) <= j || j < 0)
                        continue;

                    range.Add(new Utility.Pair<int, int>(j, i));
                }
            }
        }
    }
}
