using System;

namespace WarhammerFantasy
{
    class Program
    {
        static readonly Random Rand = new Random();

        static Fighter A = new Fighter(
            "Прекрасный эльф",
            4,  // M / Передвижение
            4,  // WS / Владение оружием
            5,  // BS / Навыки стрельбы
            3,  // S / Сила
            3,  // T / Стойкость
            3,  // W / Жизни
            5,  // I / Инициатива
            4,  // A / Атаки
            8,  // LD / Лидерство
            5,  // AS / Защита бронёй
            6,  // Ward / Особая защита
            ""  // Param / Дополнительные параметры:
            //  F - всегда бьёт первым
            //  B - большое оружие, +2 к S, по инициативе бьёт последним
            //  Rws, Rs, Rt, Ras, Rld, Rward - перебрасывать кубик при бросках на пареметр
            //  Rall - перебрасывать любой кубик
            );

        static Fighter B = new Fighter(
            "Отвратительный орк",
            4, // M
            3, // WS
            3, // BS
            4, // S
            4, // T
            3, // W
            4, // I
            4, // A
            6, // LD
            4, // AS
            0, // Ward
            "" // Param
            );

        static void Main(string[] args)
        {
            int round = 0;

            Console.SetWindowSize(140, 50);
            Print("\n\n\n" + A.Name + " vs " + B.Name + "\n", 0);

            while ((A.W > 0) && (B.W > 0))
            {
                round++;
                int roundWoundA = 0;
                int roundWoundB = 0;

                Print("\n\nраунд: " + round + "\n", 3);
                Print(A.Name + ": " + A.W + "W " + B.Name + ": " + B.W + "W\n", 0);

                if (Check_i(round) != 0)
                {
                    for (int allA = 1; allA <= A.A; allA++)
                    {
                        if (A.Attack(B) != 0)
                        {
                            roundWoundB++;
                            B.W--;
                        }
                    }
                    for (int allB = 1; allB <= B.A; allB++)
                    {
                        if (B.Attack(A) != 0)
                        {
                            roundWoundA++;
                            A.W--;
                        }
                    }
                }
                else
                {
                    for (int allB = 1; allB <= B.A; allB++)
                    {
                        if (B.Attack(A) != 0)
                        {
                            roundWoundA++;
                            A.W--;
                        }
                    }
                    for (int allA = 1; allA <= A.A; allA++)
                    {
                        if (A.Attack(B) != 0)
                        {
                            roundWoundB++;
                            B.W--;
                        }
                    }
                };
                if ((A.W > 0) && (B.W > 0))
                {
                    if (roundWoundA > roundWoundB)
                    {
                        A.W = A.test_r(roundWoundA);
                    }
                    if (roundWoundB > roundWoundA)
                    {
                        B.W = B.test_r(roundWoundB);
                    };
                };
            };
            Print("\n\nКонец:", 3);
            if (B.W < 1)
            {
                Print(" " + A.Name + " победил\n\n", 0);
            }
            if (A.W < 1)
            {
                Print(" " + B.Name + " победил\n\n", 0);
            }
            Print("\n\n нажмите Enter...", 0);
            string pressEnter = Console.ReadLine();
        }

        static void Print(string line, byte color)
        // 0 - gray, 1 - green, 2 - red, 3 - blue
        {
            Console.BackgroundColor = ConsoleColor.Black;
            if (color == 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            };
            if (color == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            };
            if (color == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            };
            if (color == 3)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Gray;
            };
            Console.Write(line);
        }

        static int Check_i(int round)
        {
            int r = 1;

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
            else if (A.I > B.I)
            {
                r = 1;
            }
            else if (A.I < B.I)
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
                };
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
            if (r == 1) { check = 0; };
            return check;
        }


    } // class Program

   
}
