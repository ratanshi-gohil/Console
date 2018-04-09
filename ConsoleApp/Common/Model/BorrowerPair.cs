using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Common.Model
{
    public class BorrowerPair
    {
        public int Index { get; set; }
        public Borrower PrimaryBorrower { get; set; }
        public Borrower CoBorrower { get; set; }

        public bool HasData()
        {
            return (PrimaryBorrower != null && PrimaryBorrower.HasData()) ||
                (CoBorrower != null && CoBorrower.HasData());
        }
    }
}
