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

        public List<AttackMeleeSkill> attackMeleeSkill = new List<AttackMeleeSkill>();
        public List<AttackLongLangeSkill> attackLongLangeSkill = new List<AttackLongLangeSkill>();

        public List<int> attackMeleeSkillCount = new List<int>();
        public List<int> attackLongLangeSkillCount = new List<int>();

        //public Fireball fireBall = new Fireball(3, 30, );

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
            AddSkill(new Rasor(5, 40, 1, '=', ConsoleColor.Blue));
            // AddSkill(new Galic(1, 10, 1, '＠', ConsoleColor.Yellow));
            AddSkill(new Fireball(3, 30, 30, '＠', ConsoleColor.DarkRed));
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

        public void Attack()
        {
            // 근거리
            for(int i = 0; i < attackMeleeSkillCount.Count; i++)
            {
                attackMeleeSkillCount[i]++;
            }

            for(int i = 0; i < attackMeleeSkill.Count; i++)
            {
                // 사용중인 스킬이 아니고 스킬을 사용할 수 있을 때 사용
                if(!attackMeleeSkill[i].isUsing && attackMeleeSkillCount[i] >= attackMeleeSkill[i].skillCount)
                {
                    attackMeleeSkillCount[i] = 0;

                    // 범위 설정
                    attackMeleeSkill[i].SetRange();

                    // 스킬 사용
                    attackMeleeSkill[i].Use();

                    // 스킬 시각화
                    attackMeleeSkill[i].Show();
                }
                // 스킬이 사용중이며 스킬 지속 시간이 끝날 때
                if (attackMeleeSkill[i].isUsing && attackMeleeSkillCount[i] >= attackMeleeSkill[i].skillDuration)
                {
                    attackMeleeSkill[i].UnShow();
                }
            }
        }

        public override void Dead()
        {
            
        }

        // 스킬 추가
        public void AddSkill(AttackMeleeSkill skill)
        {
            // 만약 있는 스킬이라면
            if(attackMeleeSkill.Contains(skill))
            {

            }
            // 없으면
            else
            {
                attackMeleeSkill.Add(skill);
                attackMeleeSkillCount.Add(0);
            }
        }

        public void AddSkill(AttackLongLangeSkill skill)
        {
            // 만약 있는 스킬이라면
            if (attackLongLangeSkill.Contains(skill))
            {

            }
            // 없으면
            else
            {
                attackLongLangeSkill.Add(skill);
                attackLongLangeSkillCount.Add(0);
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
