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

        [HttpGet("/status")]
        public ActionResult<List<Person>> GetStatus() =>
            _personService.GetStatus();

        [HttpGet("{id}")]
        public ActionResult<Person> GetId(string id)
        {
            var seachPerson = _personService.GetId(id);

            if (seachPerson == null)
                return BadRequest("Pessoa não encontrado, confira os dados e tente novamente!");

            return seachPerson;
        }

        [HttpGet("nome/{nome}", Name = "GetPerson")]
        public ActionResult<Person> GetName(string nome)
        {
            var seachPerson = _personService.GetName(nome);

            if (seachPerson == null)
                return BadRequest("Pessoa não encontrado, confira os dados e tente novamente!");

            return seachPerson;
        }

        [HttpPost]
        public ActionResult<Person> Create(Person newPerson)
        {
            _personService.Create(newPerson);

            return CreatedAtRoute("GetPerson", new { nome = newPerson.Name }, newPerson);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Person updatePerson)
        {
            var seachPerson = _personService.GetId(id);

            if (seachPerson == null)
                return BadRequest("Pessoa não encontrado, confira os dados e tente novamente!");

            _personService.Update(id, updatePerson);

            return NoContent();
        }

        [HttpPut("status/{id}")]
        public IActionResult UpdateActive(string id)
        {
            var seachPerson = _personService.UpdateActive(id);

            if (seachPerson == null)
                return BadRequest("Pessoa não encontrado, confira os dados e tente novamente!");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var seachPerson = _personService.GetId(id);

            if (seachPerson == null)
                return BadRequest("Pessoa não encontrado, confira os dados e tente novamente!");

            _personService.Remove(id);

            return NoContent();
        }
    }
}
