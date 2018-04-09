using ConsoleApp.Common.Model;
using ConsoleApp.Common.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp.Common.Repositories
{
    public interface ILoanRepository
    {
        void CreateLoans(List<CreateLoanRequest> request);
        DuplicateCheckResult CompassDuplicateLoanCheck(EncompassSession encompassSession, DuplicateCheckPoints duplicateCheckPoints);
        CreateLoanResponse CreateEncompassLoan(CreateLoanRequest request, EncompassSession encompassSession, DuplicateCheckResult duplicateCheckResult, Dictionary<string, long> timings);
        LoanData GetLoan(string loanNumber);
    }
}