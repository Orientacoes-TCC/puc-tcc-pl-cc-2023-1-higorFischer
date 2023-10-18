using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BadSmellFinder.Domain.Entities;

public class CodePropertyInfo
{
	public string Name { get; set; }
	public int Line { get; set; }

	public CodePropertyInfo(int line, MemberDeclarationSyntax member)
	{
		Line = line;

		if (member is PropertyDeclarationSyntax property)
		{
			Name = property.Identifier.Text;
		}
	}
}
