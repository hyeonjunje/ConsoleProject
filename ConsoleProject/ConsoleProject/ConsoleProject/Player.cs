using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using ConsoleProject.MySkill;

namespace ConsoleProject
{
    class Player : Entity
    {
        public int level;
        public bool isRight = true;

        public int moveSpeed = 1;
        public int expAmount = 1;

        private int _currentExp;
        public int CurrentExp
        {
            get { return _currentExp; }
            set
            {
                _currentExp = value;

                Console.SetCursorPosition(0, 1);

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                for (int i = 0; i < Console.WindowWidth; i++)
                {
                    Console.Write(' ');
                }

                Console.SetCursorPosition(0, 1);
                Console.Write($"다음 레벨까지 앞으로 {level * 10 - CurrentExp}");
                Console.ResetColor();

                if (_currentExp >= level * 10)
                {
                    ((Scene.MainScene)Scene.SceneManager.Instance.GetScene(Scene.EScene.Main)).LevelUp();
                    _currentExp = 0;
                }
            }
        }


        public List<ActiveSkill> activeSkill = new List<ActiveSkill>();

        public Player()
        {
            level = 1;
            isRight = true;

            moveSpeed = 1;
            expAmount = 1;

            PosX = Console.WindowWidth / 2;
            PosY = Console.WindowHeight / 2;

            _entity = '■';
            _unit = EUnit.Player;
            _entityColor = ConsoleColor.Black;

            CurrentHp = maxHp;

            CurrentExp = 0;
        }

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);


        protected override void Move()
        {
            if (GetAsyncKeyState((int)ConsoleKey.W) != 0)
            {
                PosY -= moveSpeed;
            }
            if (GetAsyncKeyState((int)ConsoleKey.A) != 0)
            {
                isRight = false;
                PosX -= moveSpeed;
            }
            if (GetAsyncKeyState((int)ConsoleKey.S) != 0)
            {
                PosY += moveSpeed;
            }
            if (GetAsyncKeyState((int)ConsoleKey.D) != 0)
            {
                isRight = true;
                PosX += moveSpeed;
            }
        }

        public void Attack(int count)
        {
            for(int i = 0; i < activeSkill.Count; i++)
            {
                activeSkill[i].Attack(count);
            }
        }

        public void LevelUp()
        {
            level++;
        }

        public override void Dead()
        {
            // 죽으면 엔딩씬으로 넘어감
            Scene.SceneManager.Instance.ChangeScene(Scene.EScene.Ending);
        }

        public void AddSkill(Skill skill)
        {
            if(skill.skillType == ESkillType.Active)
            {
                AddSkill((ActiveSkill)skill);
            }
            else if(skill.skillType == ESkillType.Item)
            {
                AddSkill((ItemSkill)skill);
            }
        }

        // 스킬 추가
        private void AddSkill(ActiveSkill skill)
        {
            skill.level++;
            // 만약 있는 스킬이라면
            if (activeSkill.Contains(skill))
            {
                skill.LevelUp();
            }
            // 없으면
            else
            {
                activeSkill.Add(skill);
            }
        }

        private void AddSkill(ItemSkill skill)
        {
            skill.Use();
        }

        public override void HitCheck()
        {
            if(GameManager.Instance.map[PosY, PosX] == (int)EUnit.Enemy)
            {
                CurrentHp--;
            }
        }

        public override void Update(int count)
        {
            ShowEntity();

            Attack(count);
        }
    }
}
