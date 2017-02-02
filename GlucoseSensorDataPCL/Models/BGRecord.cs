namespace GlucoseSensorDataPCL.Models
{
    using System;
    using SQLite;

    public class BGRecord
    {
        public BGRecord()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime now { get; set; }           // Date/Time of status record
        public string sgv { get; set; }             // Sensor Glucose Value
        public int bgdelta { get; set; }            // Change since last reading
        public int trend { get; set; }              // Trend of readings
        public string direction { get; set; }       // Current direction - e.g. "Flat", "Up"
        public long datetime { get; set; }          // Current date timestamp
        public int battery { get; set; }            // Percentage battery left on device
    }
}
