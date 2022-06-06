using CSharpNation.Visualizer.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Particles
{
    public class ParticlesController
    {
        public ParticlesController(int width, int height)
        {
            Particles = new List<Particle>();
            random = new Random();

            texture = new Texture(@"C:\ProgramData\CSharpNationV2.0\Resources\Particle.png");

            //Initialize random
            for (int i = 0; i < 1000; i++)
            {
                random.NextDouble();
            }

            UpdateBoundaries(width, height);
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int MaxParticles { get; set; } = 500;

        private Random random;
        private Texture texture;

        public List<Particle> Particles { get; set; }

        public void UpdateBoundaries(int width, int height)
        {
            Width = width;
            Height = height;

            X = width / 2;
            Y = height / 2;
        }

        public void Update(float multiplier)
        {
            while (Particles.Count < MaxParticles)
            {
                Particles.Add(new Particle(X, Y, (float)random.NextDouble(), (float)random.NextDouble(), random.Next(5, 20), random.NextDouble() <= 0.4f ? 1 : -1, random.Next(200, 256)));
            }

            for (int i = 0; i < Particles.Count; i++)
            {
                if (IsOutOfBoundaries(Particles[i]))
                {
                    Particles.Remove(Particles[i]);
                    i--;
                    continue;
                }

                Particles[i].Update(multiplier);
            }
        }

        private bool IsOutOfBoundaries(Particle p)
        {
            if (p.X < 0 || p.Y < 0 || p.X > Width || p.Y > Height)
            {
                return true;
            }

            return false;
        }

        public void Draw()
        {
            if (texture.LoadFailed)
            {
                return;
            }

            Particle p;
            float dist;

            for (int i = 0; i < Particles.Count; i++)
            {
                p = Particles[i];
                texture.DrawTexture(p.X - p.HalfSize, p.Y - p.HalfSize, p.X + p.HalfSize, p.Y + p.HalfSize, p.Opacity, 255, 255, 255);

                dist = p.X - X;

                texture.DrawTexture(X - dist - p.HalfSize, p.Y - p.HalfSize, X - dist + p.HalfSize, p.Y + p.HalfSize, p.Opacity, 255, 255, 255);
            }
        }

        public void LoadTexture()
        {
            texture.LoadTexture();
        }
    }
}
