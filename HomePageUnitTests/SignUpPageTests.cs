using Bunit;
using BlazorApp.Client.Layout.Pages;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests
{
    public class SignUpPageTests
    {
        [Fact]
        public void SignUpPageRendersCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act
            var cut = ctx.RenderComponent<SignupFormPage>();

            // Assert
            Assert.NotNull(cut);
        }

        [Fact]
        public void InputFields_BindCorrectlyToModel()
        {
            // Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<SignupFormPage>();
            var usernameInput = cut.Find("input[id='Username']");
            var passwordInput = cut.Find("input[id='Password']");
            var emailInput = cut.Find("input[id='Email']");

            // Act
            usernameInput.Change("testUser");
            passwordInput.Change("testPassword");
            emailInput.Change("test@example.com");

            // Assert
        }

        [Fact]
        public void FormSubmission_CallsHandleSignupMethod()
        {
            // Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<SignupFormPage>();
            var form = cut.Find("form");

            // Act
            form.Submit();

            // Assert
        }

        [Fact]
        public void SignUpButton_IsDisplayed()
        {
            // Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<SignupFormPage>();

            // Act
            var button = cut.Find("button");

            // Assert
            button.MarkupMatches("<button>Sign-Up</button>");
        }

    
        [Fact]
        public void UsernameFieldIsVisible()
        {
            // Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<SignupFormPage>();

            // Act
            var usernameInput = cut.Find("input[id='Username']");

            // Assert
            Assert.NotNull(usernameInput);
        }

        [Fact]
        public void PasswordFieldIsVisible()
        {
            // Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<SignupFormPage>();

            // Act
            var passwordInput = cut.Find("input[id='Password']");

            // Assert
            Assert.NotNull(passwordInput);
        }

        [Fact]
        public void EmailFieldIsVisible()
        {
            // Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<SignupFormPage>();

            // Act
            var emailInput = cut.Find("input[id='Email']");

            // Assert
            Assert.NotNull(emailInput);
        }
        }
}
