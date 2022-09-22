using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models.Location
{
    public class CountryViewModel
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }

    public class CountryPostModel
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }
}