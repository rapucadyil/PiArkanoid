using System;
using Explorer700Library;
using System.Drawing;
using System.Collections.Generic;

namespace PiArkanoid
{

    class Program
    {
        static List<Enemy> GenerateEnemies()
        {
            var enemies = new List<Enemy>();
            var rng = new Random();
            for (int i = 0; i < 5; i++)
            {
                enemies.Add(new Enemy(new Vector2(rng.Next(4, 120)+4, rng.Next(0, 4)+4)));
            }
            return enemies;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("PiArkanoid");
            Explorer700 exp = new Explorer700();
            Graphics g = exp.Display.Graphics;
            Pen pen = new Pen(Brushes.White);
            Pcf8574 pcf = new Pcf8574(0x20);
            Player p = new Player(4, 56, pcf);;
            List<Enemy> enemies = GenerateEnemies();
            while (true)
            {
                for (int i = 0; i < p.Projectiles.Count; i++)
                {
                    if (enemies.Count > 0)
                    {
                        for(int j = 0; j < enemies.Count; j++)
                        {
                            if (enemies[j].CollidesWith(p.Projectiles[i]))
                            {
                                enemies[j] = null;
                                enemies.Remove(enemies[j]);
                                GC.Collect();
                            }
                        }
                    }
                }
                if (p != null)
                {
                    p.Tick();
                    p.Draw(pen, g);
                }

                if (enemies.Count > 0)
                {
                    foreach(Enemy en in enemies)
                    {
                        en.Tick();
                        en.Draw(pen, g);
                    }
                }
                exp.Display.Update();
                g.Clear(Color.Black);
            }


        }
    }
}
