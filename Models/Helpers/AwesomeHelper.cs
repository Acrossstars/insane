using Core.Domain.Common;
using Core.Domain.Interfaces;
using Core.Domain.UseCases;
using Core.Extensions;

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

    public static void FillOperablePropertiesFromMetadata(IDataContext context, IMetaProperties metadata)
    {
        try
        {
            //нихуя себе!
            // а зачем мы сначала заполняем проперти, потом тащим их в OperableProperties, потом снова заполняем проперти ТЕМ же саммым ? 
            context.OperableProperties!.ForEach(x =>
            {
                metadata.Constructor.Add(new TypeName(x.Type, x.Name.FirstLetterToLower()));

                metadata.Properties.Add(new MetaProperty(x.Modificator, x.Type, x.Name, GetAccessorsArray()));

                metadata.InjectedProperties.Add(new InjectedProperty(x.Name, x.Name.FirstLetterToLower()));
            });
        }
        catch (Exception xex)
        {

        }
    }

    public static void InjectRepositoryIntoMetadata(IDataContext context, IMetaProperties metadata)
    {
        var repositoryType = $"I{context.DomainEntityName}Repository";
        //var repositoryFieldName = $"{context.DomainEntityName.FirstLetterToLower()}Repository";
        var repositoryFieldName = $"repository";

        try
        {
            //нихуя себе!
            metadata.InjectedInfrastructure.Add(new TypeName(repositoryType, repositoryFieldName));

            //TODO: first letter to underline to lower
            metadata.PrivateFields.Add(new TypeName($"{repositoryType}", $"_{repositoryFieldName}"));

            metadata.InjectedProperties.Add(new InjectedProperty($"_{repositoryFieldName}", repositoryFieldName));
        }
        catch (Exception xex)
        {

        }
    }



    public static void FillInjectedInfrastructureFromMetadata(IDataContext context, IMetaProperties metadata)
    {
        //нихуя себе!
        context.OperableProperties!.ForEach(x =>
        {
            //metadata.InjectedInfrastructure.Add(new TypeName(x.Type, x.Name.FirstLetterToLower()));

            // add private fieleds

            metadata.InjectedProperties.Add(new InjectedProperty(x.Name, x.Name.FirstLetterToLower()));
        });
    }


}