using System;
using Xunit;
using TesteJuntoSeguros.Controllers;

namespace Login.Tests
{
    public class LoginControllerTest
    {
        LoginController _loginController;

        public LoginControllerTest()
        {
            _loginController = new LoginController();
        }

        [Fact]
        public void Post_CreateToken_ReturnsNotFoundResponse_Password()
        {
            LoginModel login = new LoginModel();
            login.username = "charles";
            login.username = "12345";

            var badResponse = _loginController.CreateToken(login);
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Post_CreateToken_ReturnsNotFoundResponse_UserName()
        {
            LoginModel login = new LoginModel();
            login.username = "charless";
            login.username = "123";

            var badResponse = _loginController.CreateToken(login);
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Post_CreateToken_ReturnsNotFoundResponse_PasswordUserName()
        {
            LoginModel login = new LoginModel();
            login.username = "charless";
            login.username = "12323";

            var badResponse = _loginController.CreateToken(login);
            Assert.IsType<NotFoundResult>(badResponse);
        }
        
        [Fact]
        public void Post_CreateToken_ReturnsOkResult()
        {
            LoginModel login = new LoginModel();
            login.username = "charles";
            login.username = "123";

            var okResponse = _loginController.CreateToken(login);
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void Post_GenerateTokenChangePassword_ReturnsNotFoundResponse()
        {
            ChangePasswordModel model = ChangePasswordModel();
            model.username = "lubini";

            var badResponse = _loginController.CreateTokenPassword(model);
            Assert.IsType<NotFoundResult>(badResponse);
        }
        
        [Fact]
        public void Post_GenerateTokenChangePassword_ReturnsOkResult()
        {
            ChangePasswordModel model = ChangePasswordModel();
            model.username = "charles";

            var okResponse = _loginController.CreateTokenPassword(model);
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void Post_ChangePassword_ReturnsNotFoundResponse_Password()
        {
            ChangePasswordModel model = ChangePasswordModel();
            model.username = "charles";

            var token = _loginController.CreateTokenPassword(model);

            model.tokenChangePassword = token;
            model.password = "123";
            model.passwordConfirm = "1233";

            var badResponse = _userController.ChangePassword(model);
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Post_ChangePassword_ReturnsNotFoundResponse_Token()
        {
            ChangePasswordModel model = ChangePasswordModel();
            model.username = "charles";
            model.tokenChangePassword = "asffasur8";
            model.password = "123";
            model.passwordConfirm = "123";

            var badResponse = _userController.ChangePassword(model);
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Post_ChangePassword_ReturnsNotFoundResponse_Token()
        {
            ChangePasswordModel model = ChangePasswordModel();
            model.username = "charles";

            var token = _loginController.CreateTokenPassword(model);

            model.username = "lubini";
            model.tokenChangePassword = token;
            model.password = "123";
            model.passwordConfirm = "1233";

            var badResponse = _userController.ChangePassword(model);
            Assert.IsType<NotFoundResult>(badResponse);
        }
        
        [Fact]
        public void Post_ChangePassword_ReturnsOkResult()
        {
            ChangePasswordModel model = ChangePasswordModel();
            model.username = "charles";

            var token = _loginController.CreateTokenPassword(model);

            model.tokenChangePassword = token;
            model.password = "123";
            model.passwordConfirm = "123";

            var okResponse = _userController.ChangePassword(model);
            Assert.IsType<OkResult>(okResponse);
        }
    }
}
