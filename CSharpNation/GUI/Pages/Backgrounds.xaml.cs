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
    /// Lógica de interacción para Backgrounds.xaml
    /// </summary>
    public partial class Backgrounds : Page
    {
        public Backgrounds()
        {
            InitializeComponent();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            BackgroundsConfig.Backgrounds.Next();
        }

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            BackgroundsConfig.Backgrounds.Previous();
        }
    }
}
