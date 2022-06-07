using CSharpNation.Visualizer.Textures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Config
{
    public static class BackgroundsConfig
    {
        public static void Initialize()
        {
            LoadConfig();

            Path = @"D:\USB\Pxv";
            Opacity = 70;

            Backgrounds = new Backgrounds(Path);
        }

        public static string Path { get; set; }
        public static int Opacity { get; set; }

        public static Backgrounds Backgrounds { get; private set; }

        private static string[] Config;

        public static void LoadConfig()
        {
            if(File.Exists(ConfigInit.BackgroundsConfigPath))
            {
                Config = File.ReadAllLines(ConfigInit.BackgroundsConfigPath);
            }            
        }

        private static string SearchConfig(string name)
        {
            for(int i = 0; i < Config.Length; i++)
            {
                if (Config[i].Contains(name))
                {
                    return Config[i];
                }
            }

            return null;
        }

        public static void UpdateTextureConfig(Texture texture)
        {
            string config = SearchConfig(texture.Name);            

            if(config != null)
            {
                string[] data = config.Split('|');
                texture.DisplayMode = (Texture.Display)Enum.Parse(typeof(Texture.Display), data[1]);
                texture.BlurSigma = float.Parse(data[2]);
            }
        }

        public static void SaveConfig()
        {            
            List<string> newConfig = new List<string>();

            for(int i = 0; i < Backgrounds.Textures.Length; i++)
            {
                newConfig.Add(Backgrounds.Textures[i].ToString());
            }

            bool exists = false;

            for(int i = 0; i < Config.Length; i++)
            {
                for(int j = 0; j < newConfig.Count; j++)
                {
                    if (newConfig[j].Contains(Config[i].Split('|')[0]))
                    {
                        exists = true;
                        break;
                    }
                }

                if(!exists)
                {
                    newConfig.Add(Config[i]);
                }
                else
                {
                    exists = false;
                }
            }

            File.WriteAllLines(ConfigInit.BackgroundsConfigPath, newConfig.ToArray());
        }
    }
}
