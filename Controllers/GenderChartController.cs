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
    public class GenderChartController : Controller
    {
		private readonly GROWContext _context;

		public GenderChartController(GROWContext context)
		{
			_context = context;
		}

		// GET: Home
		public async Task <IActionResult> Index()
		{

			var member = from m in _context.Members
						 select m;

			//sort gender by datapoint
			double mInc1 = member.ToList().Count(m => m.GenderID == 1);
			double mInc2 = member.ToList().Count(m => m.GenderID == 2);
			double mInc3 = member.ToList().Count(m => m.GenderID == 3);
			double mInc4 = member.ToList().Count(m => m.GenderID == 4);
			double mInc5 = member.ToList().Count(m => m.GenderID == 5);
			double mInc6 = member.ToList().Count();


			List<DataPoint> dataPoints = new List<DataPoint>();

			dataPoints.Add(new DataPoint("Male", mInc1));
			dataPoints.Add(new DataPoint("Female", mInc2));
			dataPoints.Add(new DataPoint("Non-Binary", mInc3));
			dataPoints.Add(new DataPoint("Other", mInc4));
			dataPoints.Add(new DataPoint("Prefer not to say", mInc5));



			List<DataPoint> tabledataPoints = new List<DataPoint>();

			tabledataPoints.Add(new DataPoint("Male", mInc1));
			tabledataPoints.Add(new DataPoint("Female", mInc2));
			tabledataPoints.Add(new DataPoint("Non-Binary", mInc3));
			tabledataPoints.Add(new DataPoint("Other", mInc4));
			tabledataPoints.Add(new DataPoint("Prefer not to say", mInc5));
			tabledataPoints.Add(new DataPoint("Total", mInc6));


			ViewData["graphData"] = dataPoints.ToList();
            ViewData["tableData"] = tabledataPoints;

			return View();
		}


	}
}
