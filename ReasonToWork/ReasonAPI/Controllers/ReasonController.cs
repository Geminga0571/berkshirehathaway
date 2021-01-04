using Microsoft.AspNetCore.Mvc;
using ReasonAPI.Models;
using ReasonRepository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
			var models = new List<ReasonModel>();

			// fetch the reason data from repository
			//var response = await _reasonRepository.GetReasons();
			var response = _reasonRepository.GetReasons();

			if (response != null)
			{
				// iterate the reason data fetced from the repository
				response.ToList().ForEach(item =>
				 {
					 // declare an instance of the Model and map over it's values
					 var model = new ReasonModel
					 {
						 Id = item.Id,
						 ReasonVerbage = item.ReasonVerbage,
						 ForExample = item.ForExample
					 };
					 // dump the model into the list to be returned
					 models.Add(model);
				 });
			}

			return models;
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
				ReasonVerbage = model.ReasonVerbage,
				ForExample = model.ForExample
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
				ReasonVerbage = model.ReasonVerbage,
				ForExample = model.ForExample
			}));
		}
	}
}
