namespace WarhammerFantasy.Units
{
    public interface IUnit
    {
        int Attack(Fighter enamy);

        int MovePoints { get; set; }

    }
}
