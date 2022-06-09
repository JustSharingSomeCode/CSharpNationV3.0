using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharpNation.Visualizer.Config
{
    public static class ParticlesConfig
    {
        public static void Initialize()
        {
            LoadConfig();
        }

        public static string TexturePath { get; set; }
        public static int MaxParticles { get; set; }
        public static float BlurSigma { get; set; }

        private static string[] config;

        public static void LoadConfig()
        {
            if (File.Exists(ConfigInit.ParticlesConfigPath))
            {
                config = File.ReadAllLines(ConfigInit.ParticlesConfigPath);
            }

            string path = ConfigInit.SearchConfig(config,"TexturePath");
            string max = ConfigInit.SearchConfig(config, "MaxParticles");
            string blur = ConfigInit.SearchConfig(config, "BlurSigma");

            TexturePath = (path == null || path == "") ? ConfigInit.ResourcesDirectoryPath + @"\Particle.png" : path;
            MaxParticles = max == null ? 500 : int.Parse(max);
            BlurSigma = blur == null ? 0 : float.Parse(blur);
        }

        public static void SaveConfig()
        {
            config = new string[3];
            config[0] = "TexturePath=" + TexturePath;
            config[1] = "MaxParticles=" + MaxParticles;
            config[2] = "BlurSigma=" + BlurSigma;

            File.WriteAllLines(ConfigInit.ParticlesConfigPath, config);
        }
    }
}
