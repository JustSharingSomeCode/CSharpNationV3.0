using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Config
{
    internal class ConfigInit
    {
        public static void Initialize()
        {
            if(!Initialized)
            {
                CheckFiles();
                InitializeConfig();

                Initialized = true;
            }            
        }

        private static bool Initialized = false;

        public static string ConfigDirectoryPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\CSharpNationV5_0";
        public static string ConfigPath { get; } = ConfigDirectoryPath + @"\Config.txt";
        public static string BackgroundsConfigPath { get; } = ConfigDirectoryPath + @"\Backgrounds.txt";
        public static string ParticlesConfigPath { get; } = ConfigDirectoryPath + @"\Particles.txt";
        public static string LogoConfigPath { get; } = ConfigDirectoryPath + @"\Logo.txt";
        public static string AnalyzerConfigPath { get; } = ConfigDirectoryPath + @"\Analyzer.txt";

        private static void CheckFiles()
        {
            if (!Directory.Exists(ConfigDirectoryPath))
            {
                Directory.CreateDirectory(ConfigDirectoryPath);
            }

            CheckFile(ConfigPath);

            CheckFile(BackgroundsConfigPath);
            CheckFile(ParticlesConfigPath);
            CheckFile(LogoConfigPath);
            CheckFile(AnalyzerConfigPath);
        }

        private static void CheckFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        private static void InitializeConfig()
        {
            AnalyzerConfig.Initialize();
            LogoConfig.Initialize();
            BackgroundsConfig.Initialize();
        }

        public static string SearchConfig(string[] config, string param)
        {
            for(int i = 0; i < config.Length; i++)
            {
                if (config[i].Contains(param))
                {
                    return config[i].Split('=')[1];
                }
            }

            return null;
        }
    }
}
