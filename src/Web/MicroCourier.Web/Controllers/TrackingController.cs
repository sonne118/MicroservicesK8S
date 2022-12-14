using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroCourier.Web.DTO;
using MicroCourier.Web.RESTClients;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;

namespace MicroCourier.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private TelemetryClient telemetry;
        private readonly ITrackingAPI _trackingAPI;

        public TrackingController(ITrackingAPI trackingAPI, TelemetryClient telemetry)
        {
            _trackingAPI = trackingAPI;
            this.telemetry = telemetry;
        }


        // GET: api/Tracking
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Tracking/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var res= await _trackingAPI.GetOrderHistory(id);
                return Ok(res);
            }
            catch (BrokenCircuitException ex)
            {
                telemetry.TrackException(ex);
                // Catches error when api is in circuit-opened mode              
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry Tracking Service Is Not Available. Please try again later.");
            }
            catch (Exception ex)
            {
                telemetry.TrackException(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry Some problem Occured");
            }
           
        }

        // POST: api/Tracking
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Tracking/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
