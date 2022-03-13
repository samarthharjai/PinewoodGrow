using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models;
using PinewoodGrow.ViewModels;
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

			//sort income source by datapoint
			double mInc1 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 1));
			double mInc2 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 2));
			double mInc3 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 3));
			double mInc4 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 4));
			double mInc5 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 5));
			double mInc6 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 6));
			double mInc7 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 7));
			double mInc8 = member.ToList().Count(m => m.MemberSituations.Any(s => s.SituationID == 8));
			double mInc9 = member.ToList().Count();



			//sort income source total by datapoint
			double mInc11 = member.Select(a => a.ODSPIncome).ToList().Sum();
			double mInc22 = member.Select(a => a.OWIncome).ToList().Sum();
			double mInc33 = member.Select(a => a.CPPIncome).ToList().Sum();
			double mInc44 = member.Select(a => a.EIIncome).ToList().Sum();
			double mInc55 = member.Select(a => a.GAINSIncome).ToList().Sum();
			double mInc66 = member.Select(a => a.PSIncome).ToList().Sum();
			double mInc77 = member.Select(a => a.OIncome).ToList().Sum();
			double mInc88 = member.Select(a => a.EIncome).ToList().Sum();


			List<DataPoint> dataPoints = new List<DataPoint>();

			dataPoints.Add(new DataPoint("ODSP", mInc1));
			dataPoints.Add(new DataPoint("Ontario Works", mInc2));
			dataPoints.Add(new DataPoint("CPP-Disability", mInc3));
			dataPoints.Add(new DataPoint("EI", mInc4)); 
			dataPoints.Add(new DataPoint("GAINS (For Seniors)", mInc5));
			dataPoints.Add(new DataPoint("Post-Sec. Student", mInc6));
			dataPoints.Add(new DataPoint("Other", mInc7));
			dataPoints.Add(new DataPoint("Employed", mInc8));



			List<DataPointIncome> tabledataPoints = new List<DataPointIncome>();

			tabledataPoints.Add(new DataPointIncome("ODSP", mInc1, mInc11));
			tabledataPoints.Add(new DataPointIncome("Ontario Works", mInc2, mInc22));
			tabledataPoints.Add(new DataPointIncome("CPP-Disability", mInc3, mInc33));
			tabledataPoints.Add(new DataPointIncome("EI", mInc4, mInc44));
			tabledataPoints.Add(new DataPointIncome("GAINS (For Seniors)", mInc5, mInc55));
			tabledataPoints.Add(new DataPointIncome("Post-Sec. Student", mInc6, mInc66));
			tabledataPoints.Add(new DataPointIncome("Other", mInc7, mInc77));
			tabledataPoints.Add(new DataPointIncome("Employed", mInc8, mInc88));


			ViewData["graphData"] = dataPoints.ToList();
            ViewData["tableData"] = tabledataPoints;



			List<DataPoint> incomeStats = new List<DataPoint>();
			incomeStats.Add(new DataPoint("ODSP", mInc11));
			incomeStats.Add(new DataPoint("Ontario Works", mInc22));
			incomeStats.Add(new DataPoint("CPP-Disability", mInc33));
			incomeStats.Add(new DataPoint("EI", mInc44));
			incomeStats.Add(new DataPoint("GAINS (For Seniors)", mInc55));
			incomeStats.Add(new DataPoint("Post-Sec. Student", mInc66));
			incomeStats.Add(new DataPoint("Other", mInc77));
			incomeStats.Add(new DataPoint("Employed", mInc88));

			ViewData["incomeStats"] = incomeStats.ToList();

			return View();
		}


	}
}
