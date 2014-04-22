using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Configuration.WpfClient.Views
{
    public class ViewFactory
    {
        public static IView CreateView(string prefix)
        {
            try
            {
                Type type = Type.GetType(string.Format("QlikView.Configuration.WpfClient.Views.{0}View", prefix));
                return (IView)Activator.CreateInstance(type);
            }
            catch
            {
                throw new Exception("Can not create the instance " + string.Format("QlikView.Configuration.WpfClient.Views.{0}View", prefix));
            }
        }
    }
}
