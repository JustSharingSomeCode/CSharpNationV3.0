using CSharpNation.Visualizer.Analyzer;
using System;
using System.Collections.Generic;
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
    }
}
