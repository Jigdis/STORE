using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using STORE.Data.Commons;
using STORE.Data.Constants;
using STORE.Data.Dtos.Tienda;
using STORE.Entities.Interface;
using STORE.Entities.Models;

namespace STORE.Bussiness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendaController : ControllerBase
    {
        private readonly ITiendaRepository _tiendaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public TiendaController(ITiendaRepository tiendaRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _tiendaRepository = tiendaRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("TiendasGetAll")]
        public async Task<IActionResult> TiendasGetAll()
        {
            var response = new BaseResponse<IEnumerable<TiendaDto>>();
            try
            {
                var tiendas = await _tiendaRepository.GetAll();

                if (tiendas is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<TiendaDto>>(tiendas);
                    response.Message = GlobalMessges.MESSAGE_QUERY;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("TiendaById/{TiendaID:int}")]
        public async Task<IActionResult> TiendaById(int TiendaID)
        {
            var response = new BaseResponse<TiendaDto>();

            try
            {
                var tienda = _tiendaRepository
                .Find(t => t.TiendaID == TiendaID)
                .FirstOrDefault();

                if (tienda is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<TiendaDto>(tienda);
                    response.Message = GlobalMessges.MESSAGE_QUERY;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("TiendaCreate")]
        public async Task<IActionResult> TiendaCreate([FromBody] TiendaCreateDto tiendaCreateDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var tienda = _mapper.Map<Tienda>(tiendaCreateDto);
                await _tiendaRepository.Add(tienda);
                response.Data = await _unitOfWork.CompletedAsync() > 0;

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = GlobalMessges.MESSAGE_SAVE;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("TiendaUpdate")]
        public async Task<IActionResult> TiendaUpdate([FromBody] TiendaDto tiendaDto)
        {
            var response = new BaseResponse<bool>();

            try
            {

                var tienda = _mapper.Map<Tienda>(tiendaDto);
                response.Data = await _tiendaRepository.update(tienda);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = GlobalMessges.MESSAGE_UPDATE;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("TiendaDelete/{TiendaId:int}")]
        public async Task<IActionResult> TiendaDelete(int TiendaId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                await _tiendaRepository.Delete(TiendaId);
                response.Data = await _unitOfWork.CompletedAsync() > 0;

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = GlobalMessges.MESSAGE_DELETE;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
