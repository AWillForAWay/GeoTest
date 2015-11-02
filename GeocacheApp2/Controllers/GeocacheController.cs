using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GeocacheApp2.Models;
using Newtonsoft.Json;

namespace GeocacheApp2.Controllers
{
    public class GeocacheController : Controller
    {
        private GeocacheContext db = new GeocacheContext();
        private string baseUrl = ConfigurationManager.AppSettings["baseUrl"];

        // GET: Geocache
        public ActionResult Index()
        {
            //return the index view
            return View();
        }

        //Return the list of geocaches as a partial view in Index
        //ChildActionOnly specifies that this Action should only
        //be called as the child of another action (ie for a partial
        //view loaded into another view)
        [ChildActionOnly]
        public ActionResult GeocacheList()
        {
            //We could be using the API to retrieve this list
            //Come back if there is time
            List<Geocache> caches = db.Geocaches.ToList();
            return PartialView("_Geocaches", caches);

        }
        

        //Return the Geocache for the id in the request body
        //Authorizing this is up for debate. The same arguements
        //exist for this as the GET method in the API
        [HttpGet]
        public async Task<ActionResult> GetGeocacheById(int id)
        {
            Geocache cache;
            //Santitize the input to an int
            int _id = Convert.ToInt32(id);

            //Declare the HttpClient to make the request
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Await the GET call and store the response
                var response = await client.GetAsync(string.Format("api/geocacheapi/{0}", _id));
                //If the response code is a success
                if (response.IsSuccessStatusCode)
                {
                    //get the content as a string because it should be JSON
                    var json = response.Content.ReadAsStringAsync().Result;
                    //convert the json to a geocache object
                    cache = JsonConvert.DeserializeObject<Geocache>(json);
                    //return the partial view with the geocache object
                    return PartialView("_Geocache", cache);
                }
            }
            //if we didn't return already return with a null
            return PartialView("_Geocache", null);
        }

        //Method to return the Create View
        //Might be able to do this differently 
        // GET: Geocache/Create
        public ActionResult CreateView()
        {
            return View("Create");
        }

        //The post request from the form. We should authorize this one as well so 
        //users who shouldn't be making post requests aren't add objects. However 
        //the authorize in the API post method also requires authorization. Just another
        //layer of protection
        // POST: Geocache/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Geocache model)
        {
            //Create new HttpClient for the request
            using (var client = new HttpClient())
            {
                //Add the base address
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                //Create a new Geocache object from the model 
                Geocache geo = new Geocache() { Name = model.Name, Latitude = model.Latitude, Longitude = model.Longitude };
                //Serialize the object to JSON
                var jsonGeocache = JsonConvert.SerializeObject(geo);
                
                //Make the POST call with the json to the POST API method
                //with JSON content
                var response = await client.PostAsync("api/geocacheapi/",
                                                        new StringContent(jsonGeocache, Encoding.UTF8, "application/json"));
                //If the response is a success
                if (response.IsSuccessStatusCode)
                {
                    //Return the user to the index view.
                    //This will not allow for multiple objects to be added
                    //User will need to go back to the Add page. If requirements specify otherwise, the app
                    //could keep the user on the page to add more caches
                    return RedirectToAction("Index");
                }
                else
                {
                    //Else the creation failed and we want to show an error message
                    ViewBag.error = "Create geocache failed.";
                    return View("Create", model);
                }
            }

        }

    }
}
