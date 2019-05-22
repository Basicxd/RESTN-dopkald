using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTNødopkald.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private string ConnectionString =
            "Server=tcp:basic1997.database.windows.net,1433;Initial Catalog=Nødopkald;Persist Security Info=False;User ID=basic;Password=Polo1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET: api/Alert
        [HttpGet(Name = "GetAllAlert")]
        public IEnumerable<Sensor> GetAllAlert()
        {
            const string selectString = "select * from dbo.Nødopkald2";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Sensor> sensorList = new List<Sensor>();
                        while (reader.Read())
                        {
                            Sensor book = ReadSensor(reader);
                            sensorList.Add(book);
                        }
                        return sensorList;
                    }
                }
            }
        }

        private static Sensor ReadSensor(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            string dato = reader.IsDBNull(1) ? null : reader.GetString(1);
            string tid = reader.IsDBNull(2) ? null : reader.GetString(2);
            string motion = reader.IsDBNull(3) ? null : reader.GetString(3);
            Sensor sensor = new Sensor
            {
                Id = id,
                Dato = dato,
                Tid = tid,
                Motion = motion
            };
            return sensor;
        }

        // POST: api/Alert
        [HttpPost(Name = "PostAlert")]
        public HttpResponseMessage PostAlert([FromBody] Sensor value)
        {
            const string insertString = "insert into dbo.Nødopkald2 (dato, tid, motion) values (@dato, @tid, @motion)";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertString, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@dato", value.Dato);
                    insertCommand.Parameters.AddWithValue("@tid", value.Tid);
                    insertCommand.Parameters.AddWithValue("@motion", value.Motion);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
        }

        // PUT: api/Alert/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete(Name = "DeleteAlert")]
        public HttpResponseMessage DeleteAlert(int id)
        {
            const string insertString = "DELETE FROM dbo.Nødopkald2";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertString, databaseConnection))
                {
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
        }
    }
}
