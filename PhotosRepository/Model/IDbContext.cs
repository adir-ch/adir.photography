using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotosRepository.Model
{
    public interface IDbContext
    {
        IEnumerable<Object> Fetch(string key, string specificData = null);
        bool Save(Object entity);
        bool Update(Object entity); 
    }
}
