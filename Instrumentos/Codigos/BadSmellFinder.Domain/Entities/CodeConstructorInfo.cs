using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BadSmellFinder.Domain.Entities;

public class CodeConstructorInfo
{
	public string Name { get; set; }
	public int Line { get; set; }
	public int Lines { get; set; }
	public int Parameters { get; set; }

	public CodeConstructorInfo(int line, MemberDeclarationSyntax member)
	{
		Line = line;
		
		if (member is ConstructorDeclarationSyntax constructorSyntax)
		{
			Name = constructorSyntax.Identifier.Text;
			Lines = constructorSyntax.GetText().Lines.Count;
			Parameters = constructorSyntax.ParameterList?.Parameters.Count ?? 0;
		}
	}
}
