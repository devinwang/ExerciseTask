using ExerciseTask.Models;
using ExerciseTask.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ExerciseTask.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public IExerciseService ExerciseService { get; set; }


        public ActionResult Index()
        {
            return View();
        }

        public JsonResult FetchData(string query, int page = 1)
        {

            if (query == null)
                query = "";

            var result = ExerciseService.GetRecordsOrderByDateDesc(query, page, 10);
            var exerciseList = result.Item3.Select(x =>
                new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date.ToString(DateFormat),
                    Duration = x.DurationInMinutes
                });
            return Json(new
            {
                CurrentPage = result.Item1,
                TotalPageSize = result.Item2,
                TotalSize = ExerciseService.TotalSize,
                ExerciseList = exerciseList
            });
        }

        public JsonResult AddExercise(string name, int duration, string date)
        {
            ExerciseRecord newRecord = new ExerciseRecord
            {
                Name = name,
                DurationInMinutes = duration,
                Date = DateTime.ParseExact(date, DateFormat, null)
            };
            ValidateModel(newRecord);
            if (!ModelState.IsValid)
                return Json(1);
            if (ExerciseService.AddRecord(newRecord))
                return Json(0);
            return Json(1);
        }

        public JsonResult DeleteExercise(int id)
        {
            if (ExerciseService.DeleteRecord(id))
                return Json(0);
            return Json(1);
        }

        public JsonResult GetExercise(int id)
        {
            ExerciseRecord record = ExerciseService.GetRecord(id);
            if (record == null)
            {
                return Json(new { Result = 1 });
            }
            return Json(new { Result = 0, Exercise = new { Id = record.Id, Name = record.Name,
                Date = record.Date.ToString(DateFormat), Duration = record.DurationInMinutes } });
        }

        public JsonResult UpdateExercise(int id, string name, int duration, string date)
        {
            ExerciseRecord record = new ExerciseRecord
            {
                Id = id,
                Name = name,
                DurationInMinutes = duration,
                Date = DateTime.ParseExact(date, DateFormat, null)
            };
            ValidateModel(record);
            if (ExerciseService.UpdateRecord(record))
                return Json(0);
            return Json(1);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private string escapeString(string str)
        {
            if (str == null)
                str = "";

            return "";
        }

        private Regex rgx = new Regex(@"\w");
        private readonly string DateFormat = "MM/dd/yyyy h:mm tt";
        
    }
}