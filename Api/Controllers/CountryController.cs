using Api.Infrastructure.Attributes;
using DAS.Model.Model.Enums;
using DAS.Model.Model.Location;
using DAS.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("country")]
    [JwtAuthentication(RoleEnum.Admin)]
    public class CountryController : ApiController
    {
        ICountyService countyService;

        public CountryController(ICountyService countyService)
        {
            this.countyService = countyService;
        }

        [CacheControl]
        public HttpResponseMessage Get()
        {
            var values = countyService.GetCountries();

            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [CacheControl]
        public HttpResponseMessage Get([FromUri] Guid id)
        {
            var values = countyService.GetCountriesWithFilter(x => x.Id == id);

            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        public HttpResponseMessage Post([FromBody] CountryEntity value)
        {
            string message = countyService.AddCountry(value);

            return Request.CreateResponse(HttpStatusCode.OK, message);
        }

        public HttpResponseMessage Put([FromUri] Guid id, [FromBody] CountryEntity countryUpdate)
        {
            var country = countyService.GetSingleCountry(id);

            if (country != null )
            {
                if(countryUpdate!=null)
                {
                    country.CountryName = countryUpdate.CountryName;
                    country.CountryCode = countryUpdate.CountryCode;

                    string message = countyService.UpdateCountry(country);

                    return Request.CreateResponse(HttpStatusCode.OK, message);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "null counrty");
                
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "not find country");
        }

        public HttpResponseMessage Delete([FromUri] Guid id)
        {
            var country = countyService.GetSingleCountry(id);

            if(country!=null)
            {
                string message = countyService.DeleteCountry(country);

                return Request.CreateResponse(HttpStatusCode.Created, message);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "not find country");

        }
    }
}
