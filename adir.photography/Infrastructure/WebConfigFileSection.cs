using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace adir.photography.Infrastructure
{
    public class WebConfigFileSection : ConfigurationSection, IConfiguration
    {

        public string ImageLocation
        {
            get
            {
                return Location.Path; 
            }
            set
            {
                Location.Path = value; 
            }
        }

        public bool IsAutoDelayEnabled
        {
            get
            {
                return Cycle.AutoCycle;
            }
            set
            {
                Cycle.AutoCycle = value;
            }
        }

        public int TimeOut
        {
            get
            {
                return Cycle.TimeOut;
            }
            set
            {
                Cycle.TimeOut = value; 
            }
        }

        #region Section elemnts

        // Create a "font" element.
        [ConfigurationProperty("font")]
        public FontElement Font
        {
            get
            {
                return (FontElement)this["font"];
            }
            set
            {
                this["font"] = value;
            }
        }

        // Create a "color element."
        [ConfigurationProperty("color")]
        public ColorElement Color
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

        // Create a "Cycle element"
        [ConfigurationProperty("PhotoCycle")]
        public PhotoCycle Cycle
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

        // Create a "Path element"
        [ConfigurationProperty("ImageLocation")]
        public ImageLocation Location
        {
            get
            {
                return (ImageLocation)this["ImageLocation"];
            }
            set
            {
                this["ImageLocation"] = value;
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
        [ConfigurationProperty("timeout", DefaultValue = "10", IsRequired = true)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 600, MinValue = 5)]
        public int TimeOut
        {
            get
            {
                return (int)this["timeout"];
            }
            set
            {
                this["timeout"] = value;
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

    public class ImageLocation : ConfigurationElement
    {
        [ConfigurationProperty("path", DefaultValue = "~/Content/images/", IsRequired = true)]
        public string Path
        {
            get
            {
                return (string)this["path"];
            }
            set
            {
                this["path"] = value;
            }
        }
    }

    #endregion 
}