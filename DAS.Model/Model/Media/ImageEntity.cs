using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DAS.Model.Model.Media
{
    public class ImageEntity : BaseEntity, IBaseTimeEntity
    {
        public ImageEntity():base()
        {

        }
        
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get ; set ; }
        public DateTime? DeletedAt { get ;set ;}
    }

    public class ImageValidation : AbstractValidator<ImageEntity>
    {
        public ImageValidation()
        {
            RuleFor(x => x.CreatedAt).NotNull().NotEmpty();
            RuleFor(x => x.ImageData).NotNull().NotEmpty();
            RuleFor(x => x.ImageMimeType).Length(2, 50);
        }
    }

}
