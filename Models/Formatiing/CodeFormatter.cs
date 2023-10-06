using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Core.Formatiing;

public static class CodeFormatter
{
    public static string FormatCodeWithRoslyn(string sourceCode)
    {
        var formattedCode = CSharpSyntaxTree.ParseText(sourceCode).GetRoot().NormalizeWhitespace().SyntaxTree.GetText().ToString();

        return formattedCode;
    }
}
