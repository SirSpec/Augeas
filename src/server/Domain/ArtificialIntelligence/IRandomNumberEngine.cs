namespace Augeas.Domain.ArtificialIntelligence
{
	public interface IRandomNumberEngine
	{
		int RandomInteger(ConstraintRange<int> constraint);
		double RandomDouble(ConstraintRange<double> constraint);
	}
}