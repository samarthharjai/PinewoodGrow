using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models;
using PinewoodGrow.Data;
using Microsoft.EntityFrameworkCore;

namespace PinewoodGrow.Controllers
{
    public class DietarySourceController : Controller
    {
		private readonly GROWContext _context;

		public DietarySourceController(GROWContext context)
		{
			_context = context;
		}

		// GET: Home
		public async Task <IActionResult> Index()
		{

			var member = from m in _context.Members
				.Include(m => m.MemberDietaries).ThenInclude(m => m.Dietary)
						 select m;

			//sort gender by datapoint
			double mInc1 = member.ToList().Count(m => m.MemberDietaries.Any(s => s.DietaryID == 2));
			double mInc2 = member.ToList().Count(m => m.MemberDietaries.Any(s => s.DietaryID == 3));
			double mInc3 = member.ToList().Count(m => m.MemberDietaries.Any(s => s.DietaryID == 4));
			double mInc4 = member.ToList().Count(m => m.MemberDietaries.Any(s => s.DietaryID == 8));
			double mInc5 = member.ToList().Count(m => m.MemberDietaries.Any(s => s.DietaryID == 9));
			double mInc10 = member.ToList().Count();


			List<DataPoint> dataPoints = new List<DataPoint>();

			dataPoints.Add(new DataPoint("Obesity", mInc1));
			dataPoints.Add(new DataPoint("Lactose Intolerance", mInc2));
			dataPoints.Add(new DataPoint("Gluten Intolerance/Sensitivity", mInc3));
			dataPoints.Add(new DataPoint("Digestive Disorders", mInc4)); 
			dataPoints.Add(new DataPoint("Food Allergies", mInc5));




			List<DataPoint> tabledataPoints = new List<DataPoint>();

			tabledataPoints.Add(new DataPoint("Obesity", mInc1));
			tabledataPoints.Add(new DataPoint("Lactose Intolerance", mInc2));
			tabledataPoints.Add(new DataPoint("Gluten Intolerance/Sensitivity", mInc3));
			tabledataPoints.Add(new DataPoint("Digestive Disorders", mInc4));
			tabledataPoints.Add(new DataPoint("Food Allergies", mInc5));


			ViewData["graphData"] = dataPoints.ToList();
            ViewData["tableData"] = tabledataPoints;

			return View();
		}


	}
}
