using AutoMapper;
using Moq;
using People.Domain.DTOs.Requests;
using People.Domain.Entities;
using People.Domain.Interfaces;
using People.Domain.Interfaces.Repositories;
using People.Domain.Interfaces.Services;
using People.Domain.Mappings.DTOEntities;
using People.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace People.Tests.API
{
    public class TechnologyServiceTest
    {
        private readonly Mock<IApiNotification> apiNotification;
        private readonly Mock<ILogService> logService;
        private readonly Mock<ITechnologyRepository> technologyRepository;
        private readonly ITechnologyService technologyService;
        private readonly List<TechnologyEntity> listTechnologyEntity;

        public TechnologyServiceTest()
        {
            apiNotification = new Mock<IApiNotification>();
            logService = new Mock<ILogService>();
            technologyRepository = new Mock<ITechnologyRepository>();
            technologyService = new TechnologyService(technologyRepository.Object, logService.Object, apiNotification.Object);
            
            listTechnologyEntity = new List<TechnologyEntity>();
            listTechnologyEntity.Add(new TechnologyEntity() { Id = Guid.NewGuid().ToString(), Name = "C#", Link = "http://dotnet.microsoft.com", Logo = "teste", CreatedUserId = Guid.NewGuid().ToString(), UpdatedUserId = Guid.NewGuid().ToString(), DateCreated = DateTime.Now, DateUpdated = DateTime.Now });
            listTechnologyEntity.Add(new TechnologyEntity() { Id = Guid.NewGuid().ToString(), Name = "Visual Basic", Link = "http://dotnet.microsoft.com", Logo = "teste", CreatedUserId = Guid.NewGuid().ToString(), UpdatedUserId = Guid.NewGuid().ToString(), DateCreated = DateTime.Now, DateUpdated = DateTime.Now });
        }


        #region Tests Methods

        #region FindAll

        [Fact]
        public async void FindAll_Success()
        {
            // ARRANGE
            IMapper mapper = DTOEntityMap.MapDTOEntity(); ;
            var expected = mapper.Map<IList<TechnologyRequestDTO>>(listTechnologyEntity);

            technologyRepository.Setup(r =>
                r.FindAll(It.IsAny<Expression<Func<TechnologyEntity, bool>>?>(), It.IsAny<Func<IQueryable<TechnologyEntity>, IOrderedQueryable<TechnologyEntity>>?>())
            ).ReturnsAsync(listTechnologyEntity);

            // ACTION
            var allRows = await technologyService.FindAll();

            // ASSERT
            Assert.NotStrictEqual(expected, allRows);
        }

        [Fact]
        public void FindAll_Fail()
        {

        }

        [Fact]
        public void FindAll_Error()
        {

        }

        #region Filter

        [Fact]
        public void FindAll_Filter_Success()
        {
            var allRows = technologyService.FindAll();
        }

        [Fact]
        public void FindAll_Filter_Fail()
        {

        }

        [Fact]
        public void FindAll_Filter_Error()
        {

        }

        #endregion

        #region OrderBy

        [Fact]
        public async void FindAll_OrderBy_Success()
        {
           
        }

        [Fact]
        public void FindAll_OrderBy_Fail()
        {

        }

        [Fact]
        public void FindAll_OrderBy_Error()
        {

        }

        #endregion

        #region Filter and OrderBy

        [Fact]
        public void FindAll_Filter_OrderBy_Success()
        {
            var allRows = technologyService.FindAll();
        }

        [Fact]
        public void FindAll_Filter_OrderBy_Fail()
        {

        }

        [Fact]
        public void FindAll_Filter_OrderBy_Error()
        {

        }

        #endregion

        #endregion


        #region FindById

        [Fact]
        public void FindById_Success()
        {

        }

        [Fact]
        public void FindById_Fail()
        {

        }

        [Fact]
        public void FindById_Error()
        {

        }

        #endregion


        #region Create

        [Fact]
        public void Create_Success()
        {

        }

        [Fact]
        public void Create_Fail()
        {

        }

        [Fact]
        public void Create_Error()
        {

        }

        #endregion


        #region Update

        [Fact]
        public void Update_Success()
        {

        }

        [Fact]
        public void Update_Fail()
        {

        }

        [Fact]
        public void Update_Error()
        {

        }

        #endregion


        #region Delete

        [Fact]
        public void Delete_Success()
        {

        }

        [Fact]
        public void Delete_Fail()
        {

        }

        [Fact]
        public void Delete_Error()
        {

        }

        #endregion

        #endregion

    }
}
