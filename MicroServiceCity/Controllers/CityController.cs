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

        [HttpGet("{id}")]
        public ActionResult<City> Getid(string id)
        {
            var seachCity = _cityService.GetId(id);

            if (seachCity == null)
                return BadRequest("Cidade não encontrada, verifique as informações e tente novamente!");

            return seachCity;
        }

        [HttpGet("cidade/{cidade}")]
        public ActionResult<City> GetNameCity(string cidade)
        {
            var seachCity = _cityService.Get(cidade);

            if (seachCity == null)
                return BadRequest("Cidade não encontrada, verifique as informações e tente novamente!");

            return seachCity;
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

        [HttpPut("{id}")]
        public IActionResult Update(string id, City updateCity)
        {
            var seachCity = _cityService.GetId(id);

            if (seachCity == null)
                return NotFound("Cidade não encontrada, verifique as informações e tente novamente!");

            _cityService.Update(id, updateCity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var seachCity = _cityService.GetId(id);

            if (seachCity == null)
                return NotFound("Cidade não encontrada, verifique as informações e tente novamente!");

            _cityService.Remove(id);

            return NoContent();
        }
    }
}
