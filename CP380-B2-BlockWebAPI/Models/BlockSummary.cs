using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP380_B1_BlockList.Models; //Using part A of assignment in Part B
namespace CP380_B2_BlockWebAPI.Models
{
    
    public class BlockSummary //include only timestamp, hash of previous block and hash of current block
    {
        public DateTime timestamp { get; set; }
        public string previousHash { get; set; }
        public string hash { get; set; }
        public BlockSummary() //Constructor to assign values
        {
            List<Payload> data = new() { };
            var block = new Block(DateTime.Now, null, data);
            block.Mine(2);
            timestamp = block.TimeStamp;
            previousHash = block.PreviousHash;
            hash = block.Hash;
        }
    }
    
}
