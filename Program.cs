
Server server = new Server("127.0.0.1", 48025);
// //server.StartTunnel();
server.Start();

CustomClient client = new CustomClient("127.0.0.1", 48025);
Thread ClientThread = new Thread(client.ConnectionTest);
ClientThread.Start();

// // server.KillTunnel();

// Console.WriteLine("trying to stop");
//server.Stop();










//server.Stop();