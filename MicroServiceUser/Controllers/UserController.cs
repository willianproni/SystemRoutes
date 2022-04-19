using System.Collections.Generic;
using MicroServiceUser.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.MongoDb;

namespace MicroServiceUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAll() =>
            _userService.Get();

        [HttpGet("{id}", Name = "GetId")]
        public ActionResult<User> GetId(string id)
        {
            var seachUser = _userService.GetId(id);

            if (seachUser == null)
                return BadRequest("Usuário não encontrado, confira os dados e tente novamente!");

            return seachUser;
        }

        [HttpGet("login/{login}", Name = "GetLogin")]
        public ActionResult<User> GetLogin(string login)
        {
            var seachUser = _userService.GetLogin(login);

            if (seachUser == null)
                return BadRequest("Usuário não encontrado, confira os dados e tente novamente!");

            return seachUser;
        }

        [HttpPost]
        public ActionResult<User> Create(User newUser)
        {
            var seachUser = _userService.GetLogin(newUser.Login);

            if (seachUser != null)
                return Conflict("Nome de login não disponível, escolha outro!");

            _userService.Create(newUser);

            return CreatedAtRoute("GetId", new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, User updateUser)
        {
            var seachUser = _userService.GetId(id);

            if (seachUser == null)
                return BadRequest("Usuário não encontrado, confira os dados e tente novamente!");

            _userService.Update(id, updateUser);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var seachUser = _userService.GetId(id);

            if (seachUser == null)
                return BadRequest("Usuário não encontrado, confira os dados e tente novamente!");

            _userService.Remove(id);

            return NoContent();
        }
    }
}
