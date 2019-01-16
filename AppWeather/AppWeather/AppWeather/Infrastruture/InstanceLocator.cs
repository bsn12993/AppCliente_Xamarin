using AppWeather.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeather.Infrastruture
{
    public class InstanceLocator
    {
        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
        public MainViewModel Main { get; set; }
    }
}
