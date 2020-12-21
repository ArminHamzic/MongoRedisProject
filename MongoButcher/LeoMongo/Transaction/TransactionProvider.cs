using System;
using System.Threading.Tasks;
using LeoMongo.Database;
using MongoDB.Driver;
using Nito.AsyncEx;

namespace LeoMongo.Transaction
{
    internal sealed class TransactionProvider : ITransactionProvider, ISessionProvider
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly AsyncLock _mutex;
        private IClientSessionHandle? _session;

        public TransactionProvider(IDatabaseProvider databaseProvider)
        {
            this._databaseProvider = databaseProvider;
            this._mutex = new AsyncLock();
        }

        public IClientSessionHandle Session
        {
            get
            {
                if (!InTransaction || this._session == null)
                {
                    throw new InvalidOperationException("transaction not started");
                }

                return this._session;
            }
        }

        public bool InTransaction { get; private set; }

        public async Task<Transaction> BeginTransaction()
        {
            using (await this._mutex.LockAsync())
            {
                if (InTransaction)
                {
                    throw new InvalidOperationException("transaction already started");
                }

                this._session = await this._databaseProvider.StartSession();
                this._session.StartTransaction();
                InTransaction = true;
                return new Transaction(this._session);
            }
        }
    }
}