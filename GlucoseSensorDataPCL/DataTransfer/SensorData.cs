namespace GlucoseSensorDataPCL.DataTransfer
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SensorData
    {
        public class Status
        {
            public long now { get; set; }               // Current date timestamp
        }

        public class Bg
        {
            public string sgv { get; set; }             // Sensor Glucose Value
            public int bgdelta { get; set; }            // Change since last reading
            public int trend { get; set; }              // Trend of readings
            public string direction { get; set; }       // Current direction - e.g. "Flat", "Up"
            public long datetime { get; set; }          // Current date timestamp
            public int battery { get; set; }            // Percentage battery left on device
        }

        [JsonObject]
        public class SensorDataRecord
        {
            public List<Status> status { get; set; }    // List of Status Values
            public List<Bg> bgs { get; set; }           // List of BG values
        }
    }
}
