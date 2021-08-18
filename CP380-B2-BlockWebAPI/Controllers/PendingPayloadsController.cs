using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;

namespace CP380_B2_BlockWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PendingPayloadsController : ControllerBase
    {
        private readonly PendingPayloadsList _payloads;

        public PendingPayloadsController(PendingPayloadsList payloads)
        {
            _payloads = payloads;
        }

        [HttpGet]                                   //retrieves a list of all transactions currently stored by the server
        public ActionResult<List<Payload>> Get()
        {
            var item = _payloads.payloads;
            var payld=item.ToList();
            return (payld);
        }
        [HttpPost]                              //add a new transaction to the list of pending payloads
        public ActionResult<Payload> Post(Payload data)
        {
            _payloads.payloads.Add(data);
            return data;
        }
    }
}