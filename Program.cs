Server server = new Server("127.0.0.1", 48025);

CustomClient client = new CustomClient("127.0.0.1", 48025);
Thread ClientThread = new Thread(client.ConnectionTest);
ClientThread.Start();

server.Start();