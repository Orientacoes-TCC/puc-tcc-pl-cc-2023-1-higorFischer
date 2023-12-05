using BadSmellFinder.Domain.Entities;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BadSmell.Domain.Roslyn;

public class RoslynAnalyzer
{
	public CodeAnalysis Run(string name, string file)
	{
		var tree = CSharpSyntaxTree.ParseText(file);
		var members = tree.GetRoot().DescendantNodes().OfType<MemberDeclarationSyntax>();

		var codeAnalysis = new CodeAnalysis() 
		{
			Name = name,
			Lines = tree.GetText().Lines.Count()
		};

		foreach (var member in members)
		{
			var line = member.GetLocation().GetMappedLineSpan().StartLinePosition.Line;
			if (member is PropertyDeclarationSyntax)
				codeAnalysis.Properties.Add(new CodePropertyInfo(line, member));

			if (member is MethodDeclarationSyntax)
				codeAnalysis.Methods.Add(new CodeMethodInfo(line, member));

			if (member is ClassDeclarationSyntax)
				codeAnalysis.Classes.Add(new CodeClassInfo(line, member));

			if (member is ConstructorDeclarationSyntax)
				codeAnalysis.Constructors.Add(new CodeConstructorInfo(line, member));
		}

		return codeAnalysis;
	}
}

