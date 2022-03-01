using System;
using System.Collections.Generic;


namespace Database.DataAccess
{
    public interface IFIMDB<T>
    {
        IEnumerable<T> GetAll();
        void Update(T entity);
        void UpdateLastKnownSignature(T entity);
    }
}
