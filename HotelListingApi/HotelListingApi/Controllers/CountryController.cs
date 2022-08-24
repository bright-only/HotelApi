using AutoMapper;
using HotelListingApi.Data;
using HotelListingApi.Core.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelListingApi.Core.DTOs;
using HotelListingApi.Core.Models;

namespace HotelListingApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CountryController : ControllerBase
  {
    private readonly IUnitOfWork _unitofwork;
    private readonly IMapper _mapper;
    private readonly ILogger<CountryController> _logger;
    public CountryController(IUnitOfWork unitofwork, IMapper mapper, ILogger<CountryController> logger)
    {
      _unitofwork = unitofwork;
      _mapper = mapper;
      _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCountries([FromQuery] PaginationParam paginationParam)
    {
      _logger.LogInformation($"EXECUTING {nameof(GetCountries)} RESOURCE");
      var countries = await _unitofwork.Countries.GetPagedList(paginationParam);
      var results = _mapper.Map<List<CountryDTO>>(countries);
      return Ok(results);
    }

    [HttpGet("{id:int}", Name = "GetCountry")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCountry(int id)
    {
      _logger.LogInformation($"EXECUTING {nameof(GetCountry)} RESOURCE");

      var country = await _unitofwork.Countries.Get(query => query.CountryId == id, new List<string> { "Hotels" });
      if(country ==null)
      {
        return NotFound($"ID:{id} NOT FOUND IN THE RESOURCE, ENTER VALID ID");  
      }
      var result = _mapper.Map<CountryDTO>(country);
      return Ok(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
    {
      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var country = _mapper.Map<Country>(countryDTO);
      await _unitofwork.Countries.Insert(country);
      await _unitofwork.Save();
      return CreatedAtRoute("GetCountry", new { id = country.CountryId }, country);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
    {
      if(!ModelState.IsValid || id < 1)
      {
        _logger.LogError($"INVALID UPDATE ATTEMPT IN {nameof(UpdateCountry)}");
        return BadRequest(ModelState);
      }

      var country = await _unitofwork.Countries.Get(q => q.CountryId == id);
      if(country == null)
      {
        _logger.LogError($"INVALID UPDATE ATTEMPT IN {nameof(UpdateCountry)}");
        return BadRequest("SUBMITTED DATA IS INVALID");
      }
      _mapper.Map(countryDTO, country);
      _unitofwork.Countries.Update(country);
      await _unitofwork.Save();

      return NoContent();
    }



    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCountry(int id)
    {
      if(id < 1)
      {
        _logger.LogError($"INVALID UPDATE ATTEMPT IN {nameof(DeleteCountry)}");
        return BadRequest(id);
      }
      var country = await _unitofwork.Countries.Get(q => q.CountryId == id);
      if(country == null)
      {
        _logger.LogError($"INVALID UPDATE ATTEMPT IN {nameof(DeleteCountry)}");
        return BadRequest("SUBMITTED DATA IS INVALID");
      }
      await _unitofwork.Countries.Delete(id);
      await _unitofwork.Save();

      return NoContent();
    }
  }
}
