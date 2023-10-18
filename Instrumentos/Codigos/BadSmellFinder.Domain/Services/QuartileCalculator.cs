using BadSmellFinder.Domain.Services;
using MathNet.Numerics.Statistics;

namespace Util;

public class QuartileCalculator: MetricsCalculator
{
	public override int Calculate(IEnumerable<double> items)
	{
		var doubleItems = items.Select(i => (double)i);
		return (int)(Statistics.Percentile(doubleItems, 75) - Statistics.Percentile(doubleItems, 25));
	}
}