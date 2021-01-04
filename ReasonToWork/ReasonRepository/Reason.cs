using ReasonRepository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ReasonRepository
{
	/// <summary>
	/// Represents the data store of reasons
	/// </summary>
	public class Reason : IReason
	{
		// These constants represent each reason as a record in a database table of Reasons
		private const string REASON_ONE = @"Reason";
		private const string REASON_TWO = @"Reason";
		private const string REASON_THREE = @"Reason";
		private const string REASON_FOUR = @"Reason";
		private const string REASON_FIVE = @"Reason";
		private const string REASON_SIX = @"Reason";
		private List<Entities.Reason> _reasonDataStore;

		public Reason()
		{
			_reasonDataStore = new List<Entities.Reason>
			{
				new Entities.Reason { Id = 1, ReasonVerbage = REASON_ONE },
				new Entities.Reason { Id = 2, ReasonVerbage = REASON_TWO },
				new Entities.Reason { Id = 3, ReasonVerbage = REASON_THREE },
				new Entities.Reason { Id = 4, ReasonVerbage = REASON_FOUR },
				new Entities.Reason { Id = 5, ReasonVerbage = REASON_FIVE },
				new Entities.Reason { Id = 6, ReasonVerbage = REASON_SIX }
			};
		}

		/// <summary>
		/// Represents a <see cref="List{T}"/> of type <see cref="Entities.Reason"/>
		/// </summary>
		public IEnumerable<Entities.Reason> ReasonDataStore => _reasonDataStore;

		/// <summary>
		/// Allows the addition of a new reason
		/// </summary>
		/// <param name="entity">The <see cref="Entities.Reason"/> instance to persist</param>
		/// <returns>The <see cref="Entities.Reason"/> instance added</returns>
		public Entities.Reason AddReason(Entities.Reason entity)
		{
			// first make sure it is a new record
			if(entity.Id == 0)
			{
				// we have an add request so increment the Id value
				entity.Id = _reasonDataStore.Max(x => x.Id) + 1;
				_reasonDataStore.Add(entity);

				// return the enity saved not the whole list; list can be retrieved by the client asynchronously
				return entity;
			}

			return UpdateReason(entity);
		}

		/// <summary>
		/// Allows the modify of an existing reason
		/// </summary>
		/// <param name="entity">The <see cref="Entities.Reason"/> instance to modify</param>
		/// <returns>The <see cref="Entities.Reason"/> instance modified</returns>
		public Entities.Reason UpdateReason(Entities.Reason entity)
		{
			// make sure its an update not an add
			if(entity.Id > 0)
			{
				// set the item in list as the new item
				_reasonDataStore[_reasonDataStore.FindIndex(index => index.Id == entity.Id)] = entity;

				// return the enity saved not the whole list; list can be retrieved by the client asynchronously
				return entity;
			}

			return AddReason(entity);
		}
	}
}
