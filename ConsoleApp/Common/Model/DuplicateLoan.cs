using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsoleApp.Common.Model
{
    public class DuplicateCheckPoints
    {
        public string LoanNumber { get; set; }
        public string CompanyNMLS { get; set; }
        //public string ImportType { get; set; }        
        public string BorrowerLastName { get; set; }
        //public string BorrowerSSN { get; set; }
        //public string PropertyAddress { get; set; }
        //public string PropertyCity { get; set; }
        public string PropertyState { get; set; }
        //public string PropertyZipCode { get; set; }
        public string LoanPurpose { get; set; }
        public string LenderLoanNumber { get; set; }
    }

    public class DuplicateCheckResult
    {
        public string Result { get; set; }
        public string MatchResults { get; set; }
    }
    
    public enum DuplicateCheckStatus
    {
        Success,
        Validate,
        Fail
    }
}