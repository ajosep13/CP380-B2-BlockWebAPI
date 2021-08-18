using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP380_B1_BlockList.Models;

namespace CP380_B2_BlockWebAPI.Models
{
    public class PendingPayloadsList
    {
        public List<Payload> payloads = new List<Payload>();

        public PendingPayloadsList()
        {
            payloads = new List<Payload>() { };
        }
        public void removePendingPayloads() //remove pending payload
        {
            payloads.Clear();
        }
    }
  
}