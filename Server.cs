using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server {
    public string ServerIP {get; set;}
    public int ServerPort {get; set;}
    Thread? ServerThread = null;
    
    public Server(string ServerIP, int ServerPort){
        this.ServerIP = ServerIP;
        this.ServerPort = ServerPort;
    }
    
    public void Start(){
        if (ServerThread != null){
            Console.WriteLine("Server already running!\n");
            return;
        }
        ServerThread = new Thread(this.Run);
        ServerThread.Start();
    }
    public void Stop(){
        if (ServerThread != null){
            ServerThread.Join();
            ServerThread = null;
        }
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

            while (true){
                // Akceptuj nowe połączenie
				TcpClient client = server.AcceptTcpClient();
				Console.WriteLine("Nowe połączenie!");

				// Utwórz nowy wątek do obsługi połączenia
				ClientHandler clientHandler = new ClientHandler();
				clientHandler.HandleClient(client);
            }
        }
        catch (Exception e){
            Console.WriteLine("Błąd: " + e.Message);
        }
        finally {
            server.Stop();
        }
    }

}