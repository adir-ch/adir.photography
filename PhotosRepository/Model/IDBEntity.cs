using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotosRepository.Model
{
    public interface IDBEntity
    {
        string EntityName { get; set; }
        string EntityDescription { get; set; }
        bool IsDBUpdateNeeded(); 
    }
}
