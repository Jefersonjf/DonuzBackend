using Microsoft.EntityFrameworkCore.ChangeTracking;
using Projeto_donuz.Model;

namespace Projeto_donuz.Repositories
{
    public interface IClienteRepositories
    {
        Task<IEnumerable<Cliente>> Get();
        Task<Cliente?> GetById(int id);
        Task<Cliente?> GetByCPF(string cpf);
        Task<int> Create(Cliente cadastro);
        Task<bool> Update(int id, Cliente cadastro);
        Task<bool> DeleteById(int id);
        Task SaveChangesAsync();
        
    }  
}
