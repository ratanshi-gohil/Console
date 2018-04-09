using ConsoleApp.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Common.Model
{
    public class CreateLoanRequest
    {
        public CreateLoanRequest()
        {
            SubjectProperty = new SubjectProperty();
            BorrowerPairs = new List<BorrowerPair>();
            Locks = new List<Model.LoanCreateLocks>();
        }

        public SubjectProperty SubjectProperty { get; set; }
        public List<BorrowerPair> BorrowerPairs { get; set; }
        public List<LoanCreateLocks> Locks { get; set; }

        public string LenderLoanNumber { get; set; }
        public string DeliveryOption { get; set; }
        public string MandatoryLoanType { get; set; }
        public string BidIdNumber { get; set; }
        public string LoanProgram { get; set; }
        public decimal? InterestRate { get; set; }
        public string CreditScoreforDecisionMaking { get; set; }
        public decimal? LoanAmount { get; set; }
        public string PropertyType { get; set; }
        public string PropertyWillBe { get; set; }
        public int? NumberOfUnits { get; set; }
        public string LoanPurpose { get; set; }
        public string PurposeOfRefinance { get; set; }
        public string BuildingStatus { get; set; }
        public string CorrespondentDelegated { get; set; }
        public Guid Id { get; set; }
        public string DocumentationType { get; set; }
        public string CompanyNmls { get; set; }
        public string Company { get; set; }
        public decimal? LTV { get; set; }
        public decimal? CLTV { get; set; }
        public string Channel { get; set; }
    }

    public class LoanCreateLocks
    {
        public string DeliveryOption { get; set; }
        public decimal? TotalMargin { get; set; }
        public decimal? FinalPrice { get; set; }
        public DateTime? LockDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
