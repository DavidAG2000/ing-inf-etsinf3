﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EcoScooter.Persistence
{
    public interface IDAL
    {
        void Insert<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        T GetById<T>(IComparable id) where T : class;
        bool Exists<T>(IComparable id) where T : class;
        void Clear<T>() where T : class;
        IEnumerable<T> GetWhere<T>(Expression<Func<T, bool>> predicate) where T : class;

        void Commit();
        void RemoveAllData();

    }
}
