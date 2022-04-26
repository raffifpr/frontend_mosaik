using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;
using Mosaik.idAPI.Controllers;
using Mosaik.idAPI.Interfaces;
using Mosaik.idAPI.Services;
using Mosaik.idAPI.Dtos;
using Mosaik.idAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mosaik.idTest
{
    public class MosaikControllerTest
    {
        MosaikController _controller;
        IMosaikRepository _repository;
        IMosaikHistoryRepository _historyRepository;
        IMosaikChildRepository _childRepository;
        IMosaikParentRepository _parentRepository;
        IMosaikRestrictRepository _restrictRepository;

        public MosaikControllerTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql("Server=ec2-52-54-212-232.compute-1.amazonaws.com;Database=d8gskth7bblvmc;Port=5432;User Id=ibljtqqkcstzku;Password=4920f311fda426e428998fef2d3eab546875cf75bf7b1e30d1530082d7b88d95;Ssl Mode=Require;Trust Server Certificate=true;");
            DataContext _dataContext = new DataContext(optionsBuilder.Options);

            _repository = new MosaikRepository(_dataContext);
            _historyRepository = new MosaikHistoryRepository(_dataContext);
            _childRepository = new MosaikChildRepository(_dataContext);
            _parentRepository = new MosaikParentRepository(_dataContext);
            _restrictRepository = new MosaikRestrictRepository(_dataContext);

            _controller = new MosaikController(_repository, _historyRepository, _childRepository, _parentRepository, _restrictRepository);
        }

        [Fact]
        public async void TestWrongLogin()
        {
            var LoginTestInput = new LoginAccountDto()
            {
                Email = "testing@gmail.com",
                Password = "admin1234",
            };

            JsonResult result = await _controller.Login(LoginTestInput) as JsonResult;
            WrongLogin? response = (WrongLogin)result.Value;

            Assert.Equal("wrong", response.status);
        }

        [Fact]
        public async void LoginTestParent()
        {

            var LoginTestInput = new LoginAccountDto()
            {
                Email = "parent@gmail.com",
                Password = "newadmin1234",
            };

            JsonResult result = await _controller.Login(LoginTestInput) as JsonResult;
            ParentAuthenticated? response = (ParentAuthenticated)result.Value;

            Assert.Equal("parent500", response.Username);
            Assert.Equal("parent@gmail.com", response.Email);
            Assert.Equal("supervisor", response.AccountStatus);
            Assert.Empty(response.SupervisorAccounts);
        }

        [Fact]
        public async void FailInsertParent()
        {
            var Fail = new CreateAccountDtoParent()
            {
                Username = "parent",
                Email = "parent@gmail.com",
                Password = "admin1234",
                SupervisorEmails = { }
            };

            JsonResult result = await _controller.CreateParentAccount(Fail) as JsonResult;
            Response? response = (Response)result.Value;

            Assert.Equal("failed", response.Status);

            string[] supervisorEmails = { "child123@gmail.com" };
            Fail = new CreateAccountDtoParent()
            {
                Username = "parent",
                Email = "parent123@gmail.com",
                Password = "admin1234",
                SupervisorEmails = supervisorEmails
            };

            result = await _controller.CreateParentAccount(Fail) as JsonResult;
            response = (Response)result.Value;

            Assert.Equal("failed", response.Status);
        }

        [Fact]
        public async void FailInsertChild()
        {
            var Fail = new CreateAccountDto()
            {
                Username = "child",
                Email = "child@gmail.com",
                Password = "admin1234"
            };

            JsonResult result = await _controller.CreateChildAccount(Fail) as JsonResult;
            Response? response = (Response)result.Value;

            Assert.Equal("failed", response.Status);
        }

        [Fact]
        public async void FailChangeUser()
        {
            var Fail = new ChangeUsernameDto() { Email = "testing@gmail.com", Username = "child" };
            JsonResult result = await _controller.ChangeUsername(Fail) as JsonResult;
            Response? response = (Response)result.Value;

            Assert.Equal("failed", response.Status);
        }

        [Fact]
        public async void ChangeUser()
        {
            var Success = new ChangeUsernameDto() { Email = "child@gmail.com", Username = "child1234" };
            JsonResult result = await _controller.ChangeUsername(Success) as JsonResult;
            Response? response = (Response)result.Value;

            Assert.Equal("success", response.Status);

            Success = new ChangeUsernameDto { Email = "child@gmail.com", Username = "child" };
            result = await _controller.ChangeUsername(Success) as JsonResult;
            response = (Response)result.Value;

            Assert.Equal("success", response.Status);
        }

        [Fact]
        public async void ChangePassword()
        {
            var Success = new ChangePasswordDto() { Email = "child@gmail.com", OldPassword = "admin1234", NewPassword = "admin" };
            JsonResult result = await _controller.ChangePassword(Success) as JsonResult;
            Response? response = (Response)result.Value;

            Assert.Equal("success", response.Status);
            
            Success = new ChangePasswordDto() { Email = "child@gmail.com", OldPassword = "admin", NewPassword = "admin1234" };
            result = await _controller.ChangePassword(Success) as JsonResult;
            response = (Response)result.Value;

            Assert.Equal("success", response.Status);
        }

        [Fact]
        public async void FailChangePassword()
        {
            var Success = new ChangePasswordDto() { Email = "child@gmail.com", OldPassword = "admin", NewPassword = "admin" };
            JsonResult result = await _controller.ChangePassword(Success) as JsonResult;
            Response? response = (Response)result.Value;

            Assert.Equal("wrong password", response.Status);
        }

        [Fact]
        public async void SuccessSupervise()
        {
            var Success = new CreateSuperviseDto() { ChildEmail = "child@gmail.com", Email = "parent@gmail.com" };
            JsonResult result = await _controller.NewChildAccount(Success) as JsonResult;
            Response? response = (Response)result.Value;

            Assert.Equal("don't exist", response.Status);
        }

        [Fact]
        public async void FailSupervise()
        {
            var Success = new CreateSuperviseDto() { ChildEmail = "child@gmail.com", Email = "parent1234@gmail.com" };
            JsonResult result = await _controller.NewChildAccount(Success) as JsonResult;
            Response? response = (Response)result.Value;

            Assert.Equal("failed", response.Status);

            Success = new CreateSuperviseDto() { ChildEmail = "child1234@gmail.com", Email = "parent@gmail.com" };
            result = await _controller.DeleteChild(Success) as JsonResult;
            response = (Response)result.Value;

            Assert.Equal("don't exist", response.Status);

            var LoginTestInput = new LoginAccountDto()
            {
                Email = "parent@gmail.com",
                Password = "newadmin1234",
            };

            result = await _controller.Login(LoginTestInput) as JsonResult;
            ParentAuthenticated? Loginresponse = (ParentAuthenticated)result.Value;

            Assert.Equal("parent500", Loginresponse.Username);
            Assert.Equal("parent@gmail.com", Loginresponse.Email);
            Assert.Equal("supervisor", Loginresponse.AccountStatus);
            Assert.Empty(Loginresponse.SupervisorAccounts);
        }

        [Fact]
        public async void InsertHistory()
        {
            var history = new CreateHistoryDto() { Email = "child@gmail.com", Link = "youtube.com", Time = "04:03:14 PM", Date = "04/26/2022" };
            JsonResult result = await _controller.CreateHistory(history) as JsonResult;
            Response? response = (Response)result.Value;

            Assert.Equal("success", response.Status);

            var dateHistory = new DateHistoryRequest() { Email = "child@gmail.com" };
            result = await _controller.DateOfHistoryData(dateHistory) as JsonResult;
            DateHistoryResponse response2 = (DateHistoryResponse)result.Value;

            Assert.Equal(1, response2.Total);
            Assert.Equal("04/26/2022", response2.DateList[0]);

            var historyPerDate = new HistoryDataPerDateRequest() { Email = "child@gmail.com", Date = "04/26/2022" };
            result = await _controller.HistoryDataPerDate(historyPerDate) as JsonResult;
            HistoryDataPerDateResponse response3 = (HistoryDataPerDateResponse)result.Value;

            Assert.Equal("youtube.com", response3.Data.Links[0]);
            Assert.Equal("04:03:14 PM", response3.Data.Times[0]);
            Assert.NotEqual(0, response3.Total);
        }
    
        [Fact]
        public async void Restrict()
        {
            CreateRestrictDto createRestrictDto = new CreateRestrictDto() { Email = "child@gmail.com", Link = "forbidden.com" };

            JsonResult result = await _controller.CreateRestrict(createRestrictDto) as JsonResult;
            Response? response = (Response)result.Value;

            Assert.Equal("success", response?.Status);

            DateHistoryRequest dateHistoryRequest = new DateHistoryRequest() { Email = "child@gmail.com" };

            result = await _controller.RestrictData(dateHistoryRequest) as JsonResult;
            RestrictDataResponse? restrictDataResponse = (RestrictDataResponse?)result.Value;

            Assert.Equal(1, restrictDataResponse?.Total);
            Assert.Equal("forbidden.com", restrictDataResponse.LinkAndNotif.Links[0]);
            Assert.True(restrictDataResponse.LinkAndNotif.Notifs[0]);

            result = await _controller.DeleteLink(createRestrictDto) as JsonResult;
            response = (Response)result.Value;

            Assert.Equal("success", response.Status);
        }

        [Fact]
        public async void Authorization()
        {
            var Success = new CreateSuperviseDto() { ChildEmail = "child@gmail.com", Email = "parent@gmail.com" };
            JsonResult result = await _controller.NewChildAccount(Success) as JsonResult;
            Response? response = (Response)result.Value;

            Assert.Equal("don't exist", response.Status);
        }
    }
}