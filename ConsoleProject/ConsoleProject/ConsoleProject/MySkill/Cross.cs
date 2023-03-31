using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.MySkill
{
    class Cross : MeleeAttackSkill
    {
        public Cross(char shape, string name, int maxLevel, ConsoleColor entityColor, int damage, int skillCount, int skillDuration) : base(shape, name, maxLevel, entityColor, damage, skillCount, skillDuration)
        {

        }

        public override void LevelUp()
        {
            damage += 1;
        }

        public override void SetRange()
        {
            range.Clear();

            List<Enemy> enemies = ((Scene.MainScene)Scene.SceneManager.Instance.GetScene(Scene.EScene.Main)).enemies;

            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].CurrentHp -= damage;
            }

            int offsetX = GameManager.Instance.Player.PosX;
            int offsetY = GameManager.Instance.Player.PosY;

            range.Add(new Utility.Pair<int, int>(offsetX, offsetY));
        }
    }
}
