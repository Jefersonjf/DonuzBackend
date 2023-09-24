﻿namespace Projeto_donuz.Model
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }        
        public decimal? Saldo { get; set; } = 0;
        public List<Transacao> Transacaos { get; set; } = new List<Transacao>();

        public Cliente()
        {
                
        }

        public Cliente(string nome, string cpf, string endereco, string telefone, string email, decimal saldo)
        {
            Nome = nome;
            CPF = cpf;
            Endereco = endereco;
            Telefone = telefone;
            Email = email;
            Saldo = saldo;
        }
    }       

}
