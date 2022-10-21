namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Builders
{
	public class RandomDoubleAlleleFactory : IAlleleFactory<double>
	{
		private readonly ConstraintRange<double> constraintRange;

		public RandomDoubleAlleleFactory(ConstraintRange<double> constraintRange)
		{
			this.constraintRange = constraintRange;
		}

		public double GetValue() =>
			Randomizer.RandomDouble(constraintRange.Min, constraintRange.Max);
	}
}