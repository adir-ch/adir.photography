using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adir.photography.Infrastructure
{
    public interface IConfiguration
    {
        string GetDefaultPageFontName();
        string GetDefaultPageFontSize();
        int GetGalleryDefaultCycleInterval();
        bool IsAutoCycled(); 
    }
}
