using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BadSmellFinder.Domain.Entities;

public class CodeMethodInfo
{
	public string Name { get; set; }

	public int Line { get; set; }
	public int Lines { get; set; }
	public int Parameters { get; set; }

	public CodeMethodInfo(int line, MemberDeclarationSyntax member)
	{
		Line = line;

		if(member is MethodDeclarationSyntax method)
		{
			Name = method.Identifier.Text;
			Parameters = method.ParameterList.Parameters.Count;
			Lines = method.GetText().Lines.Count;
		}
	}
}
