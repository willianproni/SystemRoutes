using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMongoDbRouteSystemLogin.Data;
using Model.MongoDb;
using Services;

namespace MVCMongoDbRouteSystemLogin.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            return View(await SeachApi.GetAllTeamInApi());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await SeachApi.SeachTeamIdInApiAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameTeam")] Team team)
        {
            List<Person> teamPersons = new List<Person>();

            var cityCheck = Request.Form["city"].FirstOrDefault();
            var seachCity = await SeachApi.SeachCityNameInApi(cityCheck);
            var personCheck = Request.Form["checkPeopleTeam"].ToList();

            if (personCheck.Count != 0)
            {
                foreach (var item in personCheck)
                {
                    var veridyPeople = SeachApi.SeachPersonIdInApiAsync(item);
                    teamPersons.Add(await veridyPeople);
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

            team.Persons = teamPersons;
            team.City = seachCity;
            SeachApi.PostTeam(team);
            return RedirectToAction(nameof(Index));


        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seachCity = await SeachApi.SeachTeamIdInApiAsync(id);

            var seachPeple = await SeachApi.GetAllPersonStatusTrue();

            var SeachTeam = await SeachApi.SeachTeamIdInApiAsync(id);

            List<Person> person = new List<Person>();

            foreach (var personTeam in SeachTeam.Persons)
            {
                person.Add(personTeam);
            }

            ViewBag.PersonTeam = person;

            if (seachCity == null)
            {
                return NotFound();
            }

            return View(seachCity);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,NameTeam")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var personAdd = Request.Form["checkPeopleJoinTeam"].ToList();
                    var personRemove = Request.Form["checkPeopleRemoveTeam"].ToList();
                    var newCity = Request.Form["City"].FirstOrDefault();

                    var tradeCity = await SeachApi.SeachCityNameInApi(newCity);

                    if (personAdd.Count != 0)
                    {
                        foreach (var persons in personAdd)
                        {
                            var seachPerson = await SeachApi.SeachPersonIdInApiAsync(persons);

                            SeachApi.UpdateTeamInsert(id, seachPerson);
                        }
                    }

                    if (personRemove.Count != 0)
                    {
                        foreach (var persons in personRemove)
                        {
                            var seachPerson = await SeachApi.SeachPersonIdInApiAsync(persons);

                            SeachApi.UpdateTeamRemove(id, seachPerson);
                        }
                    }

                    var seachTeam = await SeachApi.SeachTeamIdInApiAsync(id);

                    seachTeam.City = tradeCity;
                    SeachApi.UpdateTeam(id, seachTeam);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await SeachApi.SeachTeamIdInApiAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var seachTeam = await SeachApi.SeachTeamIdInApiAsync(id);
            SeachApi.RemoveTeam(seachTeam.Id);
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(string id)
        {
            return _context.Team.Any(e => e.Id == id);
        }
    }
}
