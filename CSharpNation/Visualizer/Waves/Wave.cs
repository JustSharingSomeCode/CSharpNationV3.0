using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSharpNation.Visualizer.Config;

namespace CSharpNation.Visualizer.Waves
{
    public class Wave
    {
        public Wave(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public List<float> Spectrum { get; set; }
        public List<float> PreviousSpectrum { get; set; }

        public double Quality { get; set; } = 0.1;

        private float X { get; set; }
        private float Y { get; set; }

        public List<Vector2> CatmullRomPoints { get; private set; } = new List<Vector2>();

        public void Update(List<float> spectrum, float x, float y, float radius)
        {
            if (spectrum == null)
            {
                return;
            }

            X = x;
            Y = y;

            PreviousSpectrum = Spectrum;
            Spectrum = WaveTools.SmoothWave(spectrum, PreviousSpectrum, 0.5f);
            Spectrum = WaveTools.NanToZero(Spectrum);

            UpdatePoints(X, Y, radius);
        }

        public void Draw(bool debug = false)
        {
            if (Spectrum == null)
            {
                return;
            }

            for (int j = 0; j < CatmullRomPoints.Count - 1; j++)
            {
                GL.Color3(Color.FromArgb(255, R, G, B));
                GL.Begin(PrimitiveType.Triangles);

                GL.Vertex2(CatmullRomPoints[j]);
                GL.Vertex2(CatmullRomPoints[j + 1]);
                GL.Vertex2(X, Y);

                GL.End();
            }


            for (int j = 0; j < CatmullRomPoints.Count - 1; j++)
            {
                GL.Color3(Color.FromArgb(255, R, G, B));
                GL.Begin(PrimitiveType.Triangles);

                GL.Vertex2(MirrorPosition(X, CatmullRomPoints[j]), CatmullRomPoints[j].Y);
                GL.Vertex2(MirrorPosition(X, CatmullRomPoints[j + 1]), CatmullRomPoints[j + 1].Y);
                GL.Vertex2(X, Y);

                GL.End();
            }


            if (debug)
            {
                DrawDebug();
            }
        }


        private float MirrorPosition(float x, Vector2 vector)
        {
            return vector.X - ((vector.X - x) * 2);
        }


        private void UpdatePoints(float x, float y, float circleRadius)
        {
            if (Spectrum == null)
            {
                return;
            }

            CatmullRomPoints.Clear();

            Vector2 p1, p2, p3, p4;

            for (int i = 0; i < Spectrum.Count - 1; i++)
            {
                p1 = GetPosition(x, y, WaveTools.Clamp(0, Spectrum.Count, i - 1), circleRadius);

                p2 = GetPosition(x, y, i, circleRadius);
                p3 = GetPosition(x, y, i + 1, circleRadius);

                p4 = GetPosition(x, y, WaveTools.Clamp(0, Spectrum.Count - 1, i + 2), circleRadius);

                for (double j = 0; j <= 1; j += Quality)
                {
                    CatmullRomPoints.Add(CatmullRom((float)j, p1, p2, p3, p4));
                }
            }
        }



        private Vector2 GetPosition(float x, float y, int i, float circleRadius)
        {
            double degreesIncrement = 180f / (AnalyzerConfig.Lines - 1.0f);
            double rads = Math.PI * (i * degreesIncrement) / 180;

            double PosX = x + (Math.Sin(rads) * (Spectrum[i] + circleRadius));
            double PosY = y + (Math.Cos(rads) * (Spectrum[i] + circleRadius));

            return new Vector2((float)PosX, (float)PosY);
        }

        public static Vector2 CatmullRom(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            Vector2 a = 2f * p1;
            Vector2 b = p2 - p0;
            Vector2 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
            Vector2 d = -p0 + 3f * p1 - 3f * p2 + p3;

            Vector2 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

            return pos;
        }


        private void DrawDebug()
        {
            for (int j = 0; j < Spectrum.Count; j++)
            {
                GL.Begin(PrimitiveType.Quads);
                GL.Color3(Color.FromArgb(255, R, G, B));

                GL.Vertex2(j * 10, 0);
                GL.Vertex2(j * 10, Spectrum[j]);
                GL.Vertex2(j * 10 + 10, Spectrum[j]);
                GL.Vertex2(j * 10 + 10, 0);

                GL.End();
            }
        }

        public void Dump()
        {
            if (Spectrum != null)
            {
                for (int i = 0; i < Spectrum.Count; i++)
                {
                    Console.WriteLine("{0}:{1}", i, Spectrum[i]);
                }
            }
            else
            {
                Console.WriteLine("Null Spectrum");
            }

            Console.WriteLine("----");

            if (PreviousSpectrum != null)
            {
                for (int i = 0; i < PreviousSpectrum.Count; i++)
                {
                    Console.WriteLine("{0}:{1}", i, PreviousSpectrum[i]);
                }
            }
            else
            {
                Console.WriteLine("Null PreviousSpectrum");
            }
        }
    }
}
