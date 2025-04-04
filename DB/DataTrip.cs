using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask_DevelopsToday.DB
{
    public class DataTrip
    {
        public int Id { get; set; }
        public DateTime PickupDatetime { get; set; }
        public DateTime DropoffDatetime { get; set; }
        public int PassengerCount { get; set; }
        public double TripDistance { get; set; }
        public string StoreAndFwdFlag { get; set; }
        public int PULocationID { get; set; }
        public int DOLocationID { get; set; }
        public decimal FareAmount { get; set; }
        public decimal TipAmount { get; set; }
    }
    public class DataTripString
    {
        public string tpep_pickup_datetime { get; set; }
        public string tpep_dropoff_datetime { get; set; }
        public string passenger_count { get; set; }
        public string trip_distance { get; set; }
        public string store_and_fwd_flag { get; set; }
        public string PULocationID { get; set; }
        public string DOLocationID { get; set; }
        public string fare_amount { get; set; }
        public string tip_amount { get; set; }

        public DataTrip ToTaxiTrip(TimeZoneInfo est)
        {
            return new DataTrip
            {
                PickupDatetime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse(tpep_pickup_datetime, CultureInfo.InvariantCulture), est),
                DropoffDatetime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse(tpep_dropoff_datetime, CultureInfo.InvariantCulture), est),
                PassengerCount = string.IsNullOrWhiteSpace(passenger_count) ? 0 : int.Parse(passenger_count, CultureInfo.InvariantCulture),
                TripDistance = string.IsNullOrWhiteSpace(trip_distance) ? 0.0 : double.Parse(trip_distance, CultureInfo.InvariantCulture),
                StoreAndFwdFlag = store_and_fwd_flag.Trim().ToUpper() == "Y" ? "Yes" : "No",
                PULocationID = string.IsNullOrWhiteSpace(PULocationID) ? 0 : int.Parse(PULocationID, CultureInfo.InvariantCulture),
                DOLocationID = string.IsNullOrWhiteSpace(DOLocationID) ? 0 : int.Parse(DOLocationID, CultureInfo.InvariantCulture),
                FareAmount = string.IsNullOrWhiteSpace(fare_amount) ? 0.0m : decimal.Parse(fare_amount, CultureInfo.InvariantCulture),
                TipAmount = string.IsNullOrWhiteSpace(tip_amount) ? 0.0m : decimal.Parse(tip_amount, CultureInfo.InvariantCulture)
            };
        }
    }
}
