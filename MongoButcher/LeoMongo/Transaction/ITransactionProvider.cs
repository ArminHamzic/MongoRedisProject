using System.Threading.Tasks;

namespace LeoMongo.Transaction
{
    public interface ITransactionProvider
    {
        bool InTransaction { get; }
        Task<Transaction> BeginTransaction();
    }
}