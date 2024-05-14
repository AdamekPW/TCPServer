using System.IO;
using System.Reflection.Metadata;
using Newtonsoft.Json;

public class Database : IDisposable {
    public List<User> Users = new();
    public readonly static string DirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");
    public readonly static string UsersPath = Path.Combine(DirectoryPath, "Users");
    public readonly static string ChatsPath = Path.Combine(DirectoryPath, "Chats");
    
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
        Action<string> DictInit = (path) => {
            string name = Path.GetFileNameWithoutExtension(path);
            if (!Directory.Exists(path)){
                Directory.CreateDirectory(path);
                Console.WriteLine($"Created {name} folder in {path}");
            } else {
                Console.WriteLine($"Folder {name} already exists");
            }
        };
        DictInit(DirectoryPath);
        DictInit(UsersPath);
        DictInit(ChatsPath);

    }

    public void Dispose()
    {
        foreach (User user in Users){
            SaveUser(user);
        }
    }
}