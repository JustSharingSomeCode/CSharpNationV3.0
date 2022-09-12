using CSharpNation.Visualizer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharpNation.GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para Particles.xaml
    /// </summary>
    public partial class Particles : Page
    {
        public Particles()
        {
            InitializeComponent();

            TexturePathTxt.Text = ParticlesConfig.TexturePath;
            MaxParticlesNb.Value = ParticlesConfig.MaxParticles;
            BlurSigmaNb.Value = ParticlesConfig.BlurSigma;
            WaveFrequencyNb.Value = ParticlesConfig.WaveFrequency;
            WaveAmplitudeNb.Value = ParticlesConfig.WaveAmplitude;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            ParticlesConfig.SaveConfig();
        }

        private void TexturePathTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ParticlesConfig.TexturePath = TexturePathTxt.Text;
            }
        }

        private void MaxParticlesNb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ParticlesConfig.MaxParticles = (int)MaxParticlesNb.Value;
        }

        private void BlurSigmaNb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ParticlesConfig.BlurSigma = (float)BlurSigmaNb.Value;
        }

        private void WaveFrequencyNb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ParticlesConfig.WaveFrequency = (float)WaveFrequencyNb.Value;
        }

        private void WaveAmplitudeNb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ParticlesConfig.WaveAmplitude = (float)WaveAmplitudeNb.Value;
        }
    }
}
