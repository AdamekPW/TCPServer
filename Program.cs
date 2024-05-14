List<string> Usernames = new(){"Adam", "Ola", "Kasia", "Piotr"};
using Database DB = new();
foreach (var element in Usernames){
    DB.Users.Add(new User(element, "A"));
}
Chat chat = new Chat(Usernames);
chat.AddMessage(new Message("Hello"));
DB.Chats.Add(chat);
