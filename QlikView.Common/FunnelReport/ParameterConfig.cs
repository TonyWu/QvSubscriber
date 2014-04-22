using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace QlikView.Common
{
    public class ParameterConfig
    {
        private static string ConfigFile;
        private static XElement Doc;
        static ParameterConfig()
        {
            string folder = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Remove(System.Reflection.Assembly.GetExecutingAssembly().CodeBase.LastIndexOf("/"));
            folder = folder.Replace("file:///", "");
            ConfigFile = folder + "/" + System.Configuration.ConfigurationManager.AppSettings["Parameters"];
            Doc = XElement.Load(ConfigFile);
        }

        public static string GetStringValue(string category, string key)
        {
            try
            {
                return Doc.Element(category).Element(key).Value;
            }
            catch
            {
                return null;
            }
        }

        public static int GetIntValue(string category, string key)
        {
            string value = Doc.Element(category).Element(key).Value;
            int intValue;

            if (int.TryParse(value, out intValue))
            {
                return intValue;
            }
            else
            {
                return 0;
            }
        }

        public static bool GetBoolValue(string category, string key)
        {
            string value = Doc.Element(category).Element(key).Value;
            bool boolValue;

            if (bool.TryParse(value, out boolValue))
            {
                return boolValue;
            }
            else
            {
                return false;
            }
        }
    }
}
