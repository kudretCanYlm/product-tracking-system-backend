using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.Company
{
    public class WorkerRoleEntity:BaseEntity,IBaseTimeEntity
    {
        public WorkerRoleEntity():base()
        {

        }

        public WorkerRoleEntity(string role):base()
        {
            this.Role = role;
        }

        public string Role { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class WorkerRoleValidation : AbstractValidator<WorkerRoleEntity>
    {
        public WorkerRoleValidation()
        {
            RuleFor(x => x.Role).Length(5, 50).NotNull().NotEmpty();
            RuleFor(x => x.CreatedAt).NotNull().NotEmpty();
        }
    }
}
