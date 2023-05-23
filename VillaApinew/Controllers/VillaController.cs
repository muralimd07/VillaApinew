using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaApinew.Data;
using VillaApinew.Modal;

namespace VillaApinew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;

        public VillaController(ILogger<VillaController> logger,ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<VillaDto>>> GetVillaList()
        {

            // _logger.LogInformation("get information");
            //return Ok(VillaDatastore.VillaList);
            return Ok(await _db.Villas.ToListAsync());

        }

        [HttpGet("{id:int}",Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa=await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null) {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<VillaDto>> Createdata([FromBody] VillaCreateDto villadto) //changed to villadto to villacreate
        {
            if(villadto == null)
            {
                return BadRequest(villadto);
            }


            Villa modal = new()
            {
                Amenity = villadto.Amenity,
                Details = villadto.Details,
               // Id = villadto.Id,     so only removed
                ImageUrl = villadto.ImageUrl,
                Name = villadto.Name,
                Occupancy = villadto.Occupancy,
                Rate = villadto.Rate,
                Sqrt = villadto.Sqrt,
            };

           //villadto.Id = _db.Villas.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
           await _db.Villas.AddAsync(modal);
           await _db.SaveChangesAsync();
            // VillaDatastore.VillaList.Add(villadto);
            // return Ok(villadto);
            //sometimes we nned to expose the data that time createroute will helpfull
            return CreatedAtRoute("GetVilla", new { modal.Id }, modal); ;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa=await _db.Villas.FirstOrDefaultAsync(u=>u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();



        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villadto)
        {
            if (villadto == null) { 
                return BadRequest();
            }
           // var villad=_db.Villas.FirstOrDefault(v => v.Id == id);

            Villa modal = new()
            {
                Amenity = villadto.Amenity,
                Details = villadto.Details,
                Id = villadto.Id,
                ImageUrl = villadto.ImageUrl,
                Name = villadto.Name,
                Occupancy = villadto.Occupancy,
                Rate = villadto.Rate,
                Sqrt = villadto.Sqrt,
            };
            _db.Villas.Update(modal);
            await _db.SaveChangesAsync();
            return NoContent();




        }

        [HttpPatch("{id:int}",Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async  Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patch)
        {
            if (patch == null)
            {
                return BadRequest();
            }
            var villadto =await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            if(villadto == null)
            {
                return BadRequest();
            }
            //villadto

            VillaUpdateDto modal = new()
            {
                Amenity = villadto.Amenity,
                Details = villadto.Details,
                Id = villadto.Id,
                ImageUrl = villadto.ImageUrl,
                Name = villadto.Name,
                Occupancy = villadto.Occupancy,
                Rate = villadto.Rate,
                Sqrt = villadto.Sqrt,
            };

            patch.ApplyTo(modal, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modal2 = new()
            {
                Amenity = modal.Amenity,
                Details = modal.Details,
                Id = modal.Id,
                ImageUrl = modal.ImageUrl,
                Name = modal.Name,
                Occupancy = modal.Occupancy,
                Rate = modal.Rate,
                Sqrt = modal.Sqrt,
            };

            _db.Villas.Update(modal2);
            await _db.SaveChangesAsync();
            return NoContent();


        }



    }
}
