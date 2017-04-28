using System;
using System.Collections.Generic;

namespace PhotosRepository.Model
{
    public interface IDbContext
    {
        IEnumerable<Object> Fetch(string key, string specificData = null);
        bool Save(Object entity);
        bool Update(Object entity); 
    }
}
