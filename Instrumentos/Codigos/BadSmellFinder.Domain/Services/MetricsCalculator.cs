namespace BadSmellFinder.Domain.Services;

public abstract class MetricsCalculator
{
	public int Calculate(IEnumerable<int> items)
		=> Calculate(items.Select(i => (double)i));
	public abstract int Calculate(IEnumerable<double> items);

}
