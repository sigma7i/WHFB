using WarhammerFantasy.Units;

namespace WarhammerFantasy.UnitActions
{
    /// <summary>
    ///  Интерфейс для действия
    /// </summary>
    public interface IAction
    {
        /// <summary>
        ///  Атака
        /// </summary>
        int Attack(IUnit enemy);

        /// <summary>
        ///  хз
        /// </summary>
        int test_r(int roundWounds);
    }
}
