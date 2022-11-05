﻿using DAS.Model.Base;
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
    public class ProjectUserEntity : BaseEntity, IBaseTimeEntity
    {
        public ProjectUserEntity() : base()
        {

        }

        public Guid ProjectId { get; set; }
        public Guid ProjectUserId { get; set; }
        public decimal Paid { get; set; }
        public MoneyTypesEnum MoneyType { get; set; } = MoneyTypesEnum.TL;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("ProjectId")]
        public ProjectEntity Project { get; set; }

        [ForeignKey("ProjectUserId")]
        public LoginEntity ProjectUser { get; set; }
    }

    public interface IProjectUserValidation : IValidator<ProjectUserEntity>
    {

    }

    public class ProjectUserValidation : AbstractValidator<ProjectUserEntity>, IProjectUserValidation
    {
        public ProjectUserValidation()
        {
            RuleFor(x => x.Paid)
                .GreaterThanOrEqualTo(0).WithMessage("Paid greater than or equal to 0");

            RuleFor(x => x.CreatedAt)
                .NotNull().WithMessage("CreatedAt don't be null")
                .NotEmpty().WithMessage("CreatedAt don't be empty");

            RuleFor(x => x.Project)
                .SetValidator(new ProjectValidation());

            RuleFor(x => x.ProjectUser)
                .SetValidator(new LoginValidation());
        }
    }

}
