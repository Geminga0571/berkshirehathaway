using Newtonsoft.Json;
using ReasonRepository.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReasonRepository
{
	/// <summary>
	/// Represents the data store of reasons
	/// </summary>
	public class Reason : IReason
	{
		// These constants represent each reason as a record in a database table of Reasons
		private const string FILE_NAME = "data/ReasonData.json";

		/// <summary>
		/// Represents a <see cref="List{T}"/> of type <see cref="Entities.Reason"/>
		/// </summary>
		public IEnumerable<Entities.Reason> GetReasons()
		{
			// establish a list to return whether it's filled or not
			var entities = new List<Entities.Reason>();

			// fetch persistent data from the JSON file
			var stream = File.ReadAllText(FILE_NAME);
			var results = JsonConvert.DeserializeObject<List<Entities.Reason>>(stream);

			if (results != null && results.Count() > 0)
			{
				entities.AddRange(results);
			}

			return entities;
		}

		/// <summary>
		/// Allows the addition of a new reason
		/// </summary>
		/// <param name="entity">The <see cref="Entities.Reason"/> instance to persist</param>
		/// <returns>The <see cref="Entities.Reason"/> instance added</returns>
		public async Task<Entities.Reason> AddReason(Entities.Reason entity)
		{
			// first make sure it is a new record
			if(entity.Id == 0)
			{
				// serialize new object
				var entityString = JsonConvert.SerializeObject(entity);
				// open file and append text to it (creates new file if doesn't exist)
				await File.AppendAllTextAsync(FILE_NAME, entityString);

				return entity;
			}

			return await UpdateReason(entity);
		}

		/// <summary>
		/// Allows the modify of an existing reason
		/// </summary>
		/// <param name="entity">The <see cref="Entities.Reason"/> instance to modify</param>
		/// <returns>The <see cref="Entities.Reason"/> instance modified</returns>
		public async Task<Entities.Reason> UpdateReason(Entities.Reason entity)
		{
			// make sure its an update not an add
			if(entity.Id > 0)
			{
				// serialize new object
				var entityString = JsonConvert.SerializeObject(entity);

				// fetch the list of reasons from file
				var response = GetReasons();

				if(response != null)
				{
					// convert deferred IEnumerable to a list; thus executing the fetch
					var reasonList = response.ToList();

					// find record in list and over write with new data
					reasonList[reasonList.FindIndex(x => x.Id == entity.Id)] = entity;

					// finally write the data back to file
					await File.WriteAllTextAsync(FILE_NAME, entityString);
				}

				// return the enity saved not the whole list; list can be retrieved by the client asynchronously
				return entity;
			}

			return await AddReason(entity);
		}
	}
}
