using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZwinqExercise.Models
{
    /// <summary>
    /// Object that will be inserted into the database.
    /// </summary>
    public class Vehicle
    {
        public string Id { get; set; }

        public string LicensePlate { get; set; }

        public string VehicleManufacturer { get; set; }

        public string VehicleType { get; set; }

        public string FuelType { get; set; }
    }
}