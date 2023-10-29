using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BadSmellFinder.Domain.Entities;

public class CodeClassInfo
{
	public string Name { get; set; }
	public int Line { get; set; }
	public int Lines { get; set; }

	public CodeClassInfo(int line, MemberDeclarationSyntax member)
	{
		Line = line;

		if(member is ClassDeclarationSyntax classSyntax)
		{
			Name = classSyntax.Identifier.Text;
			Lines = classSyntax.GetText().Lines.Count;
		}
	}
}
