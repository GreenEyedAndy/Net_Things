using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace SharedUI_Lib.Resources
{
    public class DesignTimeResourceDictionary : ResourceDictionary
    {
        public DesignTimeResourceDictionary()
        {
            if (Application.Current == null ||
                Application.Current.MainWindow == null ||
                DesignerProperties.GetIsInDesignMode(Application.Current.MainWindow))
            {
                var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                var uriString = string.Format("pack://application:,,,/{0};component/Resources.xaml", assemblyName);
                this.Source = new Uri(uriString, UriKind.Absolute);
            }
        }
    }
}
