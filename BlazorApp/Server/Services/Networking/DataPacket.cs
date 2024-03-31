using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace BlazorApp.Shared
{
	internal class DataPacket
	{
		string version { get; set; }
		string type { get; set; }
		string content { get; set; }
		string sender_ID { get; set; }
		string recipient_ID { get; set; }
		string timestamp { get; set; }

		DataPacket()
		{
			this.version = "";
			this.type = "";
			this.content = "";
			this.sender_ID = "";
			this.recipient_ID = "";
			this.timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
		}

		DataPacket(string version, string type, string content, string sender_ID, string recipient_ID, string timestamp)
		{
			this.version = version;
			this.type = type;
			this.content = content;
			this.sender_ID = sender_ID;
			this.recipient_ID = recipient_ID;
			this.timestamp = timestamp;
		}
	}
}
