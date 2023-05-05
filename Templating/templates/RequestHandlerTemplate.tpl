{{- for item in model.Usings }}using {{ item }};{{- end }}

namespace {{ model.Namespace }};

public class {{ model.ClassName}}RequestHandler : BaseCommandHandler,
    IRequestHandler<{{ model.RequestClassName }}, CommandResult>
{
    public {{model.ClassName}}RequestHandler({{ for par in model.Constructor }}
        {{par.Type}} {{par.Name}}{{ if !for.last}},{{ end -}}{{- end }}{{ for par in model.InjectedInfrastructure }}
        {{par.Type}} {{par.Name}}{{ if !for.last}},{{ end -}}{{- end }}
        )
     :base({{ for par in model.BaseConstructor }}{{par}}{{ if !for.last}},{{ end -}}{{- end }})
    {
        {{ for prop in model.InjectedProperties ~}}
        {{ prop.Destination }} = {{ prop.Source }};
        {{ end ~}}

    }

    {{ for prop in model.Properties }} 
    {{prop.Modificator}} {{prop.Type}} {{prop.Name}} { {{ for state in prop.Accessors }} {{ state }}; {{- end }} }
    {{- end }}
    

    public async Task<CommandResult> Handle({{ model.RequestClassName }} request, CancellationToken cancellationToken)
    {


        var entityId = "";

        return CommandResult.Create(ContractsMessages.Operation_Success, entityId);
    }
}