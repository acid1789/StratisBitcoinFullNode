using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace Stratis.Bitcoin.Features.CoinTracker
{
    public class CoinTrackerLoop : IDisposable
    {
        Thread _thread;
        ManualResetEvent _killEvent;

        public CoinTrackerLoop()
        {
            _killEvent = new ManualResetEvent(false);
            _thread = new Thread(new ThreadStart(ThreadFunc)) { Name = "CoinTracker" };
            _thread.Start();
        }

        public void Stop()
        {
            if (_thread != null)
            {
                _killEvent.Set();
                _thread.Join();
                _thread = null;
            }
        }

        void ThreadFunc()
        {
            // Load in persistant data
            CoinFile coinFile = new CoinFile();

            // Process blocks forever
            while (true)
            {
                if (_killEvent.WaitOne(0))
                    break;

                //if (coinFile.Height < blockChain.Height)
                {
                }

                // Yield
                Thread.Sleep(10);
            }            
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _killEvent.Dispose();
                    _killEvent = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CoinTrackerLoop() {
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
