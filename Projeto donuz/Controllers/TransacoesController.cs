using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_donuz.Model;
using Projeto_donuz.Repositories;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Projeto_donuz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoRepositories _transacaoRepository;
        private readonly IClienteRepositories _clienteRepositories;
                
        public TransacaoController(ITransacaoRepositories transacaoRepository, IClienteRepositories clienteRepositories)
        {
            _transacaoRepository = transacaoRepository;
            _clienteRepositories = clienteRepositories;
        }
                
        [HttpPost]
        public async Task<IActionResult> AdicionarTransacao([FromBody] Transacao transacao)
        {
            var clienteCPF = await _clienteRepositories.GetByCPF(transacao.CPF.ToString());

            if (clienteCPF == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            if (transacao.Valor < 0)
            {
                if (clienteCPF.Saldo + transacao.Valor < 0)                {
                    return BadRequest("Saldo insuficiente para a transação de débito.");                }

                if (transacao == null)                
                    return BadRequest();                
            }

            if (transacao.Tipo == TipoTransacao.Credito)
                clienteCPF.Saldo += transacao.Valor;
            else
                clienteCPF.Saldo -= transacao.Valor;


            _clienteRepositories.Update(clienteCPF.Id, clienteCPF);

            _transacaoRepository.AdicionarTransacao(transacao);

            return CreatedAtAction("ObterTodasTransacoes", new { id = transacao.Id }, transacao);
        }
    }
}
