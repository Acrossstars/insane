using System.Text;

namespace Templating;

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
