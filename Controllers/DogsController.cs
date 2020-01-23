using DogIntelligence.Shared;
using DogIntelligence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace DogIntelligence.Controllers
{
    public class DogsController : ApiController
    {
        DAL dal = new DAL();
        [ResponseType(typeof(List<Dog>))]
        public async Task<IHttpActionResult> Get()
        {
            var GetDogs = Task.Run(() => dal.SendDogs());
            List<Dog> dogs = await GetDogs;
            return Content(HttpStatusCode.OK, dogs);
        }
    }
}
