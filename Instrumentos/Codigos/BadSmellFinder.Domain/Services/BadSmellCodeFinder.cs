using BadSmellFinder.Domain.Entities;

namespace BadSmellFinder.Domain.Services;

public class BadSmellCodeFinder
{
	public void Find(IEnumerable<CodeAnalysis> codeAnalyses, CodeConfig codeConfig)
	{
		if (codeConfig == null) return;

		foreach(var analysis in codeAnalyses)
		{
			if(analysis == null) continue;

			if (analysis.Lines > codeConfig.LargeClass)
				analysis.BadSmells.Add(LargeClass(
					("Name", analysis.Name),
					("Lines", analysis.Lines.ToString())
				));

			if (analysis.Methods.Count() > codeConfig.TooManyMethods)
				analysis.BadSmells.Add(TooManyMethods(
					("Name", analysis.Name),
					("Methods", analysis.Methods.Count().ToString())
				));

			if (analysis.Properties.Count() > codeConfig.TooManyProperties)
				analysis.BadSmells.Add(TooManyProperties(
					("Name", analysis.Name),
					("Properties", analysis.Properties.Count().ToString())
				));

			foreach (var method in analysis.Methods)
			{
				if (method.Parameters > codeConfig.LongParametersList)
					analysis.BadSmells.Add(LongParameterList(
						("Name", method.Name),
						("Parameters", method.Parameters.ToString()),
						("Line", method.Line.ToString())
					));

				if (method.Lines > codeConfig.LongMethod)
					analysis.BadSmells.Add(LongMethod(
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
