using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarhammerFantasy
{
    class Program
    {
        static Random rand = new Random();

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
            int Round = 0;

            Console.SetWindowSize(140, 50);
            Print("\n\n\n" + A.Name + " vs " + B.Name + "\n", 0);

            while ((A.W > 0) && (B.W > 0))
            {
                Round++;
                int Round_woundA = 0;
                int Round_woundB = 0;

                Print("\n\nраунд: " + Round + "\n", 3);
                Print(A.Name + ": " + A.W + "W " + B.Name + ": " + B.W + "W\n", 0);

                if (Check_i(Round) != 0)
                {
                    for (int all_a = 1; all_a <= A.A; all_a++)
                    {
                        if (A.Attack(B) != 0)
                        {
                            Round_woundB++;
                            B.W--;
                        }
                    }
                    for (int all_b = 1; all_b <= B.A; all_b++)
                    {
                        if (B.Attack(A) != 0)
                        {
                            Round_woundA++;
                            A.W--;
                        }
                    }
                }
                else
                {
                    for (int all_b = 1; all_b <= B.A; all_b++)
                    {
                        if (B.Attack(A) != 0)
                        {
                            Round_woundA++;
                            A.W--;
                        }
                    }
                    for (int all_a = 1; all_a <= A.A; all_a++)
                    {
                        if (A.Attack(B) != 0)
                        {
                            Round_woundB++;
                            B.W--;
                        }
                    }
                };
                if ((A.W > 0) && (B.W > 0))
                {
                    if (Round_woundA > Round_woundB)
                    {
                        A.W = A.test_r(Round_woundA);
                    }
                    if (Round_woundB > Round_woundA)
                    {
                        B.W = B.test_r(Round_woundB);
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
            string PressEnter = Console.ReadLine();
        }

        static void Print(string Line, byte Color)
        // 0 - gray, 1 - green, 2 - red, 3 - blue
        {
            Console.BackgroundColor = ConsoleColor.Black;
            if (Color == 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            };
            if (Color == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            };
            if (Color == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            };
            if (Color == 3)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Gray;
            };
            Console.Write(Line);
        }

        static int Check_i(int Round)
        {
            int r = 1;

            if ((Round == 1) && (!(B.Param.Contains("F"))) && (!(B.Param.Contains("F"))))
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

        static int check_k(string CheckType, string Param, int c, byte k)
        {
            int check = 0;
            Print("(" + c + "+", 0);

            k *= 6;

            int r = (byte)rand.Next(1, k + 1);

            if ((((r < c) && (CheckType != "ld")) || ((r > c) && (CheckType == "ld")))
                && (Param.Contains("R" + CheckType) || (Param.Contains("Rall"))))
            {
                Print(" " + r, 3);
                r = (byte)rand.Next(1, c + 1);
                Print(", " + r, 0);
            }
            else { Print(" " + r, 0); };

            Print(")", 0);
            if ((r >= c) && (CheckType != "LD")) { check = 1; };
            if ((r > c) && (CheckType == "LD")) { check = 1; };
            if (r == 1) { check = 0; };
            return check;
        }


    } // class Program

    class Fighter
    {
        static Random rand = new Random();

        public string Name;
        public int M;
        public int WS;
        public int BS;
        public int S;
        public int T;
        public int W;
        public int I;
        public int A;
        public int LD;
        public int AS;
        public int Ward;
        public string Param;

        public Fighter(string Name, int M, int WS, int BS, int S, int T, int W, int I, int A, int LD, int AS, int Ward, string Param)
        {
            this.Name = Name;
            this.M = M;
            this.WS = WS;
            this.BS = BS;
            this.S = S;
            this.T = T;
            this.W = W;
            this.I = I;
            this.A = A;
            this.LD = LD;
            this.AS = AS;
            this.Ward = Ward;
            this.Param = Param;
        }

        public int Attack(Fighter enamy)
        {
            int r = 0;

            if ((this.W > 0) && (enamy.W > 0))
            {
                Print("\n" + this.Name + " --> попадание", 0);
                if (this.hit(enamy) != 0)
                {
                    Print(" --> ранение ", 0);
                    if (this.wound(enamy) != 0)
                    {
                        Print(" --> броня ", 0);
                        if (enamy.not_as(this) != 0)
                        {
                            if (enamy.Ward != 0)
                            {
                                Print(" --> особая защита ", 0);
                            }
                            if (enamy.not_ward(this) != 0)
                            {
                                Print(" --> " + enamy.Name + " РАНЕН", 2);
                                r = 1;
                            };
                        };
                    };
                };
                if (r == 0)
                {
                    Print(" --> не получилось", 1);
                }
            }
            return r;
        }

        private int hit(Fighter enamy)
        {
            int r = 0;

            if (this.Param.Contains("A"))
            {
                r = 1;
                Print("(автопопадание)", 0);
            }
            else
            {
                if (this.WS == enamy.WS)
                {
                    if (check_k("ws", this.Param, 4, 1) != 0) { r = 1; };
                };
                if (this.WS > enamy.WS)
                {
                    if (check_k("ws", this.Param, 3, 1) != 0) { r = 1; };
                };
                if (this.WS < enamy.WS)
                {
                    if ((this.WS * 2) < enamy.WS)
                    {
                        if (check_k("ws", this.Param, 5, 1) != 0) { r = 1; };
                    }
                    else
                    {
                        if (check_k("ws", this.Param, 4, 1) != 0) { r = 1; };
                    }
                };
            };
            return r;
        }

        private int wound(Fighter enamy)
        {
            int r = 0;

            int a_s = this.S;
            if (this.Param.Contains("B")) { a_s += 2; };

            if (a_s == enamy.T)
            {
                if (check_k("s", this.Param, 4, 1) != 0) { r = 1; }
            }
            else if (a_s == (enamy.T + 1))
            {
                if (check_k("s", this.Param, 3, 1) != 0) { r = 1; }
            }
            else if (a_s > (enamy.T + 1))
            {
                if (check_k("s", this.Param, 2, 1) != 0) { r = 1; };
            }
            else if ((a_s + 1) == enamy.T)
            {
                if (check_k("s", this.Param, 5, 1) != 0) { r = 1; };
            }
            else if ((a_s + 2) == enamy.T)
            {
                if (check_k("s", this.Param, 6, 1) != 0) { r = 1; }
            }
            else if ((a_s + 2) < enamy.T)
            {
                r = 0;
            }

            return r;
        }


        private int not_as(Fighter he_attack)
        {
            int r = 1;

            if (this.AS != 0)
            {
                int su = he_attack.S - 3;
                if (su < 0) { su = 0; };
                int u = this.AS + su;
                if (check_k("AS", this.Param, u, 1) != 0)
                {
                    r = 0;
                }
            }
            return r;
        }

        private int not_ward(Fighter he_attack)
        {
            int r = 1;

            if (this.Ward != 0)
            {
                if (check_k("ward", this.Param, this.Ward, 1) != 0) { r = 0; };
            }
            return r;
        }

        public int test_r(int Round_wounds)
        {
            int r = this.W;

            Print("\n\n" + this.Name + " тест на разгром --> ", 0);
            int tmp_ld = this.LD - Round_wounds;
            if (tmp_ld < 0) { tmp_ld = 0; };

            if (check_k("LD", this.Param, (tmp_ld - 1) + '+', 2) != 0)
            {
                Print(" --> провал", 1);
                r = 0;
            }
            else { Print(" --> пройден", 1); };

            return r;
        }

        static void Print(string Line, byte Color)
        // 0 - gray, 1 - green, 2 - red, 3 - blue
        {
            if (Color == 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            };
            if (Color == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            };
            if (Color == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            };
            if (Color == 3)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            };
            Console.Write(Line);
        }

        static int check_k(string CheckType, string Param, int c, byte k)
        {
            int check = 0;
            Print("(" + c + "+", 0);

            k *= 6;

            int r = (byte)rand.Next(1, k + 1);

            if ((((r < c) && (CheckType != "ld")) || ((r > c) && (CheckType == "ld")))
                && (Param.Contains("R" + CheckType) || (Param.Contains("Rall"))))
            {
                Print(" " + r, 3);
                r = (byte)rand.Next(1, c + 1);
                Print(", " + r, 0);
            }
            else { Print(", " + r, 0); };

            Print(")", 0);
            if ((r >= c) && (CheckType != "LD")) { check = 1; };
            if ((r > c) && (CheckType == "LD")) { check = 1; };
            if (r == 1) { check = 0; };
            return check;
        }

    } // class Fighter
}
