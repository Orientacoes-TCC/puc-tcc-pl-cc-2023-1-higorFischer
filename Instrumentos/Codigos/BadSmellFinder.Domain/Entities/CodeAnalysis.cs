namespace BadSmellFinder.Domain.Entities;

public class CodeAnalysis
{
	public string Name { get; init; } = "";
	public int Lines { get; init; }

	public IList<CodePropertyInfo> Properties { get; } = new List<CodePropertyInfo>();
	public IList<CodeMethodInfo> Methods { get; } = new List<CodeMethodInfo>();
	public IList<BadSmell> BadSmells { get; } = new List<BadSmell>();
}
