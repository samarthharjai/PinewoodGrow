using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models;

namespace PinewoodGrow.Controllers
{
    public class IncomeChartController : Controller
    {
		// GET: Home
		public ActionResult Index()
		{
			List<DataPoint> dataPoints = new List<DataPoint>();

			dataPoints.Add(new DataPoint("Under $5000", 16));
			dataPoints.Add(new DataPoint("$5000 to $9999", 13));
			dataPoints.Add(new DataPoint("$10000 to $14999", 20));
			dataPoints.Add(new DataPoint("$15000 to $19999", 7));
			dataPoints.Add(new DataPoint("$20000 to $24999", 5));
			dataPoints.Add(new DataPoint("$25000 or more", 12));


			ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

			return View();
		}


	}
}
