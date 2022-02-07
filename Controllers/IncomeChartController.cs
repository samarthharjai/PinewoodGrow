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
    public class IncomeChartController : Controller
    {
		private readonly GROWContext _context;

		public IncomeChartController(GROWContext context)
		{
			_context = context;
		}

		// GET: Home
		public async Task <IActionResult> Index()
		{

			var member = from m in _context.Members
						 select m;

			//sort income by datapoint
			double mInc1 = member.ToList().Count(m => m.Income < 5000);
			double mInc2 = member.ToList().Count(m => (m.Income <= 9999) && (m.Income >= 5000));
			double mInc3 = member.ToList().Count(m => (m.Income <= 14999) && (m.Income >= 10000));
			double mInc4 = member.ToList().Count(m => (m.Income <= 19999) && (m.Income >= 15000));
			double mInc5 = member.ToList().Count(m => (m.Income <= 24999) && (m.Income >= 20000));
			double mInc6 = member.ToList().Count(m => m.Income >= 25000);
			


			List<DataPoint> dataPoints = new List<DataPoint>();

			dataPoints.Add(new DataPoint("Under $5000", mInc1));
			dataPoints.Add(new DataPoint("$5000 to $9999", mInc2));
			dataPoints.Add(new DataPoint("$10000 to $14999", mInc3));
			dataPoints.Add(new DataPoint("$15000 to $19999", mInc4));
			dataPoints.Add(new DataPoint("$20000 to $24999", mInc5));
			dataPoints.Add(new DataPoint("$25000 or more", mInc6));


            ViewData["graphData"] = JsonConvert.SerializeObject(dataPoints);
            ViewData["tableData"] = dataPoints;

			return View();
		}


	}
}
