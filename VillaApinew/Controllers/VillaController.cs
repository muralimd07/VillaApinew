using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VillaApinew.Data;
using VillaApinew.Modal;

namespace VillaApinew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<VillaDto>> GetVillaList()
        {
            return Ok(VillaDatastore.VillaList);
        }

        [HttpGet("{id:int}",Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
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
    }
}
