using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adir.photography.Infrastructure
{
    interface IConfiguration
    {
        public string ImageLocation { get; set; }
        public bool IsAutoDelayEnabled { get; set; }
        public int DelayInMiliSeconds { get; set; }

    }
}
