﻿using CSharpNation.GUI.ProgressControl;
using CSharpNation.Visualizer.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Textures
{
    public class Backgrounds
    {
        public Backgrounds(string path)
        {
            Path = path;

            LoadFiles();
        }
        
        private string Path { get; set; }

        public Texture[] Textures { get; private set; }
        public int ActualBackground { get; private set; } = -1;

        private void LoadFiles()
        {
            if (!Directory.Exists(Path))
            {
                return;
            }

            string[] jpgFiles = Directory.GetFiles(Path, "*.jpg");
            string[] pngFiles = Directory.GetFiles(Path, "*.png");

            Textures = new Texture[jpgFiles.Length + pngFiles.Length];

            for (int i = 0; i < jpgFiles.Length; i++)
            {
                Textures[i] = new Texture(jpgFiles[i]);
                BackgroundsConfig.UpdateTextureConfig(Textures[i]);
            }

            for (int i = 0; i < pngFiles.Length; i++)
            {
                Textures[i + jpgFiles.Length] = new Texture(pngFiles[i]);
                BackgroundsConfig.UpdateTextureConfig(Textures[i + jpgFiles.Length]);
            }

            Debug.WriteLine("Loaded {0} backgrounds", Textures.Length);
        }

        public void LoadTextures()
        {
            if (Textures == null)
            {
                return;
            }

            int progressStart = 40;
            double progressStep = 50.0 / Textures.Length;

            for (int i = 0; i < Textures.Length; i++)
            {
                Textures[i].LoadTexture();
                ProgressReport.Progress = progressStart + (int)(progressStep * (i + 1));
            }

            if(ActualBackground == -1)
            {
                Next();
            }            
        }

        public void UpdateScales(int width, int height)
        {
            if (Textures == null)
            {
                return;
            }

            for (int i = 0; i < Textures.Length; i++)
            {
                Textures[i].UpdateScale(width, height);
            }
        }
        /*
        public void DrawBackground(float x, float y, float xMax, float yMax, int a, int r, int g, int b)
        {
            Textures[ActualBackground].DrawTexture(x, y, xMax, yMax, a, r, g, b);
        }
        */
        public void DrawBackground(float x, float y, float xMax, float yMax, int a, int r, int g, int b, float power)
        {
            if (ActualBackground < 0)
            {
                return;
            }

            Texture td = Textures[ActualBackground];

            float wp = power * td.WidthRatio;
            float hp = wp * td.HeightRatio;

            if (xMax % 2 != 0)
            {
                xMax++;
            }

            switch (td.DisplayMode)
            {
                case Texture.Display.Fullscreen:
                    td.DrawTexture(x - td.FillX - wp, y - td.FillY - hp, xMax + td.FillX + wp, yMax + td.FillY + hp, a, r, g, b);
                    break;

                case Texture.Display.Halfscreen:
                case Texture.Display.MirroredLeftHalf:
                case Texture.Display.MirroredRightHalf:

                    //left side
                    td.DrawTexture(x - (td.FillX * 2.0f) - (wp * 2.0f), y - td.FillY - hp, xMax / 2.0f, yMax + td.FillY + hp, a, r, g, b);

                    //right side
                    td.DrawTexture(xMax + (td.FillX * 2.0f) + (wp * 2.0f), 0.0f - td.FillY - hp, xMax / 2.0f, yMax + td.FillY + hp, a, r, g, b);
                    break;
            }
        }

        public void Next()
        {
            if (Textures == null)
            {
                return;
            }

            ActualBackground++;
            if (ActualBackground >= Textures.Length)
            {
                ActualBackground = 0;
            }

            if (Textures[ActualBackground].LoadFailed)
            {
                Next();
            }
        }

        public void Previous()
        {
            if (Textures == null)
            {
                return;
            }

            ActualBackground--;
            if (ActualBackground < 0)
            {
                ActualBackground = Textures.Length - 1;

            }

            if (Textures[ActualBackground].LoadFailed)
            {
                Previous();
            }
        }

        public void SetActualBackground(int value)
        {
            if (value >= 0 && value < Textures.Length)
            {
                ActualBackground = value;
            }
        }
    }
}
