

List<string> Usernames = new(){"Adam", "Ola", "Kasia", "Piotr"};
Chat chat = Chat.ReadFromFile(Usernames);
chat.Print();
// using Database DB = new Database();
// foreach (var user in Usernames){
//     DB.ReadUser(user);
// }
// foreach (var element in DB.Users){
//     Console.WriteLine(element.Username);
// }




