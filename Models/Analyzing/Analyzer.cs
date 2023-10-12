using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace Core.Analyzing;

public class Analyzer
{
    private readonly string _solutionPath;
    private readonly string _projectPath;

    public Analyzer(string path_to_your_solution)
    {
        _solutionPath = path_to_your_solution;
    }

    public Analyzer(string solutionPath, string projectPath)
    {
        _solutionPath = solutionPath;
        _projectPath = projectPath;
    }

    async public Task ExtractUsings(List<string> projectsToSearch)
    {
        string outputFilePath = "Using.cs"; // Путь к файлу Using.cs

        try
        {
            // Список всех using-ов

            var specifiedProjects = await GetProjectsPromSolution(projectsToSearch);

            foreach (var project in specifiedProjects)
            {
                List<string> allUsings = new List<string>();
                
                foreach (var document in project.Documents)
                {
                    var root = await document.GetSyntaxRootAsync();
                    var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

                    //foreach (var classDecl in classes)
                    //{
                    //    Console.WriteLine($"Class: {classDecl.Identifier.Text}");
                    //    var methods = classDecl.DescendantNodes().OfType<MethodDeclarationSyntax>();
                    //    foreach (var method in methods)
                    //    {
                    //        Console.WriteLine($"  Method: {method.Identifier.Text}");
                    //    }
                    //}

                    // Извлечение using-директив
                    var usingDirectives = root.DescendantNodes().OfType<UsingDirectiveSyntax>();

                    foreach (var usingDirective in usingDirectives)
                    {
                        if (!allUsings.Contains(usingDirective.Name!.ToString()))
                        {
                            Console.WriteLine($"Using: {usingDirective.Name}");
                            allUsings.Add("global using " + usingDirective.Name!.ToString());
                        }
                    }
                }

                // Сохранение using-директив в файл Using.cs
                string usingFilePath = Path.Combine($"./{project.Name}{outputFilePath}");

                // Если файл существует, считываем текущие using-и
                List<string> oldUsings = File.Exists(usingFilePath) ? File.ReadAllLines(usingFilePath).ToList() : new List<string>();

                // Добавляем отсутствующие юзинги из newUsings в oldUsings
                foreach (var newUsing in allUsings)
                {
                    if (!oldUsings.Contains(newUsing))
                    {
                        oldUsings.Add(newUsing);
                    }
                }
                // Запись обновленных using-директив в файл Using.cs
                File.WriteAllLines(usingFilePath, oldUsings);

                //Перетрёт старый файл
                //File.WriteAllLines(usingFilePath, allUsings.Distinct());

                Console.WriteLine($"Saved using directives to {usingFilePath}");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    async private Task<IEnumerable<Microsoft.CodeAnalysis.Project>> GetProjectsPromSolution(List<string> projectsToSearch)
    {
        var workspace = MSBuildWorkspace.Create();

        var solution = await workspace.OpenSolutionAsync(_solutionPath);

        return solution.Projects.Where(x => projectsToSearch.Contains(x.Name));
    }
}