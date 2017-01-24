using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capl.Configuration
{
    public class ManagementSection : ConfigurationSection
    {
        public ManagementSection()
        {
        }

        [ConfigurationProperty("type", IsRequired=true)]
        public string Type
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }
    }
}
