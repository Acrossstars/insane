using Scriban;

var users = new List<User>
{
    new ( "John Doe", "gardener"),
    new ( "Roger Roe", "driver"),
    new ( "Lucy Smith", "teacher")
};

var fileName = "D:\\templates\\Templating\\Templating\\templates\\RequestHandlerTemplate.tpl";
var data = File.ReadAllText(fileName);

var tpl = Template.Parse(data);
var res = tpl.Render(new
{
    model = new
    {
        Usings = new[] { "LiveDataService.BuildingBlocks.Application.Commands" },
        Namespace = "Behavior.API.UseCases.SessionActions.AddActionToSession",
        ClassName = "BobSmith",
        RequestClassName = "BobSmithRequest",
        BaseConstructor = new[]
        {
            "inMemoryBus",
            "messageBus",
        },
    }
});

Console.WriteLine(res);

record User(string Name, string Occupation);

