using AppWeather.Models;
using AppWeather.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace AppWeather.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        public string City
        {
            get { return this.city; }
            set
            {
                if (this.city != value)
                {
                    this.city = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.City)));
                }
            }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Name)));
                }
            }
        }

        public string Temp
        {
            get { return this.temp; }
            set
            {
                if (this.temp != value)
                {
                    this.temp = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Temp)));
                }
            }
        }

        public string Wind
        {
            get { return this.wind; }
            set
            {
                if (this.wind != value)
                {
                    this.wind = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Wind)));
                }
            }
        }

        public string Humidity
        {
            get { return this.humidity; }
            set
            {
                if (this.humidity != value)
                {
                    this.humidity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Humidity)));
                }
            }
        }
        public string Visibility
        {
            get { return this.visibility; }
            set
            {
                if (this.visibility != value)
                {
                    this.visibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Visibility)));
                }
            }
        }
        public string Sunrise
        {
            get { return this.sunrise; }
            set
            {
                if (this.sunrise != value)
                {
                    this.sunrise = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Sunrise)));
                }
            }
        }
        public string Sunset
        {
            get { return this.sunset; }
            set
            {
                if (this.sunset != value)
                {
                    this.sunset = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Sunset)));
                }
            }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set
            {
                if (this.isRunning != value)
                {
                    this.isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsRunning)));
                }
            }
        }

        private string city;
        private string name;
        private string temp;
        private string wind;
        private string humidity;
        private string visibility;
        private string sunrise;
        private string sunset;
        private bool isRunning;


        public ICommand SearchCityCommand
        {
            get { return new RelayCommand(SearchCity); }
        }

        private async void SearchCity()
        {
            this.IsRunning = true;
            CleanFields();
            if (string.IsNullOrEmpty(this.City))
            {
                this.IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Info", "El campo esta vacio", "Ok");
                return;
            }

            var connectivity = await ApiService.CheckConnection();
            if (!connectivity.IsSuccess)
            {
                this.IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", connectivity.Message, "Ok");
                return;
            }
            var response = await ApiService.Get<RootObject>(this.City);
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
                return;
            }

            var weather = (RootObject)response.Result;
            var dateBase = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.Name = weather.name;
            this.Temp = string.Format("{0} {1}", (weather.main.temp - 273.15).ToString("N2"), "°C");
            this.Wind = string.Format("{0} {1}", weather.wind.speed, "mph");
            this.Humidity = string.Format("{0} {1}", weather.main.humidity, "%");
            this.Visibility = weather.weather[0].main;
            this.Sunrise = string.Format("{0} {1}", dateBase.AddSeconds((double)weather.sys.sunrise), "UTC");
            this.Sunset = string.Format("{0} {1}", dateBase.AddSeconds((double)weather.sys.sunset), "UTC");
            this.IsRunning = false;
        }

        public void CleanFields()
        {
            this.Name = string.Empty;
            this.Temp = string.Empty;
            this.Wind = string.Empty;
            this.Humidity = string.Empty;
            this.Visibility = string.Empty;
            this.Sunrise = string.Empty;
            this.Sunset = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
