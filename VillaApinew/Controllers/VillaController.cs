using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VillaApinew.Data;
using VillaApinew.Modal;
using VillaApinew.Repository.IRepository;

namespace VillaApinew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        // private readonly ApplicationDbContext _db;
        //use Irepository
        private readonly IVillaRepository _db; 
        private readonly IMapper _mapper;

        protected ApiResponse _response;

        //need to avoid db context in controller


        public VillaController(ILogger<VillaController> logger,IVillaRepository db,IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<ActionResult<List<ApiResponse>>> GetVillaList()
        {

            // _logger.LogInformation("get information");
            //return Ok(VillaDatastore.VillaList);
            //return Ok(await _db.Villas.ToListAsync());

            try
            {
                List<Villa> Villalist = await _db.GetAll();
                _response.Result = _mapper.Map<List<VillaDto>>(Villalist);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccess = false;
                _response.ErrorsMessages=new List<string> { ex.ToString() };
            }
            return Ok(_response);

        }

        [HttpGet("{id:int}",Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _db.Get(u => u.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }

                _response.Result = _mapper.Map<VillaDto>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.ToString() };
            }
            return _response;      
 }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApiResponse>> Createdata([FromBody] VillaCreateDto villadto) //changed to villadto to villacreate
        {
            try
            {
                if (villadto == null)
                {
                    return BadRequest(villadto);
                }


                Villa modal = _mapper.Map<Villa>(villadto);

                //Villa modal = new()
                //{
                //    Amenity = villadto.Amenity,
                //    Details = villadto.Details,
                //   // Id = villadto.Id,     so only removed
                //    ImageUrl = villadto.ImageUrl,
                //    Name = villadto.Name,
                //    Occupancy = villadto.Occupancy,
                //    Rate = villadto.Rate,
                //    Sqrt = villadto.Sqrt,
                //};

                //villadto.Id = _db.Villas.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
                await _db.Create(modal);
                await _db.Save();
                // VillaDatastore.VillaList.Add(villadto);
                // return Ok(villadto);
                //sometimes we nned to expose the data that time createroute will helpfull

                _response.Result = _mapper.Map<List<VillaDto>>(modal);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { modal.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _db.Get(u => u.Id == id);
                //Villa modal = _mapper.Map<Villa>(villa);

                if (villa == null)
                {
                    return NotFound();
                }

                await _db.Remove(villa);
                await _db.Save();


                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDto villadto)
        {

            try
            {
                if (villadto == null)
                {
                    return BadRequest();
                }
                // var villad=_db.Villas.FirstOrDefault(v => v.Id == id);

                Villa modal = _mapper.Map<Villa>(villadto);
                //Villa modal = new()
                //{
                //    Amenity = villadto.Amenity,
                //    Details = villadto.Details,
                //    Id = villadto.Id,
                //    ImageUrl = villadto.ImageUrl,
                //    Name = villadto.Name,
                //    Occupancy = villadto.Occupancy,
                //    Rate = villadto.Rate,
                //    Sqrt = villadto.Sqrt,
                //};
                await _db.Update(modal);
                await _db.Save();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string> { ex.ToString() };
            }
            return _response;
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
            var villadto =await _db.Get(v => v.Id == id);

            if(villadto == null)
            {
                return BadRequest();
            }
            //villadto

            VillaUpdateDto modal = _mapper.Map<VillaUpdateDto>(villadto);
            //VillaUpdateDto modal = new()
            //{
            //    Amenity = villadto.Amenity,
            //    Details = villadto.Details,
            //    Id = villadto.Id,
            //    ImageUrl = villadto.ImageUrl,
            //    Name = villadto.Name,
            //    Occupancy = villadto.Occupancy,
            //    Rate = villadto.Rate,
            //    Sqrt = villadto.Sqrt,
            //};

            patch.ApplyTo(modal, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modal2= _mapper.Map<Villa>(modal);

            //Villa modal2 = new()
            //{
            //    Amenity = modal.Amenity,
            //    Details = modal.Details,
            //    Id = modal.Id,
            //    ImageUrl = modal.ImageUrl,
            //    Name = modal.Name,
            //    Occupancy = modal.Occupancy,
            //    Rate = modal.Rate,
            //    Sqrt = modal.Sqrt,
            //};

            _db.Update(modal2);
            await _db.Save();
            return NoContent();


        }



    }
}
