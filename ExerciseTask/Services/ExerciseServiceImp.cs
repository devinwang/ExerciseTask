using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExerciseTask.Models;
using Microsoft.Practices.Unity;
using System.Text.RegularExpressions;

namespace ExerciseTask.Services
{
    public class ExerciseServiceImp : IExerciseService
    {
        [Dependency]
        public IRespository<ExerciseRecord> Respository { get; set; }

        #region IMPLIMENTATION
        public bool AddRecord(ExerciseRecord record)
        {
            return Respository.AddRecord(record);
        }


        public int TotalSize
        {
            get
            {
                return Respository.GetQuery().Count();
            }
        }

        public Tuple<int, int, List<ExerciseRecord>> GetRecordsOrderByDateDesc(string keyword, int pageNumber, int numberOfItem)
        {
            if (keyword == null)
                keyword = "";
            pageNumber -= 1;
            if (pageNumber < 0)
                pageNumber = 0;
            if (numberOfItem < 1)
                numberOfItem = 10;
            if (Respository.GetQuery().Count() <= 0)
            {
                initialization();
            }
            var exerciseList = Respository.GetQuery().Where(x => x.Name.ToLower().Contains(keyword.ToLower()))
                .OrderByDescending(x => x.Date);

            int totalSize = exerciseList.Count();
            int totalPageSize = totalSize / numberOfItem;
            if (pageNumber > totalPageSize)
                pageNumber = totalPageSize;

            return new Tuple<int, int, List<ExerciseRecord>>(pageNumber+1, totalPageSize+1, 
                exerciseList.Skip(numberOfItem * pageNumber).Take(numberOfItem).ToList());
        }
        # endregion

        #region INITIALIZATION
        // example data
        private void initialization()
        {
            if (initializated)
                return;
            for (int i = 0; i < 25; i++)
            {
                var record = new ExerciseRecord()
                {
                    Id = i + 1,
                    Name = RandomName(random.Next(6, 10)),
                Date = RandomDay(new DateTime(2010, 1, 1)),
                    DurationInMinutes = random.Next(1, 100)
                };
                Respository.AddRecord(record);
            }
            initializated = true;
        }

        private Random random = new Random();

        private string RandomName(int length)
        {
            var regex = new Regex(@"^\d.*$");
            while (true)
            {
                string name = RandomString(length);
                if (!regex.IsMatch(name))
                    return name;
            }
        }
        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private DateTime RandomDay(DateTime startDate)
        {
            TimeSpan timeSpan = DateTime.Today - startDate;
            TimeSpan newSpan = new TimeSpan(0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = startDate + newSpan;
            return newDate;
        }

        public bool DeleteRecord(int id)
        {
            return Respository.DeleteRecord(id);
        }

        public ExerciseRecord GetRecord(int id)
        {
            return Respository.GetRecord(id);
        }

        public bool UpdateRecord(ExerciseRecord record)
        {
            return Respository.UpdateRecord(record);
        }
        #endregion

        private static bool initializated = false;
    }
}