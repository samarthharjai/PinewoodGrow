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
using Microsoft.AspNetCore.Authorization;

namespace PinewoodGrow.Controllers
{
    [Authorize(Roles = "Admin, Supervisor")]
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
            //Testing Travel detail calculations
            /*var add = new Address
            {
                PlaceID = "ChIJTxN00As404kRsWMFndgNP6I",
                FullAddress = "658 Doan's Ridge Rd",
                City = "Welland",
                PostalCode = "L3B 5N7",
                Latitude = 42.96808,
                Longitude = -79.18249999999999,
            };
            if (_context.Addresses.All(a => a.PlaceID != add.PlaceID))
            {
                _context.Add(add);
                await _context.SaveChangesAsync();
            }


            var (travel, store) = await TravelDataRepository.GetTravelTimes(_context.Addresses.FirstOrDefault(a=> a.PlaceID == add.PlaceID));

            if (_context.TravelDetails.Include(a => a.Address).All(a => a.Address.PlaceID != add.PlaceID))
            {
               if (_context.GroceryStores.All(a=> a.ID != store.ID))
               {
                   _context.Add(store);
                   await _context.SaveChangesAsync();
               }

               _context.Add(travel);
               await _context.SaveChangesAsync();
            }*/

            var Markers = new List<MapMarker>();

            var households = _context.Households.Include(a => a.Address)
                .ThenInclude(t => t.TravelDetail)
                .ThenInclude(t => t.GroceryStore)
                .Include(a => a.Members).ThenInclude(m=> m.MemberSituations)
                .Where(a => a.AddressID != null && a.Address.Latitude != 0 && a.Address.Longitude != 0);
           

            //Converts List of Addresses to map markers exudes all entries that do not have enter lat/longs

            Markers = households.Select(a => new MapMarker(a, Math.Round(a.HouseIncome), a.Members.Count)).ToList();

            
            ViewData["Stores"] = households.Select(g =>
                    new StoreMapMarker(g.Address.TravelDetail.GroceryStore, households.Where(a => a.Address.TravelDetail.GroceryStore.ID == g.Address.TravelDetail.GroceryStore.ID).ToList())
                ).ToList();


            ViewData["TravelStats"] = new TravelStats(households.Select(a => a.Address.TravelDetail).ToList());

            ViewData["TravelData"] = households
                .Select(a => new TravelDataPoints(a, a.Address.TravelDetail)).ToList();


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