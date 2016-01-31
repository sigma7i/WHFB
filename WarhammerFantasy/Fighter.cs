using System;

namespace WarhammerFantasy
{
    public class Fighter
    {
        private static readonly Random Rand = new Random();

        public string Name;
        public int MovePoints { get; set; } // имена надо давать более осмысленные, доделай для других
                                            // такие важные данные нужно делать свойствами, посмотри IUnit я теперь могу добавить в интерфейс
        public int WeaponSkill;
        public int ShootSkill;
        public int Strength;
        public int Resistance;
        public int LifePoints;
        public int Initiative;
        public int AttackPoints;
        public int Leadership;
        public int Armour;
        public int Ward;
        public string Param;

        public Fighter(string name, int m, int ws, int bs, int s, int t, int w, int i, int a, int ld, int aS, int ward, string param) // потом это пределаем в более красивое, я тебе покажу:)
        {
            Name = name;
            MovePoints = m;
            WeaponSkill = ws;
            ShootSkill = bs;
            Strength = s;
            Resistance = t;
            LifePoints = w;
            Initiative = i;
            AttackPoints = a;
            Leadership = ld;
            Armour = aS;
            Ward = ward;
            Param = param;
        }


    } // class Fighter
}
