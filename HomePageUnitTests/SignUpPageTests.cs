using Bunit;
using BlazorApp.Client.Layout.PageComponents;
using BlazorApp.Client.Layout.Pages;
using BlazorApp.Shared;


namespace UnitTests
{
    public class SignUpPageTests : TestContext
    {
        [Fact]

        // This test verifies that the HomePage component renders correctly
        public void SignUpPageRendersCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act
            var cut = ctx.RenderComponent<SignupFormPage>();

            // Assert component renders correctly
            Assert.NotNull(cut);

        }
       

    }
}
