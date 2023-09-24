using Microsoft.EntityFrameworkCore;
using Projeto_donuz.AppDbContex;
using Projeto_donuz.Model;
using System.Linq;

namespace Projeto_donuz.Repositories
{
    public class ClienteRepositories : IClienteRepositories
    {

        public readonly AppDbContext _context;

        public ClienteRepositories(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<int> Create(Cliente cadastro)
        {
            var newCliente = _context.Cadastros.Add(cadastro);
            await _context.SaveChangesAsync();

            return newCliente.Entity.Id;
            
        }

        public async Task<bool> DeleteById(int id)
        {
            var DeleteById = await _context.Cadastros.FindAsync(id);
            if (DeleteById == null) 
                return false;

            _context.Cadastros.Remove(DeleteById);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Cliente>> Get()
        {
           return await _context.Cadastros.ToListAsync();
        }                    

        public async Task<bool> Update(int id, Cliente cadastro)
        {
            var findedCustomer = await _context.Cadastros.FirstOrDefaultAsync(c => c.Id == id);
            if ( findedCustomer == null)
            {
                return false;
            }
            
            findedCustomer.Nome = cadastro.Nome;
            findedCustomer.CPF = cadastro.CPF; 
            findedCustomer.Endereco = cadastro.Endereco;
            findedCustomer.Telefone = cadastro.Telefone;
            findedCustomer.Saldo = cadastro.Saldo;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Cliente?> GetById(int id)
        {
            var cliente = await _context.Cadastros.Where(s => s.Id == id).FirstOrDefaultAsync();

            return cliente;

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente?> GetByCPF(string cpf)
        {
            var clienteCPF = await _context.Cadastros.Where(c => c.CPF == cpf).FirstOrDefaultAsync();
            return clienteCPF;
        }

        
    }
}
