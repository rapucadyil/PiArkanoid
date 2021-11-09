using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace PiArkanoid
{
    public class Projectile
    {

        public Vector2 Position { get; set; }

        public Rectangle Sprite { get; set; }

        public Projectile(Vector2 _pos)
        {
            Position = _pos;
            this.Sprite = new Rectangle((int)Position.X, (int)Position.Y, 2, 2);
        }

        public void Tick()
        {
            this.Position.Move(Vector2.Up, 2f);
        }

        public void Draw(Pen p,Graphics g)
        {
            g.DrawRectangle(p, this.Position.X, this.Position.Y, 2, 2);
        }
    }
}
