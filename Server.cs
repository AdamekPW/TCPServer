using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
class Server {
    public string ServerIP {get; set;}
    public int ServerPort {get; set;}

    public List<string> ActiveUsers = new();
    Thread ServerThread = null!;
    Process? TunnelProcess = null;
    
    private bool IsServerRunning = false;
    public Server(string ServerIP, int ServerPort){
        this.ServerIP = ServerIP;
        this.ServerPort = ServerPort;
    }

    ~Server() {
        Stop();
        KillTunnel();
        Console.WriteLine("Server is offline");
    }
    
    public void Start(){
        if (IsServerRunning){
            Console.WriteLine("Server already running!\n");
            return;
        }
        ServerThread = new Thread(this.Run);
        ServerThread.Start();
        IsServerRunning = true;
        Console.WriteLine("Server started");
    }
    public void Stop(){
        if (!IsServerRunning){
            Console.WriteLine("Server is not running");
            return;
        }
        IsServerRunning = false;
        Console.WriteLine("Server stoped");       
    }
    

    private void Run(){
        TcpListener server = null!;
        IPAddress iPAddress = IPAddress.Parse(ServerIP);
        try 
        {
            server = new TcpListener(iPAddress, ServerPort);
            server.Start();
            Console.WriteLine("Serwer TCP uruchomiony na adresie: " + ServerIP + " na porcie: " + ServerPort);
			Console.WriteLine("Oczekiwanie na połączenia...");

            while (IsServerRunning){
                if (server.Pending()){
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Nowe połączenie!");

                    // Utwórz nowy wątek do obsługi połączenia
                    
                    Message? message = HandleClient(client);
                    if (message != null){
                        Console.WriteLine(message.Data);
                        if (message.Data == "Stop"){
                            server.Stop();
                        }
                    }
                }     
				
            }
        }
        catch (Exception e){
            Console.WriteLine("Błąd: " + e.Message);
        }
        finally {
            server.Stop();
        }
    }

    public void StartTunnel(){
        if (TunnelProcess != null){
            Console.WriteLine("Tunnel already running");
            return;
        }
        TunnelProcess = new Process();
        TunnelProcess.StartInfo.FileName = @"C:\Program Files\playit_gg\bin\playit.exe";
        TunnelProcess.Start();
        
    }

    public void KillTunnel(){
        if (TunnelProcess == null){
            Console.WriteLine("Tunnel doesn't exist");
            return;
        }
        TunnelProcess.Kill();
        TunnelProcess = null;
        Console.WriteLine("Tunnel terminated");
    }


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