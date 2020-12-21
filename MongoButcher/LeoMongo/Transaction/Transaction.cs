using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace LeoMongo.Transaction
{
    public sealed class Transaction : IDisposable
    {
        private readonly IClientSessionHandle _session;
        private bool _committed;

        public Transaction(IClientSessionHandle session)
        {
            this._session = session;
        }

        public void Dispose()
        {
            if (!this._committed)
            {
                // we usually don't want to die during dispose, so try/catch & log would be nice here
                this._session.AbortTransaction();
            }

            this._session.Dispose();
        }

        public async Task CommitAsync()
        {
            await this._session.CommitTransactionAsync();
            this._committed = true;
        }

        public Task RollbackAsync()
        {
            return this._session.AbortTransactionAsync();
        }
    }
}