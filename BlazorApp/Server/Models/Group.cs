// Group.cs
using System.Collections.Generic;

namespace BlazorApp.Server.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property to messages sent in this group
        public virtual ICollection<Message> Messages { get; set; }

        // Navigation property to users in this group
        public virtual ICollection<ApplicationUser> Users { get; set; }

        // Additional properties as needed
    }
}