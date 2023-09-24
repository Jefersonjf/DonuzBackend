using Projeto_donuz.Model;

namespace Projeto_donuz.Repositories
{
    public interface ITransacaoRepositories
    {
        IEnumerable<Transacao> ObterTodasTransacoes();
        void AdicionarTransacao(Transacao transacao);
    }
}
