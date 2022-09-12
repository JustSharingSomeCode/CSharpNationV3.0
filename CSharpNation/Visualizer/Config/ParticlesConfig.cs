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

        public static string TexturePath { get; set; } = ConfigInit.ResourcesDirectoryPath + @"\Particle.png";
        public static int MaxParticles { get; set; } = 500;
        public static float BlurSigma { get; set; } = 0;
        public static float WaveFrequency { get; set; } = 0.02f;
        public static float WaveAmplitude { get; set; } = 8;

        private static string[] config;

        public static void LoadConfig()
        {
            if (!File.Exists(ConfigInit.ParticlesConfigPath))
            {
                return;
            }

            config = File.ReadAllLines(ConfigInit.ParticlesConfigPath);

            string path = ConfigInit.SearchConfig(config,"TexturePath");
            string max = ConfigInit.SearchConfig(config, "MaxParticles");
            string blur = ConfigInit.SearchConfig(config, "BlurSigma");
            string waveFreq = ConfigInit.SearchConfig(config, "WaveFrequency");
            string waveAmpl = ConfigInit.SearchConfig(config, "WaveAmplitude");

            TexturePath = (path == null || path == "") ? ConfigInit.ResourcesDirectoryPath + @"\Particle.png" : path;
            MaxParticles = max == null ? 500 : int.Parse(max);
            BlurSigma = blur == null ? 0 : float.Parse(blur);
            WaveFrequency = waveFreq == null ? 0.02f : float.Parse(waveFreq);
            WaveAmplitude = waveAmpl == null ? 8 : float.Parse(waveAmpl);
        }

        public static void SaveConfig()
        {
            config = new string[5];
            config[0] = "TexturePath=" + TexturePath;
            config[1] = "MaxParticles=" + MaxParticles;
            config[2] = "BlurSigma=" + BlurSigma;
            config[3] = "WaveFrequency=" + WaveFrequency;
            config[4] = "WaveAmplitude=" + WaveAmplitude;

            File.WriteAllLines(ConfigInit.ParticlesConfigPath, config);
        }
    }
}
