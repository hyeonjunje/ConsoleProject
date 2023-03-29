﻿using System;
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

        public List<ActiveSkill> activeSkill = new List<ActiveSkill>();

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

            AddSkill(new Whip('∫', "채찍", 5, 20, 20, ConsoleColor.Magenta));
            AddSkill(new Rasor('=', "레이저", 1, 50, 20, ConsoleColor.Blue));
            AddSkill(new Fireball('@', "파이어볼", 3, 30, 100, ConsoleColor.DarkRed));
            // AddSkill(new Galic(1, 1, 100, '＠', ConsoleColor.Yellow));
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
            
        }

        // 스킬 추가
        public void AddSkill(ActiveSkill skill)
        {
            // 만약 있는 스킬이라면
            if(activeSkill.Contains(skill))
            {
                skill.level++;
                skill.LevelUp();
            }
            // 없으면
            else
            {
                activeSkill.Add(skill);
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
