using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using GlucoseSensorDataPCL.Models;
using Newtonsoft.Json;
using GlucoseSensorDataPCL.Utilities;

namespace GlucoseSensorDataPCL.Services
{
    public class SensorDataService
    {
        public static async Task<BGRecord> GetCurrentSensorReading()
        {
            DataTransfer.SensorData.SensorDataRecord sensorReading = new DataTransfer.SensorData.SensorDataRecord();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://dareidband.azurewebsites.net"); //using my dad's Fake Blood Sugar service on Azure
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("api/Band"); // for pebble endpoint use pebble?units=mmol
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    sensorReading = JsonConvert.DeserializeObject<DataTransfer.SensorData.SensorDataRecord>(json);
                }
            }

            BGRecord currentBG = new BGRecord()
            {
                now = DateConverter.ToDateTime(sensorReading.status[0].now),
                sgv = sensorReading.bgs[0].sgv,
                bgdelta = sensorReading.bgs[0].bgdelta,
                trend = sensorReading.bgs[0].trend,
                direction = sensorReading.bgs[0].direction,
                datetime = sensorReading.bgs[0].datetime,
                battery = sensorReading.bgs[0].battery
            };

            return currentBG;   
        }
    }
}
