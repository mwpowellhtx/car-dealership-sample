using System;

namespace Powell.Vehicles
{
    /// <summary>
    /// Provides some boiler plate disposable functionality.
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        /// <summary>
        /// Finalizer
        /// </summary>
        ~Disposable()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Gets whether IsDisposed.
        /// </summary>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            IsDisposed = true;
        }
    }
}
