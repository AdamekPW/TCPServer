using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
class CustomClient
{
    public string ServerIP { get; set; } = null!;
	public int ServerPort { get; set; }
    //147.185.221.19:48025
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
        Thread.Sleep(1000);
        Console.WriteLine($"Próba nawiązania połączenia z ServerIP:{ServerPort}");
        try {
            TcpClient client = new TcpClient(ServerIP, ServerPort);
            Console.WriteLine("Polączono z serwerem");
            NetworkStream stream = client.GetStream();
            
            Message message = new Message("Hello from client");
            string jsonData = JsonConvert.SerializeObject(message);
            byte[] jsonDataBytes = Encoding.UTF8.GetBytes(jsonData);

            int bufferSize = 1024; // Rozmiar bufora
            int bytesSent = 0;
            while (bytesSent < jsonDataBytes.Length)
            {
                int remainingBytes = jsonDataBytes.Length - bytesSent;
                int bytesToSend = Math.Min(bufferSize, remainingBytes);
                stream.Write(jsonDataBytes, bytesSent, bytesToSend);
                bytesSent += bytesToSend;
                Console.WriteLine("Doszlo");
            }
            

        } catch (Exception e){
            Console.WriteLine("Błąd: " + e.Message);
        }
    }



}