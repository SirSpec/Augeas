using System.Collections.Generic;
using Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.DataStructures;

namespace Augeas.Domain.ArtificialIntelligence.GenerticAlgorithm.Operators.Selection
{
	// https://en.wikipedia.org/wiki/Selection_(genetic_algorithm)
	public interface ISelectionAlgorithm
	{
		IEnumerable<Phenotype<TAllele>> Select<TAllele>(IEnumerable<Phenotype<TAllele>> phenotypes);
	}
}