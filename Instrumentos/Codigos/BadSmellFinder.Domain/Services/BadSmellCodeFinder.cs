using BadSmellFinder.Domain.Entities;

namespace BadSmellFinder.Domain.Services;

public class BadSmellCodeFinder
{
	public void Find(IEnumerable<CodeAnalysis> codeAnalyses, CodeConfig codeConfig)
	{
		if (codeConfig == null) return;

		foreach(var code in codeAnalyses)
		{
			if(code == null) continue;

			if (code.Lines > codeConfig.LargeClass)
				code.BadSmells.Add(LargeClass(
					("Name", code.Name),
					("Lines", code.Lines.ToString())
				));

			if (code.Methods.Count() > codeConfig.TooManyMethods)
				code.BadSmells.Add(TooManyMethods(
					("Name", code.Name),
					("Methods", code.Methods.Count().ToString())
				));

			if (code.Properties.Count() > codeConfig.TooManyProperties)
				code.BadSmells.Add(TooManyProperties(
					("Name", code.Name),
					("Properties", code.Properties.Count().ToString())
				));

			foreach (var method in code.Methods)
			{
				if (method.Parameters > codeConfig.LongParametersList)
					code.BadSmells.Add(LongParameterList(
						("Name", method.Name),
						("Parameters", method.Parameters.ToString()),
						("Line", method.Line.ToString())
					));

				if (method.Lines > codeConfig.LongMethod)
					code.BadSmells.Add(LongMethod(
						("Name", method.Name),
						("Lines", method.Lines.ToString()),
						("Line", method.Line.ToString())
					));
			}
		}
	}

	BadSmell LargeClass(params (string key, string value)[] data)
		=> new BadSmell("Large Class", "Shows if the class has more than a specific number of lines")
				.AddParameter(data);
	BadSmell LongMethod(params (string key, string value)[] data)
		=> new BadSmell("Long Method", "Shows if the method has more than a specific number of lines")
				.AddParameter(data);
	BadSmell TooManyMethods(params (string key, string value)[] data)
		=> new BadSmell("Too Many Methods", "Shows if the amount of methods has more than a specific number")
				.AddParameter(data);
	BadSmell LongParameterList(params (string key, string value)[] data)
		=> new BadSmell("Long Parameter List", "Shows if the method has more than a specific number of parameters")
				.AddParameter(data);
	BadSmell TooManyProperties(params (string key, string value)[] data)
		=> new BadSmell("Too Many Properties", "Shows if the amount of properties has more than a specific number")
				.AddParameter(data);
}
