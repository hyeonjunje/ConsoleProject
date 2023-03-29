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

        public List<AttackSkill> attackSKill = new List<AttackSkill>();

        public Player()
        {
            level = 1;
            isRight = true;

            PosX = Console.WindowWidth / 2;
            PosY = Console.WindowHeight / 2;

            _entity = '■';
            _unit = EUnit.Player;
            _entityColor = ConsoleColor.Black;

            CurrentHp = maxHp;

            AddSkill(new Whip(5, 20, 20, '∫', ConsoleColor.Magenta));
            AddSkill(new Rasor(1, 50, 20, '=', ConsoleColor.Blue));
            //AddSkill(new Galic(1, 1, 1, '＠', ConsoleColor.Yellow));
        }

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);


        protected override void Move()
        {
            if (GetAsyncKeyState((int)ConsoleKey.W) != 0)
            {
                PosY--;
            }
            if (GetAsyncKeyState((int)ConsoleKey.A) != 0)
            {
                isRight = false;
                PosX--;
            }
            if (GetAsyncKeyState((int)ConsoleKey.S) != 0)
            {
                PosY++;
            }
            if (GetAsyncKeyState((int)ConsoleKey.D) != 0)
            {
                isRight = true;
                PosX++;
            }
        }

        public void Attack(int count)
        {
            for(int i = 0; i < attackSKill.Count; i++)
            {
                // 사용중인 스킬이 아니고 스킬을 사용할 수 있을 때 사용
                if(!attackSKill[i].isUsing && count % attackSKill[i].skillCount == 0)
                {
                    // 범위 설정
                    attackSKill[i].SetRange();

                    // 스킬 사용
                    attackSKill[i].Use();

                    // 스킬 시각화
                    attackSKill[i].Show();
                }
                // 스킬이 사용중이며 스킬 지속 시간이 끝날 때
                else if (attackSKill[i].isUsing && count % attackSKill[i].skillDuration == 0)
                {
                    attackSKill[i].UnShow();
                }
            }
        }

        public override void Dead()
        {
            
        }

        // 스킬 추가
        public void AddSkill(AttackSkill skill)
        {
            // 만약 있는 스킬이라면
            if(attackSKill.Contains(skill))
            {

            }
            // 없으면
            else
            {
                attackSKill.Add(skill);
            }
        }

        public override void HitCheck()
        {
            if(Game.Instance.map[PosY, PosX] == (int)EUnit.Enemy)
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
