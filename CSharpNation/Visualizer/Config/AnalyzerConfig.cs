using CSharpNation.Visualizer.Analyzer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Config
{
    public static class AnalyzerConfig
    {
        public static void Initialize()
        {
            Lines = 50;

            SpectrumAnalyzer = new SpectrumAnalyzer() { _lines = Lines };
        }

        public static int Lines { get; set; }

        public static SpectrumAnalyzer SpectrumAnalyzer { get; private set; }

        private static string[] config;


        public static void LoadConfig()
        {
            if (!File.Exists(ConfigInit.AnalyzerConfigPath))
            {
                return;
            }

            config = File.ReadAllLines(ConfigInit.AnalyzerConfigPath);

            string path = ConfigInit.SearchConfig(config, "Lines");

            Lines = (path == null) ? 50 : int.Parse(path);
        }

        public static void SaveConfig()
        {
            config = new string[1];
            config[0] = "Lines=" + Lines;

            File.WriteAllLines(ConfigInit.AnalyzerConfigPath, config);
        }
    }
}
