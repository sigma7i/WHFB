using System;

namespace WarhammerFantasy
{
    public class Fighter
    {
        private static readonly Random Rand = new Random();

        public string Name;
        public int MovePoints { get; set; } // имена надо давать более осмысленные, доделай для других
                                            // такие важные данные нужно делать свойствами, посмотри IUnit я теперь могу добавить в интерфейс
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

        public Fighter(string name, int m, int ws, int bs, int s, int t, int w, int i, int a, int ld, int aS, int ward, string param) // потом это пределаем в более красивое, я тебе покажу:)
        {
            Name = name;
            MovePoints = m;
            WS = ws;
            BS = bs;
            S = s;
            T = t;
            W = w;
            I = i;
            A = a;
            LD = ld;
            AS = aS;
            Ward = ward;
            Param = param;
        }

        /// <summary>
        ///  Атака персонажа  <- это хмл документация, так нам будет легче определять что делает функция, добавь для других
        /// </summary>
        public int Attack(Fighter enamy)
        {
            int r = 0;

            if ((W > 0) && (enamy.W > 0))
            {
                Print("\n" + Name + " --> попадание", 0);
                if (Hit(enamy) != 0)
                {
                    Print(" --> ранение ", 0);
                    if (Wound(enamy) != 0)
                    {
                        Print(" --> броня ", 0);
                        if (enamy.NotAs(this) != 0)
                        {
                            if (enamy.Ward != 0)
                            {
                                Print(" --> особая защита ", 0);
                            }
                            if (enamy.not_ward() != 0)
                            {
                                Print(" --> " + enamy.Name + " РАНЕН", 2);
                                r = 1;
                            }
                        }
                    }
                }
                if (r == 0)
                {
                    Print(" --> не получилось", 1);
                }
            }
            return r;
        }

        private int Hit(Fighter enamy)
        {
            int r = 0;

            if (Param.Contains("A"))
            {
                r = 1;
                Print("(автопопадание)", 0);
            }
            else
            {
                if (WS == enamy.WS)
                {
                    if (check_k("ws", Param, 4, 1) != 0) { r = 1; }
                }
                if (WS > enamy.WS)
                {
                    if (check_k("ws", Param, 3, 1) != 0) { r = 1; }
                }
                if (WS < enamy.WS)
                {
                    if ((WS * 2) < enamy.WS)
                    {
                        if (check_k("ws", Param, 5, 1) != 0) { r = 1; }
                    }
                    else
                    {
                        if (check_k("ws", Param, 4, 1) != 0) { r = 1; }
                    }
                }
            }
            return r;
        }

        private int Wound(Fighter enamy)
        {
            int r = 0;

            int a_s = S;
            if (Param.Contains("B")) { a_s += 2; }

            if (a_s == enamy.T)
            {
                if (check_k("s", Param, 4, 1) != 0) { r = 1; }
            }
            else if (a_s == (enamy.T + 1))
            {
                if (check_k("s", Param, 3, 1) != 0) { r = 1; }
            }
            else if (a_s > (enamy.T + 1))
            {
                if (check_k("s", Param, 2, 1) != 0) { r = 1; }
            }
            else if ((a_s + 1) == enamy.T)
            {
                if (check_k("s", Param, 5, 1) != 0) { r = 1; }
            }
            else if ((a_s + 2) == enamy.T)
            {
                if (check_k("s", Param, 6, 1) != 0) { r = 1; }
            }
            else if ((a_s + 2) < enamy.T)
            {
                r = 0;
            }

            return r;
        }


        private int NotAs(Fighter heAttack)
        {
            int r = 1;

            if (AS != 0)
            {
                int su = heAttack.S - 3;
                if (su < 0) { su = 0; }
                int u = AS + su;
                if (check_k("AS", Param, u, 1) != 0)
                {
                    r = 0;
                }
            }
            return r;
        }

        private int not_ward()
        {
            int r = 1;

            if (Ward != 0)
            {
                if (check_k("ward", Param, Ward, 1) != 0) { r = 0; }
            }
            return r;
        }

        public int test_r(int roundWounds)
        {
            int r = W;

            Print("\n\n" + Name + " тест на разгром --> ", 0);
            int tmpLd = LD - roundWounds;
            if (tmpLd < 0) { tmpLd = 0; }

            if (check_k("LD", Param, (tmpLd - 1) + '+', 2) != 0)
            {
                Print(" --> провал", 1);
                r = 0;
            }
            else { Print(" --> пройден", 1); }

            return r;
        }

        static void Print(string line, byte color)
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
            else { Print(", " + r, 0); }

            Print(")", 0);
            if ((r >= c) && (checkType != "LD")) { check = 1; }
            if ((r > c) && (checkType == "LD")) { check = 1; }
            if (r == 1) { check = 0; }
            return check;
        }

    } // class Fighter
}
