using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesforceCodeGenerator
{
    class ReportParameter
    {
        
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string ObjectId { get; set; }

        public List<string> FilterValue { get; set; }

        public static Dictionary<string, string> ProductLine = new Dictionary<string, string>();
        public static Dictionary<string, string> FilterDescription = new Dictionary<string, string>();
        public static Dictionary<string, List<string>> FilterValueMapping = new Dictionary<string, List<string>>();
        public static Dictionary<string, string> StudentAttObjectId = new Dictionary<string, string>();
        public static Dictionary<string, string> StudentDescription = new Dictionary<string, string>();
        public static Dictionary<string, List<string>> StudentFilterValueMapping = new Dictionary<string, List<string>>();

        static ReportParameter()
        {
            ProductLine.Add("Writing", "Writing");
            ProductLine.Add("EFTVDropoff", "EFTV Drop off");
            ProductLine.Add("EFTV", "EFTV");
            ProductLine.Add("PLDropoff", "PL Drop off");
            ProductLine.Add("PL", "PL");
            ProductLine.Add("GL", "GL");
            ProductLine.Add("PLLateCancel", "PL Late Cancellation");

            StudentAttObjectId.Add("GL", "CH848");
            StudentAttObjectId.Add("PL", "CH849");
            StudentAttObjectId.Add("PLLateCancel", "CH905");
            StudentAttObjectId.Add("PLDropoff", "CH873");
            StudentAttObjectId.Add("EFTV", "CH858");
            StudentAttObjectId.Add("EFTVDropoff", "CH874");
            StudentAttObjectId.Add("Writing", "CH871");

            StudentDescription.Add("EFLabsStaff", "EFLabs Staff");
            StudentDescription.Add("SmartOld", "Smart Old");
            StudentDescription.Add("SmartHome", "SmartHome");
            StudentDescription.Add("SmartSch", "SmartSch");
            StudentDescription.Add("Sochi", "Sochi");
            StudentDescription.Add("E1", "E1");
            StudentDescription.Add("ILS", "ILS");
            StudentDescription.Add("Mini", "Mini");
            StudentDescription.Add("B2S", "B2S");
            StudentDescription.Add("B2B", "B2B");
            StudentDescription.Add("Freetown", "Freetown");
            StudentDescription.Add("Others", "Others");
            StudentDescription.Add("Groupon", "Groupon");
            StudentDescription.Add("B2CAMXUOL", "B2CAMXUOL");
            StudentDescription.Add("B2COS", "OS");
            StudentDescription.Add("B2CCS", "CS");
            StudentDescription.Add("B2CTS", "TS");

            StudentFilterValueMapping.Add("B2CTS", new List<string>() { "TeleSales" });
            StudentFilterValueMapping.Add("B2CCS", new List<string>() { "CenterSales" });
            StudentFilterValueMapping.Add("B2COS", new List<string>() { "Online" });
            StudentFilterValueMapping.Add("B2CAMXUOL", new List<string>() { "B2CAMXUOL" });
            StudentFilterValueMapping.Add("Groupon", new List<string>() { "Groupon" });
            StudentFilterValueMapping.Add("Others", new List<string>() { "B2COthers" });
            StudentFilterValueMapping.Add("Freetown", new List<string>() { "Freetown" });
            StudentFilterValueMapping.Add("B2B", new List<string>() { "CLLS", "Ola_Tourista" });
            StudentFilterValueMapping.Add("B2S", new List<string>() { "B2S" });
            StudentFilterValueMapping.Add("Mini", new List<string>() { "Mini" });
            StudentFilterValueMapping.Add("ILS", new List<string>() { "Ilabs", "MYEF" });
            StudentFilterValueMapping.Add("E1", new List<string>() { "COEF", "Owned" });
            StudentFilterValueMapping.Add("Sochi", new List<string>() { "Sochi" });
            StudentFilterValueMapping.Add("SmartSch", new List<string>() { "SmartSchool" });
            StudentFilterValueMapping.Add("SmartHome", new List<string>() { "SmartHome" });
            StudentFilterValueMapping.Add("EFLabsStaff", new List<string>() { "EFLabsStaff" });
            StudentFilterValueMapping.Add("SmartOld", new List<string>() { "SmartOld" });

            FilterDescription.Add("EFLabsStaff", "EFLabs Staff");
            FilterDescription.Add("SmartOld", "Smart Old");
            FilterDescription.Add("SmartHome", "SmartHome");
            FilterDescription.Add("SmartSch", "SmartSch");
            FilterDescription.Add("Sochi", "Sochi");
            FilterDescription.Add("E1", "E1");
            FilterDescription.Add("ILS", "ILS");
            FilterDescription.Add("Mini", "Mini");
            FilterDescription.Add("B2S", "B2S");
            FilterDescription.Add("B2B", "B2B");
            FilterDescription.Add("Freetown", "Freetown");
            FilterDescription.Add("Others", "Other");
            FilterDescription.Add("Groupon", "Groupon");
            FilterDescription.Add("B2CAMXUOL", "B2CAMXUOL");
            FilterDescription.Add("B2COS", "OS");
            FilterDescription.Add("B2CCS", "Center Sales");
            FilterDescription.Add("B2CTS", "TS");


            FilterValueMapping.Add("B2CTS", new List<string>() { "TeleSales" });
            FilterValueMapping.Add("B2CCS", new List<string>() { "CenterSales" });
            FilterValueMapping.Add("B2COS", new List<string>() { "Online" });
            FilterValueMapping.Add("B2CAMXUOL", new List<string>() { "B2CAMXUOL" });
            FilterValueMapping.Add("Groupon", new List<string>() { "Groupon" });
            FilterValueMapping.Add("Others", new List<string>() { "B2COthers" });
            FilterValueMapping.Add("Freetown", new List<string>() { "Freetown" });
            FilterValueMapping.Add("B2B", new List<string>() { "CLLS", "Ola_Tourista" });
            FilterValueMapping.Add("B2S", new List<string>() { "B2S" });
            FilterValueMapping.Add("Mini", new List<string>() { "MiniHome","MiniSchool" });
            FilterValueMapping.Add("ILS", new List<string>() { "Ilabs", "MYEF" });
            FilterValueMapping.Add("E1", new List<string>() { "COEF", "Owned" });
            FilterValueMapping.Add("Sochi", new List<string>() { "Sochi" });
            FilterValueMapping.Add("SmartSch", new List<string>() { "SmartSchool" });
            FilterValueMapping.Add("SmartHome", new List<string>() { "SmartHome" });
            FilterValueMapping.Add("EFLabsStaff", new List<string>() { "EFLabsStaff" });
            FilterValueMapping.Add("SmartOld", new List<string>() { "SmartOld" });
        }

        public static List<ReportParameter> PopulateSTN()
        {
            var stn = new List<ReportParameter>();

            foreach (var item in StudentDescription.Keys)
            {
                stn.Add(new ReportParameter() { 
                Name = item,
                FilterValue = StudentFilterValueMapping[item],
                Description = StudentDescription[item],
                ObjectId = "CH866"
                });
            }

            return stn;
        }

        public static List<ReportParameter> PopulateDirectCost()
        {
            var sta = new List<ReportParameter>();

            foreach (var productLine in ProductLine.Keys)
            {
                foreach (var item in FilterDescription.Keys)
                {
                    sta.Add(new ReportParameter()
                    {
                        Name = productLine + item,
                        FilterValue = FilterValueMapping[item],
                        Description = FilterDescription[item] + " " +ProductLine[productLine],
                        ObjectId = StudentAttObjectId[productLine]
                    });
                }
            }

            return sta;
        }
    }
}
