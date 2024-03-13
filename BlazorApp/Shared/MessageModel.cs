using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public class MessageModel
    {
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public bool IsSent { get; set; } //True for sent, false for received
        public string SenderName { get; set; } //Add this for the sender's name
        public string SenderProfileImageUrl { get; set; } //Add this for the sender's profile image URL
        public string ImageUrl { get; set; }
    }
}
