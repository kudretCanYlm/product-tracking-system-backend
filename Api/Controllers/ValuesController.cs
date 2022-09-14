using Api.Infrastructure.Attributes;
using DAS.Model.Model.Location;
using DAS.Service.Services.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class ValuesController : ApiController
    {

        ICountyService countyService;

        public ValuesController(ICountyService countyService)
        {
            this.countyService = countyService;
        }

        // GET api/values
        [CacheControl]
        public HttpResponseMessage Get()
        {
            var values = countyService.GetCountries();

            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // GET api/values/5
        [CacheControl]
        public HttpResponseMessage Get([FromUri]Guid id)
        {
            var values = countyService.GetCountriesWithFilter(x => x.Id == id);

            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody] CountryEntity value)
        {
            string message = countyService.AddCountry(value);

            return Request.CreateResponse(HttpStatusCode.OK, message);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
