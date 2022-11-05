using DAS.Model.Base;
using DAS.Model.Base.Enums;
using DAS.Model.Base.Interfaces;
using DAS.Model.Model.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.Project
{
    public class ProjectSupporterEntity:BaseEntity,IBaseTimeEntity
    {
        public ProjectSupporterEntity():base()
        {

        }

        public Guid ProjectId { get; set; }
        public Guid ProjectSupporterId { get; set; }
        public decimal DonateAmount { get; set; }
        public MoneyTypesEnum MoneyType { get; set; } = MoneyTypesEnum.TL;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("ProjectId")]
        public ProjectEntity Project { get; set; }

        [ForeignKey("ProjectSupporterId")]
        public LoginEntity ProjectSupporter { get; set; }
    }

    public interface IProjectSupporterValidation : IValidator<ProjectSupporterEntity>
    {

    }

    public class ProjectSupporterValidation : AbstractValidator<ProjectSupporterEntity>, IProjectSupporterValidation
    {
        public ProjectSupporterValidation()
        {
            RuleFor(x=>x.DonateAmount)
                .GreaterThanOrEqualTo(0).WithMessage("DonateAmount greater than or equal to 0");

            RuleFor(x => x.CreatedAt)
                .NotNull().WithMessage("CreatedAt don't be null")
                .NotEmpty().WithMessage("CreatedAt don't be empty");

            RuleFor(x => x.Project)
                .SetValidator(new ProjectValidation());

            RuleFor(x => x.ProjectSupporter)
                .SetValidator(new LoginValidation());
        }
    }
}
