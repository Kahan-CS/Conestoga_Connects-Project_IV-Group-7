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
        public DateTimeOffset Time { get; set; }
        public bool IsSent { get; set; }
        public string SenderName { get; set; }
        public string SenderId { get; set; } // Change to SenderId
        public string SenderProfileImageUrl { get; set; }
        public string ReceiverId { get; set; }
        public string ImageUrl { get; set; }
    }
}

