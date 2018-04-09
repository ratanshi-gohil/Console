using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Common.Model
{
    public class CreateLoanResponse : ApiResult
    {
        public string LoanGUID { get; set; }
        public string LoanNumber { get; set; }
    }
}


