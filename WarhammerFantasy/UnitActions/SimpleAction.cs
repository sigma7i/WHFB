using WarhammerFantasy.Units;

namespace WarhammerFantasy.UnitActions
{
    public class SimpleAction : ActionBase
    {

        public SimpleAction(IUnit unit): base(unit)
        {
        }

        public override int Attack(IUnit enemy)
        {
            int r = 0;

            if ((Unit.LifePoints > 0) && (enemy.LifePoints > 0))
            {
                Print("\n" + Unit.Name + " --> попадание", 0);
                if (Hit(enemy) != 0)
                {
                    Print(" --> ранение ", 0);
                    if (Wound(enemy) != 0)
                    {
                        Print(" --> броня ", 0);
                        if (NotAs(Unit) != 0)
                        {
                            if (enemy.Ward != 0)
                            {
                                Print(" --> особая защита ", 0);
                            }
                            if (not_ward(enemy) != 0)
                            {
                                Print(" --> " + enemy.Name + " РАНЕН", 2);
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

        public override int test_r(int roundWounds)
        {
            int r = Unit.LifePoints;

            Print("\n\n" + Unit.Name + " тест на разгром --> ", 0);
            int tmpLd = Unit.Leadership - roundWounds;
            if (tmpLd < 0) { tmpLd = 0; }

            if (check_k("LD", Unit.Param, (tmpLd - 1) + '+', 2) != 0)
            {
                Print(" --> провал", 1);
                r = 0;
            }
            else { Print(" --> пройден", 1); }

            return r;
        }
    }
}
