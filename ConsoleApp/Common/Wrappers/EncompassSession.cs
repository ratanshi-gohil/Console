using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.Client;
using EllieMae.Encompass.Query;
using ConsoleApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Common.Wrappers
{
    public class EncompassSession : IDisposable, IEncompassSession
    {
        private Session _Session;
        private string EncompassServerUri;
        private string EncompassUserName;
        private string EncompassPassword;

        public EncompassSession()
        {
            _Session = new Session();
        }

        public void Open()
        {
            _Session.Start(EncompassServerUri, EncompassUserName, EncompassPassword);
        }

        public ISession Session
        {
            get { return _Session; }
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
                        // Dispose managed state (managed objects).
                        _Session.End();
                        //this.Log().Debug("EncompassSession: Disposed Session");
                    }
                    catch (Exception e)
                    {
                        string msg = string.Format("Exception disposing of Encompass session: {0} : {1}.", e.Message, e.StackTrace);
                        //this.Log().Error(msg, e);
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EncompassSession() {
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
