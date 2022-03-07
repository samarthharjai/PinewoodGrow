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
				.Include(m => m.MemberIllnesses).ThenInclude(m => m.Illness)
						 select m;

			//sort gender by datapoint
			double mInc1 = member.ToList().Count(m => m.MemberDietaries.Any(s => s.DietaryID == 2));
			double mInc2 = member.ToList().Count(m => m.MemberDietaries.Any(s => s.DietaryID == 3));
			double mInc3 = member.ToList().Count(m => m.MemberDietaries.Any(s => s.DietaryID == 4));
			double mInc4 = member.ToList().Count(m => m.MemberDietaries.Any(s => s.DietaryID == 8));
			double mInc5 = member.ToList().Count(m => m.MemberDietaries.Any(s => s.DietaryID == 9));
			double mInc6 = member.ToList().Count(m => m.MemberIllnesses.Any(s => s.IllnessID == 1));
			double mInc7 = member.ToList().Count(m => m.MemberIllnesses.Any(s => s.IllnessID == 5));
			double mInc8 = member.ToList().Count(m => m.MemberIllnesses.Any(s => s.IllnessID == 6));
			double mInc9 = member.ToList().Count(m => m.MemberIllnesses.Any(s => s.IllnessID == 7));
			double mInc10 = member.ToList().Count();


			List<DataPoint> dataPoints = new List<DataPoint>();

			dataPoints.Add(new DataPoint("Obesity", mInc1));
			dataPoints.Add(new DataPoint("Lactose Intolerance", mInc2));
			dataPoints.Add(new DataPoint("Gluten Intolerance/Sensitivity", mInc3));
			dataPoints.Add(new DataPoint("Digestive Disorders", mInc4)); 
			dataPoints.Add(new DataPoint("Food Allergies", mInc5));
			dataPoints.Add(new DataPoint("Diabetes", mInc6));
			dataPoints.Add(new DataPoint("Cancer", mInc7));
			dataPoints.Add(new DataPoint("Heart Disease", mInc8));
			dataPoints.Add(new DataPoint("Osteoperosis", mInc9));



			List<DataPoint> tabledataPoints = new List<DataPoint>();

			tabledataPoints.Add(new DataPoint("Obesity", mInc1));
			tabledataPoints.Add(new DataPoint("Lactose Intolerance", mInc2));
			tabledataPoints.Add(new DataPoint("Gluten Intolerance/Sensitivity", mInc3));
			tabledataPoints.Add(new DataPoint("Digestive Disorders", mInc4));
			tabledataPoints.Add(new DataPoint("Food Allergies", mInc5));
			tabledataPoints.Add(new DataPoint("Diabetes", mInc6));
			tabledataPoints.Add(new DataPoint("Cancer", mInc7));
			tabledataPoints.Add(new DataPoint("Heart Disease", mInc8));
			tabledataPoints.Add(new DataPoint("Osteoperosis", mInc9));


			ViewData["graphData"] = JsonConvert.SerializeObject(dataPoints);
            ViewData["tableData"] = tabledataPoints;

			return View();
		}


	}
}
