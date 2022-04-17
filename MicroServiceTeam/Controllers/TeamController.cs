using System.Collections.Generic;
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
    }
}
