using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

public class Message
{
	public string Version { get; set; }
	public string Type { get; set; }
	public string Content { get; set; }
	public string SenderID { get; set; }
	public string ReceiverID { get; set; }
	public string Timestamp { get; set; }
}

public class Program
{
	public static void Main()
	{
		// Sample message to send
		Message messageToSend = new Message
		{
			Version = "1.0",
			Type = "message",
			Content = "Hello, how are you?",
			SenderID = "user1",
			ReceiverID = "user2",
			Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
		};

		// Serialize the message to JSON
		string jsonMessage = JsonConvert.SerializeObject(messageToSend);

		// Create a TCP/IP socket
		TcpListener listener = new TcpListener(IPAddress.Any, 12345);
		listener.Start();

		// Accept incoming client connection
		TcpClient client = listener.AcceptTcpClient();
		NetworkStream stream = client.GetStream();

		// Send the JSON message over the network
		byte[] jsonData = Encoding.UTF8.GetBytes(jsonMessage);
		stream.Write(jsonData, 0, jsonData.Length);

		// Receive the JSON message from the client
		byte[] buffer = new byte[1024];
		int bytesRead = stream.Read(buffer, 0, buffer.Length);
		string receivedJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

		// Deserialize the received JSON message
		Message receivedMessage = JsonConvert.DeserializeObject<Message>(receivedJson);

		// Display received message
		Console.WriteLine("Received message:");
		Console.WriteLine($"Version: {receivedMessage.Version}");
		Console.WriteLine($"Type: {receivedMessage.Type}");
		Console.WriteLine($"Content: {receivedMessage.Content}");
		Console.WriteLine($"Sender ID: {receivedMessage.SenderID}");
		Console.WriteLine($"Receiver ID: {receivedMessage.ReceiverID}");
		Console.WriteLine($"Timestamp: {receivedMessage.Timestamp}");

		// Close the connection
		stream.Close();
		client.Close();
		listener.Stop();
	}
}


