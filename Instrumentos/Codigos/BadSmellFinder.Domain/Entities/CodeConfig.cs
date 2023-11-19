namespace BadSmellFinder.Domain.Entities;

public class CodeConfig
{
	public long LongMethod { get; set; } 
	public long LongParametersList { get; set; }
	public long TooManyMethods { get; set; } 
	public long TooManyProperties { get; set; } 
	public long LargeClass { get; set; }

	public bool IsValid()
		=> LongMethod != 0
		&& LongParametersList != 0
		&& LargeClass != 0
		&& TooManyMethods != 0
		&& TooManyProperties != 0;
}