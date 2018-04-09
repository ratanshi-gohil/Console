using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Common.Model
{
    public class Borrower
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Ssn { get; set; }
        public string Experianfico { get; set; }

        public bool HasData()
        {
            return !string.IsNullOrWhiteSpace(FirstName) ||
                !string.IsNullOrWhiteSpace(LastName) ||
                !string.IsNullOrWhiteSpace(Ssn);
        }
    }   
}
