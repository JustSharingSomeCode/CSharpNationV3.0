using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace CSharpNation.Visualizer.Textures
{
    public class Texture
    {
        public Texture(string path)
        {
            Path = path;
        }

        public enum Display
        {
            NotFound,
            Fullscreen,
            Halfscreen,
            MirroredLeftHalf,
            MirroredRightHalf
        }

        public string Path { get; set; }
        public string Name
        {
            get
            {
                return Path.Split('\\').Last();
            }
        }
        public int TextureHandle { get; set; }
        public float BlurSigma { get; set; }

        public bool LoadFailed { get; private set; }
        public Display DisplayMode { get; set; } = Display.Halfscreen;

        public int OriginalWidth { get; private set; }
        public int OriginalHeight { get; private set; }

        public float WidthRatio { get; private set; }
        public float HeightRatio { get; private set; }

        public float ActualWidth { get; private set; }
        public float ActualHeight { get; private set; }

        public float FillY { get; private set; }
        public float FillX { get; private set; }

        public void LoadTexture()
        {
            try
            {
                TextureHandle = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, TextureHandle);

                //Load the image
                using (Image<Rgba32> image = Image.Load<Rgba32>(Path))
                {
                    OriginalWidth = image.Width;
                    OriginalHeight = image.Height;

                    WidthRatio = (float)OriginalWidth / OriginalHeight;
                    HeightRatio = (float)OriginalHeight / OriginalWidth;

                    switch (DisplayMode)
                    {
                        case Display.MirroredLeftHalf:
                            OriginalWidth /= 2;
                            image.Mutate(x => x.Crop(image.Width / 2, image.Height));
                            break;
                        case Display.MirroredRightHalf:
                            OriginalWidth /= 2;
                            image.Mutate(x => x.Crop(new Rectangle(image.Width / 2, 0, image.Width / 2, image.Height)));
                            break;
                    }

                    if(BlurSigma != 0)
                    {
                        image.Mutate(x => x.GaussianBlur(BlurSigma));
                    }                    

                    //Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
                    var pixels = new List<byte>(4 * image.Width * image.Height);

                    for (int y = 0; y < image.Height; y++)
                    {
                        var row = image.GetPixelRowSpan(y);

                        for (int x = 0; x < image.Width; x++)
                        {
                            pixels.Add(row[x].R);
                            pixels.Add(row[x].G);
                            pixels.Add(row[x].B);
                            pixels.Add(row[x].A);
                        }
                    }

                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.MirroredRepeat);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.MirroredRepeat);
                }

                GC.Collect();
            }
            catch (Exception ex)
            {
                LoadFailed = true;
                //Console.WriteLine(ex.Message);
                Debug.WriteLine("Failed loading texture at: {0}", Path);
                Debug.WriteLine(ex.ToString());
            }
        }

        public void UpdateScale(float screenWidth, float screenHeight)
        {
            if (TextureHandle == -1)
            {
                return;
            }

            if (DisplayMode != Display.Fullscreen)
            {
                screenWidth /= 2.0f;
            }

            float missingWidth = screenWidth - OriginalWidth;
            float missingHeight = screenHeight - OriginalHeight;

            if (missingWidth > missingHeight)
            {
                ActualWidth = screenWidth;
                ActualHeight = screenWidth * HeightRatio;
            }
            else
            {
                ActualHeight = screenHeight;
                ActualWidth = screenHeight * WidthRatio;
            }

            if (ActualHeight < screenHeight)
            {
                float scale = screenHeight / ActualHeight;
                ActualHeight *= scale;
                ActualWidth *= scale;
            }

            if (ActualWidth < screenWidth)
            {
                float scale = screenWidth / ActualWidth;
                ActualHeight *= scale;
                ActualWidth *= scale;
            }

            FillY = (ActualHeight - screenHeight) / 2.0f;
            FillX = (ActualWidth - screenWidth) / 2.0f;
        }

        public void DrawTexture(float x, float y, float xMax, float yMax, int a, int r, int g, int b)
        {
            if (TextureHandle == -1)
            {
                return;
            }

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, TextureHandle);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Color4(System.Drawing.Color.FromArgb(a, r, g, b));
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1);
            GL.Vertex2(x, y);

            GL.TexCoord2(0, 0);
            GL.Vertex2(x, yMax);

            GL.TexCoord2(1, 0);
            GL.Vertex2(xMax, yMax);

            GL.TexCoord2(1, 1);
            GL.Vertex2(xMax, y);

            GL.End();
            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Blend);
        }
    }
}
