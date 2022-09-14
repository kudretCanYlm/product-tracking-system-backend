using DAS.Core.Infrastructure;
using DAS.Core.Repository.Company;
using DAS.Model.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Company
{
    public interface ICompanyService
    {

    }
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICompanyValidation validator;

        public CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork, ICompanyValidation validator)
        {
            this.companyRepository = companyRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

    }
}
