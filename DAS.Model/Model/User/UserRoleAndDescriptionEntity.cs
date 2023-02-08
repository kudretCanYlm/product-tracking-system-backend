using DAS.Model.Base;
using DAS.Model.Model.Authentication;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAS.Model.Model.User
{
	public class UserRoleAndDescriptionEntity : BaseEntity
	{
		public UserRoleAndDescriptionEntity() : base()
		{

		}

		public Guid OwnerUserId { get; set; }
		public string MiniRole { get; set; }
		public string MiniDescription { get; set; }
		[ForeignKey("OwnerUserId")]
		public LoginEntity OwnerUser { get; set; }
	}

	public interface IUserRoleAndDescriptionValidation : IValidator<UserRoleAndDescriptionEntity>
	{
	}

	public class UserRoleAndDescriptionValidation : AbstractValidator<UserRoleAndDescriptionEntity>, IUserRoleAndDescriptionValidation
	{
		public UserRoleAndDescriptionValidation()
		{
			RuleFor(x => x.MiniRole)
				.NotNull().WithMessage("Mini Role don't be null")
				.NotEmpty().WithMessage("Mini Role don't be empty")
				.Length(1, 50).WithMessage("Mini Role length must be between 1-50");
			
			RuleFor(x => x.MiniDescription)
				.NotNull().WithMessage("Mini Description don't be null")
				.NotEmpty().WithMessage("Mini Description don't be empty")
				.Length(5, 100).WithMessage("Mini Description length must be between 1-100");

		}
	}
}
