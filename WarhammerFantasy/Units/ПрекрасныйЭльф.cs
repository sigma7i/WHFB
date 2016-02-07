using System.Diagnostics;
using WarhammerFantasy.UnitActions;

namespace WarhammerFantasy.Units
{
    [DebuggerDisplay("Имя = {Name}, Жизнь = {LifePoints}")]
    public class ПрекрасныйЭльф : UnitBase
    {
        public ПрекрасныйЭльф()
        {
            Name = "Прекрасный эльф";
            MovePoints = 4;
            WeaponSkill = 4;
            ShootSkill = 5;
            Strength = 3;
            Resistance = 3;
            LifePoints = 3;
            Initiative = 5;
            AttackPoints = 4;
            Leadership = 8;
            Armour = 5;
            Ward = 6;
            //Param = ""; //  в базовом классе уже есть

            Action = new SimpleAction(this);
        }

    }
}
