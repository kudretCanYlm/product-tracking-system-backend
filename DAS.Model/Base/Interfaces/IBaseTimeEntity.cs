using System;

namespace DAS.Model.Base.Interfaces
{
    internal interface IBaseTimeEntity
    {
        DateTime? CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
