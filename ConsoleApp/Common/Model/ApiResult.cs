using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Common.Model
{
    public abstract class ApiResult
    {
        public bool IsSuccess { get; set; }
        public bool IsException { get; set; }
        public string ErrorMessage { get; set; }
    }
}
