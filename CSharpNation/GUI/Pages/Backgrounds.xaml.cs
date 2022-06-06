using CSharpNation.Visualizer.Config;
using CSharpNation.Visualizer.Textures;
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

            UpdateBackgroundsList();
        }        

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            BackgroundsConfig.Backgrounds.Next();
        }

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            BackgroundsConfig.Backgrounds.Previous();
        }       
        
        private void UpdateBackgroundsList()
        {
            TextureDetails.Texture = null;
            BackgroundsList.Items.Clear();

            Texture[] textures = BackgroundsConfig.Backgrounds.Textures;

            for(int i = 0; i < textures.Length; i++)
            {
                BackgroundsList.Items.Add(textures[i].Path);
            }
        }        

        private void BackgroundsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(BackgroundsList.SelectedIndex >= 0 && BackgroundsList.SelectedIndex < BackgroundsConfig.Backgrounds.Textures.Length)
            {
                TextureDetails.Texture = BackgroundsConfig.Backgrounds.Textures[BackgroundsList.SelectedIndex];
            }            
        }
    }
}
