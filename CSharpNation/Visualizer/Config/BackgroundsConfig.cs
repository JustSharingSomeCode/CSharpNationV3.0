using CSharpNation.Visualizer.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Config
{
    public static class BackgroundsConfig
    {
        public static void Initialize()
        {
            Path = @"D:\Backgrounds\Geo";
            Opacity = 80;

            Backgrounds = new Backgrounds(Path);
        }

        public static string Path { get; set; }
        public static int Opacity { get; set; }

        public static Backgrounds Backgrounds { get; private set; }
    }
}
