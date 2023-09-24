using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Projeto_donuz.Controllers
{
    public class TranferenciasController : ControllerBase
    {
        private readonly AppDbContex _context;

        public TransferenciaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("TransferirFundos")]
        public async Task<IActionResult> TransferirFundos([FromBody] PedidoTransferencia pedidoTransferencia)
        {
            // Verificar se os clientes de origem e destino existem e se o cliente de origem tem saldo suficiente.
            var clienteOrigem = await _context.Clientes
                .Include(c => c.Conta)
                .FirstOrDefaultAsync(c => c.Id == pedidoTransferencia.ClienteOrigemId);

            var clienteDestino = await _context.Clientes
                .Include(c => c.Conta)
                .FirstOrDefaultAsync(c => c.Id == pedidoTransferencia.ClienteDestinoId);

            if (clienteOrigem == null || clienteDestino == null)
            {
                return BadRequest("Cliente de origem ou destino não encontrado.");
            }

            if (clienteOrigem.Conta.Saldo < pedidoTransferencia.Valor)
            {
                return BadRequest("Saldo insuficiente na conta de origem.");
            }

            // Realizar a transferência de fundos entre os clientes.
            clienteOrigem.Conta.Saldo -= pedidoTransferencia.Valor;
            clienteDestino.Conta.Saldo += pedidoTransferencia.Valor;

            // Atualizar os saldos dos clientes no banco de dados.
            _context.Entry(clienteOrigem).State = EntityState.Modified;
            _context.Entry(clienteDestino).State = EntityState.Modified;
        }
}
