using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.BusinessObjects.Loans.Templates;
using EllieMae.Encompass.Client;
using EllieMae.Encompass.Collections;
using EllieMae.Encompass.Query;
using ConsoleApp.Common;
using ConsoleApp.Common.Model;
using System;

namespace ConsoleApp.Common.Wrappers
{
    public class LoanWrapper : IDisposable
    {
        public string LoanNumber { get; set; }
        private Loan _Loan;
        private ISession _Session;
        private bool _HasLoan = false;        

        public LoanWrapper(ISession session)
        {
            _Session = session;            
        }
        
        public Loan CreateLoan()
        {
            _Loan = _Session.Loans.CreateNew();
            //this.Log().Debug("LoanWrapper: Created new empty loan.");

            return _Loan;
        }        

        public Loan GetLoan (string loanNumber, ApiLockType lockType)
        {
            GetLoanFromLoanNumber(loanNumber, lockType);

            return _Loan;
        }

        private void GetLoanFromLoanNumber(string loanNumber, ApiLockType lockType)
        {
            StringFieldCriterion c = new StringFieldCriterion();
            c.FieldName = "Loan.LoanNumber";
            c.Value = loanNumber;
            c.MatchType = StringFieldMatchType.Exact;
            var loanList = _Session.Loans.Query(c);
            if (loanList == null || loanList.Count < 1 || string.IsNullOrWhiteSpace(loanList[0].Guid))
            {
                return;
            }

            _Loan = _Session.Loans.Open(loanList[0].Guid);
            if (_Loan == null)
            {
                return;
            }

            _HasLoan = true;
            LoanNumber = loanNumber;

            //this.Log().Debug("LoanWrapper: Opened loan {0}.", LoanNumber);

            Lock(lockType);
        }

        public void Lock(ApiLockType lockType)
        {
            if (lockType == ApiLockType.Lock)
            {
                _Loan.Lock();
                //this.Log().Debug("LoanWrapper: Locked loan {0}.", LoanNumber);
            }
            else if (lockType == ApiLockType.ForceLock)
            {
                _Loan.ForceLock();
                //this.Log().Debug("LoanWrapper: Force locked loan {0}.", LoanNumber);
            }
        }
        
        public bool HasLoan
        {
            get { return _HasLoan; }
        }

        public Loan Loan
        {
            get { return _Loan; }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        if (_Loan != null)
                        {
                            if (_Loan.Locked || _Loan.LockedExclusive)
                            {
                                _Loan.Unlock();
                                //this.Log().Debug("LoanWrapper: Unlocked loan {0}.", LoanNumber);
                            }
                            else
                            {
                                //this.Log().Debug("LoanWrapper: Loan {0} already not locked.", LoanNumber);
                            }

                            _Loan.Close();
                            //this.Log().Debug("LoanWrapper: Closed loan {0}.", LoanNumber);
                        }
                        else
                        {
                            //this.Log().Debug("LoanWrapper: No current loan to close.");
                        }

                        // Dispose managed state (managed objects).
                    }
                    catch (Exception e)
                    {
                        string msg = string.Format("Exception disposing of Encompass loan {0}: {1} : {2}.", LoanNumber, e.Message, e.StackTrace);
                        //this.Log().Error(msg, e);
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~LoanWrapper() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
