using Domain.DTOs.Compaines;
using Domain.DTOs.Companies;
using Domain.DTOs.Users;
using Domain.Entities.Users;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Infrastructre.Services.Companies;
using Infrastructre.Services.Users;
using Infrastructre.ValidatorExtentions;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using Xunit;

namespace Tests
{
    public class CompanyServiceTests
    {
        #region Property  
        public Mock<IUserRepository> mockUserRepository = new();
        public Mock<ICompanyRepository> mockCompanyRepository = new();
        public Mock<IValidator<AddCompanyRequest>> mockCompanyAddValidator = new();
        public Mock<IValidator<UpdateCompanyRequest>> mockCompanyUpdateValidator = new();
        public Mock<IValidator<CompanyBaseRequest>> mockGetValidator = new();
        public Mock<IValidator<DeleteCompanyRequest>> mockCompanyDeleteValidator = new();

        CompanyService companyService;

        #endregion

        public CompanyServiceTests()
        {
            companyService = new CompanyService(mockCompanyRepository.Object,
                mockCompanyUpdateValidator.Object,
                mockCompanyAddValidator.Object,
                mockGetValidator.Object,
                mockCompanyDeleteValidator.Object
                );

        }

        [Fact]
        public async void Test_GetCompanybyId_That_Not_Exist_Throw_NotFoundExcpetion()
        {
            //

        }

        [Fact]
        public async void Test_RemoveCompany_HaveUsers_ShouldThrowExcpetion()
        {
            //


        }

    }
}
