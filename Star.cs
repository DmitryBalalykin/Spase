using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Csharp_Lesson1
{
    internal class Star : BaseObject
    {
        /// <summary>
        /// Начальная позиция звезд
        /// </summary>
        /// <param name="pos">Первая точка</param>
        /// <param name="dir">Вторая точка</param>
        /// <param name="size">Размер звезды</param>
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Создаём звезду.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        /// <summary>
        /// Движение звёзд.
        /// </summary>
        public override void Update()
        {
            this.Pos.X = Pos.X - Dir.X;
            this.Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Heing) Dir.Y = -Dir.Y;
        }
    }
}
