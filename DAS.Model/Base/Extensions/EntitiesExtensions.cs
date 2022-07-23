using DAS.Model.Base.Enums;
using DAS.Model.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Base.Extensions
{
    public static class EntitiesExtensions
    {
        public static void SetTimeNow(this IBaseTimeEntity entity,DateTypesEnum dateTypes = DateTypesEnum.CreatedAt)
        {
            switch (dateTypes)
            {
                case DateTypesEnum.CreatedAt:
                    entity.CreatedAt = DateTime.Now;
                    break;
                case DateTypesEnum.ModifiedAt:
                    entity.ModifiedAt = DateTime.Now;
                    break;
                case DateTypesEnum.DeletedAt:
                    entity.DeletedAt = DateTime.Now;
                    break;
            }
        }

        public static TimeSpan DiffrenceOfCreatedAtAndModifiedAt(this IBaseTimeEntity entity)
        {
            var diffrence = (entity.ModifiedAt-entity.CreatedAt);

            return (TimeSpan)diffrence;
        }

        public static TimeSpan DiffrenceOfCreatedAtAndDeletedAt(this IBaseTimeEntity entity)
        {
            var diffrence = (entity.DeletedAt - entity.CreatedAt);

            return (TimeSpan)diffrence;
        }

        public static TimeSpan DiffrenceOfModifiedAtAndDeletedAt(this IBaseTimeEntity entity)
        {
            var diffrence = (entity.DeletedAt - entity.ModifiedAt);

            return (TimeSpan)diffrence;
        }
    }
}
