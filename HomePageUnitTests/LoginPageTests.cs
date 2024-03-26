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
        public void LOginPageRendersCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act
            var cut = ctx.RenderComponent<LoginFormPage>();

            // Assert
            // Assert component renders correctly
            Assert.NotNull(cut);

        }

    }
}
