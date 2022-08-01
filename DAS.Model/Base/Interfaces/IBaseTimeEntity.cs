using DAS.Model.Base.Enums;
using System;

namespace DAS.Model.Base.Interfaces
{
    public interface IBaseTimeEntity
    {
        DateTime? CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        DateTime? DeletedAt { get; set; }

    }
}
