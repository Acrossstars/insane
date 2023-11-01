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

    public static void AttachRepositoryAlignToContext(IDataContext context, IMetaProperties metadata)
    {
        try
        {
            //нихуя себе!
            context.OperableProperties!.ForEach(x =>
            {
                metadata.Constructor.Add(new TypeName(x.Type, x.Name.FirstLetterToLower()));

                //TODO: first letter to underline to lower
                metadata.PrivateFields.Add(new TypeName(x.Type, x.Name.FirstLetterToLower()));

                metadata.InjectedProperties.Add(new InjectedProperty(x.Name, x.Name.FirstLetterToLower()));
            });
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