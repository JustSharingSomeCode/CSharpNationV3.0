using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Config
{
    public static class WavesConfig
    {
        public static bool EnableGlow { get; set; } = true;

        public static float GlowSize { get; set; } = 20;
        public static float MaxGlowAlpha { get; set; } = 80;
        public static float GlowThreshold { get; set; } = 40;
    }
}
