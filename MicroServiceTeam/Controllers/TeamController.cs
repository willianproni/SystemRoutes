using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServiceTeam.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.MongoDb;

namespace MicroServiceTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teamService;

        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public ActionResult<List<Team>> GetAll() =>
            _teamService.Get();

        [HttpGet("{time}", Name = "GetTeam")]
        public ActionResult<Team> GetNameTeam(string time)
        {
            var seachTeam = _teamService.Get(time);

            if (seachTeam == null)
                return Conflict("Time não Cadastrado!");

            return seachTeam;
        }

        [HttpPost]
        public async Task<ActionResult<Team>> Create(Team newTeam)
        {
            var seachTeam = _teamService.Get(newTeam.NameTeam);

            if (seachTeam != null)
                return Conflict("Time já cadastrado!");

            await _teamService.Create(newTeam);

            return CreatedAtRoute("GetTeam", new { time = newTeam.NameTeam }, newTeam);
        }

        [HttpPut("{time}")]
        public IActionResult Update(string time, Team updateTime)
        {
            var seachTeam = _teamService.Get(time);

            if (seachTeam == null)
                return Conflict("Time não encontrado!");

            _teamService.Update(time, updateTime);

            return NoContent();
        }

        [HttpDelete("{time}")]
        public IActionResult Delete (string time)
        {
            var seachTeam = _teamService.Get(time);

            if (seachTeam == null)
                return Conflict("Time não encontrado!");

            _teamService.Remove(time);

            return NoContent();
        }
    }
}
