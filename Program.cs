Server server = new Server();
server.Start();
CustomClient customClient = new();
customClient.Send(new User("Adam", "D"), true);



server.Stop();

