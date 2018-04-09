using ConsoleApp.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Common.Model
{
    public class LoanData
    {
        public LoanData()
        {
            SubjectProperty = new SubjectProperty();
            BorrowerPairs = new List<BorrowerPair>();            
        }

        public SubjectProperty SubjectProperty { get; set; }
        public List<BorrowerPair> BorrowerPairs { get; set; }
    }    
}
