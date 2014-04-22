using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{

    public enum EnumExpress
    {
        Today,
        Now,
        AddDays
    }

    public class ExpressionCondition
    {
        public string Condition { get; set; }
        public DateTime Boundary { get; set; }

        public bool IsChecked(DateTime date)
        {
            switch (Condition)
            {
                case "=":
                    return date == Boundary;
                case ">":
                    return date > Boundary;
                case ">=":
                    return date >= Boundary;
                case "<":
                    return date < Boundary;
                case "<=":
                    return date <= Boundary;
            }

            return false;
        }
    }

    public class DateFunctions
    {
        public static List<ExpressionCondition> CalculateExpression(string expression)
        {
            expression = expression.Trim();
            string[] expressions = expression.Split(' ');

            List<ExpressionCondition> conditions = new List<ExpressionCondition>();

            foreach (var item in expressions)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    string temp = string.Empty;

                    if (item.StartsWith("="))
                    {
                        temp = "=";
                    }
                    else if (item.StartsWith(">"))
                    {
                        temp = ">";
                    }
                    else if (item.StartsWith("<"))
                    {
                        temp = "<";
                    }
                    else if (item.StartsWith(">="))
                    {
                        temp = ">=";
                    }
                    else if (item.StartsWith("<="))
                    {
                        temp = "<=";
                    }

                    conditions.Add(new ExpressionCondition()
                    {
                        Condition = temp,
                        Boundary = GetValueFromExpression(item.Replace(temp, ""))
                    });
                }
            }

            return conditions;
        }

        public static DateTime GetValueFromExpression(string p)
        {
            if (p.StartsWith("Today.AddDays"))
            {
                int span = int.Parse(p.Replace("Today.AddDays(", "").Replace(")",""));
                return DateTime.Now.Date.AddDays(span);
            }
            else if (p.StartsWith("Now.AddDays"))
            {
                int span = int.Parse(p.Replace("Now.AddDays(", "").Replace(")", ""));
                return DateTime.Now.AddDays(span);
            }
            else if (p == "Today")
            {
                return DateTime.Now.Date;
            }
            else if (p.StartsWith("CDate("))
            {
                string dateStr = p.Replace("CDate(\"", "").Replace("\")", "");
                return DateTime.Parse(dateStr).Date;
            }
            else if(p.StartsWith("LastWeekendDate"))
            {
                return DateTime.Now.LastWeekendDate();
            }
            else if (p.StartsWith("LastTwoWeeksEndDate"))
            {
                return DateTime.Now.LastTwoWeeksEndDate();
            }
            else if (p.StartsWith("LastMonthendDate"))
            {
                return DateTime.Now.Date.AddDays(0 - DateTime.Now.Day);
            }

            return DateTime.Now;
        }

        public bool ValidateExpression(string expression)
        {
            return true;
        }
    }
}
