using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using GeocacheApp2.Controllers;
using GeocacheApp2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace GeocacheApp2.Tests.Controllers
{
    [TestClass]
    public class GeocacheApiControllerTest
    {
        private GeocacheApiController controller;

        //[TestInitialize]
        //void Initialize()
        //{
        //    controller = new GeocacheController();
        //}

        [TestMethod]
        public void Get()
        {

            controller = new GeocacheApiController();
            var response = controller.Get();
            var contentResult = response as OkNegotiatedContentResult<List<Geocache>>;

            Assert.IsInstanceOfType(response, typeof(OkResult));
            Assert.IsNotNull(response);
            Assert.IsNotNull(contentResult.Content);


        }

        [TestMethod]
        public void GetAllGeocaches()
        {
            controller = new GeocacheApiController();

            var response = controller.Get();
            var contentResult = response as OkNegotiatedContentResult<List<Geocache>>;

            List<Geocache> list = contentResult.Content;
            int count = list.Count;
            Geocache cache = list[0];

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkResult));
            Assert.AreEqual(12, count);
            Assert.IsNotNull(response);
            Assert.AreEqual("RustonWay", cache.Name);
        }

        [TestMethod]
        public void GetGeocacheById()
        {
            controller = new GeocacheApiController();
            var response = controller.Get(2);
            var contentResult = response as OkNegotiatedContentResult<Geocache>;
            Geocache geocache = contentResult.Content;

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<Geocache>));
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("BarCache", geocache.Name);
            Assert.AreEqual(47.256219M, geocache.Latitude);
            Assert.AreEqual(-122.439684M, geocache.Longitude);

        }

        [TestMethod]
        public void PostGeocache()
        {
            controller = new GeocacheApiController();
            Geocache geocache = new Geocache() { Name = "RustonWay2", Latitude = 47.238302M, Longitude = -122.491245M };
            var response = controller.Post(geocache);
            var contentResult = response as OkNegotiatedContentResult<Geocache>;

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<Geocache>));
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("RustonWay2", geocache.Name);
            Assert.AreEqual(47.238302M, geocache.Latitude);
            Assert.AreEqual(-122.491245M, geocache.Longitude);
        }

        [TestMethod]
        public void PutNewGeocache()
        {
            controller = new GeocacheApiController();
            Geocache geocache = new Geocache() { ID = 13, Name = "RustonWay3", Latitude = 47.258302M, Longitude = -122.401245M };
            var response = controller.Put(geocache.ID, geocache);
            var contentResult = response as OkNegotiatedContentResult<Geocache>;

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(StatusCodeResult));
        }
        [TestMethod]
        public void PutAlreadyExistingGeocache()
        {
            controller = new GeocacheApiController();
            Geocache geocache = new Geocache() { ID = 2, Name = "BarCache2", Latitude = 47.258302M, Longitude = -122.401245M };
            var response = controller.Put(geocache.ID, geocache);
            var contentResult = response as OkNegotiatedContentResult<Geocache>;

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(StatusCodeResult));
        }
        [TestMethod]
        public void DeleteGeocacheThatDoesNotExistAndExpectNotFound()
        {
            controller = new GeocacheApiController();
            var response = controller.Delete(20);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));

        }
        [TestMethod]
        public void DeleteGeocacheThatDoesExistAndExpectOk()
        {
            controller = new GeocacheApiController();
            var response = controller.Delete(15);
            var contentResult = response as OkNegotiatedContentResult<Geocache>;

            Assert.IsNotNull(response);
            Assert.IsNotInstanceOfType(response, typeof(OkResult));
        }


    }
}
