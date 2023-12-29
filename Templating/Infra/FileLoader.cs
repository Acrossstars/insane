using Core.ConfigurationModels;
using System.Text;

namespace Templating.Infra;

internal class FileLoader
{
    private readonly string _outputDirectory;

    public FileLoader()
    {
    }

    public FileLoader(string configPath)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile(configPath, optional: false, reloadOnChange: true);

        var configuration = configurationBuilder.Build();

        // Допустим, у вас есть класс конфигурации OutputFolderConfig, который соответствует структуре вашего файла конфигурации
        var outputConfig = configuration.GetSection("OutputFolderConfig").Get<OutputFolderConfig>();
        _outputDirectory = outputConfig.GeneratedCodeSolutionRootPath;
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

    public void AddOutputFilesToFolder(string fileName, string fileContent)
    {
        // C:\\Users\\human\\source\\repos\\ZephyrTeam\\arduino-api\\User.cs - GeneratedCode ?
        var fullPath = Path.Combine(_outputDirectory, fileName);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        File.WriteAllText(fullPath, fileContent, Encoding.UTF8);
    }
}
