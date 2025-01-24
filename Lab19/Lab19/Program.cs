using MyHashMap;
using System.Reflection;
using System.Text.RegularExpressions;
namespace Lab19
{
    public class Lab5
    {
        static string file = "input.txt";
        static StreamReader sr = new StreamReader(file);
        static public MyHashMap<string, int> Teg()
        {
            string line = sr.ReadLine();
            if (line == null) { throw new Exception("Пустая строка"); }
            var result = new MyHashMap<string,int>();
            while (line != null)
            {
                bool isOpen = false;
                bool isTeg = false;
                string teg = "";
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '<' && line[i + 1] != null)
                    {
                        if (line[i + 1] == '/' || char.IsLetter(line[i + 1]) && !isOpen) { isOpen = true; }
                    }
                    if (line[i] == '>' && isOpen == true) { teg += line[i]; isTeg = true; isOpen = false; }
                    if (isOpen && (line[i] == '<' || line[i] == '/' || char.IsLetter(line[i]) || char.IsDigit(line[i]))) teg += line[i];
                    if (isTeg) 
                    {
                        string replacedTeg = teg.Replace("/", "");
                        string lowerTeg = replacedTeg.ToLower();
                        if (result.ContainsKey(lowerTeg))
                        {
                            int cnt = result.Get(lowerTeg);
                            result.PutValue(lowerTeg, cnt + 1);
                        }
                        else
                        {
                            result.Put(lowerTeg, 1);
                        }
                        teg = "";
                        isTeg = false; 
                    }
                }
                line = sr.ReadLine();
            }
            return result;
        }
        static void Main(string[] args)
        {
            var array = Teg();
            var teg = array.KeySet().ToArray();
            for (int i = 0; i < teg.Length; i++)
            {
                Console.WriteLine($"{teg[i]} : {array.Get(teg[i])}");
            }
        }
    }
}
