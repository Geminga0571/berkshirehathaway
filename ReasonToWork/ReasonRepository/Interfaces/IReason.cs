using System.Collections.Generic;

namespace ReasonRepository.Interfaces
{
	/// <summary>
	/// Represents a gateway to the Reason Repository
	/// </summary>
	public interface IReason
    {
		/// <summary>
		/// Represents a <see cref="IEnumerable{T}"/> of type <see cref="Entities.Reason"/>
		/// </summary>
		IEnumerable<Entities.Reason> ReasonDataStore { get; }

		/// <summary>
		/// Allows the addition of a new reason
		/// </summary>
		/// <param name="entity">The <see cref="Entities.Reason"/> instance to persist</param>
		/// <returns>The <see cref="Entities.Reason"/> instance added</returns>
		Entities.Reason AddReason(Entities.Reason entity);

		/// <summary>
		/// Allows the modify of an existing reason
		/// </summary>
		/// <param name="entity">The <see cref="Entities.Reason"/> instance to modify</param>
		/// <returns>The <see cref="Entities.Reason"/> instance modified</returns>
		Entities.Reason UpdateReason(Entities.Reason entity);
	}
}
