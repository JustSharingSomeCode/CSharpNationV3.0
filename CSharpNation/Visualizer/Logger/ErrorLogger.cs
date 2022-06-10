using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Logger
{
    public static class ErrorLogger
    {
        public static event EventHandler OnErrorAdded;

        public static List<Error> Errors { get; private set; } = new List<Error>();

        public static void AddError(Error.Type errorType, string errorMessage)
        {
            Error error = new Error(errorType, errorMessage);
            Errors.Add(error);

            OnErrorAdded?.Invoke(error, EventArgs.Empty);
        }

        public static void Clear()
        {
            Errors.Clear();
        }
    }
}
