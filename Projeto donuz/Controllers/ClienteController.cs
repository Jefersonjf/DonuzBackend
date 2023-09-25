using Microsoft.AspNetCore.Mvc;
using Projeto_donuz.InputModel;
using Projeto_donuz.Model;
using Projeto_donuz.NovaPasta2;
using Projeto_donuz.Repositories;
using System.Linq;
using System.Security.Claims;

namespace Projeto_donuz.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepositories _clienteRepositories;
        public ClienteController(IClienteRepositories clienteRepositories)
        {
            _clienteRepositories = clienteRepositories;
        }

        [HttpGet]
        public async Task<IEnumerable<ClienteViewModel>> Get()
        {
            var clientes = await _clienteRepositories.Get(); 

            if(clientes == null)            
                return Enumerable.Empty<ClienteViewModel>();

            var clientesVM = clientes.Select(c => new ClienteViewModel
            {
                Id = c.Id,  
                Nome = c.Nome,
                CPF = c.CPF,
                Endereco = c.Endereco,
                Telefone = c.Telefone,
                Email = c.Email,
                Saldo = (decimal)c.Saldo
            });

            return clientesVM;
            

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente?>> Get(int id)
        {
            var cliente =  await _clienteRepositories.GetById(id);

            if(cliente == null) 
                return NotFound("Id não localizado");

            var clienteVM = new ClienteViewModel
            {   
                Id = id,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                Endereco = cliente.Endereco,
                Telefone = cliente.Telefone,
                Email = cliente.Email,
                Saldo = (decimal)cliente.Saldo
            };

            return Ok(clienteVM);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Post([FromBody] ClienteInputModel createModel)
        {
            var customer = new Cliente(
                createModel.Nome,
                createModel.CPF,
                createModel.Endereco,
                createModel.Telefone,
                createModel.Email,
                createModel.Saldo);

            var createdCustomer = await _clienteRepositories.Create(customer);
            
            if (createdCustomer == null)
                return NotFound("Clinte não foi criado");

            var clienteVM = new ClienteViewModel
            {
                Id = customer.Id,
                Nome = customer.Nome,
                CPF = customer.CPF,
                Endereco = customer.Endereco,
                Telefone = customer.Telefone,
                Email = customer.Email,
                Saldo = (decimal)customer.Saldo
                
            };

            return Ok(clienteVM);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var clienteToDelete = await _clienteRepositories.GetById(id);
           
            if (clienteToDelete == null)
            {
                return NotFound();
            }                    

            await _clienteRepositories.DeleteById(clienteToDelete.Id);
            return Ok("Cliente excluido com sucesso");
                          
                       
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ClienteInputModel clienteIM)
        {
            var clientetoUpdate = await _clienteRepositories.GetById(clienteIM.Id);

            if (clientetoUpdate == null)
            {
                return NotFound("Cliente não encontrado.");
            }
            
            clientetoUpdate.Nome = clienteIM.Nome;
            clientetoUpdate.CPF = clienteIM.CPF;
            clientetoUpdate.Endereco = clienteIM.Endereco;
            clientetoUpdate.Telefone = clienteIM.Telefone;
            clientetoUpdate.Email = clienteIM.Email;
            clientetoUpdate.Saldo = clienteIM.Saldo;

            await _clienteRepositories.Update(clienteIM.Id, clientetoUpdate);
            return NoContent();
        }

        [HttpGet("saldo")]
        public IActionResult GetSaldo(int clienteId)
        {
            if (clienteId <= 0)
            {
                return BadRequest("ID de cliente inválido");
            }

            var cliente = _clienteRepositories.GetById(clienteId);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }

            return Ok(cliente);
        }
    }
}
