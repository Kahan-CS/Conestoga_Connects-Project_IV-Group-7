// Message.cs
using System;

namespace BlazorApp.Server.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now; // Initialize to current local time with offset

        // Foreign key to the user who sent and received the message
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        // Foreign key to the chat (either individual or group) the message belongs to
        public int ChatId { get; set; }

        // Indicates whether the chat is an individual chat or a group chat
        public bool IsGroupChat { get; set; }
    }
}