
using System.Dynamic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
public class Model {
    
    public string FileName;

    public Model(){
        FileName = "";
    }

    public Model(string FileName){
        this.FileName = FileName;
    }

    public void Save(){
        string ModelPath = Path.Combine(Database.DatabasePath, GetType().ToString() + 's', FileName + ".json");
        string jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(ModelPath, jsonData);
    }


}

public class ModelList<T>: List<T> where T : Model {
    public readonly string FilePath;

    public ModelList(){
        FilePath = Path.Combine(Database.DatabasePath, typeof(T).Name + 's');
    }

    public void Read(string FileName){
        foreach (T model in this){
            if (model.FileName == FileName){
                this.Add(model);
            }
        }
       
        string ModelPath = Path.Combine(FilePath, FileName + ".json");
        if (!File.Exists(ModelPath)){
            Console.WriteLine($"File {FileName}.json doesn't exists in the Users folder");
            return;
        }
        
        string jsonData = File.ReadAllText(ModelPath);
        T? Result = JsonConvert.DeserializeObject<T>(jsonData);

        return;
    }

    public void SaveAll(){
        Console.WriteLine($"Saving {typeof(T).Name}s...");
        foreach (T element in this){
            element.Save();
        }
        Console.WriteLine("Done");
    }

}