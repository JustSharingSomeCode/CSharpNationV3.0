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
    /// Lógica de interacción para Waves.xaml
    /// </summary>
    public partial class Waves : Page
    {
        public Waves()
        {
            InitializeComponent();

            SizeNb.Value = WavesConfig.GlowSize;
            AlphaNb.Value = WavesConfig.MaxGlowAlpha;
            ThresholdNb.Value = WavesConfig.GlowThreshold;

            GlowCb.IsChecked = WavesConfig.EnableGlow;

            GlowCb.Checked += (sender, args) =>
            {
                WavesConfig.EnableGlow = true;
            };

            GlowCb.Unchecked += (sender, args) =>
            {
                WavesConfig.EnableGlow = false;
            };
        }       

        private void SizeNb_TextChanged(object sender, TextChangedEventArgs e)
        {
            WavesConfig.GlowSize = (float)SizeNb.Value;
        }

        private void AlphaNb_TextChanged(object sender, TextChangedEventArgs e)
        {
            WavesConfig.MaxGlowAlpha = (float)AlphaNb.Value;
        }

        private void ThresholdNb_TextChanged(object sender, TextChangedEventArgs e)
        {
            WavesConfig.GlowThreshold = (float)ThresholdNb.Value;
        }
    }
}
