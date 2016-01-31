using WarhammerFantasy.UnitActions;

namespace WarhammerFantasy.Units
{
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
            //Param = "";

            Action = new SimpleAction(this);
        }

    }
}
