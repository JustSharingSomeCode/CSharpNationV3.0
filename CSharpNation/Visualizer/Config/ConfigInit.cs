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
            CheckFiles();
        }

        public static string ConfigDirectoryPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\CSharpNationV5_0";
        public static string ConfigPath { get; } = ConfigDirectoryPath + @"\Config.txt";
        public static string BackgroundsConfigPath { get; } = ConfigDirectoryPath + @"\Backgrounds.txt";
        public static string ParticlesConfigPath { get; } = ConfigDirectoryPath + @"\Particles.txt";
        public static string LogoConfigPath { get; } = ConfigDirectoryPath + @"\Logo.txt";

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
        }

        private static void CheckFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }
    }
}
