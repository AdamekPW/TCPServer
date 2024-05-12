


List<string> Users = new List<string>(){ "Piotr","Adam", "Basia", "Ola"};
// Chat chat = new Chat(Users);
// for (int i = 0; i < 30; i++){
//     chat.AddMessage(new Message(i.ToString()));
// }

Chat chat1 = Chat.ReadFromFile(Users);
chat1.Print();
Chat.WriteToFile(chat1);
Console.WriteLine("-------");
List<Message> History = chat1.GetHistory(10);
History.AddRange(chat1.GetHistory(10));
foreach (var element in History){
    Console.WriteLine(element.Data);
}




