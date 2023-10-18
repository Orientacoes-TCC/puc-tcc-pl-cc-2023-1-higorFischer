using BadSmellFinder.Domain.Entities;

namespace BadSmellFinder.Domain.Services;

public class CodeConfigBuilder
{
	readonly MetricsCalculator MetricsCalculator;

	public CodeConfigBuilder(MetricsCalculator metricsCalculator)
	{
		MetricsCalculator = metricsCalculator;
	}

	public CodeConfig Build(IEnumerable<CodeAnalysis> codeAnalyses)
	{
		var linesAmount = new List<int>();
		var parametersAmount = new List<int>();
		var classesSizes = new List<int>();
		var methodsAmount = new List<int>();
		var propertiesAmount = new List<int>();

		foreach (var methods in codeAnalyses.SelectMany(c => c.Methods))
		{
			linesAmount.Add(methods.Lines);
			parametersAmount.Add(methods.Parameters);
		}

		foreach(var file in codeAnalyses)
		{
			classesSizes.Add(file.Lines);
			methodsAmount.Add(file.Methods.Count());
			propertiesAmount.Add(file.Properties.Count());
		}

		var codeConfig = new CodeConfig
		{
			LongMethod = MetricsCalculator.Calculate(linesAmount),
			LongParametersList = MetricsCalculator.Calculate(parametersAmount),
			LargeClass = MetricsCalculator.Calculate(classesSizes),
			TooManyMethods = MetricsCalculator.Calculate(methodsAmount),
			TooManyProperties = MetricsCalculator.Calculate(propertiesAmount)
		};

		return codeConfig;
	}
}
