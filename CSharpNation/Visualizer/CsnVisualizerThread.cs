using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CSharpNation.Visualizer
{
    public class CsnVisualizerThread
    {
        private Thread vth;
        private CsnVisualizer visualizer;

        public CsnVisualizerThread()
        {
            vth = new Thread(VisualizerThread);
        }

        public void StartVisualizer()
        {
            if (vth.IsAlive)
            {
                MessageBoxResult result = MessageBox.Show("The visualizer is actually running, ¿Do you want to start a new one?", "Visualizer actually running", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    visualizer.Close();

                    StartVisualizerThread();
                }
            }
            else
            {
                StartVisualizerThread();
            }
        }

        private void StartVisualizerThread()
        {
            vth = new Thread(VisualizerThread);

            vth.Start();
        }

        private void VisualizerThread()
        {
            using (visualizer = new CsnVisualizer(1280, 720, "CSharpNation"))
            {
                visualizer.Run(60.0f, 60.0f);
            }
        }

        public void Cleanup()
        {
            if (vth.IsAlive)
            {
                visualizer.Close();
            }
        }

        public void MakeDump()
        {
            if (vth.IsAlive)
            {
                visualizer.WaveDump();
            }
        }
    }
}
