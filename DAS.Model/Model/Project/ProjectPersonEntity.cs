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
    public class ProjectPersonEntity : BaseEntity, IBaseTimeEntity
    {
        public ProjectPersonEntity():base()
        {

        }

        public Guid ProjectId { get; set; }
        public Guid ProjectPersonId { get; set; }
        public DateTime? CreatedAt { get ; set ; }
        public DateTime? ModifiedAt { get ; set ; }
        public DateTime? DeletedAt { get ; set ; }

        [ForeignKey("ProjectId")]
        public ProjectEntity Project { get; set; }
        [ForeignKey("ProjectPersonId")]
        public LoginEntity ProjectPerson { get; set; }
    }

    public interface IProjectPersonValidation : IValidator<ProjectPersonEntity>
    {

    }

    public class ProjectPersonValidation:AbstractValidator<ProjectPersonEntity>, IProjectPersonValidation
    {
        public ProjectPersonValidation()
        {
            RuleFor(x => x.CreatedAt)
                .NotNull().WithMessage("CreatedAt don't be null")
                .NotEmpty().WithMessage("CreatedAt don't be empty");

            RuleFor(x => x.Project)
                .SetValidator(new ProjectValidation());

            RuleFor(x => x.ProjectPerson)
                .SetValidator(new LoginValidation());
        }
    }
}
