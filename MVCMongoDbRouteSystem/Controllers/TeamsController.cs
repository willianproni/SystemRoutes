using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMongoDbRouteSystem.Data;
using Model.MongoDb;
using Services;

namespace MVCMongoDbRouteSystem.Controllers
{
    public class TeamsController : Controller
    {
        private readonly MVCMongoDbRouteSystemContext _context;

        public TeamsController(MVCMongoDbRouteSystemContext context)
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
            var team = await SeachApi.SeachTeamNameInApi(id);

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
        public async Task<IActionResult> Create([Bind("Id,NameTeam,City")] Team team)
        {
            List<Person> teamPersons = new List<Person>();

            var cityCheck = Request.Form["city"].FirstOrDefault();
            var seachCity = await SeachApi.SeachCityNameInApi(cityCheck);
            var personCheck = Request.Form["checkPeopleTeam"].ToList();

            foreach (var item in personCheck)
            {
                var veridyPeople = SeachApi.SeachPersonIdInApiAsync(item);
                teamPersons.Add(await veridyPeople);
            }

            if (ModelState.IsValid)
            {
                team.Persons = teamPersons;
                team.City = seachCity;
                SeachApi.PostTeam(team);
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seachCity = await SeachApi.SeachTeamNameInApi(id);

            if (id == null)
            {
                return NotFound();
            }
            return View(id);
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
                    _context.Update(team);
                    await _context.SaveChangesAsync();
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

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var team = await _context.Team.FindAsync(id);
            _context.Team.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(string id)
        {
            return _context.Team.Any(e => e.Id == id);
        }

        private static async Task<IEnumerable<Person>> PeopleActive()
        {
            var people = await SeachApi.GetAllPeopleInApi();

            List<Person> peopleAvailable = new List<Person>();

            foreach (var item in people)
            {
                if (item.Active == true)
                    peopleAvailable.Add(item);
            }

            return peopleAvailable;
        }
    }
}
