using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Text;

namespace Core.Formatiing;

public static class CodeFormatter
{
    public static string FormatCodeWithRoslyn(string sourceCode)
    {
        var formattedCode = CSharpSyntaxTree.ParseText(sourceCode).GetRoot().NormalizeWhitespace().SyntaxTree.GetText().ToString();

        return formattedCode;
    }
}
