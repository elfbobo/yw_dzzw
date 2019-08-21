using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Yawei.SupportCore.SupportApi.Configuration
{
    public class DefaultConfiguration : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            Dictionary<string, string> systemInfo = new Dictionary<string, string>();
            systemInfo["defaultDatabase"] = section.Attributes["defaultDatabase"].Value;
            return systemInfo;
        }
    }
}
