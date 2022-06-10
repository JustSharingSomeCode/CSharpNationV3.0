using CSharpNation.Visualizer.Textures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Config
{
    public static class LogoConfig
    {
        public static void Initialize()
        {
            //TexturePath = ConfigInit.ResourcesDirectoryPath + @"\Logo.png";
            //BlurSigma = 1f;

            //Logo = new Logo(TexturePath);
            //Logo.BlurSigma = BlurSigma;

            LoadConfig();
        }

        public static string TexturePath { get; set; } = ConfigInit.ResourcesDirectoryPath + @"\Logo.png";
        public static float BlurSigma { get; set; } = 0;

        private static string[] config;

        //public static Logo Logo { get; private set; }

        public static void LoadConfig()
        {
            if(!File.Exists(ConfigInit.LogoConfigPath))
            {
                return;
            }

            config = File.ReadAllLines(ConfigInit.LogoConfigPath);

            string path = ConfigInit.SearchConfig(config, "TexturePath");
            string blur = ConfigInit.SearchConfig(config, "BlurSigma");

            TexturePath = (path == null || path == "") ? ConfigInit.ResourcesDirectoryPath + @"\Logo.png" : path;
            BlurSigma = blur == null ? 0 : float.Parse(blur);
        }

        public static void SaveConfig()
        {
            config = new string[2];
            config[0] = "TexturePath=" + TexturePath;
            config[1] = "BlurSigma=" + BlurSigma;

            File.WriteAllLines(ConfigInit.LogoConfigPath, config);
        }
    }
}
