using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ClientHandler
{
	public void HandleClient(TcpClient client)
	{
		NetworkStream stream = null!;
		try
		{
			stream = client.GetStream();

			byte[] buffer = new byte[1024];
			int bytesRead;

			while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
			{
				string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
				Console.WriteLine("Odebrano: " + data);

				// Odpowiedz klientowi
				byte[] response = Encoding.ASCII.GetBytes("Otrzymano: " + data);
				stream.Write(response, 0, response.Length);
			}
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
	}
}