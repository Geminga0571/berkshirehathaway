using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReasonToWork.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReasonToWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _reasonAPIBaseUrl;

        /// <summary>
        /// Constructor for dependenct injection
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            // inject the configuration instance
            _configuration = configuration;
            // fetch the base uri for the API instances to be used
            _reasonAPIBaseUrl = _configuration.GetValue<string>("ReasonAPIBaseUrl");
        }

        /// <summary>
        /// Default action to fetch the reason data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // create an empty list (if nothing is found it displays nothing
            var reasonList = new List<ReasonViewModel>();

            // use http client to instantiate the API 
            using (HttpClient client = new HttpClient())
            {
                // invoke the fetch operation from the API
                using (var response = client.GetAsync(_reasonAPIBaseUrl))
				{
                    // extract data as string from the http client response package
                    var result = await response.Result.Content.ReadAsStringAsync();
                    // deserialize the string into JSON and load it into our generic list
                    reasonList = JsonConvert.DeserializeObject<List<ReasonViewModel>>(result);
                }
			}
               
            // bind the list to the view
            return View(reasonList);
        }

        /// <summary>
        /// Reprsents a GET version of Add such that nothing happens
        /// </summary>
        /// <returns></returns>
        public ViewResult AddReason() => View();

        // POST: AddReason
        /// <summary>
        /// Allows the addition of a new reason into the repository via API Service
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddReason(ReasonViewModel model)
		{
            var viewModel = new ReasonViewModel();

            using (HttpClient client = new HttpClient())
			{
                // convert the model to a JSON object using string content
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                // invoke the API and send the content
                using (var response = await client.PostAsync(_reasonAPIBaseUrl, content))
                {
                    // extract data as string from the http client response package
                    var result = await response.Content.ReadAsStringAsync();

                    // check status code
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // deserialize the JSON into the object
                        viewModel = JsonConvert.DeserializeObject<ReasonViewModel>(result);
                    } else
					{
                        // something bad happened...
                        ViewBag.Result = result;
                        return View();
					}
                }
            }

            return View(viewModel);
		}

        // PUT: UpdateReason
        /// <summary>
        /// Allows the modification of an existing reason from the repository via API Service
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateReason(ReasonViewModel model)
        {
            var viewModel = new ReasonViewModel();

            using (HttpClient client = new HttpClient())
            {
                // declare multipart to add required content disposition headers
                var content = new MultipartFormDataContent();
                // convert the model to a JSON object using string content
                content.Add(new StringContent(model.ReasonVerbage), "ReasonVerbage");

                // invoke the API and send the content
                using (var response = await client.PutAsync(_reasonAPIBaseUrl, content))
                {
                    // extract data as string from the http client response package
                    var result = await response.Content.ReadAsStringAsync();

                    // check status code
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // deserialize the JSON into the object
                        viewModel = JsonConvert.DeserializeObject<ReasonViewModel>(result);
                    }
                    else
                    {
                        // something bad happened...
                        ViewBag.Result = result;
                        return View();
                    }
                }
            }

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
