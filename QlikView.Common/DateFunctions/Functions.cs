using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace QlikView.Common
{
    public class Functions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<FieldValue> GetValuesFromLastMonthFiscalYearMonth()
        {
            ObservableCollection<FieldValue> values = new ObservableCollection<FieldValue>();

            int currentMonth = DateTime.Today.AddMonths(-1).Month;
            int currentYear = DateTime.Today.AddMonths(-1).Year;
            if (currentMonth >= 10)
            {
                for (int i = 10; i <= currentMonth; i++)
                {
                    values.Add(new FieldValue()
                    {
                        Number = double.Parse("1E+300"),
                        IsNumeric = false,
                        Value = currentYear.ToString() + "/" + i.ToString("00") + "/01"
                    });
                }
            }
            else
            {
                values.Add(new FieldValue()
                {
                    Number = double.Parse("1E+300"),
                    IsNumeric = false,
                    Value = (currentYear - 1).ToString() + "/10" + "/01"
                });

                values.Add(new FieldValue()
                {
                    Number = double.Parse("1E+300"),
                    IsNumeric = false,
                    Value = (currentYear - 1).ToString() + "/11" + "/01"
                });

                values.Add(new FieldValue()
                {
                    Number = double.Parse("1E+300"),
                    IsNumeric = false,
                    Value = (currentYear - 1).ToString() + "/12" + "/01"
                });            

                for (int i = 1; i <= currentMonth; i++)
                {
                    values.Add(new FieldValue()
                    {
                        Number = double.Parse("1E+300"),
                        IsNumeric = false,
                        Value = currentYear.ToString() + "/" + i.ToString("00") + "/01"
                    });
                }
            }

            return values;
        }

        /// <summary>
        /// Just for getting current Year Month
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<FieldValue> GetValuesFromCurrentYearMonth()
        {
            ObservableCollection<FieldValue> values = new ObservableCollection<FieldValue>();

            int currentMonth = DateTime.Today.Month;
            int currentYear = DateTime.Today.Year;

            values.Add(new FieldValue()
            {
                Number = double.Parse("1E+300"),
                IsNumeric = false,
                Value = currentYear.ToString() + "-" + currentMonth.ToString("00")
            });

            return values;
        }

        /// <summary>
        /// Just for getting current Year Month
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<FieldValue> GetValuesFromCurrentYearMonthNumeric()
        {
            ObservableCollection<FieldValue> values = new ObservableCollection<FieldValue>();

            int currentMonth = DateTime.Today.Month;
            int currentYear = DateTime.Today.Year;

            values.Add(new FieldValue()
            {
                Number = new TimeSpan(new DateTime(currentYear, currentMonth, 1).Ticks - DateTime.Parse("12/30/1899").Ticks).Days,
                IsNumeric = true,
                Value = currentYear.ToString() + "-" + currentMonth.ToString("00")
            });

            return values;
        }

        /// <summary>
        /// For getting the whole Year Month for the current fiscal year
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<FieldValue> GetValuesFromCurrentFiscalYearMonthEx()
        {
            ObservableCollection<FieldValue> values = new ObservableCollection<FieldValue>();

            int currentMonth = DateTime.Today.Month;
            int currentYear = DateTime.Today.Year;
            if (currentMonth >= 10)
            {
                for (int i = 10; i <= currentMonth; i++)
                {
                    values.Add(new FieldValue()
                    {
                        Number = new TimeSpan(new DateTime(currentYear, i, 1).Ticks - DateTime.Parse("12/30/1899").Ticks).Days,
                        IsNumeric = true,
                        Value = currentYear.ToString() + "-" + i.ToString("00")
                    });
                }
            }
            else
            {

                for (int i = 10; i <= 12; i++)
                {
                    values.Add(new FieldValue()
                    {
                        Number = new TimeSpan(new DateTime(currentYear-1, i, 1).Ticks - DateTime.Parse("12/30/1899").Ticks).Days,
                        IsNumeric = true,
                        Value = (currentYear - 1).ToString() + "-" + i.ToString("00")
                    });
                }

                for (int i = 1; i <= currentMonth; i++)
                {
                    values.Add(new FieldValue()
                    {
                        Number = new TimeSpan(new DateTime(currentYear, i, 1).Ticks - DateTime.Parse("12/30/1899").Ticks).Days,
                        IsNumeric = true,
                        Value = currentYear.ToString() + "-" + i.ToString("00")
                    });
                }
            }

            return values;
        }

        /// <summary>
        /// For getting the whole Year Month for the current fiscal year
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<FieldValue> GetValuesFromCurrentFiscalYearMonth()
        {
            ObservableCollection<FieldValue> values = new ObservableCollection<FieldValue>();

            int currentMonth = DateTime.Today.Month;
            int currentYear = DateTime.Today.Year;
            if (currentMonth >= 10)
            {
                for (int i = 4; i <= currentMonth; i++)
                {
                    values.Add(new FieldValue()
                    {
                        Number = double.Parse("1E+300"),
                        IsNumeric = false,
                        Value = currentYear.ToString() + "-" + i.ToString("00")
                    });
                }
            }
            else
            {

                for (int i = 4; i <= 12; i++)
                {
                    values.Add(new FieldValue()
                    {
                        Number = double.Parse("1E+300"),
                        IsNumeric = false,
                        Value = (currentYear - 1).ToString() + "-" + i.ToString("00")
                    });
                }               

                for (int i = 1; i <= currentMonth; i++)
                {
                    values.Add(new FieldValue()
                    {
                        Number = double.Parse("1E+300"),
                        IsNumeric = false,
                        Value = currentYear.ToString() + "-" + i.ToString("00")
                    });
                }
            }

            return values;
        }

        /// <summary>
        /// For getting the whole Year Month for the latest two fiscal years
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<FieldValue> GetValuesFromLatestTwoFiscalYearMonth()
        {
            ObservableCollection<FieldValue> values = new ObservableCollection<FieldValue>();

            int currentMonth = DateTime.Today.Month;
            int currentYear = DateTime.Today.Year;
            if (currentMonth >= 10)
            {
                values.Add(new FieldValue()
                {
                    Number = double.Parse("1E+300"),
                    IsNumeric = false,
                    Value = (currentYear - 1).ToString() + "-10"
                });

                values.Add(new FieldValue()
                {
                    Number = double.Parse("1E+300"),
                    IsNumeric = false,
                    Value = (currentYear - 1).ToString() + "-11"
                });

                values.Add(new FieldValue()
                {
                    Number = double.Parse("1E+300"),
                    IsNumeric = false,
                    Value = (currentYear - 1).ToString() + "-12"
                });

                for (int i = 1; i <= currentMonth; i++)
                {
                    values.Add(new FieldValue()
                    {
                        Number = double.Parse("1E+300"),
                        IsNumeric = false,
                        Value = currentYear.ToString() + "-" + i.ToString("00")
                    });
                }

            }
            else
            {
                values.Add(new FieldValue()
                {
                    Number = double.Parse("1E+300"),
                    IsNumeric = false,
                    Value = (currentYear - 2).ToString() + "-10"
                });

                values.Add(new FieldValue()
                {
                    Number = double.Parse("1E+300"),
                    IsNumeric = false,
                    Value = (currentYear - 2).ToString() + "-11"
                });

                values.Add(new FieldValue()
                {
                    Number = double.Parse("1E+300"),
                    IsNumeric = false,
                    Value = (currentYear - 2).ToString() + "-12"
                });

                for (int i = 1; i <= 12; i++)
                {
                    values.Add(new FieldValue()
                    {
                        Number = double.Parse("1E+300"),
                        IsNumeric = false,
                        Value = (currentYear - 1).ToString() + "-" + i.ToString("00")
                    });
                }

                for (int i = 1; i <= currentMonth; i++)
                {
                    values.Add(new FieldValue()
                    {
                        Number = double.Parse("1E+300"),
                        IsNumeric = false,
                        Value = currentYear.ToString() + "-" + i.ToString("00")
                    });
                }
            }

            return values;
        }

        public static List<int> GetDateValueFromExpression(string p)
        {
            if (p.StartsWith("YearOfTheDayBeforeCurrent"))
            {
                return new List<int>(){ DateTime.Today.AddDays(-1).Year};
            }
            else if (p.StartsWith("MonthOfTheDayBeforeCurrent"))
            {
                return new List<int>(){ DateTime.Today.AddDays(-1).Month};
            }
            else if (p.StartsWith("DayOfTheDayBeforeCurrent"))
            {
                return new List<int>(){DateTime.Today.AddDays(-1).Day};
            }
            else if (p.StartsWith("YearOfTheDayCurrent"))
            {
                return new List<int>(){DateTime.Today.Year};
            }
            else if (p.StartsWith("MonthOfTheDayCurrent"))
            {
                return new List<int>(){DateTime.Today.Month};
            }
            else if (p.StartsWith("LatestTwoMonthOfTheDayCurrent"))
            {
                return new List<int>() { 1, 2 };
            }
            else if (p.StartsWith("DayOfTheDayCurrent"))
            {
                return new List<int>(){DateTime.Today.Day};
            }
            else
            {
                return new List<int>(){0};
            }
        }
    }
}
