using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using REST.API.Controllers;
using REST.API.Controllers.Model;
using Xunit;

namespace REST.API.Tests
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _controller = new UsersController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            UsersController.usersList = User.GetSampleUsers();
        }

        [Fact]
        public void Get_Call_ReturnOk()
        {
            //Arrange
            var userFilter = new UserFilter {PageNumber = 0, PageSize = 10};

            //Act
            var okResult = _controller.GetAllUsersPageSize(userFilter);

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Get_Call_ReturnList()
        {
            //Arrange
            var userFilter = new UserFilter {PageNumber = 0, PageSize = 5};

            //Act
            var okResult = _controller.GetAllUsersPageSize(userFilter);

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<List<User>>((okResult as OkObjectResult).Value);
            var items = (List<User>) (okResult as OkObjectResult).Value;
            Assert.Equal(5, items.Count);
        }

        [Fact]
        public void Get_Call_ReturnCountInHeader()
        {
            //Arrange
            var userFilter = new UserFilter {PageNumber = 0, PageSize = 5};

            //Act
            var okResult = _controller.GetAllUsersPageSize(userFilter);

            //Assert
            var countHeader = _controller.Response.Headers.FirstOrDefault(s => s.Key == "Count").Value;
            Assert.True(int.TryParse(countHeader, out var countHeaderInt));
            Assert.Equal(UsersController.usersList.Count(), countHeaderInt);
        }
    }
}