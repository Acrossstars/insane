using System.Text;
using System.Text.RegularExpressions;

namespace Templating.Infra;

internal class FileLoader
{
    public FileLoader()
    {
    }

    public void AddFileToProject(string fileDirectory, string fileName, string fileContent)
    {
        Directory.CreateDirectory(fileDirectory);

        using (var file = File.Open($"{fileDirectory}\\{fileName}", FileMode.OpenOrCreate))
        {
            var bytes = Encoding.UTF8.GetBytes(fileContent);
            file.Write(bytes, 0, bytes.Length);
        }
    }
}

internal class FileWriter
{
    public void Write(string fileDirectory, string fileName, string fileContent)
    {
        Directory.CreateDirectory(fileDirectory);

        var path = $"{fileDirectory}\\{fileName}";

        using (var file = File.Open(path, FileMode.Create))
        {
            var bytes = Encoding.UTF8.GetBytes(fileContent);
            file.Write(bytes, 0, bytes.Length);
        }
    }

    public void Append(string fileDirectory, string fileName, FileAppendArea area, string content)
    {
        var path = $"{fileDirectory}\\{fileName}";
        
        if (!File.Exists(path))
        {
            // TODO: В перспективе прикрутить логги
            throw new FileNotFoundException();
        }

        var lines = File.ReadAllLines(path);
        
        if (area == FileAppendArea.Usings)
        {
            var usings = lines.Where(x => x.StartsWith("using")).ToList();
            usings.Add(content);

            // TODO: Это может занять тысячу памяти, нужно потестить
            usings = usings.OrderBy(x => x).ToList();
        }
    }
}

internal enum FileAppendArea
{
    Usings = 0,

}
