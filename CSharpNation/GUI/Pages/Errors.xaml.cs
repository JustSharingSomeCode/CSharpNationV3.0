using CSharpNation.GUI.Controls;
using CSharpNation.Visualizer.Logger;
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
    /// Lógica de interacción para Errors.xaml
    /// </summary>
    public partial class Errors : Page
    {
        public Errors()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            ErrorLogger.OnErrorAdded += ErrorLogger_OnErrorAdded;

            if (ErrorLogger.Errors.Count != 0)
            {
                for(int i = 0; i < ErrorLogger.Errors.Count; i++)
                {
                    AddError(ErrorLogger.Errors[i]);
                }
            }
        }

        private void ErrorLogger_OnErrorAdded(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                AddError((Error)sender);
            }));
        }

        private void AddError(Error error)
        {
            ErrorDetails details = new ErrorDetails()
            {
                Error = error,
                Margin = new Thickness(0, 0, 0, 5),
            };

            ErrorsStack.Children.Add(details);
            ScrollStack.ScrollToBottom();
        }

        private void ClearLogBtn_Click(object sender, RoutedEventArgs e)
        {
            //AddError(new Error(Error.Type.Fatal, "Fatal error demo"));
            ErrorsStack.Children.Clear();
        }

        private void SaveLogBtn_Click(object sender, RoutedEventArgs e)
        {
            //AddError(new Error(Error.Type.Information, "Information error demo"));
        }
    }
}
