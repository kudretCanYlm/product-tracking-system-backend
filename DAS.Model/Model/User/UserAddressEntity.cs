using DAS.Model.Base;
using DAS.Model.Model.Location;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.User
{
    public class UserAddressEntity:BaseEntity
    {
        public UserAddressEntity():base()
        {

        }
        
        public string AddressLine_1 { get; set; }
        public string AddressLine_2 { get; set; }

        public Guid CityId { get; set; }
        public Guid CountryId { get; set; }
        public string PostalCode { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity UserEntity { get; set; }
        [ForeignKey("CityId")]
        public CityEntity City { get; set; }
        [ForeignKey("CountryId")]
        public CountryEntity Country { get; set; }
    }
}
