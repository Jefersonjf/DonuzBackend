namespace Projeto_donuz.Model
{
    public class Transacao
    {
        public int Id { get; set; }
        public int CPF { get; set; }
        public decimal Valor { get; set; }
        public DateTime? Date { get; set; }
        public TipoTransacao Tipo { get; set; }
    }
    public enum TipoTransacao
    {
        Credito,
        Debito
    }
}
