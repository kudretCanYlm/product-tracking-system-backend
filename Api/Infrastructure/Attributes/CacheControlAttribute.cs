using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Filters;

namespace Api.Infrastructure.Attributes
{
    public class CacheControlAttribute:ActionFilterAttribute
    {

        private int _maxAge { get; set; }
        /// <summary>
        /// auto set cashe time as 3600
        /// </summary>
        public CacheControlAttribute()
        {
            _maxAge = 3600;
        }

        /// <summary>
        /// set cashe time
        /// </summary>
        /// <param name="maxAge">time as seconds</param>
        public CacheControlAttribute(int maxAge)
        {
            this._maxAge = maxAge;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext != null)
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    Public=true,
                    MaxAge=TimeSpan.FromSeconds(_maxAge),
                    //will add other options
                };
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}