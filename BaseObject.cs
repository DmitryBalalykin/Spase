using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;


namespace Game_Csharp_Lesson1
{
    abstract class BaseObject : ICollision
    {
        /// <summary>
        /// Первая точка.
        /// </summary>
        protected Point Pos;

        /// <summary>
        /// Вторая точка.
        /// </summary>
        protected Point Dir;

        /// <summary>
        /// Третья точка.
        /// </summary>
        protected Size Size;

        public delegate void Message();

        /// <summary>
        /// Начальная позиция объекта.
        /// </summary>
        /// <param name="pos">Первая точка</param>
        /// <param name="dir">Вторая точка</param>
        /// <param name="size">Размер</param>
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public Rectangle Rect => new Rectangle(Pos,Size);

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        /// <summary>
        /// Создаём объект
        /// </summary>
        public abstract void Draw();

        //{
        //    Image image = new Bitmap("Apple.gif");
        ////Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        //Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        //}

        /// <summary>
        /// Движение объектов.
        /// </summary>
        public abstract void Update();
        //{
        //    Pos.X = Pos.X + Dir.X;
        //    //Pos.Y = Pos.Y + Dir.Y;
        //    if (Pos.X < 0) Pos.X = Game.Width + Size.Width;   //Dir.X = -Dir.X;
        //    //if (Pos.X > Game.Width) Dir.X = -Dir.X;
        //    //if (Pos.Y < 0) Dir.Y = -Dir.Y;
        //    //if (Pos.Y > Game.Heing) Dir.Y = -Dir.Y;
        //}
    }
}
