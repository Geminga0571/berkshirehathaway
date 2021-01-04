using System.Collections.Generic;

namespace ReasonRepository.Entities
{
	/// <summary>
	/// Represents an instance of a job which holds many reasons to work 
	/// </summary>
	public class Job
	{
		public IEnumerable<Reason> Reasons { get; set; }
	}
}
