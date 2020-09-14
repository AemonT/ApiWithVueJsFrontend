using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ZwinqExercise.Models;
using ZwinqExercise.Services;

namespace ZwinqExercise.Controllers
{
    [EnableCors(origins: "http://localhost:1337", headers: "*", methods: "*")]
    public class VehicleController : ApiController
    {

        private VehicleRepository vehicleRepository;
        public VehicleController()
        {
            this.vehicleRepository = new VehicleRepository();
        }
        /// <summary>
        /// Meant for adding new vehicles to the database
        /// </summary>
        /// <param name="vehicle">the vehicle that is going to be added send from the inputs on the frontend</param>
        /// <returns>all the vehicles</returns>
        public List<Vehicle> Create(Vehicle vehicle)
        {
            return this.vehicleRepository.CreateNewVehicle(vehicle);
        }
        /// <summary>
        /// Meant for getting vehicles from the api
        /// </summary>
        /// <param name="vehicle">Can be a certain type of vehicle</param>
        /// <returns>List of all asked vehicles</returns>
        public List<Vehicle> Get(Vehicle vehicle)
        {
            return this.vehicleRepository.SearchOnFuelType(vehicle);
        }
        /// <summary>
        /// Meant for deleting the given vehicle out of the database
        /// </summary>
        /// <param name="vehicle">The vehicle that needs to be deleted</param>
        /// <returns>The list without the deleted vehicle</returns>
        public List<Vehicle> Delete(Vehicle vehicle)
        {
            return this.vehicleRepository.DeleteExistingVehicle(vehicle);
        }
    }
}
