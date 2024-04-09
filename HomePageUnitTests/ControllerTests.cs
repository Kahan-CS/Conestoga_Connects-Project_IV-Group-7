using BlazorApp.Shared;
using System.Net;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorApp.Shared;
using Xunit;
using BlazorApp.Server.Services;

namespace BlazorApp.Tests.IntegrationTests

{

    public class AuthControllerIntegrationTests

    {

        private readonly HttpClient _client;

        public AuthControllerIntegrationTests()

        {

            // Assuming your server is running at http://localhost:5018

            _client = new HttpClient { BaseAddress = new Uri("http://localhost:5018") };

        }

        [Fact]

        public async Task Login_ValidCredentials_ReturnsOk()

        {

            // Arrange

            var loginModel = new LoginFormModel { Username = "dummy1", Password = "dummypassword" };

            var content = new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json");

            // Act

            var response = await _client.PostAsync("/api/Auth/login", content);

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Contains("Login successful", responseContent);

        }

        [Fact]

        public async Task Login_InvalidCredentials_ReturnsUnauthorized()

        {

            // Arrange

            var loginModel = new LoginFormModel { Username = "invalid_username", Password = "invalid_password" };

            var content = new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json");

            // Act

            var response = await _client.PostAsync("/api/Auth/login", content);

            // Assert

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Contains("Invalid username or password", responseContent);

        }

        [Fact]
        public async Task Login_ValidUsernameAndInvalidPassword()

        {

            // Arrange

            var loginModel = new LoginFormModel { Username = "dummy1", Password = "invalid_password" };

            var content = new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json");

            // Act

            var response = await _client.PostAsync("/api/Auth/login", content);

            // Assert

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Contains("Invalid username or password", responseContent);

        }


        [Fact]
        public async Task Login_InvalidUsernameAndValidPassword()
        {
            // Arrange

            var loginModel = new LoginFormModel { Username = "invalid", Password = "dummypassword" };

            var content = new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json");

            // Act

            var response = await _client.PostAsync("/api/Auth/login", content);

            // Assert

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Contains("Invalid username or password", responseContent);

        }

        // SignUp Tests
         [Fact]
        public async Task SignUp_ValidCredentials_ReturnsOk()
        {
            // Arrange
            var signupModel = new SignupFormModel { Email = "dummyuser123@gmail.com", Username = "dummyid2", Password = "dummypassword" };
            var content = new StringContent(JsonSerializer.Serialize(signupModel), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Signup/signup", content); // Ensure this matches your actual API endpoint

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

        }

    }

}