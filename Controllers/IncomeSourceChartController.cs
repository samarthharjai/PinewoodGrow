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
    public class IncomeSourceController : Controller
    {
		private readonly GROWContext _context;

		public IncomeSourceController(GROWContext context)
		{
			_context = context;
		}

		// GET: Home
		public async Task <IActionResult> Index()
		{

			var member = from m in _context.Members
						 .Include(m => m.MemberSituations)
						 .ThenInclude(m => m.Situation)
						 select m;

			//sort gender by datapoint
			double mInc1 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 1));
			double mInc2 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 2));
			double mInc3 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 3));
			double mInc4 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 4));
			double mInc5 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 5));
			double mInc6 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 6));
			double mInc7 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 7));
			double mInc8 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 8));
			double mInc9 = member.ToList().Count();


			List<DataPoint> dataPoints = new List<DataPoint>();

			dataPoints.Add(new DataPoint("ODSP", mInc1));
			dataPoints.Add(new DataPoint("Ontario Works", mInc2));
			dataPoints.Add(new DataPoint("CPP-Disability", mInc3));
			dataPoints.Add(new DataPoint("EI", mInc4)); 
			dataPoints.Add(new DataPoint("GAINS (For Seniors)", mInc5));
			dataPoints.Add(new DataPoint("Post-Sec. Student", mInc6));
			dataPoints.Add(new DataPoint("Other", mInc7));
			dataPoints.Add(new DataPoint("Employed", mInc8));



			List<DataPoint> tabledataPoints = new List<DataPoint>();

			tabledataPoints.Add(new DataPoint("ODSP", mInc1));
			tabledataPoints.Add(new DataPoint("Ontario Works", mInc2));
			tabledataPoints.Add(new DataPoint("CPP-Disability", mInc3));
			tabledataPoints.Add(new DataPoint("EI", mInc4));
			tabledataPoints.Add(new DataPoint("GAINS (For Seniors)", mInc5));
			tabledataPoints.Add(new DataPoint("Post-Sec. Student", mInc6));
			tabledataPoints.Add(new DataPoint("Other", mInc7));
			tabledataPoints.Add(new DataPoint("Employed", mInc8));



			ViewData["graphData"] = JsonConvert.SerializeObject(dataPoints);
            ViewData["tableData"] = tabledataPoints;

			return View();
		}


	}
}
