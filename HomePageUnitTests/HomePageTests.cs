using Bunit;
using BlazorApp.Client.Layout.PageComponents;
using BlazorApp.Client.Layout.Pages;
using BlazorApp.Shared;
using BlazorApp.

// cut is short for Component Under Test 

namespace UnitTests
{
    public class HomePageTests : TestContext
    {
        [Fact]

        // This test verifies that the HomePage component renders correctly
        public void HomePageRendersCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act
            var cut = ctx.RenderComponent<HomePage>();

            // Assert
            // Assert component renders correctly
            Assert.NotNull(cut);

        }

        // This test ensures that the contacts are rendered correctly within the ContactSection.
        [Fact]
        public void ContactsRenderCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act
            var cut = ctx.RenderComponent<HomePage>();
            var contactSection = cut.FindComponent<ContactSection>();

            // Assert
            // Assert ContactSection renders with correct number of contacts
            Assert.NotNull(contactSection);
            Assert.Equal(20, contactSection.Instance.Contacts.Count); // Using the 20 contacts that are in the homepage
        }


        // This test ensures that the messages are rendered correctly within the MessageSection.
        [Fact]
        public void MessagesRenderCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act
            var cut = ctx.RenderComponent<HomePage>();
            var messageSection = cut.FindComponent<MessageSection>();

            // Assert
            // Assert MessageSection renders with correct number of messages

            Assert.NotNull(messageSection);
            Assert.Equal(20, messageSection.Instance.messages.Count); //  Using the 20 messages tht are in the homepage
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

            // Assert
            // Assert header displays correct title
            Assert.Contains("Conestoga Connects", header.InnerHtml);
        }

        // Verifies if the contact section is displayed correctly
        [Fact]
        public void ContactSectionDisplaysCorrectWidthAndHeight()
        {
            // Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<HomePage>();

            // Act
            var contactSection = cut.FindComponent<ContactSection>();

            // Assert
            // Assert ContactSection displays correct width and height
            Assert.Equal("30vw", contactSection.Instance.Width);
            Assert.Equal("81vh", contactSection.Instance.Height);
        }

        // Tests if the components filters contacts based on a search query 
        [Fact]
        public void FiltersContactsCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();
            var contacts = new List<ContactModel>
        {
            new ContactModel { Name = "John Doe", ImageUrl = "https://www.example.com/john.jpg", IntroText = "Hello, I am John Doe" },
            new ContactModel { Name = "Jane Smith", ImageUrl = "https://www.example.com/jane.jpg", IntroText = "Hi there, I'm Jane Smith" }
        };

            var width = "50vw";
            var height = "400px";
            var title = "Contacts";

            var cut = ctx.RenderComponent<ContactSection>(
                ("Width", width),
                ("Height", height),
                ("Title", title),
                ("Contacts", contacts)
            );

            // Act
            var searchInput = cut.Find("input[type='text']");
            searchInput.Change("John");

            // Assert
            var contactItems = cut.FindAll(".ContactListContainer ");
            Assert.Single(contactItems); // Only one contact (John Doe) should match the search query
            Assert.Contains("John", contactItems[0].TextContent);
        }


        // Checks if the comment "No results..." is displayed to the screen if there are no matching contacts after searching.
        [Fact]
        public void NoResultsMessageDisplayed()
        {
            // Arrange
            using var ctx = new TestContext();

            var contacts = new List<ContactModel>();

            var width = "50vw";
            var height = "400px";
            var title = "Contacts";

            // Act
            var cut = ctx.RenderComponent<ContactSection>(
                ("Width", width),
                ("Height", height),
                ("Title", title),
                ("Contacts", contacts)
            );

            // Assert
            Assert.Contains("No results...", cut.Find(".EmptyListMessage").TextContent);
        }


        // Checks if the MessageSection Component renders with the correct width and height specified
        [Fact]
        public void MessageSectionDisplaysCorrectWidthAndHeight()
        {
            // Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<HomePage>();

            // Act
            var messageSection = cut.FindComponent<MessageSection>();

            // Assert
            Assert.Equal("63.5vw", messageSection.Instance.Width);
            Assert.Equal("81vh", messageSection.Instance.Height);
        }

        //TODO:
        // Test if clicking on the logout button in the 'NavBar' component navigates ti the logout page.
        // Same for all other buttons
    }

}

    