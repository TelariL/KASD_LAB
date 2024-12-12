using System.Text.RegularExpressions;
using MyHashMap;
public class Var
{
    enum TypeEnum
    {
        Integer,
        Double,
        Float
    }
    static void Main(string[] args)
    {
        try
        {
            MyHashMap<string, string> variable = new MyHashMap<string, string>();
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
                    string nameValue = parts[1].Trim() + "(" + valuable + ")";
                    if (variable.ContainsKey(nameValue)) Console.WriteLine("повтор" + " " + $"{type} {name}={valuable}");
                    else variable.Put(nameValue, type);
                }
                line = sr.ReadLine();
            }
            sr.Close();
            var keys = variable.KeySet().ToArray();
            for (int i = 0; i < keys.Length; i++)
                Console.WriteLine(variable.Get(keys[i]) + " => " + keys[i]);
        }
        catch (Exception ex) { Console.WriteLine("Exception : " + ex.Message); }

    }
}