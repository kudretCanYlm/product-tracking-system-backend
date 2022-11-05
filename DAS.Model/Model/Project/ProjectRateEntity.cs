using DAS.Model.Base;
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
    public class ProjectRateEntity : BaseEntity, IBaseTimeEntity
    {
        public ProjectRateEntity():base()
        {

        }

        public Guid ProjectId { get; set; }
        public Guid RatedUserId { get; set; }
        public int Rate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("ProjectId")]
        public ProjectEntity Project { get; set; }

        [ForeignKey("RatedUserId")]
        public LoginEntity RatedUser { get; set; }
    }

    public interface IProjectRateValidation : IValidator<ProjectRateEntity>
    {

    }

    public class ProjectRateValidation : AbstractValidator<ProjectRateEntity>, IProjectRateValidation
    {
        public ProjectRateValidation()
        {
            RuleFor(x => x.Rate)
                .InclusiveBetween(0, 10).WithMessage("Rate must be between 0-10");

            RuleFor(x => x.CreatedAt)
                .NotNull().WithMessage("CreatedAt don't be null")
                .NotEmpty().WithMessage("CreatedAt don't be empty");

            RuleFor(x => x.Project)
                .SetValidator(new ProjectValidation());

            RuleFor(x => x.RatedUser)
                .SetValidator(new LoginValidation());
        }
    }
}
