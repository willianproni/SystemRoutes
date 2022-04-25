using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMongoDbRouteSystemLogin.Data;
using Model;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace MVCMongoDbRouteSystemLogin.Controllers
{
    [Authorize]
    public class CitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await SeachApi.GetAllCityInApi());
        }

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

        public IActionResult Create()
        {
            return View();
        }

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
