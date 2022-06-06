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
            TexturePath = @"C:\ProgramData\CSharpNationV2.0\Resources\Logo.png";

            Logo = new Logo(TexturePath);
        }

        public static string TexturePath { get; set; }

        public static Logo Logo { get; private set; }
    }
}
