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
using System.Windows.Shapes;

namespace CSharpNation.GUI
{
    /// <summary>
    /// Lógica de interacción para CSharpNationGUI.xaml
    /// </summary>
    public partial class CSharpNationGUI : Window
    {
        public CSharpNationGUI()
        {
            InitializeComponent();
            InitializeUi();
        }

        private void InitializeUi()
        {
            Loaded += (sender, args) =>
            {                
                WPFUI.Appearance.Watcher.Watch(this, WPFUI.Appearance.BackgroundType.Mica, true, true);
            };
        }

        private void ChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            var newTheme = WPFUI.Appearance.Theme.GetAppTheme() == WPFUI.Appearance.ThemeType.Dark
            ? WPFUI.Appearance.ThemeType.Light
            : WPFUI.Appearance.ThemeType.Dark;

            WPFUI.Appearance.Theme.Apply(
                themeType: newTheme,
                backgroundEffect: WPFUI.Appearance.BackgroundType.Mica,
                updateAccent: true,
                forceBackground: false);
        }

        private void RootNavigation_Navigated(WPFUI.Controls.Interfaces.INavigation sender, WPFUI.Common.RoutedNavigationEventArgs e)
        {

        }
    }
}
