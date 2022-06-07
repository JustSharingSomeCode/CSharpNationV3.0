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

namespace CSharpNation.GUI.Controls
{
    /// <summary>
    /// Lógica de interacción para TextureDetails.xaml
    /// </summary>
    public partial class TextureDetails : UserControl
    {
        public TextureDetails()
        {
            InitializeComponent();

            InitializeComboBoxValues();
        }

        private Texture texture;
        private bool Handled = false;

        public Texture Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;

                if(texture == null)
                {
                    NothingSelectedGrid.Visibility = Visibility.Visible;
                    TextureDetailsGrid.Visibility = Visibility.Hidden;
                }
                else
                {
                    NothingSelectedGrid.Visibility = Visibility.Hidden;
                    TextureDetailsGrid.Visibility = Visibility.Visible;

                    UpdateDetails();
                }                
            }
        }

        private void UpdateDetails()
        {
            NameLbl.Content = texture.Name;

            Handled = true;
            DisplayModeCb.SelectedItem = texture.DisplayMode.ToString();
            Handled = false;

            BlurTxt.Text = texture.BlurSigma.ToString();
        }

        private void InitializeComboBoxValues()
        {
            List<string> displayModes = Enum.GetNames(typeof(Texture.Display)).ToList();

            for (int i = 1; i < displayModes.Count; i++)
            {
                DisplayModeCb.Items.Add(displayModes[i]);
            }
        }

        private void DisplayModeCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if(Handled)
            {
                return;
            }

            texture.DisplayMode = (Texture.Display)Enum.Parse(typeof(Texture.Display), DisplayModeCb.SelectedValue.ToString());
        }

        private void BlurTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                try
                {
                    float sigma = float.Parse(BlurTxt.Text);
                    texture.BlurSigma = sigma;
                }
                catch
                {
                    BlurTxt.Text = texture.BlurSigma.ToString();
                }
            }
        }
    }
}
