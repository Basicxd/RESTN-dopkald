using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RESTNødopkald;
using RESTNødopkald.Controllers;

namespace TestNødopkald
{
    [TestClass]
    public class UnitTest1
    {
        public static string SensorUri = "https://xn--restndopkald20190514095809-zwc.azurewebsites.net/api/nødopkald";

        public static async Task<IList<Sensor>> GetCustomersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(SensorUri);
                IList<Sensor> cList = JsonConvert.DeserializeObject<IList<Sensor>>(content);
                return cList;
            }
        }

        public static async Task<HttpResponseMessage> AddCustomerAsync(Sensor sensor)
        {
            using (HttpClient client = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(sensor);
                Console.WriteLine("Data" + sensor);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(SensorUri, content);
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new Exception("Customer already exists. Try another id");
                }
                response.EnsureSuccessStatusCode();

                return response;
            }
        }

        [TestMethod]
        public void AntalRegistrering()
        {
            IList<Sensor> result = GetCustomersAsync().Result;

            var counted = result.Count;

            Assert.AreEqual(1, counted);
        }

        [TestMethod]
        public void DatoTest()
        {
            IList<Sensor> result = GetCustomersAsync().Result;

            var gettingTheFirstOne = result[0];

            Assert.AreEqual(gettingTheFirstOne.Dato, "18/05/2019");
        }

        [TestMethod]
        public void TidTest()
        {
            IList<Sensor> result = GetCustomersAsync().Result;

            var gettingTheFirstOne = result[0];

            Assert.AreEqual(gettingTheFirstOne.Tid, "25:24:25");
        }

        [TestMethod]
        public void MotionTest()
        {
            IList<Sensor> result = GetCustomersAsync().Result;

            var gettingTheFirstOne = result[0];

            Assert.AreEqual(gettingTheFirstOne.Motion, "Intruders here");
        }

        [TestMethod]
        public void AddingTest()
        {
            //Remove the // to test this metoded cause it POST it to the DB and in which it fails the other tests

            //Sensor newSensor = new Sensor("18/05/2019", "25:24:25", "Intruders here");

            //Thread.Sleep(3000);

            //AddCustomerAsync(newSensor);

            Thread.Sleep(3000);

            IList<Sensor> result= GetCustomersAsync().Result;

            var gettingTheFirstOne = result[1];

            Assert.AreEqual(gettingTheFirstOne.Tid, "25:24:25");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingMyFilterForPOST()
        {
            Sensor newSensor = new Sensor("18/05/2019", "25:24:25", "This Make My Filter Work :)");

            AddCustomerAsync(newSensor);
           
            Assert.Fail();
        }

    }
}
