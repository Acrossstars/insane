using Core.Domain;

namespace Core.Helpers;

public static class AwesomeHelper
{
    public static string[] GetAccessorsArray()
    {
        return new string[] { "get", "set" };
    }

    public static string GetMethodReturnType(MetaUseCase useCase)
    {
        return useCase.RequestType switch
        {
            RequestType.Query => useCase.QueryReturnTypeDto,
            RequestType.Command => "CommandResult",
        };
    }
}