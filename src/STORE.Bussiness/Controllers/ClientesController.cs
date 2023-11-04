using Microsoft.AspNetCore.Mvc;
using STORE.Entities.Dtos.Clientes;
using STORE.Entities.Interface;

namespace STORE.Bussiness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        [Route("ListClientes")]
        public async Task<IActionResult> ListClientes()
        {
            var response = await _clienteRepository.ListClientesAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("ClienteById/{ClienteArticuloID:int}")]
        public async Task<IActionResult> ClienteById(int ClienteArticuloID)
        {
            var response = await _clienteRepository.ClienteByIdAsync(ClienteArticuloID);
            return Ok(response);
        }

        [HttpPost]
        [Route("ClienteCreate")]
        public async Task<IActionResult> ClienteCreate([FromBody] ClientesCreateDto clientesCreateDto)
        {
            var response = await _clienteRepository.ClienteCreateAsync(clientesCreateDto);
            return Ok(response);
        }

        [HttpPost]
        [Route("ClienteCompraArticulo")]
        public async Task<IActionResult> ClienteCompraArticulo([FromBody] ClienteCompraArticuloDto clienteCompraArticuloDto)
        {
            var response = await _clienteRepository.ClienteCompraArticuloAsync(clienteCompraArticuloDto);
            return Ok(response);
        }

        [HttpPut]
        [Route("ClienteEdit")]
        public async Task<IActionResult> ClienteEdit([FromBody] ClientesCreateDto clientesCreateDto)
        {
            var response = await _clienteRepository.ClienteEditAsync(clientesCreateDto);
            return Ok(response);
        }

        [HttpDelete]
        [Route("ClienteDelete/{ClienteID:int}")]
        public async Task<IActionResult> ClienteDelete(int ClienteID)
        {
            var response = await _clienteRepository.ClienteDeleteAsync(ClienteID);
            return Ok(response);
        }
    }
}
