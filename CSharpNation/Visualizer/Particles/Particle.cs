using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Particles
{
    public class Particle
    {
        public Particle(float x, float y, float speedX, float speedY, float size, int dirY, int opacity)
        {
            X = x;
            Y = y;

            if (speedX <= 0.05f && speedY <= 0.05f)
            {
                speedX = 0.2f;
            }


            xSpeed = speedX;
            ySpeed = speedY;

            Size = size;
            HalfSize = size / 2.0f;

            yDir = dirY;

            Opacity = opacity;
        }

        public float X { get; set; }
        public float xSpeed { get; set; } // 0 to 1

        public float Y { get; set; }
        public float ySpeed { get; set; } // 0 to 1

        public float Size { get; set; }
        public float HalfSize { get; set; }

        public int yDir { get; set; } // 1 | -1

        public int Opacity { get; set; }

        public float SinAngle { get; set; } = 0;

        public void Update(float multiplier)
        {
            if (multiplier < 1)
            {
                multiplier = 1;
            }

            //0.02f = adjusts frequency
            SinAngle += xSpeed * 0.02f * multiplier;

            if(SinAngle >= 360)
            {
                SinAngle = 0;
            }

            X += xSpeed * multiplier;
            Y += ySpeed * multiplier * yDir;
        }
    }
}
