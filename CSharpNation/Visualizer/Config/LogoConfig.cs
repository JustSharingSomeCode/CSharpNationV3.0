using CSharpNation.Visualizer.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Config
{
    public static class LogoConfig
    {
        public static void Initialize()
        {
            TexturePath = ConfigInit.ResourcesDirectoryPath + @"\Logo.png";
            BlurSigma = 1f;

            Logo = new Logo(TexturePath);
            Logo.BlurSigma = BlurSigma;
        }

        public static string TexturePath { get; set; }
        public static float BlurSigma { get; set; }

        public static Logo Logo { get; private set; }
    }
}
