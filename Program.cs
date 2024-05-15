List<string> Usernames = new(){"Adam", "Ola", "Kasia", "Piotr"};
using Database DB = new();

for (int i = 0; i < 10; i++){
    Console.WriteLine(DB.UsersSequence.Next);
}

