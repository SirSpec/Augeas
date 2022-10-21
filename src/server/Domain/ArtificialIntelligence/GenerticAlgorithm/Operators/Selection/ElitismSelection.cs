using System.Collections.Generic;
using System.Linq;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Selection
{
	public class ElitismSelection : ISelectionAlgorithm
	{
		private readonly int numberOfSelectedPhenotypes;

		public ElitismSelection(int numberOfSelectedPhenotypes) =>
			this.numberOfSelectedPhenotypes = numberOfSelectedPhenotypes;

		public IEnumerable<Phenotype<TAllele>> Select<TAllele>(IEnumerable<Phenotype<TAllele>> phenotypes) =>
			phenotypes
				.OrderByDescending(phenotype => phenotype.Fitness)
				.Take(numberOfSelectedPhenotypes);
	}
}