using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExerciseTask.Services
{
    public interface IRepository<T>
    {
        bool AddRecord(T record);
        bool DeleteRecord(int id);
        T GetRecord(int id);
        bool UpdateRecord(T record);
        IQueryable<T> GetQuery();
    }
}