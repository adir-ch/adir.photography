using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace adir.photography.Infrastructure
{
    public class WebConfigFileSection : ConfigurationSection, IConfiguration
    {
        public string GetDefaultPageFontName()
        {
            throw new NotImplementedException();
        }

        public string GetDefaultPageFontSize()
        {
            throw new NotImplementedException();
        }

        public int GetGalleryDefaultCycleInterval()
        {
            return Cycle.Delay;
        }

        public bool IsAutoCycled()
        {
            return Cycle.AutoCycle;
        }

        #region Section elemnts 

        // Create a "font" element.
        [ConfigurationProperty("font")]
        private FontElement Font
        {
            get
            { 
                return (FontElement)this["font"]; }
            set
            { 
                this["font"] = value; 
            }
        }

        // Create a "color element."
        [ConfigurationProperty("color")]
        private ColorElement Color
        {
            get
            {
                return (ColorElement)this["color"];
            }
            set
            { 
                this["color"] = value; 
            }
        }

        // Create a "color element."
        [ConfigurationProperty("PhotoCycle")]
        private PhotoCycle Cycle
        {
            get
            {
                return (PhotoCycle)this["PhotoCycle"];
            }
            set
            {
                this["PhotoCycle"] = value; 
            }
        }

        #endregion 
    }

    #region Elements implementation 
    // Define the "font" element
    // with "name" and "size" attributes.
    public class FontElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue="Arial", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String Name
        {
            get
            {
                return (String)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("size", DefaultValue = "12", IsRequired = false)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 24, MinValue = 6)]
        public int Size
        {
            get
            { 
                return (int)this["size"]; 
            }
            
            set
            { 
                this["size"] = value; 
            }
        }
    }

    // Define the "color" element 
    // with "background" and "foreground" attributes.
    public class ColorElement : ConfigurationElement
    {
        [ConfigurationProperty("background", DefaultValue = "FFFFFF", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\GHIJKLMNOPQRSTUVWXYZ", MinLength = 6, MaxLength = 6)]
        public String Background
        {
            get
            {
                return (String)this["background"];
            }
            set
            {
                this["background"] = value;
            }
        }

        [ConfigurationProperty("foreground", DefaultValue = "000000", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\GHIJKLMNOPQRSTUVWXYZ", MinLength = 6, MaxLength = 6)]
        public String Foreground
        {
            get
            {
                return (String)this["foreground"];
            }
            set
            {
                this["foreground"] = value;
            }
        }
    }

    public class PhotoCycle : ConfigurationElement
    {
        [ConfigurationProperty("delay", DefaultValue = "10", IsRequired = true)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 3600, MinValue = 10)]
        public int Delay
        {
            get
            {
                return (int)this["delay"];
            }
            set
            {
                this["delay"] = value;
            }
        }

        [ConfigurationProperty("AutoCycle", DefaultValue = true, IsRequired = true)]
        // TODO: add regex validator
        public bool AutoCycle
        {
            get
            {
                return bool.Parse(this["AutoCycle"] as string);
            }
            set
            {
                this["AutoCycle"] = value;
            }
        }
    }

    #endregion 
}