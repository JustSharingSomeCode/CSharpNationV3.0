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
    /// Lógica de interacción para Analyzer.xaml
    /// </summary>
    public partial class Analyzer : Page
    {
        public Analyzer()
        {
            InitializeComponent();

            LinesNb.Value = AnalyzerConfig.Lines;

            List<string> devices = AnalyzerConfig.SpectrumAnalyzer.GetDevices();

            devices.Insert(0, "Default");

            DeviceCb.ItemsSource = devices;
        }

        private void DeviceCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DeviceCb.SelectedIndex == 0)
            {
                AnalyzerConfig.SpectrumAnalyzer.ChangeDevice(0);
            }

            AnalyzerConfig.SpectrumAnalyzer.ChangeDevice(DeviceCb.SelectedIndex - 1);
        }

        private void LinesNb_TextChanged(object sender, TextChangedEventArgs e)
        {
            AnalyzerConfig.Lines = (int)LinesNb.Value;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
