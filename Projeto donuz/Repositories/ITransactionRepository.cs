using Projeto_donuz.Model;

namespace Projeto_donuz.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transacao> GetAll();
        Transacao GetById(int id);
        void Add(Transacao transaction);
    }
}
