using DAS.Core.Infrastructure;
using DAS.Core.Repository.Media;
using DAS.Model.Base.Enums;
using DAS.Model.Model.Media;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using DAS.Service.common;

namespace DAS.Service.Services.Media
{
    public interface IImageService
    {
        void SaveImage();
        string AddImage(ImageEntity imageEntity);
        ImageEntity GetSingleGlobalImage(Guid ownerId, ImageTypesEnum imageType);
        IEnumerable<ImageEntity> GetMultipleGlobalImage(Guid ownerId, ImageTypesEnum imageTypes);
        ImageEntity GetSinglePrivateImage(Guid ownerId, ImageTypesEnum imageType);
        IEnumerable<ImageEntity> GetMultiplePrivateImage(Guid ownerId, ImageTypesEnum imageTypes);
    }
    public class ImageService : IImageService
    {
        private IImageRepository imageRepository;
        private IUnitOfWork unitOfWork;
        private readonly ImageValidation validator = new ImageValidation();

        public ImageService(IImageRepository imageRepository, IUnitOfWork unitOfWork)
        {
            this.imageRepository = imageRepository;
            this.unitOfWork = unitOfWork;
        }

        public string AddImage(ImageEntity imageEntity)
        {
            imageEntity.CreatedAt = DateTime.Now;

            if (validator.IsValidEntity(imageEntity))
            {
                //control according to imagetype
                imageRepository.Add(imageEntity);

                try
                {
                    SaveImage();
                }
                catch (Exception ex)
                {

                    return ex.Message;
                }

                return "saved";
            }

            return validator.GetValidErrorMessages(imageEntity);
        }

        public ImageEntity GetSingleGlobalImage(Guid ownerId, ImageTypesEnum imageType)
        {
            return imageRepository.Get(x => x.OwnerId == ownerId && x.ImageType == imageType && x.IsPublic == true);
        }

        public IEnumerable<ImageEntity> GetMultipleGlobalImage(Guid ownerId, ImageTypesEnum imageTypes)
        {
            return imageRepository.GetMany(x => x.OwnerId == ownerId && x.ImageType == imageTypes && x.IsPublic == true);
        }
        public ImageEntity GetSinglePrivateImage(Guid ownerId, ImageTypesEnum imageType)
        {
            return imageRepository.Get(x => x.OwnerId == ownerId && x.ImageType == imageType && x.IsPublic == false);

        }

        public IEnumerable<ImageEntity> GetMultiplePrivateImage(Guid ownerId, ImageTypesEnum imageTypes)
        {
            return imageRepository.GetMany(x => x.OwnerId == ownerId && x.ImageType == imageTypes && x.IsPublic == false);
        }

        public void SaveImage()
        {
            unitOfWork.Commit();
        }


    }
}
