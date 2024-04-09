// IndividualChat.cs
namespace BlazorApp.Server.Models
{
    public class IndividualChat
    {
        public int Id { get; set; }
        public string User1Id { get; set; } // User 1 ID
        public string User2Id { get; set; } // User 2 ID

/*        // Navigation properties to the users participating in the chat
        public virtual ApplicationUser User1 { get; set; }
        public virtual ApplicationUser User2 { get; set; }
*/
        // Navigation property to messages exchanged in this chat
        public virtual ICollection<Message> Messages { get; set; }

    }
}