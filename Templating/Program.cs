using Scriban;
using Templating;

var users = new List<User>
{
    new ( "John Doe", "gardener"),
    new ( "Roger Roe", "driver"),
    new ( "Lucy Smith", "teacher")
};

//var fileName = "D:\\templates\\Templating\\Templating\\templates\\RequestHandlerTemplate.tpl";
var fileName = $"{Directory.GetCurrentDirectory()}\\templates\\RequestHandlerTemplate.tpl";
var data = File.ReadAllText(fileName);

var tpl = Template.Parse(data);
try
{
    //var model = new
    //{
    //    Usings = new string[] { "LiveDataService.BuildingBlocks.Application.Commands" },
    //    Namespace = "Behavior.API.UseCases.SessionActions.AddActionToSession",
    //    ClassName = "BobSmith",
    //    RequestClassName = "BobSmithRequest",
    //    BaseConstructor = new string[]
    //    {
    //        "inMemoryBus",
    //        "messageBus",
    //    }
    //};

    var model = new Dictionary<string, object>();
    model.Add("Usings", new string[] { "LiveDataService.BuildingBlocks.Application.Commands" });
    model.Add("Namespace", "Behavior.API.UseCases.SessionActions.AddActionToSession");
    model.Add("ClassName", "BobSmith");
    model.Add("RequestClassName", "BobSmithRequest");
    model.Add("BaseConstructor", new string[]
        {
            "inMemoryBus",
            "messageBus",
        });

    //only with new { model }
    var res = tpl.Render(new { model });

    var file = "GeneratedClass.cs";

    //File.WriteAllText(file, res);               

    Console.WriteLine(res);

    var fileLoader = new FileLoader();
    fileLoader.AddFileToProject(@"D:\Projects\CS\ConsoleAppForSomeTesting\src\ConsoleApp\SomeFolder", "SampleFile.cs", res);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

record User(string Name, string Occupation);