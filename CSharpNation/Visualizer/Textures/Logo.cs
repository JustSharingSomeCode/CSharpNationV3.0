using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Textures
{
    public class Logo : Texture
    {
        public Logo(string path) : base(path)
        {
            DisplayMode = Display.Fullscreen;
        }

        public float Size { get; set; }
        //logo offset
    }
}
