using System.IO;
using Newtonsoft.Json;

public class Database : IDisposable {
    public List<User> Users = new();
    private static string DirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");
    private static string UsersPath = Path.Combine(DirectoryPath, "Users");

    
    public Database(){
        
    }

    public void CreateUser(User user){
        Users.Add(user);
    }
    public void ReadUser(string Username){
        string UserPath = Path.Combine(UsersPath, Username + ".json");
        if (!File.Exists(UserPath)){
            Console.WriteLine($"File {Username}.json doesn't exists in the Users folder");
            return;
        }
        
        string jsonData = File.ReadAllText(UserPath);
        User? user = JsonConvert.DeserializeObject<User>(jsonData);
        
        if (user == null){
            Console.WriteLine($"Something wrong with file {Username}.json");
            return;
        }

        Users.Add(user);
    }

    public void SaveUser(User user){
        string UserPath = Path.Combine(UsersPath, user.Username + ".json");
        string jsonData = JsonConvert.SerializeObject(user, Formatting.Indented);
        File.WriteAllText(UserPath, jsonData);
    }

    public static void Init(){
        if (!Directory.Exists(DirectoryPath)){
            Directory.CreateDirectory(DirectoryPath);
            Console.WriteLine($"Created Database folder in {DirectoryPath}");
        } else {
            Console.WriteLine($"Database folder already exists");
        }

        if (!Directory.Exists(UsersPath)){
            Directory.CreateDirectory(UsersPath);
            Console.WriteLine($"Created Users folder in {UsersPath}");
        } else {
            Console.WriteLine($"Users folder already exists");
        }
        


    }

    public void Dispose()
    {
        foreach (User user in Users){
            SaveUser(user);
        }
    }
}