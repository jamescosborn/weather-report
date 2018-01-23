﻿using System; using System.Collections.Generic; using System.Threading.Tasks; using RestSharp; using RestSharp.Authenticators; using Newtonsoft.Json; using Newtonsoft.Json.Linq;  namespace WeatherChannel.Models {     public class WeatherReport     {         public string Summary { get; set; }         public int Temperature { get; set; }         public int precipProbability { get; set; }         public int cloudCover { get; set; }         public int Longitude { get; set; }         public int Latitude { get; set; }           public List<WeatherReport> GetWeatherReports()         {             var client = new RestClient("https://api.darksky.net/forecast");             var request = new RestRequest(EnvironmentVariables.AccountSid + "/" + this.Longitude + "," + this.Latitude + ".json", Method.GET);             var response = new RestResponse();             Task.Run(async () =>             {                 response = await GetResponseContentAsync(client, request) as RestResponse;             }).Wait();             JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);             var weatherList = JsonConvert.DeserializeObject<List<WeatherReport>>(jsonResponse["currently"].ToString());             return weatherList;         }          public void Request()         {             var client = new RestClient("https://api.darksky.net/forecast");             var request = new RestRequest(EnvironmentVariables.AccountSid + "/" + Longitude + "," + Latitude, Method.POST);             request.AddParameter("Longitude", Longitude);             request.AddParameter("Latitude", Latitude);              client.ExecuteAsync(request, response => {                 Console.WriteLine(response.Content);             });         }          public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)         {             var tcs = new TaskCompletionSource<IRestResponse>();             theClient.ExecuteAsync(theRequest, response => {                 tcs.SetResult(response);             });             return tcs.Task;         }     } } 

