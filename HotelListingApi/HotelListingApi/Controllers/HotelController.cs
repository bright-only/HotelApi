using AutoMapper;
using HotelListingApi.Data;
using HotelListingApi.IRepository;
using HotelListingApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListingApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HotelController : ControllerBase
  {
    private readonly IUnitOfWork _unitofwork;
    private readonly IMapper _mapper;
    private readonly ILogger<CountryController> _logger;
    public HotelController(IUnitOfWork unitofwork, IMapper mapper, ILogger<CountryController> logger)
    {
      _unitofwork = unitofwork;
      _mapper = mapper;
      _logger = logger; 
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetHotels([FromQuery] PaginationParam paginationParam)
    {
      _logger.LogInformation($"EXECUTING {nameof(GetHotels)} RESOURCE");
      var Hotels = await _unitofwork.Hotels.GetPagedList(paginationParam);
      var results = _mapper.Map<List<HotelDTO>>(Hotels);
      return Ok(results);
    }

    [HttpGet("{id:int}", Name = "GetHotel")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetHotel(int id)
    {
      _logger.LogInformation($"EXECUTING {nameof(GetHotel)} RESOURCE");
      var Hotel = await _unitofwork.Hotels.Get(query => query.HotelId == id, new List<string> { "Country" });
      if(Hotel == null)
      {
        return NotFound($"ID:{id} NOT FOUND IN THE RESOURCE, ENTER VALID ID");
      }
      var result = _mapper.Map<HotelDTO>(Hotel);
      return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO hotelDTO)
    {
      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var hotel = _mapper.Map<Hotel>(hotelDTO);
      await _unitofwork.Hotels.Insert(hotel);
      await _unitofwork.Save();
      return CreatedAtRoute("GetHotel", new {id=hotel.HotelId},hotel);
    }


    [Authorize(Roles = "Administrator")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelDTO hotelDTO)
    {
      if(!ModelState.IsValid || id<1)
      {
        _logger.LogError($"INVALID UPDATE ATTEMPT IN {nameof(UpdateHotel)}");
        return BadRequest(ModelState);
      }
      var hotel = await _unitofwork.Hotels.Get(q => q.HotelId == id);
      if(hotel==null)
      {
        _logger.LogError($"INVALID UPDATE ATTEMPT IN {nameof(UpdateHotel)}");
        return BadRequest("SUBMITTED DATA IS INVALID");
      }
      _mapper.Map(hotelDTO, hotel);
      _unitofwork.Hotels.Update(hotel);
      await _unitofwork.Save();

      return NoContent();
    }


    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteHotel(int id)
    {
      if(id < 1)
      {
        _logger.LogError($"INVALID UPDATE ATTEMPT IN {nameof(DeleteHotel)}");
        return BadRequest(id);
      }
      var hotel = await _unitofwork.Hotels.Get(q => q.HotelId == id);
      if(hotel == null)
      {
        _logger.LogError($"INVALID UPDATE ATTEMPT IN {nameof(DeleteHotel)}");
        return BadRequest("SUBMITTED DATA IS INVALID");
      }
      await _unitofwork.Hotels.Delete(id);
      await _unitofwork.Save();

      return NoContent();
    }
  }
}
