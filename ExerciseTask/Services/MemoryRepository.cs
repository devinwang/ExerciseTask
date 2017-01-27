using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ExerciseTask.Services
{
    public class MemoryRepository<T> : IRepository<T>
    {
        private ISet<T> _records = new HashSet<T>();
        private object _lock = new object();
        private PropertyInfo _IdProperty = new HashSet<T>().GetType().GetGenericArguments()[0].GetProperty("Id");


        public bool AddRecord(T record)
        {
            lock (this._lock)
            {
                int maxId = _records.Count() <= 0 ? 0 : _records.Max<T>(x => (int)_IdProperty.GetValue(x));
                _IdProperty.SetValue(record, maxId + 1);
                _records.Add(record);
            }
            return true;
        }

        public IQueryable<T> GetQuery()
        {
            return _records.AsQueryable();
        }

        public bool DeleteRecord(int id)
        {
            var record = _records.Where(x => (int)_IdProperty.GetValue(x) == id).FirstOrDefault();
            if (record != null)
            {
                _records.Remove(record);
            }
            return true;
        }

        public T GetRecord(int id)
        {
            return _records.Where(x => (int)_IdProperty.GetValue(x) == id).FirstOrDefault();
        }

        public bool UpdateRecord(T record)
        {
            lock (_lock)
            {
                DeleteRecord((int)_IdProperty.GetValue(record));
                _records.Add(record);
            }
            return true;
        }
    }
}