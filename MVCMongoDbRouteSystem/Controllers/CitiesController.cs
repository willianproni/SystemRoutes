using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMongoDbRouteSystem.Data;
using Model;
using Services;

namespace MVCMongoDbRouteSystem.Controllers
{
    public class CitiesController : Controller
    {
        private readonly MVCMongoDbRouteSystemContext _context;

        public CitiesController(MVCMongoDbRouteSystemContext context)
        {
            _context = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(await SeachApi.GetAllCityInApi());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seachCity = await SeachApi.SeachCityIdInApiAsync(id);
            if (seachCity == null)
            {
                return NotFound();
            }

            return View(seachCity);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,NameCity,State")] City city)
        {
            if (ModelState.IsValid)
            {
                SeachApi.PostCity(city);
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seachCity = await SeachApi.SeachCityIdInApiAsync(id);
            if (seachCity == null)
            {
                return NotFound();
            }
            return View(seachCity);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,NameCity,State")] City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var seachCity = await SeachApi.SeachCityIdInApiAsync(id);
                    SeachApi.UpdateCity(id, city);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id))
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
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seachCity = await SeachApi.SeachCityIdInApiAsync(id);
            if (seachCity == null)
            {
                return NotFound();
            }

            return View(seachCity);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var seachCity = await SeachApi.SeachCityIdInApiAsync(id);
            SeachApi.RemoveCity(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(string id)
        {
            return _context.City.Any(e => e.Id == id);
        }
    }
}
