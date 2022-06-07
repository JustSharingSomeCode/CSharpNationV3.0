using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.GUI.ProgressControl
{
    public static class ProgressReport
    {
        public static event EventHandler OnProgressChanged;

        private static int progress = 0;
        public static int Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                OnProgressChanged?.Invoke(null, EventArgs.Empty);
            }
        }
    }
}
