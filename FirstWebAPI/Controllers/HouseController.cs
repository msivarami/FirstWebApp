using System;
using System.Linq;
using System.Net;
using SampleWebApiAspNetCore.Models;
using SampleWebApiAspNetCore.Repositories;
using SampleWebApiAspNetCore.Services;
using System.Web.Http;
using System.Net.Http;
namespace FirstWebAPI.Controllers
{
    
    public class HouseController : ApiController
    {
        private readonly IHouseMapper _houseMapper;
        private readonly IHouseRepository _houseRepository;
        public HouseController()
        {
            _houseMapper = new HouseMapper();
            _houseRepository = new HouseRepository();
        }
        //public HouseController(IHouseMapper houseMapper, IHouseRepository houseRepository)
        //{
        //    _houseMapper = houseMapper;
        //    _houseRepository = houseRepository;
        //}

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_houseRepository.GetAll().Select(x => _houseMapper.MapToDto(x)));
            }
            catch (Exception exception)
            {
                //logg exception or do anything with it
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet()]
        //[HttpGet("{id:int}", Name = "GetSingleHouse")]
        public IHttpActionResult GetSingle(int id)
        {
            try
            {
                HouseEntity houseEntity = _houseRepository.GetSingle(id);

                if (houseEntity == null)
                {
                    return NotFound();
                }

                return Ok(_houseMapper.MapToDto(houseEntity));
            }
            catch (Exception exception)
            {
                //logg exception or do anything with it
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPatch()]
        //[HttpPatch("{id:int}")]
        public IHttpActionResult Patch(int id, HouseDto housePatchDocument)
        {
            try
            {
                if (housePatchDocument == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                HouseEntity houseEntity = _houseRepository.GetSingle(id);

                if (houseEntity == null)
                {
                    return NotFound();
                }

                HouseDto existingHouse = _houseMapper.MapToDto(houseEntity);

                //housePatchDocument.ApplyTo(existingHouse, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _houseRepository.Update(_houseMapper.MapToEntity(existingHouse));

                return Ok(existingHouse);
            }
            catch (Exception exception)
            {
                //logg exception or do anything with it
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] HouseDto houseDto)
        {
            try
            {
                if (houseDto == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                HouseEntity houseEntity = _houseMapper.MapToEntity(houseDto);

                _houseRepository.Add(houseEntity);
   
                return CreatedAtRoute("GetSingleHouse", new { id = houseEntity.Id }, _houseMapper.MapToDto(houseEntity));
            }
            catch (Exception exception)
            {
                //logg exception or do anything with it
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut()]
        //[HttpPut("{id:int}")]
        public IHttpActionResult Update(int id, [FromBody] HouseDto houseDto)
        {
            try
            {
                if (houseDto == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                HouseEntity houseEntityToUpdate = _houseRepository.GetSingle(id);

                if (houseEntityToUpdate == null)
                {
                    return NotFound();
                }

                houseEntityToUpdate.ZipCode = houseDto.ZipCode;
                houseEntityToUpdate.Street = houseDto.Street;
                houseEntityToUpdate.City = houseDto.City;

                _houseRepository.Update(houseEntityToUpdate);

                return Ok(_houseMapper.MapToDto(houseEntityToUpdate));
            }
            catch (Exception exception)
            {
                //logg exception or do anything with it
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
        [HttpDelete()]
        //[HttpDelete("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                HouseEntity houseEntityToDelete = _houseRepository.GetSingle(id);

                if (houseEntityToDelete == null)
                {
                    return NotFound();
                }

                _houseRepository.Delete(id);

                return null;
            }
            catch (Exception exception)
            {
                //logg exception or do anything with it
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
