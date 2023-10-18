namespace BadSmellFinder.Domain.Entities;

public class CodeConfig
{
	public long LongMethod { get; set; } = 3;
	public long LongParametersList { get; set; } = 3;

	public long TooManyMethods { get; set; } = 2;
	public long TooManyProperties { get; set; } = 2;
	public long LargeClass { get; set; } = 50;
}