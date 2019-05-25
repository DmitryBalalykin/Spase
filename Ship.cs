using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Csharp_Lesson1
{
    class Ship : BaseObject
    {
        public static event Message MessageDie;

        /// <summary>
        /// Количество жизней корабля.
        /// </summary>
        private int _energy = 100;

        /// <summary>
        /// Колличетво жизней в ремонтном наборе.
        /// </summary>
        private int _energy1 = 10;

        public int Energy => _energy;

        /// <summary>
        /// Получение повреждений кораблю.
        /// </summary>
        /// <param name="n">Колличество повреждений.</param>
        public void EnergyLow(int n)
        {
            _energy -= n;
        }

        /// <summary>
        /// Ремонт корабля
        /// </summary>
        /// <param name="n">Количество жизней</param>
        public void EnergyTall(int n)
        {
            _energy += n;
        }

        /// <summary>
        /// Точка появления корабля
        /// </summary>
        /// <param name="pos">Первая точка</param>
        /// <param name="dir">Вторая точка</param>
        /// <param name="size">Размер</param>
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Прорисовка корабля.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Движение корабля.
        /// </summary>
        public override void Update()
        {
        }

        /// <summary>
        /// Движение корабря вверх.
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        /// <summary>
        /// Движение корабля вниз.
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Game.Heing) Pos.Y = Pos.Y + Dir.Y;
        }
        /// <summary>
        /// Смерть корабля.
        /// </summary>
        public void Die() => MessageDie?.Invoke();

    }
}
