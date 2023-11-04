using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using STORE.Data.Constants;
using STORE.Data.Repository;
using STORE.Data.UnitOfWork;
using STORE.Entities.Commons;
using STORE.Entities.Dtos.Articulo;
using STORE.Entities.Dtos.Tienda;
using STORE.Entities.Interface;
using STORE.Entities.Models;

namespace STORE.Bussiness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IArticuloRepository _articuloRepository;

        public ArticulosController(IArticuloRepository articuloRepository)
        {
            _articuloRepository = articuloRepository;
        }

        [HttpGet]
        [Route("ListArticulos")]
        public async Task<IActionResult> ListArticulos()
        {
            var response = await _articuloRepository.ListArticulosAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("ArticuloById/{ArticuloTiendaID:int}")]
        public async Task<IActionResult> ArticuloById(int ArticuloTiendaID)
        {
            var response = await _articuloRepository.ArticuloByIdAsync(ArticuloTiendaID);
            return Ok(response);
        }

        [HttpPost]
        [Route("ArticuloCreate")]
        public async Task<IActionResult> ArticuloCreate([FromBody] ArticuloCreateDto articuloCreateDto)
        {
            var response = await _articuloRepository.ArticuloCreateAsync(articuloCreateDto);
            return Ok(response);
        }

        [HttpPut]
        [Route("ArticuloEdit")]
        public async Task<IActionResult> ArticuloEdit([FromBody] ArticuloCreateDto articuloCreateDto)
        {
            var response = await _articuloRepository.ArticuloEditAsync(articuloCreateDto);
            return Ok(response);
        }

        [HttpDelete]
        [Route("ArticuloDelete/{ArticuloID:int}")]
        public async Task<IActionResult> ArticuloDelete(int ArticuloID)
        {
            var response = await _articuloRepository.ArticuloDeleteAsync(ArticuloID);
            return Ok(response);
        }
    }
}
