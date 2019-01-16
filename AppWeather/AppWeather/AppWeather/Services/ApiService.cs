using AppWeather.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppWeather.Services
{
    public class ApiService
    {
        static string Key = "4c8430757945f58bf2a0cd66e96629cd";

        public static async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet settings."
                };
            }

            var isReachable = await CrossConnectivity.Current.IsReachable("google.com");
            //if (!isReachable)
            //{
            //    return new Response
            //    {
            //        IsSuccess = false,
            //        Message = "check you internet connection."
            //    };
            //}

            return new Response
            {
                IsSuccess = true,
                Message = "Ok"
            };
        }

        public static async Task<Response> Get<T>(string ciudad)
        {
            var conexion = $"http://api.openweathermap.org/data/2.5/weather?q={ciudad}&appid={Key}";
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(conexion);
                    if (response != null)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        if (!response.IsSuccessStatusCode)
                        {
                            return new Response
                            {
                                IsSuccess = false,
                                Message = response.RequestMessage.ToString(),
                                Result = null
                            };
                        }

                        var data = JsonConvert.DeserializeObject<T>(result);

                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Ok",
                            Result = data
                        };

                        //if (datos["weather"] != null)
                        //{
                        //    var clima = new Weather();
                        //    clima.Titulo = (string)datos["name"];
                        //    clima.Temperatura = ((float)datos["main"]["temp"] - 273.15).ToString("N2") + "°C";
                        //    clima.Viento = (string)datos["wind"]["speed"] + "mph";
                        //    clima.Humedad = (string)datos["main"]["humidity"] + "%";
                        //    clima.Visibilidad = (string)datos["weather"][0]["main"];

                        //    var fechaBase = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                        //    var amanecer = fechaBase.AddSeconds((double)datos["sys"]["sunrise"]);
                        //    var ocaso = fechaBase.AddSeconds((double)datos["sys"]["sunset"]);
                        //    clima.Amanecer = amanecer.ToString() + "UTC";
                        //    clima.Ocaso = ocaso.ToString() + "UTC";
                        //    return clima;
                    }
                    else
                        throw new Exception("No se pudo conectar con el servidor");
                }
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Result = null
                };
            }
        }
    }
}
