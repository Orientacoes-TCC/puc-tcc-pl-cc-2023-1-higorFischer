namespace BadSmellFinder.Domain.Entities;

public class ProjectAnalysis
{
	public int TotalVerifiedFiles { get; private set; }
	public int FilesWithBadSmells { get; private set; }
	public int TotalBadSmells { get; private set; }
	public int TotalLines { get; private set; } 
	public int TotalClasses { get; private set; }
	public int TotalMethods { get; private set; }

	public void Add(CodeAnalysis code)
	{
		FilesWithBadSmells += code.BadSmells.Any() ? 1 : 0;
		TotalBadSmells += code.BadSmells.Count();

		TotalLines += code.Lines;
		TotalClasses += code.Classes.Count();
		TotalMethods += code.Methods.Count();
	}

	public void AddTotalVerifiedFiles() => TotalVerifiedFiles += 1;

	public void Add(IEnumerable<CodeAnalysis> codes)
	{
		foreach (var code in codes) Add(code);
	}
}
