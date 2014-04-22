using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class ReportConfigManagerFactory<T> where T : new()
    {
        private static T instance = default(T);
        static readonly object padlock = new object();

        public static T Create()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }
            return instance;
        }
    }
}
