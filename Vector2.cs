using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiArkanoid
{
    public class Vector2
    {

        public static Vector2 Right = new Vector2(1, 0);
        public static Vector2 Left = new Vector2(-1, 0);
        public static Vector2 Up = new Vector2(0, -1);
        public static Vector2 Down = new Vector2(0, 1);

        public float X { get; set; }
        public float Y { get; set; }

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }


        public void Move(Vector2 dir, float multiplier)
        {
            this.X += dir.X * multiplier;
            this.Y += dir.Y * multiplier;
        }
    }
}
