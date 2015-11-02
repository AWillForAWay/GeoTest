using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GeocacheApp2.Models;
using Newtonsoft.Json;

namespace GeocacheApp2.Controllers
{
    
    public class GeocacheApiController : ApiController
    {
        private GeocacheContext db = new GeocacheContext();

        
        //Ideally we would have user accounts that would be authenticated
        //before a request could get a list of geocaches
        //But I think that a basic list of geocaches shouldn't be bad
        [HttpGet]
        // GET api/values
        public IHttpActionResult Get()
        {
            if (db.Geocaches.Count() > 0)
            {
                //return 200 and add the list of geocaches to the response body
                return Ok(db.Geocaches.ToList());
            }
            else
            {
                //return not found if there aren't any caches yet in the database
                return NotFound();
            }
        }

        //Ideally we would have user accounts that would be authenticated
        //for this one as well
        //Depending on requirements of data security keeping this one 
        //unauthorized as well would be okay
        //My initial thought would be to require authorization for everything
        //but then a user wouldn't be able to see anything about what
        //caches look like or any coordinates to get started with. If they 
        //shouldn't see anything until they've created an account then both GET
        //methods should require authorization
        [HttpGet]
        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            //Find the geocache with the id from the request
            Geocache geocache = db.Geocaches.Find(id);
            
            //if a geocache was not found
            if (geocache == null)
            {
                //Return 404
                return NotFound();
            }
            //Return 200 and the geocache in the response body
            return Ok(geocache);
        }

        //[Authorize]
        //The POST and PUT methods we definitely want to lock down
        //Users should have an account to create caches plus
        //we shouldn't accept bad input from random sources
        //They should be authorized through Federation Trust or 
        //Individual user accounts
        [HttpPost]
        // POST api/values
        public IHttpActionResult Post([FromBody]Geocache geocache)
        {
            //Check if the geocache is null from the request body
            if (geocache == null)
            {
                return BadRequest("Invalid data");
            }
            //Check that the modelstate is valid
            //return a 400 error if is invalid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //If the cache already exists we don't want to create another one
            //per requirements for geocaches to be unique
            if (!GeocacheExists(geocache.ID))
            {
                db.Geocaches.Add(geocache);
            }
            else
            {
                //Return a 409 response if the geocache exists already
                return Conflict();
            }

            try
            {
                //Save changes to the database
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //Check again if it exists. This might be overboard but is good validation
                if (GeocacheExists(geocache.ID)){
                    return Conflict(); 
                }
                else
                {
                    //Return 500 if there was an updating error not caused by already existing
                    return InternalServerError();
                }
            }
            //Return 200 and the geocache that was just created
            return Ok(geocache);
        }

        //[Authorize]
        //Echoing the comment from above. We shouldn't accept bad input from random sources
        //and we shouldn't allow just anyone to create a new geocache
        //They should be authorized through Federation Trust or 
        //Individual user accounts
        [HttpPut]
        // PUT api/values/5
        public IHttpActionResult Put(int id, [FromBody]Geocache geocache)
        {
            //Check the model state and verify it is valid
            if (!ModelState.IsValid)
            {
                //return a 400 error if is invalid
                return BadRequest(ModelState);
            }
            //If the id in the request does not match the id of the geocache in the request
            if(id != geocache.ID)
            {
                //return a 400 error if is invalid
                return BadRequest();
            }
            //Update the state of the geocache to show that it has been modified
            db.Entry(geocache).State = EntityState.Modified;

            try
            {
                //Save changes to the database
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Check if a geocache exists with that id.
                if (!GeocacheExists(id))
                {
                    //Return 404 that the geocache was not found
                    return NotFound();
                }
                else
                {
                    //Return 500 if there was an updating error not caused by already existing
                    return InternalServerError();
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //[Authorize]
        //DELETE is the same. A user should be authenticated and authorized to
        //delete only the caches that they have created and own. Unless the user
        //is an admin that has control over all of the caches. Functionality for
        //ownership of a cache has not been implemented but should be one of the 
        //first things to add if we take this further
        // DELETE api/values/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Geocache geocache = db.Geocaches.Find(id);
            //Check if the geocache is null from the request body
            if (geocache == null)
            {
                return NotFound();
            }

            //Remove the geocache from the database
            var status = db.Geocaches.Remove(geocache);
            try
            {
                //Save changes to the database
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //If there was an error with the save and the cache
                //does not exist
                if (!GeocacheExists(id))
                {
                    return Conflict();
                }
                else
                {
                    //Return 500 if there was an updating error not handled by the if statement
                    return InternalServerError();
                }
            }

            return Ok(geocache);
        }

        //Method override for DbContext.Dispose 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GeocacheExists(int id)
        {
            //return true if we found one, false otherwise
            return db.Geocaches.Count(e => e.ID == id) > 0;
        }

    }
}
