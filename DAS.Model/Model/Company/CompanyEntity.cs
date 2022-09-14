using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using DAS.Model.Model.Authentication;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAS.Model.Model.Company
{
    public class CompanyEntity : BaseEntity, IBaseTimeEntity
    {
        public CompanyEntity() : base()
        {
        }

        public CompanyEntity(string companyName, string about, string companyWebSite, string address) : base()
        {
            this.CompanyName = companyName;
            this.CompanyWebSite = companyWebSite;
            this.Address = address;
            this.About = about;
        }

        public string CompanyName { get; set; }
        public Guid CompanyOwnerId { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public string CompanyWebSite { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("CompanyOwnerId")]
        public LoginEntity CompanyOwner { get; set; }
    }

    public interface ICompanyValidation : IValidator<CompanyEntity>
    {

    }

    public class CompanyValidation : AbstractValidator<CompanyEntity>,ICompanyValidation
    {
        public CompanyValidation()
        {
            RuleFor(x => x.CompanyName).Length(2, 50).NotNull().NotEmpty();
            RuleFor(x => x.About).Length(10, 500).NotNull().NotEmpty();
            RuleFor(x => x.Address).Length(5, 100).NotNull().NotEmpty();
            RuleFor(x => x.CompanyWebSite).Length(5, 30).NotNull().NotEmpty();
            RuleFor(x => x.CreatedAt).NotNull().NotEmpty();
            RuleFor(x => x.CompanyOwner).SetValidator(new LoginValidation());
        }
    }
}
