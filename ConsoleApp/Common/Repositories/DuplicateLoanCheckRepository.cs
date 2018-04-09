using EllieMae.Encompass.Collections;
using EllieMae.Encompass.Query;
using EllieMae.Encompass.Reporting;
using ConsoleApp.Common.Model;
using ConsoleApp.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Common.Repositories
{
    public class DuplicateLoanCheckRepository : IDuplicateLoanCheckRepository
    {
        public DuplicateCheckResult CompassDuplicateLoanCheck(EncompassSession encompassSession, DuplicateCheckPoints duplicateCheckPoints)
        {
            DuplicateCheckResult result = new DuplicateCheckResult();
            StringList searchFields = new StringList();
            searchFields.Add("Loan.LoanNumber"); // LoanNumber
            searchFields.Add("Fields.4002"); // BorrowerLastName
            searchFields.Add("Fields.65"); // BorrowerLastSSN
            searchFields.Add("Fields.11"); // Property Address
            searchFields.Add("Fields.12"); // Property City
            searchFields.Add("Fields.14"); // Property State
            searchFields.Add("Fields.15"); // Property ZipCode
            searchFields.Add("Fields.19"); // Loan Purpose
            searchFields.Add("Fields.2288"); // Lender Loan Number

            var cnmlsCriterion = new StringFieldCriterion("Fields.3237", duplicateCheckPoints.CompanyNMLS, StringFieldMatchType.Exact, true);
            var activeCriterion = new StringFieldCriterion("Fields.1393", "Active Loan", StringFieldMatchType.Exact, true);

            var searchCriteria = cnmlsCriterion.And(activeCriterion);

            LoanReportDataList results;
            using (var cursor = encompassSession.Session.Reports.OpenReportCursor(searchFields, searchCriteria))
            {
                results = cursor.GetItems(0, cursor.Count); //Pull the result set in one shot
                cursor.Close(); //Release the server resources
            }

            List<DuplicateCheckPoints> duplicateLoans = new List<DuplicateCheckPoints>();

            if (results != null)
            {
                foreach (LoanReportData data in results)
                {
                    // It is possible for the loan number to be blank.  Ignore those.
                    var loanNumber = data["Loan.LoanNumber"].ToString();
                    if (string.IsNullOrWhiteSpace(loanNumber))
                    {
                        continue;
                    }

                    duplicateLoans.Add(new DuplicateCheckPoints()
                    {
                        LoanNumber = data["Loan.LoanNumber"].ToString(),
                        BorrowerLastName = data["Fields.4002"].ToString(),
                        //PropertyAddress = data["Fields.11"].ToString(),
                        //PropertyCity = data["Fields.12"].ToString(),
                        PropertyState = data["Fields.14"].ToString(),
                        //PropertyZipCode = data["Fields.15"].ToString(),
                        LoanPurpose = data["Fields.19"].ToString(),
                        LenderLoanNumber = data["Fields.2288"].ToString()
                    });
                }
            }

            string matchString = "No match found";
            if (duplicateLoans.Count != 0)
            {
                var possibleDups = new List<DuplicateCheckPoints>();

                if (!string.IsNullOrWhiteSpace(duplicateCheckPoints.BorrowerLastName))
                {
                    var lastNameDups = duplicateLoans.Where(x => x.BorrowerLastName.Equals(duplicateCheckPoints.BorrowerLastName));
                    possibleDups.AddRange(lastNameDups);
                }

                if (!string.IsNullOrWhiteSpace(duplicateCheckPoints.PropertyState))
                {
                    var lastNameDups = duplicateLoans.Where(x => x.PropertyState.Equals(duplicateCheckPoints.PropertyState));
                    possibleDups.AddRange(lastNameDups);
                }
                                
                if (!string.IsNullOrWhiteSpace(duplicateCheckPoints.LenderLoanNumber))
                {
                    var lenderLoanNumberDups = duplicateLoans.Where(x => x.LenderLoanNumber.Equals(duplicateCheckPoints.LenderLoanNumber));
                    possibleDups.AddRange(lenderLoanNumberDups);
                }

                if (!string.IsNullOrWhiteSpace(duplicateCheckPoints.LoanPurpose))
                {
                    var loanPurposeDups = duplicateLoans.Where(x => x.LoanPurpose.Equals(duplicateCheckPoints.LoanPurpose));
                    possibleDups.AddRange(loanPurposeDups);
                }

                var matchResult = new List<string>();

                if (possibleDups.Count > 0)
                {
                    string mostMatchesLoanId = possibleDups.GroupBy(x => x.LoanNumber).OrderByDescending(y => y.Count()).First().Key;

                    DuplicateCheckPoints mostMatchesLoan = duplicateLoans.Where(x => x.LoanNumber.Equals(mostMatchesLoanId)).FirstOrDefault();
                    if (mostMatchesLoan == null)
                    {
                        throw new Exception("Unexpected error: duplicateLoan is null.");
                    }

                    if (!string.IsNullOrWhiteSpace(mostMatchesLoan.BorrowerLastName) && mostMatchesLoan.BorrowerLastName.Equals(duplicateCheckPoints.BorrowerLastName))
                    {
                        matchResult.Add("Last Name");
                    }

                    if (!string.IsNullOrWhiteSpace(mostMatchesLoan.PropertyState) && mostMatchesLoan.PropertyState.Equals(duplicateCheckPoints.PropertyState))
                    {
                        matchResult.Add("Property Address");
                    }

                    if (!string.IsNullOrWhiteSpace(mostMatchesLoan.LoanPurpose) && mostMatchesLoan.LoanPurpose.Equals(duplicateCheckPoints.LoanPurpose))
                    {
                        matchResult.Add("Loan Purpose");
                    }

                    if (!string.IsNullOrWhiteSpace(mostMatchesLoan.LenderLoanNumber) && mostMatchesLoan.LenderLoanNumber.Equals(duplicateCheckPoints.LenderLoanNumber))
                    {
                        matchResult.Add("Lender Loan Number");
                    }

                    matchString = string.Format("Loan ID: {0} - matches {1}", mostMatchesLoanId, string.Join(",", matchResult));
                }

                switch (matchResult.Count)
                {
                    case 0:
                    case 1:
                    case 2:
                        result.Result = DuplicateCheckStatus.Success.ToString();
                        break;
                    case 3:
                        result.Result = DuplicateCheckStatus.Validate.ToString();
                        break;
                    default:
                        result.Result = DuplicateCheckStatus.Fail.ToString();
                        break;
                }
            }
            else
            {
                result.Result = DuplicateCheckStatus.Success.ToString();
            }

            result.MatchResults = matchString;
            return result;
        }
        
        private bool ShouldCompare(string value1, string value2)
        {
            if (string.IsNullOrWhiteSpace(value1) && string.IsNullOrWhiteSpace(value2))
            {
                return false;
            }

            return true;
        }

    }
}
