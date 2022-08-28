using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using DAS.Model.Model.Authentication;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAS.Model.Model.Company
{
    public class CompanyWorkersEntity : BaseEntity, IBaseTimeEntity
    {
        public CompanyWorkersEntity():base()
        {

        }

        public Guid CompanyId { get; set; }
        public Guid WorkerId { get; set; }
        public Guid WorkerRoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("CompanyId")]
        public CompanyEntity Company { get; set; }
        [ForeignKey("WorkerId")]
        public LoginEntity Worker { get; set; }
        [ForeignKey("WorkerRoleId")]
        public WorkerRoleEntity WorkerRole { get; set; }
    }

    public class CompanyWorkerValidation : AbstractValidator<CompanyWorkersEntity>
    {
        public CompanyWorkerValidation()
        {
            RuleFor(x => x.CreatedAt).NotNull().NotEmpty();
            RuleFor(x => x.Company).SetValidator(new CompanyValidation());
            RuleFor(x => x.Worker).SetValidator(new LoginValidation());
            RuleFor(x => x.WorkerRole).SetValidator(new WorkerRoleValidation());
        }
    }
}
