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
        public List<int> attackSkillCount = new List<int>();

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

            AddSkill(new Whip(3, 20, 1, '∫', ConsoleColor.Magenta));
            AddSkill(new Rasor(5, 50, 1, '=', ConsoleColor.Blue));
            //AddSkill(new Galic(1, 1, 1, '＠', ConsoleColor.Yellow));
        }

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);


        protected override void Move()
        {
            if (GetAsyncKeyState((int)ConsoleKey.W) != 0)
            {
                PosY-=2;
            }
            if (GetAsyncKeyState((int)ConsoleKey.A) != 0)
            {
                isRight = false;
                PosX-=2;
            }
            if (GetAsyncKeyState((int)ConsoleKey.S) != 0)
            {
                PosY+=2;
            }
            if (GetAsyncKeyState((int)ConsoleKey.D) != 0)
            {
                isRight = true;
                PosX+=2;
            }
        }

        public void Attack()
        {
            for(int i = 0; i < attackSkillCount.Count; i++)
            {
                attackSkillCount[i]++;
            }

            for(int i = 0; i < attackSKill.Count; i++)
            {
                // 사용중인 스킬이 아니고 스킬을 사용할 수 있을 때 사용
                if(!attackSKill[i].isUsing && attackSkillCount[i] >= attackSKill[i].skillCount)
                {
                    attackSkillCount[i] = 0;

                    // 범위 설정
                    attackSKill[i].SetRange();

                    // 스킬 사용
                    attackSKill[i].Use();

                    // 스킬 시각화
                    attackSKill[i].Show();
                }
                // 스킬이 사용중이며 스킬 지속 시간이 끝날 때
                if (attackSKill[i].isUsing && attackSkillCount[i] >= attackSKill[i].skillDuration)
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
                attackSkillCount.Add(0);
            }
        }

        public override void HitCheck()
        {
            if(Game.Instance.map[PosY, PosX] == (int)EUnit.Enemy)
            {
                CurrentHp--;
            }
        }
    }
}
