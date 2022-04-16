using System.Collections.Generic;
using MicroServiceCity.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace MicroServiceCity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityService _cityService;

        public CityController(CityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public ActionResult<List<City>> GetAll() =>
            _cityService.Get();

        [HttpGet("{cidade}")]
        public ActionResult<City> GetNameCity(string cidade)
        {
            var SeachCity = _cityService.Get(cidade);

            if (SeachCity == null)
                return BadRequest("Cidade não encontrada!");

            return SeachCity;
        }

        [HttpPost]
        public ActionResult<City> Create(City newCity)
        {
            var seachCity = _cityService.Get(newCity.NameCity);

            if (seachCity != null)
                return Conflict("Cidade já cadastrada!");

            _cityService.Create(newCity);

            return CreatedAtRoute("GetPerson", new { cidade = newCity.NameCity }, newCity);
        }

        [HttpPut("{cidade}")]
        public IActionResult Update(string cidade, City updateCity)
        {
            var seachCity = _cityService.Get(cidade);

            if (seachCity == null)
                return NotFound("Cidade não cadastrada!");

            _cityService.Update(cidade, updateCity);

            return NoContent();
        }

        [HttpDelete("{cidade}")]
        public IActionResult Delete(string cidade)
        {
            var seachCity = _cityService.Get(cidade);

            if (seachCity == null)
                return NotFound("Cidade não cadastrada!");

            _cityService.Remove(cidade);

            return NoContent();
        }
    }
}
