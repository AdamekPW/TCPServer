using System.IO;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

public class Database : IDisposable {
    public static readonly string DatabaseDirectory = "Database";
    public static readonly string DatabasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DatabaseDirectory);
    public ModelList<User> Users = new();
    public ModelList<Chat> Chats = new();


    
    public Database(){
        
    }


    public void Init(){
        Action<string> DictInit = (path) => {
            string name = Path.GetFileNameWithoutExtension(path);
            if (!Directory.Exists(path)){
                Directory.CreateDirectory(path);
                Console.WriteLine($"Created {name} folder in {path}");
            } else {
                Console.WriteLine($"Folder {name} already exists");
            }
        };
        DictInit(DatabasePath);
        DictInit(Users.FilePath);
        DictInit(Chats.FilePath);
      
    }

    public void Dispose()
    {
       
        Users.SaveAll();
        Chats.SaveAll();


    }
}