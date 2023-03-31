using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Lightning : MeleeAttackSkill
    {
        int count = 2;

        public Lightning(char shape, string name, int maxLevel, ConsoleColor entityColor, int damage, int skillCount, int skillDuration) : base(shape, name, maxLevel, entityColor, damage, skillCount, skillDuration)
        {
            explanation = "무작위 적에게 번개를 내립니다.";
            levelUpexplanation = "개수, 데미지 상승";

            count = 2;
        }

        public override void LevelUp()
        {
            // 데미지 상승,  개수 상승
            damage += 2;
            count += 2;
        }

        public override void SetRange()
        {
            range = new List<Utility.Pair<int, int>>();

            List<Enemy> enemies = ((Scene.MainScene)Scene.SceneManager.Instance.GetScene(Scene.EScene.Main)).enemies;
            int[,] map = GameManager.Instance.map;

            for (int i = 0; i < count; i++)
            {
                if(enemies.Count > i)
                {
                    if (map.GetLength(1) > enemies[i].PosX && enemies[i].PosX >= 0 && map.GetLength(0) > enemies[i].PosY && enemies[i].PosY >= 1)
                    {
                        range.Add(new Utility.Pair<int, int>(enemies[i].PosX, enemies[i].PosY));
                    }

                    if (map.GetLength(1) > enemies[i].PosX && enemies[i].PosX >= 0 && map.GetLength(0) > enemies[i].PosY + 1 && enemies[i].PosY + 1 >= 1)
                    {
                        range.Add(new Utility.Pair<int, int>(enemies[i].PosX, enemies[i].PosY + 1));
                    }

                    if (map.GetLength(1) > enemies[i].PosX + 1 && enemies[i].PosX + 1 >= 0 && map.GetLength(0) > enemies[i].PosY && enemies[i].PosY >= 1)
                    {
                        range.Add(new Utility.Pair<int, int>(enemies[i].PosX + 1, enemies[i].PosY));
                    }

                    if (map.GetLength(1) > enemies[i].PosX && enemies[i].PosX >= 0 && map.GetLength(0) > enemies[i].PosY - 1 && enemies[i].PosY - 1 >= 1)
                    {
                        range.Add(new Utility.Pair<int, int>(enemies[i].PosX, enemies[i].PosY - 1));
                    }

                    if (map.GetLength(1) > enemies[i].PosX - 1 && enemies[i].PosX - 1 >= 0 && map.GetLength(0) > enemies[i].PosY && enemies[i].PosY >= 1)
                    {
                        range.Add(new Utility.Pair<int, int>(enemies[i].PosX - 1, enemies[i].PosY));
                    }
                }
            }
        }
    }
}
