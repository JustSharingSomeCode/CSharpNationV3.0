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

namespace CSharpNation.GUI.Controls
{
    /// <summary>
    /// Lógica de interacción para ErrorDetails.xaml
    /// </summary>
    public partial class ErrorDetails : UserControl
    {
        public ErrorDetails()
        {
            InitializeComponent();
        }

        private Error error;
        public Error Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
                UpdateDetails();
            }
        }

        private void UpdateDetails()
        {
            if(error.ErrorType == Error.Type.Fatal)
            {
                ControlBorder.SetResourceReference(Border.BorderBrushProperty, "SystemFillColorCriticalBrush");
                ControlBorder.SetResourceReference(Border.BackgroundProperty, "SystemFillColorCriticalBackgroundBrush");

                MessageTxt.Text = "Fatal error: \n" + error.ErrorMessage;
                MessageTxt.SetResourceReference(TextBlock.ForegroundProperty, "SystemFillColorCriticalBrush");
            }
            else
            {
                ControlBorder.SetResourceReference(Border.BorderBrushProperty, "SystemFillColorCautionBrush");
                ControlBorder.SetResourceReference(Border.BackgroundProperty, "SystemFillColorCautionBackgroundBrush");

                MessageTxt.Text = "Information: \n" + error.ErrorMessage;
                MessageTxt.SetResourceReference(TextBlock.ForegroundProperty, "SystemFillColorCautionBrush");
            }
        }
    }
}
