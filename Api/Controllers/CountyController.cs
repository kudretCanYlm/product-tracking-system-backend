using DAS.Model.Model.Location;
using DAS.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{
    public class CountyController:ApiController
    {
        ICountyService countyService;

        public CountyController(ICountyService countyService)
        {
            this.countyService = countyService;
        }

        public HttpResponseMessage GetCountries()
        {
            var values = countyService.GetCountries();

            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
    }
}