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
    /// Lógica de interacción para Logo.xaml
    /// </summary>
    public partial class Logo : Page
    {
        public Logo()
        {
            InitializeComponent();

            TexturePathTxt.Text = LogoConfig.TexturePath;
            BlurSigmaNb.Value = LogoConfig.BlurSigma;
        }

        private void TexturePathTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LogoConfig.TexturePath = TexturePathTxt.Text;
            }
        }

        private void BlurSigmaNb_TextChanged(object sender, TextChangedEventArgs e)
        {
            LogoConfig.BlurSigma = (float)BlurSigmaNb.Value;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            LogoConfig.SaveConfig();
        }
    }
}
