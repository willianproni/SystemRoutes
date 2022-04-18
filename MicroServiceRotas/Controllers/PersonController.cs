using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServiceRotas.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.MongoDb;

namespace MicroServiceRotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public ActionResult<List<Person>> GetAll() =>
            _personService.Get();

        [HttpGet("{nome}", Name ="GetPerson")]
        public ActionResult<Person> GetName(string nome)
        {
            var seachPerson = _personService.Get(nome);

            if (seachPerson == null)
                return BadRequest("Pessoa não econtranda");

            return seachPerson;
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Create(Person newPerson)
        {
            var seachPerson = _personService.Get(newPerson.Name);

            await _personService.Create(newPerson);

            return CreatedAtRoute("GetPerson", new { nome = newPerson.Name }, newPerson);
        }

        [HttpPut("{nome}")]
        public IActionResult Update(string nome, Person updatePerson)
        {
            var seachPerson = _personService.Get(nome);

            if (seachPerson == null)
                return BadRequest("Pessoa não cadastrada!");

            _personService.Update(nome, updatePerson);

            return NoContent();
        }

        [HttpDelete("{nome}")]
        public IActionResult Delete(string nome)
        {
            var seachPerson = _personService.Get(nome);

            if (seachPerson == null)
                return BadRequest("Pessoa não cadastrada!");

            _personService.Remove(nome);

            return NoContent();
        }
    }
}
