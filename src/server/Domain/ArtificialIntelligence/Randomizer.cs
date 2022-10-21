namespace Augeas.Domain.ArtificialIntelligence
{
	public static class Randomizer
	{
		private static IRandomNumberEngine randomNumberEngine;

		static Randomizer() =>
			randomNumberEngine = new RandomAdapter();

		public static int RandomInteger(int minimum, int maximum) =>
			randomNumberEngine.RandomInteger(new ConstraintRange<int>(minimum, maximum));

		public static double RandomDouble(double minimum, double maximum) =>
			randomNumberEngine.RandomDouble(new ConstraintRange<double>(minimum, maximum));

		public static void Is(IRandomNumberEngine randomNumberEngine) =>
			Randomizer.randomNumberEngine = randomNumberEngine;
	}
}
