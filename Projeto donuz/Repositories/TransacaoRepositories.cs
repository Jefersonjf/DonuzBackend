using Projeto_donuz.Model;

namespace Projeto_donuz.Repositories
{
    public class TransacaoRepositories : ITransacaoRepositories
    {
        private List<Transacao> _transacoes = new List<Transacao>();

        public IEnumerable<Transacao> ObterTodasTransacoes()
        {
            return _transacoes;
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            transacao.Id = _transacoes.Any() ? _transacoes.Max(t => t.Id) + 1 : 1;
            transacao.Date = DateTime.UtcNow;
            _transacoes.Add(transacao);
        }
    }
}
