using Microsoft.AspNetCore.Mvc;
using ReasonAPI.Models;
using ReasonRepository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ReasonAPI.Controllers
{
	// specify controller as an API controller and derive from ControllerBase class not Controller class; define the route
	[ApiController]
	[Route("api/[controller]")]
	public class ReasonController : ControllerBase
	{
		// define an instance to the Reason Repository
		private readonly IReason _reasonRepository;

		/// <summary>
		/// Constructor; inject the repository using DI
		/// </summary>
		/// <param name="reasonRepository"></param>
		public ReasonController(IReason reasonRepository)
		{
			_reasonRepository = reasonRepository;
		}

		// GET: ReasonController
		/// <summary>
		/// Fetch the reasons from the repository
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<ReasonModel> Get()
		{
			// declare instance of data to be returned
			var results = new List<ReasonModel>();
			// iterate the reason data fetced from the repository
			_reasonRepository.ReasonDataStore.ToList().ForEach(item =>
			{
				// declare an instance of the Model and map over it's values
				var model = new Models.ReasonModel
				{
					Id = item.Id,
					ReasonVerbage = item.ReasonVerbage
				};
				// dump the model into the list to be returned
				results.Add(model);
			});

			return results;
		}

		// POST: AddReason
		/// <summary>
		/// Adds new reason to the repository using model binding attribute
		/// </summary>
		/// <param name="model">The <see cref="ReasonModel"/> instance to add</param>
		/// <returns>The <see cref="ReasonModel"/> instance added</returns>
		[HttpPost]
		public IActionResult AddReason([FromBody] ReasonModel model)
		{
			// declare a new instance of a repository entity
			// invoke the repository to add reason
			// return result with status code
			return Ok(_reasonRepository.AddReason(new ReasonRepository.Entities.Reason { 
				Id = model.Id,
				ReasonVerbage = model.ReasonVerbage
			}));
		}

		// PUT: UpdateReason
		/// <summary>
		/// Modifies an existing reason in repository using model binding attribute
		/// </summary>
		/// <param name="model">The <see cref="ReasonModel"/> instance to modify</param>
		/// <returns>The <see cref="ReasonModel"/> instance modified</returns>
		[HttpPut]
		public IActionResult UpdateReason([FromBody] ReasonModel model)
		{
			// declare a new instance of a repository entity
			// invoke the repository to modify reason
			return Ok(_reasonRepository.UpdateReason(new ReasonRepository.Entities.Reason
			{
				ReasonVerbage = model.ReasonVerbage
			}));
		}
	}
}
