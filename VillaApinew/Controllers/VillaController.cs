using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaApinew.Data;
using VillaApinew.Modal;

namespace VillaApinew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;

        public VillaController(ILogger<VillaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<VillaDto>> GetVillaList()
        {

            _logger.LogInformation("get information");
            return Ok(VillaDatastore.VillaList);
        }

        [HttpGet("{id:int}",Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa=VillaDatastore.VillaList.FirstOrDefault(u => u.Id == id);
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

        public ActionResult<VillaDto> Createdata([FromBody] VillaDto villadto)
        {
            if(villadto == null)
            {
                return BadRequest(villadto);
            }

           villadto.Id = VillaDatastore.VillaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaDatastore.VillaList.Add(villadto);
            // return Ok(villadto);
            //sometimes we nned to expose the data that time createroute will helpfull
            return CreatedAtRoute("GetVilla", new { villadto.Id }, villadto);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa=VillaDatastore.VillaList.FirstOrDefault(u=>u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            VillaDatastore.VillaList.Remove(villa);
            return NoContent();



        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villa)
        {
            if (villa == null) { 
                return BadRequest();
            }
            var villad=VillaDatastore.VillaList.FirstOrDefault(v => v.Id == id);

            villad.Id=villa.Id;
            villad.Name=villa.Name;

            return NoContent();




        }

        [HttpPatch("{id:int}",Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patch)
        {
            if (patch == null)
            {
                return BadRequest();
            }
            var villad = VillaDatastore.VillaList.FirstOrDefault(v => v.Id == id);

            if(villad == null)
            {
                return BadRequest();
            }

            patch.ApplyTo(villad,ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();


        }



    }
}
