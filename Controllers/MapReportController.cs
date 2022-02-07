using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PinewoodGrow.Data;
using PinewoodGrow.ViewModels;

namespace PinewoodGrow.Controllers
{
    public class MapReportController : Controller
    {
        private readonly GROWContext _context;

        public MapReportController(GROWContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {

            var Markers = new List<MapMarker>();

            //Converts List of Addresses to map markers exudes all entries that do not have enter lat/longs

            Markers = _context.Addresses
                .Where(a => a.Latitude != 0 && a.Longitude != 0)
                .Select(a => new MapMarker() { Address = a.FullAddress, Lat = (double)a.Latitude, Lng = (double)a.Longitude }).ToList();

            ViewData["Markers"] = Markers;

            /*ViewData["Points"] = Markers.Select(a => new LatLong() { Lat = a.Lat, Long = a.Long }).ToList();*/

            return View();
        }
    }
}