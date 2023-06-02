using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;

        public VillaController(
            ILogger<VillaController> logger,
            ApplicationDbContext db
            ) 
        {
            _logger = logger;
            _db = db;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Obtener las villas");
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer la villa con Id " + id);
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.Villas.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "El nombre de villa ingresado ya existe");
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest();
            }

            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa model = new Villa() { 
                Nombre = villaDto.Nombre,
                Amenidad = villaDto.Amenidad,
                Detalle = villaDto.Detalle,
                FechaActualizacion = DateTime.Now,
                FechaCreacion = DateTime.Now,
                ImagenUrl = villaDto.ImagenUrl,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa
            };

            _db.Villas.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id) 
        {
            if (id == 0) 
            {
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            if (villa is null) 
            {
                return NotFound();
            }

            _db.Villas.Remove(villa);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto) 
        {
            if (villaDto == null || id != villaDto.Id) 
            {
                return BadRequest();
            }

            Villa model = new Villa { 
                Id = villaDto.Id,
                Tarifa = villaDto.Tarifa,
                Amenidad = villaDto.Amenidad,
                Detalle = villaDto.Detalle,
                FechaActualizacion = DateTime.Now,
                ImagenUrl = villaDto.ImagenUrl,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Nombre = villaDto.Nombre,
                Ocupantes = villaDto.Ocupantes,
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);

            VillaDto villaDto = new VillaDto { 
                Ocupantes = villa.Ocupantes,
                Amenidad = villa.Amenidad,
                Detalle = villa.Detalle,
                Id = id,
                ImagenUrl = villa.ImagenUrl,
                MetrosCuadrados = villa.MetrosCuadrados,
                Nombre = villa.Nombre,
                Tarifa = villa.Tarifa
            };

            if (villa is null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid) 
            {
                new Villa { Id = 1, Nombre = "Vista a la Piscina" },
                new Villa { Id = 2, Nombre = "Vista a la Playa" }
            };
        }
    }
}
