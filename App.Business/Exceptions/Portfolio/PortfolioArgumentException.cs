using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Exceptions.Portfolio
{
    public class PortfolioArgumentException : Exception
    {
        public string ParamName { get; set; }
        public PortfolioArgumentException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}
