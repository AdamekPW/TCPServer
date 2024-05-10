using System.Diagnostics;

Server server = new Server("127.0.0.1", 48025);
//server.Start();

//CustomClient client = new CustomClient("127.0.0.1", 48025);
//Thread ClientThread = new Thread(client.ConnectionTest);
//ClientThread.Start();
string File = @"C:\Program Files\playit_gg\bin\playit.exe";
Process process = new Process();
process.StartInfo.FileName = File;
process.Start();
Console.WriteLine("Aplikacja uruchomiona!");
Console.ReadLine();











//server.Stop();