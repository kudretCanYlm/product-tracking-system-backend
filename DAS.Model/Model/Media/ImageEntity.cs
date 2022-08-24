using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using DAS.Model.Base.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using DAS.Model.Model.Authentication;

namespace DAS.Model.Model.Media
{
    public class ImageEntity : BaseEntity, IBaseTimeEntity
    {
        public ImageEntity():base()
        {
            this.ImageType = ImageTypesEnum.UserIcon;
            this.IsPublic = false;
        }
        
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get ; set ; }
        public DateTime? DeletedAt { get ;set ;}
        public ImageTypesEnum ImageType { get; set; }
        public bool IsPublic { get; set; }
        
    }

    public class ImageValidation : AbstractValidator<ImageEntity>
    {
        public ImageValidation()
        {
            RuleFor(x => x.CreatedAt).NotNull().NotEmpty();
            RuleFor(x => x.ImageData).NotNull().NotEmpty();
            RuleFor(x => x.ImageMimeType).Length(2, 50);
            RuleFor(x => x.IsPublic).NotNull().NotEmpty();
            RuleFor(x => x.OwnerId).NotEmpty().NotNull();
        }
    }

}
