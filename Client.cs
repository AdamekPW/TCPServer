using System.Net.Sockets;
using System.Text;

class CustomClient
{
    public string ServerIP { get; set; } = null!;
	public int ServerPort { get; set; }

	public CustomClient(string ServerIP, int ServerPort)
	{
		this.ServerIP = ServerIP;
		this.ServerPort = ServerPort;
	}
	public CustomClient(string ServerIP, string ServerPort) 
	{
		this.ServerIP = ServerIP;
		this.ServerPort = int.Parse(ServerPort);
	}

    public void ConnectionTest() 
    {
        Console.WriteLine($"Próba nawiązania połączenia z ServerIP:{ServerPort}");
        try {
            TcpClient client = new TcpClient(ServerIP, ServerPort);
            Console.WriteLine("Polączono z serwerem");
            NetworkStream stream = client.GetStream();
            while (true){
                //wysylanie wiadomosci
				Console.Write("Wpisz wiadomość: ");
				byte[] data = Encoding.ASCII.GetBytes("Hello from client");
				stream.Write(data, 0, data.Length);

				//odbieranie odpowiedzi
				data = new byte[256];
				string responseData = string.Empty;
				int bytesRead = stream.Read(data, 0, data.Length);
				responseData = Encoding.ASCII.GetString(data, 0, bytesRead);
				Console.WriteLine("Odpowiedź serwera: " + responseData);
            }

        } catch (Exception e){
            Console.WriteLine("Błąd: " + e.Message);
        }
    }



}