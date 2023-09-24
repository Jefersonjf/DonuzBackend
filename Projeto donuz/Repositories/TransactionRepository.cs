using Microsoft.AspNetCore.Mvc;
using Projeto_donuz.Model;

namespace Projeto_donuz.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private List<Transacao> transactions = new List<Transacao>();

        public IEnumerable<Transacao> GetAll()
        {
            return transactions;
        }

        public Transacao GetById(int id)
        {
            return transactions.FirstOrDefault(t => t.Id == id);
        }

        public void Add(Transacao transaction)
        {
            transactions.Add(transaction);
        }
    }
}
