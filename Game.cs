using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Game_Csharp_Lesson1
{
    internal class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        /// <summary>
        /// Ширина поля.
        /// </summary>
        public static int Width { get; set; }

        /// <summary>
        /// Высота поля
        /// </summary>
        public static int Heing { get; set; }

        /// <summary>
        /// Колличество очков.
        /// </summary>
        public static int C { get; set; } = 0;

        /// <summary>
        /// Создаем массив объектов.
        /// </summary>
        public static BaseObject[] _objs;

        /// <summary>
        /// Создаем пулю.
        /// </summary>
        private static List<Bullet> _bullets = new List<Bullet>();

        /// <summary>
        /// Создаем массив из астеройдов.
        /// </summary>
        private static Asteroid[] _asteroids;

        /// <summary>
        /// Создаем массив из планет.
        /// </summary>
        private static Plates[] _plates;

        /// <summary>
        /// Статический объект корабля.
        /// </summary>
        private static Ship _ship = new Ship(new Point(10,400),new Point(5,5), new Size(10,10));

        /// <summary>
        /// Создаём объекты (звезды, астеройды, летающие тарелки) в нашем космосе.
        /// </summary>
        public static void Load()
        {
            _objs = new BaseObject[50];
            _asteroids = new Asteroid[50];
            _plates = new Plates[10];

            var rnd = new Random();

            for (int i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(rnd.Next(0, Game.Width), rnd.Next(0, Game.Heing)), new Point(-r, r), new Size(3, 3));
            }
            for (int i = 0; i < _asteroids.Length ; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(rnd.Next(0,Game.Width), rnd.Next(0, Game.Heing)), new Point(-r / 5, r), new Size(r, r));
            }

            for (int i = 0; i < _plates.Length; i++)
            {
                int r = rnd.Next(3, 20);
                _plates[i] = new Plates(new Point(rnd.Next(0, Game.Width), rnd.Next(0, Game.Heing)), new Point(-r * 2, r), new Size(r, r));
            }
        }

        /// <summary>
        /// Завершение игры.
        /// </summary>
        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60,FontStyle.Underline), Brushes.White, 200,100);
            Buffer.Render();
        }

        private static Timer _timer = new Timer() { Interval = 100 };
        public static Random Rnd = new Random();

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Обновляем и проверям на ошибки фон.
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            try
            {
                Width = form.ClientSize.Width;
                Heing = form.ClientSize.Height;
                if (Width > 1000 || Width < 0) throw new ArgumentOutOfRangeException();
                if(Heing > 1000 || Heing < 0) throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Неверно указаны ширина или высота поля.");
            }
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Heing));
            form.KeyDown += From_KeyDown;
            Ship.MessageDie += Finish;

            //Timer _timer = new Timer { Interval = 100 };
            _timer.Start();
            _timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Обработчик событий управления кораблём.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void From_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _bullets.Add(new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }



        /// <summary>
        /// +Вывод энергии корабля
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }
            foreach (Bullet b in _bullets) b.Draw();
            _ship?.Draw();
            if (_ship != null)
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            if (C != 0)
                Buffer.Graphics.DrawString("Number of points:" + Convert.ToString(C) , SystemFonts.DefaultFont, Brushes.RosyBrown, 15, 15);
            foreach (Plates obj in _plates)
                obj.Draw();
            Buffer.Render();
        }

        /// <summary>
        /// Движение и перемещение объектов по космосу.
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            foreach (Bullet b in _bullets) b.Update();
            foreach (Plates obj in _plates)
                obj.Update();
            for (int i = 0; i < _plates.Length; i++)
            {
                if (_plates[i] == null) continue;
                if (!_ship.Collision(_plates[i])) continue;
                var rnd1 = new Random();
                _ship?.EnergyTall(rnd1.Next(1,10));
            }

            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                for (int j = 0; j < _bullets.Count; j++)
                if(_asteroids[i] != null && _bullets[j].Collision(_asteroids[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _asteroids[i] = null;
                    _bullets.RemoveAt(j);
                    C++;
                    j--;
                }
                if (_asteroids[i] == null || !_ship.Collision(_asteroids[i])) continue;
                _ship.EnergyLow(Rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship.Die();
            }           
        }
    }
}
