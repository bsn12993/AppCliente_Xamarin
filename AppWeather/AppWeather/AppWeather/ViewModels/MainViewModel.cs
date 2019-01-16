using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeather.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Weather = new WeatherViewModel();
        }

        public WeatherViewModel Weather { get; set; }
    }
}
