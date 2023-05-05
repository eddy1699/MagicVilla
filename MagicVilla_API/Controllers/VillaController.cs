using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
       public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable <VillaDto>> GetVillas()
        {
            _logger.LogInformation("Obtener las villas");
            return Ok(_db.Villas.ToList());
        }


        [HttpGet("id:int", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if(id == 0)
            {
                _logger.LogInformation("Error al trar Villa con Id "+ id);
                return BadRequest();
            }
           
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_db.Villas.FirstOrDefault(v => v.Name.ToLower() == villaDto.Name.ToLower()) != null) {
                ModelState.AddModelError("NameExisted", "La villa con ese Nombre ya existe!"); 
           return BadRequest(ModelState);   
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if (villaDto.Id> 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa modelo = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                ImgUrl = villaDto.ImageURL,
                Ocuppants = villaDto.Occupants,
                Price = villaDto.Price,
                SquareMeters = villaDto.SquareMeters,
                Amenity = villaDto.Amenity,

            };
            _db.Villas.Add(modelo);
            _db.SaveChanges();

            //villaDto.Id = VillaStore.villaList.OrderByDescending(v=>v.Id).FirstOrDefault().Id +1;
            //VillaStore.villaList.Add(villaDto);
            return CreatedAtRoute("GetVilla",new {id=villaDto.Id },villaDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(v=>v.Id == id);
            if(villa== null) {
            return NotFound();}

            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id ,[FromBody] VillaDto villaDto) {
            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }

            //var villa  = VillaStore.villaList.FirstOrDefault(v=> v.Id == id);
            //villa.Name = villaDto.Name;
            //villa.Occupants = villaDto.Occupants;
            //villa.SquareMeters = villaDto.SquareMeters;

            Villa model = new() {

                Id = villaDto.Id,
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                ImgUrl = villaDto.ImageURL,
                Ocuppants = villaDto.Occupants,
                Price = villaDto.Price,
                SquareMeters = villaDto.SquareMeters,
                Amenity = villaDto.Amenity,

            };
            _db.Villas.Update(model);
            _db.SaveChanges();


            return NoContent();
        
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);

            VillaDto villaDto = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Detail = villa.Detail,
                ImageURL = villa.ImgUrl,
                Occupants = villa.Ocuppants,
                Price = villa.Price,
                SquareMeters = villa.SquareMeters,
                Amenity = villa.Amenity,

            };
            if(villa==null) return BadRequest();


           patchDto.ApplyTo(villaDto, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                ImgUrl=villaDto.ImageURL,
                Ocuppants=villaDto.Occupants,
                Price=villaDto.Price,
                SquareMeters=villaDto.SquareMeters,
                Amenity=villaDto.Amenity,

            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();

        }

    }
}
