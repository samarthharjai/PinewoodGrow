using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            /*Markers = _context.Addresses
                .Where(a => a.Latitude != 0 && a.Longitude != 0)
                .Select(a => new MapMarker() { Address = a.FullAddress, Lat = (double)a.Latitude, Lng = (double)a.Longitude }).ToList();*/

            Markers = _context.Households.Include(a => a.Address)
                .Where(a => a.Address.Latitude != 0 && a.Address.Longitude != 0)
                .Select(a=> new MapMarker()
                {
                    Address = a.Address.FullAddress,
                    Lat = (double)a.Address.Latitude, 
                    Lng = (double)a.Address.Longitude, 
                    Income = (double)a.HouseIncome,
                    Color = GetColor((double)a.HouseIncome),
                    FamilySize = a.FamilySize,
                    Category = GetCategory((double)a.HouseIncome)

                }).ToList();


            ViewData["Markers"] = Markers;

            /*ViewData["Points"] = Markers.Select(a => new LatLong() { Lat = a.Lat, Long = a.Long }).ToList();*/

            return View();
        }

        private static string GetColor(double income)
        {
            return income switch
            {
                var i when i <= 5000 => "http://maps.google.com/mapfiles/ms/icons/blue-dot.png",
                var i when i <= 10000 => "http://maps.google.com/mapfiles/ms/icons/red-dot.png",
                var i when i <= 15000 => "http://maps.google.com/mapfiles/ms/icons/purple-dot.png",
                var i when i <= 20000 => "http://maps.google.com/mapfiles/ms/icons/yellow-dot.png",
                _ => "http://maps.google.com/mapfiles/ms/icons/green-dot.png"
            };
        }
        private static string GetCategory(double income)
        {
            return income switch
            {
                var i when i <= 5000 => "Lowest",
                var i when i <= 10000 => "Low",
                var i when i <= 15000 => "Mid",
                var i when i <= 20000 => "High",
                _ => "Top"
            };
        }
    }

}