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
    public interface ICompanyWorkersService
    {

    }
    public class CompanyWorkersService:ICompanyWorkersService
    {
        private readonly ICompanyWorkersRepository companyWorkersRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICompanyWorkerValidation validator;

        public CompanyWorkersService(ICompanyWorkersRepository companyWorkersRepository, IUnitOfWork unitOfWork, ICompanyWorkerValidation validator)
        {
            this.companyWorkersRepository = companyWorkersRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

    }
}
