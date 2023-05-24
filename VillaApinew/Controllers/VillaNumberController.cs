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
    //[Route("api/[controller]")]
    [Route("api/VillaNumberApi")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        // private readonly ApplicationDbContext _db;
        //use Irepository
        private readonly IVillaNumberRepository _db; 
        private readonly IMapper _mapper;

        protected ApiResponse _response;

        //need to avoid db context in controller


        public VillaNumberController(ILogger<VillaController> logger,IVillaNumberRepository db,IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<ActionResult<List<ApiResponse>>> GetVillaNumberList()
        {

            // _logger.LogInformation("get information");
            //return Ok(VillaDatastore.VillaList);
            //return Ok(await _db.Villas.ToListAsync());

            try
            {
                List<VillaNumber> VillaNumberlist = await _db.GetAll();
                _response.Result = _mapper.Map<List<VillaNumberDto>>(VillaNumberlist);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccess = false;
                _response.ErrorsMessages=new List<string> { ex.ToString() };
            }
            return Ok(_response);

        }

        [HttpGet("{id:int}",Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _db.Get(u => u.VillaNo == id);
                if (villa == null)
                {
                    return NotFound();
                }

                _response.Result = _mapper.Map<VillaNumberDto>(villa);
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

        public async Task<ActionResult<ApiResponse>> CreateNumberdata([FromBody] VillaNumberCreateDto villadto) //changed to villadto to villacreate
        {
            try
            {
                if (villadto == null)
                {
                    return BadRequest(villadto);
                }


                //VillaNumber modal = _mapper.Map<VillaNumber>(villadto);


                VillaNumber modal = new()
                {
                    VillaId = villadto.VillaId,
                    VillaNo = villadto.VillaNo,
                    specialdetils = villadto.specialdetials

                };

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
                //sometimes we need to expose the data that time createroute will helpfull

                //_response.Result = _mapper.Map<List<VillaNumberDto>>(modal);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillaNumber", new { modal.VillaNo }, _response);
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
        public async Task<ActionResult<ApiResponse>> DeleteNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _db.Get(u => u.VillaNo == id);
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
        public async Task<ActionResult<ApiResponse>> UpdateNumberVilla(int id, [FromBody] VillaNumberUpdateDto villadto)
        {

            try
            {
                if (villadto == null)
                {
                    return BadRequest();
                }
                // var villad=_db.Villas.FirstOrDefault(v => v.Id == id);

                VillaNumber modal = _mapper.Map<VillaNumber>(villadto);
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

        [HttpPatch("{id:int}",Name = "UpdatePartialNumberVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async  Task<IActionResult> UpdatePartialNumberVilla(int id, JsonPatchDocument<VillaNumberUpdateDto> patch)
        {
            if (patch == null)
            {
                return BadRequest();
            }
            var villadto =await _db.Get(v => v.VillaNo == id);

            if(villadto == null)
            {
                return BadRequest();
            }
            //villadto

            VillaNumberUpdateDto modal = _mapper.Map<VillaNumberUpdateDto>(villadto);
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

            VillaNumber modal2= _mapper.Map<VillaNumber>(modal);

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
