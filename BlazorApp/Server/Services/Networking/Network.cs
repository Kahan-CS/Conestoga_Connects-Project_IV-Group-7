using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.IO;
using System.Net.Sockets;
using Newtonsoft.Json;

public class NetworkCommunication
{
	private const int BufferSize = 1024;

	public void SendData(TcpClient client, object data)
	{
		// Serialize data to JSON
		string jsonData = JsonConvert.SerializeObject(data);
		byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonData);

		// Prepend message with length
		byte[] messageLengthBytes = BitConverter.GetBytes(jsonBytes.Length);
		byte[] sendMessage = new byte[messageLengthBytes.Length + jsonBytes.Length];
		Buffer.BlockCopy(messageLengthBytes, 0, sendMessage, 0, messageLengthBytes.Length);
		Buffer.BlockCopy(jsonBytes, 0, sendMessage, messageLengthBytes.Length, jsonBytes.Length);

		// Send data
		NetworkStream stream = client.GetStream();
		stream.Write(sendMessage, 0, sendMessage.Length);
	}

	public object ReceiveData(TcpClient client)
	{
		NetworkStream stream = client.GetStream();

		// Read message length
		byte[] lengthBytes = new byte[sizeof(int)];
		stream.Read(lengthBytes, 0, lengthBytes.Length);
		int messageLength = BitConverter.ToInt32(lengthBytes, 0);

		// Read message data
		byte[] buffer = new byte[BufferSize];
		using (MemoryStream memoryStream = new MemoryStream())
		{
			int bytesRead;
			while (memoryStream.Length < messageLength && (bytesRead = stream.Read(buffer, 0, Math.Min(BufferSize, messageLength - (int)memoryStream.Length))) > 0)
			{
				memoryStream.Write(buffer, 0, bytesRead);
			}

			// Deserialize JSON data
			byte[] jsonData = memoryStream.ToArray();
			string jsonString = Encoding.UTF8.GetString(jsonData);
			return JsonConvert.DeserializeObject(jsonString);
		}
	}
}

//// Example usage:
//var networkCommunication = new NetworkCommunication();
//var client = new TcpClient("localhost", 1234);

//// Send data
//var dataToSend = new
//{
//	version = "1.0",
//	type = "message",
//	content = "Hello, how are you?",
//	sender_ID = "user1",
//	receiver_ID = "user2",
//	timestamp = "2024-01-28T15:30:00Z"
//};
//networkCommunication.SendData(client, dataToSend);

//// Receive data
//var receivedData = networkCommunication.ReceiveData(client);
//Console.WriteLine("Received data: " + JsonConvert.SerializeObject(receivedData));


