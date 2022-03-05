using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PinewoodGrow.Data;
using PinewoodGrow.Data.Repositorys;
using PinewoodGrow.Models;
using PinewoodGrow.ViewModels;

namespace PinewoodGrow.Controllers
{
    public class MapReportController : Controller
    {
        private readonly GROWContext _context;
        private readonly TravelDataRepository travelDataRepository;

        public MapReportController(GROWContext context)
        {
            travelDataRepository = new TravelDataRepository();
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
       
            var Markers = new List<MapMarker>();

            //Converts List of Addresses to map markers exudes all entries that do not have enter lat/longs

            Markers = _context.Households.Include(a => a.Address)
                .ThenInclude(t => t.TravelDetail)
                .ThenInclude(t => t.GroceryStore)
                .Where(a => a.Address.Latitude != 0 && a.Address.Longitude != 0)
                .Select(a => new MapMarker(a, a.HouseIncome)).ToList();

  
            ViewData["Stores"] = _context.GroceryStores.Select(g => 
                new StoreMapMarker(g, _context.Households
                    .Include(a => a.Address)
                    .ThenInclude(a => a.TravelDetail)
                    .ThenInclude(a => a.GroceryStore)
                    .Where(a => a.Address.TravelDetail.GroceryStore.ID == g.ID)
                    .ToList())
                ).ToList();

            ViewData["Markers"] = Markers;

            /*ViewData["Points"] = Markers.Select(a => new LatLong() { Lat = a.Lat, Long = a.Long }).ToList();*/

            return View();
        }

    }

}//http://maps.google.com/mapfiles/ms/icons/purple-dot.png

/*if (!_context.TravelDetails.Any())
{
    var (travel, groceryStores) = await TravelDataRepository.GetAllDetails(_context.Addresses.ToList(), _context.GroceryStores.ToList());
    await _context.AddRangeAsync(groceryStores);
    await _context.SaveChangesAsync();
    await _context.AddRangeAsync(travel);
    await _context.SaveChangesAsync();
}

var x = "";*/








/*
ViewData["stores"] = _context.GroceryStores.ToList();

ViewData["travel"] = _context.TravelDetails.Include(a => a.Address).ToList();*/