using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZwinqExercise.Models;
using ZwinqExercise.Services;

namespace ZwinqExercise.Controllers
{
    public class ContactController : ApiController
    {
        
        private ContactRepository contactRepository;
        public ContactController()
        {
            this.contactRepository = new ContactRepository();
        }
        public List<Vehicle> Get()
        {
            return contactRepository.GetAllVehicles();
        }
        public HttpResponseMessage Post(Vehicle contact)
        {
            this.contactRepository.SaveContact(contact);

            var response = Request.CreateResponse<Vehicle>(HttpStatusCode.Created, contact);

            return response;
        }
    }
}
