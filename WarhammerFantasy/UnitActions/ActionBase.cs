using System;
using WarhammerFantasy.Units;

namespace WarhammerFantasy.UnitActions
{
    /// <summary>
    ///  Базовый класс для действия
    /// </summary>
    public class ActionBase : IAction
    {
        private static readonly Random Rand = new Random();
        protected static IUnit Unit;

        public ActionBase(IUnit unit)
        {
            Unit = unit;
        }

        public virtual int Attack(IUnit enemy)
        {
            return 0;
        }

        public int NotAs(IUnit heAttack)
        {

            // переделал, нужно проверить
            int r = 1;

            if (heAttack.Armour != 0)
            {
                int su = Unit.Strength - 3;
                if (su < 0) { su = 0; }
                int u = heAttack.Armour + su;
                if (check_k("AS", heAttack.Param, u, 1) != 0)
                {
                    r = 0;
                }
            }
            return r;
        }

        protected int Hit(IUnit enamy)
        {
            int r = 0;

            if (Unit.Param.Contains("A"))
            {
                r = 1;
                Print("(автопопадание)", 0);
            }
            else
            {
                if (Unit.WeaponSkill == enamy.WeaponSkill)
                {
                    if (check_k("ws", Unit.Param, 4, 1) != 0) { r = 1; }
                }
                if (Unit.WeaponSkill > enamy.WeaponSkill)
                {
                    if (check_k("ws", Unit.Param, 3, 1) != 0) { r = 1; }
                }
                if (Unit.WeaponSkill < enamy.WeaponSkill)
                {
                    if ((Unit.WeaponSkill * 2) < enamy.WeaponSkill)
                    {
                        if (check_k("ws", Unit.Param, 5, 1) != 0) { r = 1; }
                    }
                    else
                    {
                        if (check_k("ws", Unit.Param, 4, 1) != 0) { r = 1; }
                    }
                }
            }
            return r;
        }

        protected int Wound(IUnit enemy)
        {
            int r = 0;

            int a_s = Unit.Strength;
            if (Unit.Param.Contains("B")) { a_s += 2; }

            if (a_s == enemy.Resistance)
            {
                if (check_k("s", Unit.Param, 4, 1) != 0) { r = 1; }
            }
            else if (a_s == (enemy.Resistance + 1))
            {
                if (check_k("s", Unit.Param, 3, 1) != 0) { r = 1; }
            }
            else if (a_s > (enemy.Resistance + 1))
            {
                if (check_k("s", Unit.Param, 2, 1) != 0) { r = 1; }
            }
            else if ((a_s + 1) == enemy.Resistance)
            {
                if (check_k("s", Unit.Param, 5, 1) != 0) { r = 1; }
            }
            else if ((a_s + 2) == enemy.Resistance)
            {
                if (check_k("s", Unit.Param, 6, 1) != 0) { r = 1; }
            }
            else if ((a_s + 2) < enemy.Resistance)
            {
                r = 0;
            }

            return r;
        }


        public virtual int test_r(int roundWounds)
        {
            return Unit.LifePoints;
        }

        public int not_ward(IUnit enemy)
        {
            int r = 1;

            if (enemy.Ward != 0)
            {
                if (check_k("ward", enemy.Param, enemy.Ward, 1) != 0) { r = 0; }
            }
            return r;
        }

        protected static void Print(string line, byte color)
        // 0 - gray, 1 - green, 2 - red, 3 - blue
        {
            if (color == 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            if (color == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            if (color == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (color == 3)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            Console.Write(line);
        }

        protected static int check_k(string checkType, string param, int c, byte k)
        {
            int check = 0;
            Print("(" + c + "+", 0);

            k *= 6;

            int r = (byte)Rand.Next(1, k + 1);

            if ((((r < c) && (checkType != "ld")) || ((r > c) && (checkType == "ld")))
                && (param.Contains("R" + checkType) || (param.Contains("Rall"))))
            {
                Print(" " + r, 3);
                r = (byte)Rand.Next(1, c + 1);
                Print(", " + r, 0);
            }
            else { Print(", " + r, 0); }

            Print(")", 0);
            if ((r >= c) && (checkType != "LD")) { check = 1; }
            if ((r > c) && (checkType == "LD")) { check = 1; }
            if (r == 1) { check = 0; }
            return check;
        }
    }
}
