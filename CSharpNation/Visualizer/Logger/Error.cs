using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Logger
{
    public class Error
    {
        public enum Type
        {
            Fatal,
            Information
        }

        public Error(Type errorType, string errorMessage)
        {
            ErrorType = errorType;
            ErrorMessage = errorMessage;
        }

        public Type ErrorType { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}
