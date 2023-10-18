using BadSmellFinder.Domain.Services;

namespace Util;

public class AverageCalculator : MetricsCalculator
{
	public override int Calculate(IEnumerable<double> items)
		=> (int)(items.Average());
}