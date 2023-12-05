using BadSmell.Domain.Roslyn;
using BadSmellFinder.Domain.Entities;
using BadSmellFinder.Domain.Services;
using Util;

namespace BadSmellFinder.Services;

public class BadSmellFinderService
{
	readonly BadSmellCodeFinder BadSmellCodeFinder;
	readonly RoslynAnalyzer RoslynAnalyzer;
	readonly BadSmellFinderStorage Storage;
	public readonly IDictionary<string, CodeConfig> Hints;

	public BadSmellFinderService(BadSmellFinderStorage storage)
	{
		BadSmellCodeFinder = new BadSmellCodeFinder();
		RoslynAnalyzer = new RoslynAnalyzer();
		Hints = new Dictionary<string, CodeConfig>();
		Storage = storage;
	}

	public IEnumerable<CodeAnalysis> Find(string filePath, CodeConfig? config)
	{
		if (config == null) return Enumerable.Empty<CodeAnalysis>();

		var files = new FileReader("*.cs", recursively: true).Read(filePath);

		var analysis = new List<CodeAnalysis>();
		foreach (var file in files)
		{
			if (file.Key.ToLower().Contains("test") || file.Key.ToLower().Contains("fake")) continue;

			Storage.ProjectAnalysis.AddTotalVerifiedFiles();
			var roslynAnalyzer = RoslynAnalyzer.Run(file.Key, file.Value);
			analysis.Add(roslynAnalyzer);
		}

		Hints.Add("quartille", new CodeConfigBuilder(new QuartileCalculator()).Build(analysis));
		Hints.Add("average", new CodeConfigBuilder(new AverageCalculator()).Build(analysis));

		Storage.AddConfig(config);
		BadSmellCodeFinder.Find(analysis, config);

		Storage.ProjectAnalysis.Add(analysis);
		return analysis.Where(c => c.BadSmells.Count() > 0);
	}

}