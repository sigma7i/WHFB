using System.Diagnostics;
using WarhammerFantasy.UnitActions;

namespace WarhammerFantasy.Units
{
    [DebuggerDisplay("Имя = {Name}, Жизнь = {LifePoints}")]
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
            //Ward = 0; // Ward   //  в базовом классе и так 0
            //Param = ""; // Param //  в базовом классе

            Action = new SimpleAction(this);
        }
    }
}
