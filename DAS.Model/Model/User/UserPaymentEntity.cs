using DAS.Model.Base;
using DAS.Model.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.User
{
    public class UserPaymentEntity:BaseEntity
    {
        public UserPaymentEntity():base()
        {

        }

        public UserPaymentEntity(string provider,string accountNo,PaymentEnum paymentType):base()
        {
            //provider control
            if (String.IsNullOrEmpty(provider))
                this.Provider = "Empty";
            else
                this.Provider = provider;
            //TEST

            //accountNo control
            if (String.IsNullOrEmpty(accountNo))
                this.AccountNo = "Empty";
            else
                this.AccountNo = accountNo;
        }

        
        public string Provider { get; set; }
        public string AccountNo { get; set; }
        public DateTime Expiry { get; set; }
        public PaymentEnum PaymentType { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity UserEntity { get; set; }
    }
}
