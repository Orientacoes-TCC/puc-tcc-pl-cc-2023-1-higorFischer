namespace BadSmellFinder.Domain.Entities;

public class CodeAnalysis
{
	public string Name { get; init; } = "";
	public int Lines { get; init; }

	public IList<CodePropertyInfo> Properties { get; } = new List<CodePropertyInfo>();
	public IList<CodeMethodInfo> Methods { get; } = new List<CodeMethodInfo>();
	public IList<CodeClassInfo> Classes { get; } = new List<CodeClassInfo>();
	public IList<CodeConstructorInfo> Constructors { get; } = new List<CodeConstructorInfo>();

	public IList<BadSmell> BadSmells { get; } = new List<BadSmell>();
}
