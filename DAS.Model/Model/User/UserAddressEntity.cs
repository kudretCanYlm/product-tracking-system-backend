using DAS.Model.Base;
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

        //will change
        public int City { get; set; }
        public int Country { get; set; }
        public string PostalCode { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity UserEntity { get; set; }
    }
}
