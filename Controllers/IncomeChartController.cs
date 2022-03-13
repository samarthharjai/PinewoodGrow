using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PinewoodGrow.Models;
using PinewoodGrow.Data;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Microsoft.AspNetCore.Http.Features;
using System.IO;

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
			double mInc9 = member.ToList().Count();



			List<DataPoint> dataPoints = new List<DataPoint>();

			dataPoints.Add(new DataPoint("Under $5000", mInc1));
			dataPoints.Add(new DataPoint("$5000 to $9999", mInc2));
			dataPoints.Add(new DataPoint("$10000 to $14999", mInc3));
			dataPoints.Add(new DataPoint("$15000 to $19999", mInc4));
			dataPoints.Add(new DataPoint("$20000 to $24999", mInc5));
			dataPoints.Add(new DataPoint("$25000 or more", mInc6));

			List<DataPoint> tabledataPoints = new List<DataPoint>();
			tabledataPoints.Add(new DataPoint("Under $5000", mInc1));
			tabledataPoints.Add(new DataPoint("$5000 to $9999", mInc2));
			tabledataPoints.Add(new DataPoint("$10000 to $14999", mInc3));
			tabledataPoints.Add(new DataPoint("$15000 to $19999", mInc4));
			tabledataPoints.Add(new DataPoint("$20000 to $24999", mInc5));
			tabledataPoints.Add(new DataPoint("$25000 or more", mInc6));
			tabledataPoints.Add(new DataPoint("Total", mInc9));

			ViewData["graphData"] = dataPoints.ToList();
            ViewData["tableData"] = tabledataPoints;

			return View();
		}

        public IActionResult DownloadReport()
        {

            var mem = from a in _context.Members
                        .Include(m => m.MemberDietaries).ThenInclude(m => m.Dietary)
                        .Include(m => m.MemberSituations).ThenInclude(m => m.Situation)
                      select new
                      {
                          Age = a.Age,
                          Dietary = a.MemberDietaries,
                          Health = a.MemberIllnesses,
                          Situation = a.MemberSituations,
                          Gender = a.Gender,
                          Income = a.Income
                        };
            //How many rows?
            int numRows = mem.Count();

            if (numRows > 0) //We have data
            {
                //Create a new spreadsheet from scratch.
                using (ExcelPackage excel = new ExcelPackage())
                {

                    //Note: you can also pull a spreadsheet out of the database if you
                    //have saved it in the normal way we do, as a Byte Array in a Model
                    //such as the UploadedFile class.
                    //
                    // Suppose...
                    //
                    // var theSpreadsheet = _context.UploadedFiles.Include(f => f.FileContent).Where(f => f.ID == id).SingleOrDefault();
                    //
                    //    //Pass the Byte[] FileContent to a MemoryStream
                    //
                    // using (MemoryStream memStream = new MemoryStream(theSpreadsheet.FileContent.Content))
                    // {
                    //     ExcelPackage package = new ExcelPackage(memStream);
                    // }

                    var workSheet = excel.Workbook.Worksheets.Add("Members");

                    //Note: Cells[row, column]
                    workSheet.Cells[3, 1].LoadFromCollection(mem, true);

                    //Style first column for dates
                    workSheet.Column(1).Style.Numberformat.Format = "yyyy-mm-dd";

                    //Style fee column for currency
                    workSheet.Column(4).Style.Numberformat.Format = "###,##0.00";

                    //Note: You can define a BLOCK of cells: Cells[startRow, startColumn, endRow, endColumn]
                    //Make Date and Patient Bold
                    workSheet.Cells[4, 1, numRows + 3, 2].Style.Font.Bold = true;

                    //Note: these are fine if you are only 'doing' one thing to the range of cells.
                    //Otherwise you should USE a range object for efficiency
                    using (ExcelRange totalfees = workSheet.Cells[numRows + 4, 4])//
                    {
                        totalfees.Formula = "Sum(" + workSheet.Cells[4, 4].Address + ":" + workSheet.Cells[numRows + 3, 4].Address + ")";
                        totalfees.Style.Font.Bold = true;
                        totalfees.Style.Numberformat.Format = "$###,##0.00";
                    }

                    //Set Style and backgound colour of headings
                    using (ExcelRange headings = workSheet.Cells[3, 1, 3, 7])
                    {
                        headings.Style.Font.Bold = true;
                        var fill = headings.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(Color.LightBlue);
                    }

                    ////Boy those notes are BIG!
                    ////Lets put them in comments instead.
                    //for (int i = 4; i < numRows + 4; i++)
                    //{
                    //    using (ExcelRange Rng = workSheet.Cells[i, 7])
                    //    {
                    //        string[] commentWords = Rng.Value.ToString().Split(' ');
                    //        Rng.Value = commentWords[0] + "...";
                    //        //This LINQ adds a newline every 7 words
                    //        string comment = string.Join(Environment.NewLine, commentWords
                    //            .Select((word, index) => new { word, index })
                    //            .GroupBy(x => x.index / 7)
                    //            .Select(grp => string.Join(" ", grp.Select(x => x.word))));
                    //        ExcelComment cmd = Rng.AddComment(comment, "Apt. Notes");
                    //        cmd.AutoFit = true;
                    //    }
                    //}

                    //Autofit columns
                    workSheet.Cells.AutoFitColumns();
                    //Note: You can manually set width of columns as well
                    //workSheet.Column(7).Width = 10;

                    //Add a title and timestamp at the top of the report
                    workSheet.Cells[1, 1].Value = "Member's Report";
                    using (ExcelRange Rng = workSheet.Cells[1, 1, 1, 6])
                    {
                        Rng.Merge = true; //Merge columns start and end range
                        Rng.Style.Font.Bold = true; //Font should be bold
                        Rng.Style.Font.Size = 18;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    //Since the time zone where the server is running can be different, adjust to 
                    //Local for us.
                    DateTime utcDate = DateTime.UtcNow;
                    TimeZoneInfo esTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    DateTime localDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, esTimeZone);
                    using (ExcelRange Rng = workSheet.Cells[2, 6])
                    {
                        Rng.Value = "Created: " + localDate.ToShortTimeString() + " on " +
                            localDate.ToShortDateString();
                        Rng.Style.Font.Bold = true; //Font should be bold
                        Rng.Style.Font.Size = 12;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }

                    //Ok, time to download the Excel

                    //I usually stream the response back to avoid possible
                    //out of memory errors on the server if you have a large spreadsheet.
                    //NOTE: Since .NET Core 3 most Web Servers disallow sync IO so we
                    //need to temporarily change the setting for the server.
                    //If we can't then we will try to build the file and return a FileContentResult
                    var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
                    if (syncIOFeature != null)
                    {
                        syncIOFeature.AllowSynchronousIO = true;
                        using (var memoryStream = new MemoryStream())
                        {
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.Headers["content-disposition"] = "attachment;  filename=Member Report.xlsx";
                            excel.SaveAs(memoryStream);
                            memoryStream.WriteTo(Response.Body);
                        }
                    }
                    else
                    {
                        try
                        {
                            Byte[] theData = excel.GetAsByteArray();
                            string filename = "Member Report.xlsx";
                            string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            return File(theData, mimeType, filename);
                        }
                        catch (Exception)
                        {
                            return NotFound();
                        }
                    }
                }
            }
            return NotFound();
        }


    }
}
