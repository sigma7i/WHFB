using WarhammerFantasy.UnitActions;

namespace WarhammerFantasy.Units
{
    /// <summary>
    /// Интерфейс для юнита
    /// </summary>
    public interface IUnit
    {
        IAction Action { get; set; } // надо будет подумат как лучше сделать пока делаю так

        string Name { get; set; }
        int MovePoints { get; set; }
        int WeaponSkill { get; set; }
        int ShootSkill { get; set; }
        int Strength { get; set; }
        int Resistance { get; set; }
        int LifePoints { get; set; }
        int Initiative { get; set; }
        int AttackPoints { get; set; }
        int Leadership { get; set; }
        int Armour { get; set; }
        int Ward { get; set; }
        string Param { get; set; }
    }
}
