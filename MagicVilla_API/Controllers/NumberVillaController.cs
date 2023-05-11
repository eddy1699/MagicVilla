using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repsository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.CompilerServices;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberVillaController : ControllerBase
    {
        private readonly ILogger<NumberVillaController> _logger;
        private readonly IVillaRepository _villaRepo;
        private readonly INumberVillaRepository _numberRepo;

        private readonly IMapper _mapper;
        protected APIResponse _response;
        public NumberVillaController(ILogger<NumberVillaController> logger, IVillaRepository villaRepo,INumberVillaRepository numberRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numberRepo= numberRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetNumberVillas()
        {
            try
            {
                _logger.LogInformation("Obtener numero de villas");
                IEnumerable<NumberVilla> numberVillaList = await _numberRepo.GetAll();
                _response.Result = _mapper.Map<IEnumerable<NumberVillaDto>>(numberVillaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _response;


        }



        [HttpGet("id:int", Name = "GetNumberVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumberVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogInformation("Error al trar Numero Villa con Id " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                var numberVilla = await _numberRepo.Get(v => v.VillaNo == id);
                if (numberVilla == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess=false;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<NumberVillaDto>(numberVilla);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return _response;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateNumberVilla([FromBody] NumberVillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _numberRepo.Get(v => v.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("Number  Existed", "La villa con ese numero ya existe!");
                    return BadRequest(ModelState);
                }

                if(await _villaRepo.Get(v=>v.Id == createDto.VillaId) == null){

                    ModelState.AddModelError("Error", "CEl ID de la villa no existe");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                NumberVilla model = _mapper.Map<NumberVilla>(createDto);
                model.CreatedDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                await _numberRepo.Create(model);
                _response.Result = model;
                _response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetNumberVilla", new { id = model.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteNumberVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var numbervilla = await _numberRepo.Get(v => v.VillaNo == id);
                if (numbervilla == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _numberRepo.Remove(numbervilla);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumberVilla(int id, [FromBody] NumberVillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.VillaNo)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if(await _villaRepo.Get(V=>V.Id == updateDto.VillaId) == null)
            {
                ModelState.AddModelError("Clave foranea", "El id de la villa no existe");
                return BadRequest(ModelState);
            }

            NumberVilla model = _mapper.Map<NumberVilla>(updateDto);
            await _numberRepo.Update(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);

        }

    }
}
