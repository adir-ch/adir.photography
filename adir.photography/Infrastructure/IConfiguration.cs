using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adir.photography.Infrastructure
{
    public interface IConfiguration
    {
        string ImageLocation { get; set; }
        bool IsAutoDelayEnabled { get; set; }
        int TimeOut { get; set; }
    }
}
