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

            Backgrounds = new Backgrounds(Path);
        }

        private static string path;
        public static string Path
        {
            get { return path; }
            set
            {
                path = value;
                Backgrounds = new Backgrounds(path);
            }
        }

        public static int Opacity { get; set; } = 100;

        public static Backgrounds Backgrounds { get; private set; }

        private static string[] config;
        private static string[] bgConfig;

        public static void LoadConfig()
        {
            if (File.Exists(ConfigInit.BackgroundsConfigPath))
            {
                bgConfig = File.ReadAllLines(ConfigInit.BackgroundsConfigPath);
            }

            if (File.Exists(ConfigInit.ConfigPath))
            {
                config = File.ReadAllLines(ConfigInit.ConfigPath);

                string path = ConfigInit.SearchConfig(config, "BgPath");
                string opacity = ConfigInit.SearchConfig(config, "Opacity");

                Path = path == null ? "" : path;
                Opacity = opacity == null ? 100 : int.Parse(opacity);
            }            
        }

        private static string SearchConfig(string name)
        {
            for(int i = 0; i < bgConfig.Length; i++)
            {
                if (bgConfig[i].Contains(name))
                {
                    return bgConfig[i];
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

            for(int i = 0; i < bgConfig.Length; i++)
            {
                for(int j = 0; j < newConfig.Count; j++)
                {
                    if (newConfig[j].Contains(bgConfig[i].Split('|')[0]))
                    {
                        exists = true;
                        break;
                    }
                }

                if(!exists)
                {
                    newConfig.Add(bgConfig[i]);
                }
                else
                {
                    exists = false;
                }
            }

            File.WriteAllLines(ConfigInit.BackgroundsConfigPath, newConfig.ToArray());

            config = new string[2];
            config[0] = "BgPath=" + Path;
            config[1] = "Opacity=" + Opacity.ToString();

            File.WriteAllLines(ConfigInit.ConfigPath, config);
        }
    }
}
