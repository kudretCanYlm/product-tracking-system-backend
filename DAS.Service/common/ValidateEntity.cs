using DAS.Model.Base;
using FluentValidation;
using FluentValidation.Results;
using System.Text;

namespace DAS.Service.common
{
    public static class ValidateEntity
    {
        public static bool IsValidEntity<T>(this IValidator<T> validator, T entity) where T : BaseEntity
        {
            return ValidateResultEntity(validator, entity).IsValid;
        }

        public static string GetValidErrorMessages<T>(this IValidator<T> validator, T entity) where T : BaseEntity
        {
            StringBuilder errors = new StringBuilder();

            foreach (var error in ValidateResultEntity(validator, entity).Errors)
                errors.AppendLine(error.ErrorMessage);

            return errors.ToString();
        }

        private static ValidationResult ValidateResultEntity<T>(IValidator<T> validator, T entity) where T : BaseEntity
        {
            return validator.Validate(entity);
        }
    }
}
