using System;
using Xunit;
using TesteJuntoSeguros.Controllers;

namespace User.Tests
{
    public class UserControllerTest
    {        

        UserController _userController;

        public UserControllerTest()
        {
            _userController = new UserController();
        }

        [Fact]
        public void Get_ReturnsOkResult()
        {
            var okResult = _userController.GetUser();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }
    
        [Fact]
        public void Ge_ReturnsAllItems()
        {
            var okResult = _userController.GetUser().Result as OkObjectResult;
            var items = Assert.IsType<List<UserModel>>(okResult.Value);
            Assert.Equal(1, items.Count);
        }

        [Fact]
        public void GetById_ReturnsNotFoundResult()
        {
            var notFoundResult = _userController.GetUser(2);
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }
        
        [Fact]
        public void GetById_ReturnsOkResult()
        {
            var okResult = _userController.Get(1);
            Assert.IsType<OkObjectResult>(okResult.Result);
        }
        
        [Fact]
        public void Add_ReturnsBadRequest()
        {
            var idMissing = new UserModel()
            {
                username = "charles",
                password = "1234"
            };
            _userController.ModelState.AddModelError("Id", "Required");
        
            var badResponse = _userController.PostUser(idMissing);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_ReturnsCreatedResponse()
        {
            UserModel testItem = new UserModel()
            {
                id = 2,
                username = "charles",
                password = "123"
            };
        
            var createdResponse = _userController.PostUser(testItem);
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }
        
        [Fact]
        public void Put_ReturnsNotFoundResponse()
        {
            var user = _userController.GetUser(1);

            var badResponse = _userController.PutUser(user, 6);
            Assert.IsType<NotFoundResult>(badResponse);
        }
        
        [Fact]
        public void Put_ReturnsOkResult()
        {
            var user = _userController.GetUser(1);

            var okResponse = _userController.PutUser(user);
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void Remove_ReturnsNotFoundResponse()
        {
            var badResponse = _userController.RemoveUser(5);
            Assert.IsType<NotFoundResult>(badResponse);
        }
        
        [Fact]
        public void Remove_ReturnsOkResult()
        {
            var okResponse = _userController.RemoveUser(1);
            Assert.IsType<OkResult>(okResponse);
        }
    }
}
