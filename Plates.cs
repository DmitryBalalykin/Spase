using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Csharp_Lesson1
{
    internal class Plates : BaseObject
    {
        /// <summary>
        /// Начальная позиция летающей тарелки
        /// </summary>
        /// <param name="pos">Первая точка</param>
        /// <param name="dir">Вторая точка</param>
        /// <param name="size">Размер</param>
        public Plates(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Создаём летающую тарелку.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.Red, Pos.X, Pos.Y, Size.Width, Size.Height / 2);
        }

        /// <summary>
        /// Движение летающей тарелки.
        /// </summary>
        public override void Update()
        {

            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;   
            //Dir.X = -Dir.X;
            //this.Pos.X = Pos.X - Dir.X;
            //this.Pos.Y = Pos.Y + Dir.Y;
            //if (Pos.X < 0) Dir.X = -Dir.X;
            //if (Pos.X > Game.Width) Dir.X = -Dir.X;
            //if (Pos.Y < 0) Dir.Y = -Dir.Y;
            //if (Pos.Y > Game.Heing) Dir.Y = -Dir.Y;
        }
    }
}
