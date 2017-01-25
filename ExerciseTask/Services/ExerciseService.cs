using ExerciseTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseTask.Services
{
    public interface IExerciseService
    {
        bool AddRecord(ExerciseRecord record);
        bool DeleteRecord(int id);
        ExerciseRecord GetRecord(int id);
        bool UpdateRecord(ExerciseRecord record);
        int TotalSize { get; }
        Tuple<int, int, List<ExerciseRecord>> GetRecordsOrderByDateDesc(string keyword, int pageNumber, int numberOfItem);
    }
}
