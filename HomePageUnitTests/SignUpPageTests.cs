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
        public void HeaderDisplaysCorrectTitle()
        {
            // Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<SignupFormPage>();

            // Act
            var header = cut.Find("title");


            // Assert header displays correct title
            Assert.Contains("Conestoga Connects", header.InnerHtml);
        }
    }
}
