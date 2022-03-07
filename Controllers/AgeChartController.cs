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
    public class AgeChartController : Controller
    {
		private readonly GROWContext _context;

		public AgeChartController(GROWContext context)
		{
			_context = context;
		}

		// GET: Home
		public async Task <IActionResult> Index()
		{

			var member = from m in _context.Members
						 select m;


			//sort gender by datapoint
			double mInc1 = member.ToList().Count(m => Int32.Parse(m.Age) <= 12);
			double mInc2 = member.ToList().Count(m => Int32.Parse(m.Age) >= 13 && (Int32.Parse(m.Age) <= 18));
			double mInc3 = member.ToList().Count(m => Int32.Parse(m.Age) >= 19 && (Int32.Parse(m.Age) <= 64));
			double mInc4 = member.ToList().Count(m => Int32.Parse(m.Age) >= 65);
			double mInc6 = member.ToList().Count();


			List<DataPoint> dataPoints = new List<DataPoint>();

			dataPoints.Add(new DataPoint("0-12", mInc1));
			dataPoints.Add(new DataPoint("13-18", mInc2));
			dataPoints.Add(new DataPoint("19-64", mInc3));
			dataPoints.Add(new DataPoint("65+", mInc4));



			List<DataPoint> tabledataPoints = new List<DataPoint>();

			tabledataPoints.Add(new DataPoint("0-12", mInc1));
			tabledataPoints.Add(new DataPoint("13-18", mInc2));
			tabledataPoints.Add(new DataPoint("19-64", mInc3));
			tabledataPoints.Add(new DataPoint("65+", mInc4));
			tabledataPoints.Add(new DataPoint("Total", mInc6));


			ViewData["graphData"] = JsonConvert.SerializeObject(dataPoints);
            ViewData["tableData"] = tabledataPoints;

			return View();
		}


	}
}
