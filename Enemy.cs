using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PiArkanoid
{
    public class Enemy
    {
        public Vector2 Position { get; set; }

        public Rectangle Sprite { get; set; }

        private bool canMove = true;

        private float cooldown = 1000;
        private float countdown = 0;

        

        public Enemy(Vector2 pos)
        {
            this.Position = pos;
            this.Sprite = new Rectangle((int)this.Position.X, (int)this.Position.Y, 6, 6);
        }

        public void Tick()
        {
            if(canMove)
            {
                this.Position.Move(Vector2.Down, 2.5f);
                canMove = false;
            }
            if(!canMove)
            {
                int current = DateTime.Now.Millisecond;
                countdown++;
                if(countdown + current > cooldown)
                {
                    countdown = 0;
                    canMove = true;
                }
            }
        }

        public void Draw(Pen p, Graphics g)
        {
            g.DrawRectangle(p, this.Position.X, this.Position.Y, this.Sprite.Width, this.Sprite.Height);       
        }

        public bool CollidesWith(Projectile other)
        {
            return this.Sprite.Contains(new Point((int)other.Position.X, (int)other.Position.Y));
        }
    }
}
