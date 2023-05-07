using CSharpNation.Visualizer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Particles
{
    public class Particle
    {
        public Particle(float x, float y, float speedX, float speedY, float waveInit, float size, int dirY, int opacity)
        {
            _x = x;
            _y = y;

            if (speedX <= 0.05f && speedY <= 0.05f)
            {
                speedX = 0.2f;
            }

            SinAngle = waveInit;

            xSpeed = speedX;
            ySpeed = speedY;

            Size = size;
            HalfSize = size / 2.0f;

            yDir = dirY;

            Opacity = opacity;
        }

        private float _x, _y;

        public float X { get; set; }
        public float xSpeed { get; set; } // 0 to 1

        public float Y { get; set; }
        public float ySpeed { get; set; } // 0 to 1

        public float Size { get; set; }
        public float HalfSize { get; set; }

        public int yDir { get; set; } // 1 | -1

        public int Opacity { get; set; }

        public float SinAngle { get; set; } = 0;

        private float sinWave;

        public void Update(float multiplier)
        {
            if (multiplier < 1)
            {
                multiplier = 1;
            }

            //0.02f = adjusts frequency
            //SinAngle += xSpeed * 0.02f * multiplier;
            SinAngle += xSpeed * ParticlesConfig.WaveFrequency * multiplier;

            if (SinAngle >= 360)
            {
                SinAngle = 0;
            }

            _x += xSpeed * multiplier;
            _y += ySpeed * multiplier * yDir;


            X = _x;
            //Y += ySpeed * multiplier * yDir;
            sinWave = (float)Math.Sin(SinAngle) * ParticlesConfig.WaveAmplitude;
            Y = _y + sinWave;
        }
    }
}
