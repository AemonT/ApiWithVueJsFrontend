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
    [EnableCors(origins: "http://localhost:1337", headers: "", methods: "")]
    public class VehicleController : ApiController
    {
        
        private VehicleRepository  vehicleRepository;
        public VehicleController()
        {
            this.vehicleRepository = new VehicleRepository();
        }
        public List<Vehicle> Get()
        {
            return vehicleRepository.GetAllVehicles();
        }
        public HttpResponseMessage Create(Vehicle vehicle)
        {
            this.vehicleRepository.CreateNewVehicle(vehicle);

            var response = Request.CreateResponse<Vehicle>(HttpStatusCode.Created, vehicle);

            return response;
        }
        public HttpResponseMessage Delete(Vehicle vehicle)
        {
            this.vehicleRepository.DeleteExistingVehicle(vehicle);

            return null;
        }
    }
}
