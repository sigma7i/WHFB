using WarhammerFantasy.UnitActions;

namespace WarhammerFantasy.Units
{
    public class ОтвратительныйОрк : UnitBase
    {
        public ОтвратительныйОрк()
        {
            Name = "Отвратительный орк";
            MovePoints = 4; // M
            WeaponSkill = 3; // WS
            ShootSkill = 3; // BS
            Strength = 4; // S
            Resistance = 4; // T
            LifePoints = 3; // W
            Initiative = 4; // I
            AttackPoints = 4; // A
            Leadership = 6; // LD
            Armour = 4; // AS
            //Ward = 0; // Ward
            //Param = ""; // Param

            Action = new SimpleAction(this);
        }
    }
}
