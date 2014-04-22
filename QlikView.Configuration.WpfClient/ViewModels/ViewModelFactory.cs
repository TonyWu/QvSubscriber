using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Configuration.WpfClient.ViewModels
{
    public class ViewModelFactory
    {
        public static IViewModel CreateViewModel(string prefix)
        {
            Type type = null;
            try
            {
                type = Type.GetType(string.Format("QlikView.Configuration.WpfClient.ViewModels.{0}ViewModel", prefix));

                if(type == null)
                    type = Type.GetType("QlikView.Configuration.WpfClient.ViewModels.ViewModelBase");
            }
            catch
            {
                throw new Exception("Can not create instance " + string.Format("QlikView.Configuration.WpfClient.ViewModels.{0}ViewModel", prefix));
            }

            return (IViewModel)Activator.CreateInstance(type);
        }
    }
}
