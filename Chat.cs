using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.IO;
public class Chat {

    public string name = "MyChat";
    public List<string> Users;
    public LinkedList<Message> Messages = new LinkedList<Message>();
    private LinkedListNode<Message>? HistoryPointer = null;
    public static string ChatsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Chats");
    private string ChatFileName = "";

    public Chat(){
        Users = new List<string>();
    }

    public Chat(string User1, string User2){
        Users = new List<string>(){ User1, User2 };
        ChatFileName = CreateFileName(this.Users);     
    }
    
    public Chat(List<string> Users){
        this.Users = Users;
        ChatFileName = CreateFileName(this.Users);
    }

    public void AddMessage(Message message){
        Messages.AddLast(message);
    }

    public List<Message> GetHistory(int n){
        if (HistoryPointer == null){
            HistoryPointer = Messages.Last;
        }
        List<Message> messages = new List<Message>();
        while (HistoryPointer != null && n-- > 0){
            messages.Add(HistoryPointer.Value);
            HistoryPointer = HistoryPointer.Previous;
        }
        return messages;
    }

    public string FilePath {
        get { return Path.Combine(ChatsDirectory, ChatFileName); }
    }

    private static string CreateFileName(List<string> Users){
        Users.Sort();
        string input = "";
        string ChatFileName;
        foreach (string user in Users) input += user+"::";

        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                builder.Append(bytes[i].ToString("x2"));
            
            ChatFileName = builder.ToString();
        }
        return ChatFileName;
    }
    
    public static void WriteToFile(Chat chat){
        chat.ChatFileName = CreateFileName(chat.Users);
        string jsonData = JsonConvert.SerializeObject(chat, Formatting.Indented);
        File.WriteAllText(chat.FilePath + ".json", jsonData);
    }

    public static Chat ReadFromFile(List<string> Users){
        string FilePath = Path.Combine(ChatsDirectory, CreateFileName(Users));
        string jsonData = File.ReadAllText(FilePath + ".json");
        Chat? chat = JsonConvert.DeserializeObject<Chat>(jsonData);
        if (chat == null){
            Console.WriteLine("Nie znaleziono chatu, tworzenie nowego");
            return new Chat();
        } 
        return chat;
       
    }
    
    public void Print(){
            foreach (var item in Messages){
                Console.WriteLine(item.Data);
            }
        }
}