using System.Text.RegularExpressions;
using MyHashMap;
public class Var
{
    static void Main(string[] args)
    {
        MyHashMap<string, string> variableAndType = new MyHashMap<string, string>();
        MyHashMap<string, string> variableAndValue = new MyHashMap<string, string>();
        string pattern = @"(double|int|float) \S* ?(?:=) ?(\S)+?(?=;)";
        string path = "input.txt";
        StreamReader sr = new StreamReader(path);
        string? line = sr.ReadLine();
        if (line == null) Console.WriteLine("Строчка пуста");
        while (line != null)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            foreach (Match match in matches)
            {
                string[] parts = match.Value.Split(' ');
                string type = parts[0].Trim();
                string valuable = parts[3].Trim();
                string name = parts[1].Trim();
                if (variableAndType.ContainsKey(name)) Console.WriteLine("повтор" + " " + $"{type} {name}={valuable}");
                else
                {
                    variableAndType.Put(name, type);
                    variableAndValue.Put(name, valuable);
                }
            }
            line = sr.ReadLine();
        }
        sr.Close();
        var values = variableAndValue.KeySet().ToArray();
        var keys = variableAndValue.KeySet().ToArray();
        for (int i = 0; i < keys.Length; i++)
            Console.WriteLine(variableAndType.Get(keys[i]) + " => " + $"{values[i]}" + $"({variableAndValue.Get(values[i])})");
    }
}
