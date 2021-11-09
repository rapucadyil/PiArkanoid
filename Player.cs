using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer700Library;
using System.Drawing;
namespace PiArkanoid
{

    public class Player {

        public static float HORIZONTAL_VERTEX_OFFSET = 2;
        public static float VERTICAL_VERTEX_OFFSET = 6;

        public Vector2 Position { get; set; }

        public Joystick Input { get; set; }

        public Point[] Sprite { get; set; }

        public List<Projectile> Projectiles { get; set; }

        private bool canShoot = true;

        private float coolDown = 1000;

        private float countdown = 0;

        public Player(int start_x, int start_y, Pcf8574 pcf8574)
        {
            Position = new Vector2(start_x, start_y);
            Input = new Joystick(pcf8574);
            Input.JoystickChanged += InputHandler;
            Sprite = new Point[]
            {
                new Point((int)this.Position.X, (int)this.Position.Y),
                new Point((int)(this.Position.X+HORIZONTAL_VERTEX_OFFSET), (int)(this.Position.Y+VERTICAL_VERTEX_OFFSET)),
                new Point((int)(this.Position.X-HORIZONTAL_VERTEX_OFFSET), (int)(this.Position.Y+VERTICAL_VERTEX_OFFSET))
            };
            Projectiles = new List<Projectile>();
        }

        private void InputHandler(object sender, KeyEventArgs e)
        {
            if(e.Keys == Keys.Right)
            {
                this.Position.Move(Vector2.Right, 2.5F);
            } 
            if ((e.Keys == Keys.Up) && canShoot)
            {
                Projectiles.Add(new Projectile(new Vector2(this.Sprite[0].X, this.Sprite[0].Y)));
                canShoot = false;
            }
            if (e.Keys == Keys.Left)
            {
                this.Position.Move(Vector2.Left, 2.5F);
            }
        }

        public void Tick()
        {
            if (!canShoot)
            {
                int current = DateTime.Now.Millisecond;
                countdown++;
                if (countdown + current > coolDown)
                {
                    countdown = 0;
                    canShoot = true;
                }
            }

            // Updating the top vertex position
            this.Sprite[0].X = (int)this.Position.X;
            this.Sprite[0].Y = (int)this.Position.Y;
            // Updating the right vertex position
            this.Sprite[1].X = (int)(this.Position.X+HORIZONTAL_VERTEX_OFFSET);
            this.Sprite[1].Y = (int)(this.Position.Y+VERTICAL_VERTEX_OFFSET);
            // Updating the left vertex position
            this.Sprite[2].X = (int)(this.Position.X - HORIZONTAL_VERTEX_OFFSET);
            this.Sprite[2].Y = (int)(this.Position.Y + VERTICAL_VERTEX_OFFSET);

            if (this.Projectiles.Count > 0)
            {
                foreach (Projectile proj in Projectiles)
                {
                    proj.Tick();
                }
            }
            if(this.Projectiles.Count > 10)
            {
                this.Projectiles.Clear();
            }

        }

        public void Draw(Pen p, Graphics g)
        {
            //g.DrawRectangle(p, this.Position.X, this.Position.Y, GameObject.Width, GameObject.Height);
            g.DrawPolygon(p, this.Sprite);
            if (this.Projectiles.Count > 0)
            {
                foreach (Projectile proj in Projectiles)
                {
                    proj.Draw(p, g);
                }
            }
        }

    }
}
