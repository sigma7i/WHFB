using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WarhammerFantasy.Units;

namespace WarhammerFantasy
{
    class Program
    {
        static readonly Random Rand = new Random();


        static void Main(string[] args)
        {
            // теперь можно будет сделать армия на армию, вот так
            //List<IUnit> army = new List<IUnit>();
            //army.Add(new ОтвратительныйОрк());
            //army.Add(new ОтвратительныйОрк());
            // но для этого нужно будет пределать код

            IUnit A = new ПрекрасныйЭльф();
            IUnit B = new ОтвратительныйОрк();
            


           //  var test = Assembly.GetExecutingAssembly().GetExportedTypes().Where(i => i.BaseType == typeof(UnitBase));
    

            // саму битву лучше перенести в отдельную функцию, а еще лучше в класс
            int round = 0;

            Console.SetWindowSize(140, 50);
            Print("\n\n\n" + A.Name + " vs " + B.Name + "\n", 0);

            while ((A.LifePoints > 0) && (B.LifePoints > 0))
            {
                round++;
                int roundWoundA = 0;
                int roundWoundB = 0;

                Print("\n\nраунд: " + round + "\n", 3);
                Print(A.Name + ": " + A.LifePoints + "W " + B.Name + ": " + B.LifePoints + "W\n", 0);

                if (Check_i(round, A, B) != 0)
                {
                    for (int allA = 1; allA <= A.AttackPoints; allA++)
                    {
                        if (A.Action.Attack(B) != 0)
                        {
                            roundWoundB++;
                            B.LifePoints--;
                        }
                    }
                    for (int allB = 1; allB <= B.AttackPoints; allB++)
                    {
                        if (B.Action.Attack(A) != 0)
                        {
                            roundWoundA++;
                            A.LifePoints--;
                        }
                    }
                }
                else
                {
                    for (int allB = 1; allB <= B.AttackPoints; allB++)
                    {
                        if (B.Action.Attack(A) != 0)
                        {
                            roundWoundA++;
                            A.LifePoints--;
                        }
                    }
                    for (int allA = 1; allA <= A.AttackPoints; allA++)
                    {
                        if (A.Action.Attack(B) != 0)
                        {
                            roundWoundB++;
                            B.LifePoints--;
                        }
                    }
                }
                if ((A.LifePoints > 0) && (B.LifePoints > 0))
                {
                    if (roundWoundA > roundWoundB)
                    {
                        A.LifePoints = A.Action.test_r(roundWoundA);
                    }
                    if (roundWoundB > roundWoundA)
                    {
                        B.LifePoints = B.Action.test_r(roundWoundB);
                    }
                }
            }
            Print("\n\nКонец:", 3);
            if (B.LifePoints < 1)
            {
                Print(" " + A.Name + " победил\n\n", 0);
            }
            if (A.LifePoints < 1)
            {
                Print(" " + B.Name + " победил\n\n", 0);
            }
            Print("\n\n нажмите Enter...", 0);
            Console.ReadLine();
        }

        static void Print(string line, byte color)
        // 0 - gray, 1 - green, 2 - red, 3 - blue
        {
            Console.BackgroundColor = ConsoleColor.Black;
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
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.Write(line);
        }

        static int Check_i(int round, IUnit A, IUnit B)
        {
            int r;

            if ((round == 1) && (!(B.Param.Contains("F"))) && (!(B.Param.Contains("F"))))
            {
                r = 1;
                Print("правило первого хода\n", 0);
            }
            else if ((A.Param.Contains("F")) && (!(B.Param.Contains("F"))))
            {
                r = 1;
                Print(A.Name + " -> всегда бьёт первым\n", 0);
            }
            else if ((!(A.Param.Contains("F"))) && (B.Param.Contains("F")))
            {
                r = 0;
                Print(B.Name + " -> всегда бьёт первым\n", 0);
            }
            else if (A.Initiative > B.Initiative)
            {
                r = 1;
            }
            else if (A.Initiative < B.Initiative)
            {
                r = 0;
            }
            else
            {
                Print("первым бьёт случайный ", 0);
                if (check_k("i", "", 4, 1) != 0)
                {
                    r = 1;
                    Print(" --> " + A.Name + "\n", 0);
                }
                else
                {
                    r = 0;
                    Print(" --> " + B.Name + "\n", 0);
                }
            }
            return r;
        }

        static int check_k(string checkType, string param, int c, byte k)
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
            else { Print(" " + r, 0); }

            Print(")", 0);
            if ((r >= c) && (checkType != "LD")) { check = 1; }
            if ((r > c) && (checkType == "LD")) { check = 1; }
            if (r == 1) { check = 0; }
            return check;
        }


    } // class Program
}
