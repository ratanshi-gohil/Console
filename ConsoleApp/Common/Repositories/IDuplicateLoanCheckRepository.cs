using ConsoleApp.Common.Model;
using ConsoleApp.Common.Wrappers;

namespace ConsoleApp.Common.Repositories
{
    public interface IDuplicateLoanCheckRepository
    {
        DuplicateCheckResult CompassDuplicateLoanCheck(EncompassSession encompassSession, DuplicateCheckPoints duplicateCheckPoints);
    }
}