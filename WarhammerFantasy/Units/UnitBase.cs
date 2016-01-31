using System;
using WarhammerFantasy.UnitActions;

namespace WarhammerFantasy.Units
{
    /// <summary>
    ///  Базовый класc для юнита
    /// </summary>
    public abstract class UnitBase : IUnit
    {
        public IAction Action { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Передвижение
        /// </summary>
        public int MovePoints { get; set; }

        /// <summary>
        /// Владение оружием
        /// </summary>
        public int WeaponSkill { get; set; }

        /// <summary>
        /// Навыки стрельбы
        /// </summary>
        public int ShootSkill { get; set; }

        /// <summary>
        /// Сила
        /// </summary>
        public int Strength { get; set; }

        /// <summary>
        /// Стойкость
        /// </summary>
        public int Resistance { get; set; }

        /// <summary>
        ///  Жизни
        /// </summary>
        public int LifePoints { get; set; }

        /// <summary>
        ///  Инициатива
        /// </summary>
        public int Initiative { get; set; }

        /// <summary>
        /// Атаки
        /// </summary>
        public int AttackPoints { get; set; }

        /// <summary>
        /// Лидерство
        /// </summary>
        public int Leadership { get; set; }

        /// <summary>
        /// Защита бронёй
        /// </summary>
        public int Armour { get; set; }

        /// <summary>
        /// Особая защита
        /// </summary>
        public int Ward { get; set; }

        /// <summary>
        ///  нужно будет чтонибудь другое сделать например пределать в enum
        /// </summary>
        public string Param { get; set; }

        protected UnitBase()
        {
            Name = "";
            MovePoints = 0;
            WeaponSkill = 0;
            ShootSkill = 0;
            Strength = 0;
            Resistance = 0;
            LifePoints = 0;
            Initiative = 0;
            AttackPoints = 0;
            Leadership = 0;
            Armour = 0;
            Ward = 0;
            Param = "";

            Action = new ActionBase(this);
        }
    }
}
