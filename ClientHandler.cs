using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
class ClientHandler
{
	public Message? HandleClient(TcpClient client)
	{
		Message? message = null;
		NetworkStream stream = null!;
		try
		{
			stream = client.GetStream();

			byte[] buffer = new byte[1024];
			int bytesRead;
			string JsonString = "";
			
			while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
			{
				JsonString += Encoding.UTF8.GetString(buffer, 0, bytesRead);
				if (bytesRead < buffer.Length)
				{
					break;
				}
			}
			message = JsonConvert.DeserializeObject<Message>(JsonString);

		}
		catch (Exception e)
		{
			Console.WriteLine("Błąd obsługi klienta: " + e.Message);
		}
		finally
		{
			
			stream.Close();
			client.Close();
		}
		return message;
	}
}