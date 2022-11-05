using DAS.Model.Base;
using DAS.Model.Base.Enums;
using DAS.Model.Base.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.Project
{
    public class ProjectEntity:BaseEntity,IBaseTimeEntity
    {
        public ProjectEntity():base()
        {

        }

        public string Name { get; set; }
        public string ContentText { get; set; }
        public decimal Price { get; set; }
        public MoneyTypesEnum MoneyType { get; set; } = MoneyTypesEnum.TL;
        public DateTime? CreatedAt { get ; set ; }
        public DateTime? ModifiedAt { get ; set ; }
        public DateTime? DeletedAt { get ; set ; }
    }
    
    public interface IProjectValidation : IValidator<ProjectEntity>
    {

    }

    public class ProjectValidation : AbstractValidator<ProjectEntity>, IProjectValidation
    {
        public ProjectValidation()
        {
            RuleFor(x=>x.Name)
                .Length(5,50).WithMessage("Name length must be between 5-50")
                .NotNull().WithMessage("Name don't be null")
                .NotEmpty().WithMessage("Name don't be empty");
           
            RuleFor(x=>x.ContentText)
                .Length(50,10000).WithMessage("ContentText length must be between 50-10000")
                .NotNull().WithMessage("ContentText don't be null")
                .NotEmpty().WithMessage("ContentText don't be empty");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price greater than or equal to 0");

            RuleFor(x=>x.CreatedAt)
                .NotNull().WithMessage("CreatedAt don't be null")
                .NotEmpty().WithMessage("CreatedAt don't be empty");

        }
    }
}
