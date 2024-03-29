using Bunit;
using BlazorApp.Client.Layout.PageComponents;
using BlazorApp.Client.Layout.Pages;
using BlazorApp.Shared;


namespace UnitTests
{
    public class LoginPageTests : TestContext
    {
        [Fact]

        // This test verifies that the HomePage component renders correctly
        public void LoginPageRendersCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act
            var cut = ctx.RenderComponent<LoginFormPage>();

            // Assert component renders correctly
            Assert.NotNull(cut);

        }
        // verifies that the correct title is displayed
        [Fact]
        public void HeaderDisplaysCorrectTitle()
        {
            // Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<HomePage>();

            // Act
            var header = cut.Find("title");


            // Assert header displays correct title
            Assert.Contains("Conestoga Connects", header.InnerHtml);
        }

    } 
}
