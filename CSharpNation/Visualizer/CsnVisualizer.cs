using CSharpNation.Visualizer.Analyzer;
using CSharpNation.Visualizer.Particles;
using CSharpNation.Visualizer.Textures;
using CSharpNation.Visualizer.Waves;
using CSharpNation.Visualizer.Config;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpNation.GUI.ProgressControl;

namespace CSharpNation.Visualizer
{
    internal class CsnVisualizer : GameWindow
    {
        internal CsnVisualizer(int width, int height, string title) : base(width, height, new GraphicsMode(new ColorFormat(8, 8, 8, 0), 24, 8, 8), title)
        {
            //CsnConfig.Initialize();
            ConfigInit.Initialize();

            ProgressReport.Progress = 0;

            Waves = new List<Wave>
            {
                new Wave(255, 255, 255),
                new Wave(255, 255, 0),
                new Wave(255, 150, 0),
                new Wave(255, 0, 0),
                new Wave(255, 100, 255),
                new Wave(50, 50, 155),
                new Wave(0, 0, 255),
                new Wave(50, 200, 255),
                new Wave(0, 255, 0)
            };

            ProgressReport.Progress = 10;

            Analyzer = AnalyzerConfig.SpectrumAnalyzer;
            Analyzer.multiplier = height / 3;

            ProgressReport.Progress = 20;

            //Logo = LogoConfig.Logo;
            Logo = new Logo(LogoConfig.TexturePath);
            Logo.BlurSigma = LogoConfig.BlurSigma;
            Logo.LoadTexture();
            Logo.Size = height / 2;

            ProgressReport.Progress = 40;

            Backgrounds = BackgroundsConfig.Backgrounds;
            Backgrounds.LoadTextures();

            ProgressReport.Progress = 90;

            random = new Random();
            particles = new ParticlesController(width, height);
            particles.LoadTexture();

            ProgressReport.Progress = 100;
        }

        private List<Wave> Waves;
        private SpectrumAnalyzer Analyzer;
        private Logo Logo;
        private Backgrounds Backgrounds;
        private ParticlesController particles;

        private Random random;

        private float Power { get; set; } = 0;

        private float posX, posY;
        private float Rx, Ry;
        private int Dx = -1, Dy = 1;
        private int ShakeCount = 0;

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(new Color4(150, 150, 150, 255));

            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0f, Width, 0.0f, Height, 0.0f, 1.0f);

            Analyzer.multiplier = Height / 3;
            Logo.Size = Height / 2;
            Backgrounds.UpdateScales(Width, Height);
            particles.UpdateBoundaries(Width, Height);

            base.OnResize(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            List<float> actualSpectrum = WaveTools.FixDiscontinuities(Analyzer.GetSpectrum());
            actualSpectrum = WaveTools.AvgWave(actualSpectrum, 1);
            actualSpectrum = WaveTools.NanToZero(actualSpectrum);

            Power = CalculateWavePower(actualSpectrum);
            float waveSize = Logo.Size / 2 + Power;

            CalculateShake();

            posX = Width / 2 + Rx;
            posY = Height / 2 + Ry;

            for (int i = 0; i < Waves.Count; i++)
            {
                if (i == 0)
                {
                    Waves[i].Update(actualSpectrum, posX, posY, waveSize);
                }
                else
                {
                    if (Waves[i - 1].PreviousSpectrum == null)
                    {
                        Console.WriteLine("null spec");
                        continue;
                    }

                    Waves[i].Update(WaveTools.AvgWave(Waves[i - 1].PreviousSpectrum, 1), posX, posY, waveSize);
                }
            }

            particles.Update(Power);

            base.OnUpdateFrame(e);
        }

        private void CalculateShake()
        {
            switch (ShakeCount)
            {
                case 0:
                    Dx = -1;
                    Dy = 1;
                    break;
                case 1:
                    Dx = 1;
                    Dy = -1;
                    break;
                case 2:
                    Dx = -1;
                    Dy = -1;
                    break;
                case 3:
                    Dx = 1;
                    Dy = 1;
                    ShakeCount = 0;
                    break;
            }

            //float pw = WaveTools.Clamp(Power - (0.015f * Height), 0.0f, float.MaxValue); //ajustar
            float pw = WaveTools.Clamp(Power - (15 * (Height / 720f)), 0.0f, float.MaxValue); //ajustar

            Rx = (float)random.NextDouble() * (pw * 0.5f) * Dx;
            Ry = (float)random.NextDouble() * (pw * 0.5f) * Dy;

            ShakeCount++;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            int opacity = (int)(BackgroundsConfig.Opacity / 100.0f * 255);

            Backgrounds.DrawBackground(0, 0, Width, Height, 255, opacity, opacity, opacity, Power / 2);

            particles.Draw();

            for (int i = Waves.Count - 1; i >= 0; i--)
            {
                Waves[i].Draw();
            }

            float logoSize = Logo.Size / 2.0f + Power;
            float x = Width / 2 + Rx;
            float y = Height / 2 + Ry;
            Logo.DrawTexture(x - logoSize, y - logoSize, x + logoSize, y + logoSize, 255, 255, 255, 255);


            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }

        private float CalculateWavePower(List<float> spectrum)
        {
            float power = 0;

            for (int i = 0; i < spectrum.Count; i++)
            {
                power += spectrum[i];
            }

            power /= spectrum.Count;

            return power;
        }


        public void WaveDump()
        {
            for (int i = 0; i < Waves.Count; i++)
            {
                Console.WriteLine("Wave {0}", i);
                Waves[i].Dump();
            }
        }
    }
}
