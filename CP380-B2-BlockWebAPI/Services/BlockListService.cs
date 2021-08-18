using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;
using Microsoft.Extensions.Configuration;

namespace CP380_B2_BlockWebAPI.Services
{
    public class BlockListService
    {
        private PendingPayloadsList _payloads;
        private BlockList _blockList;
        private readonly IConfiguration _configure;
        private readonly JsonSerializerOptions _config = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        public BlockListService(IConfiguration configuration, BlockList blockList, PendingPayloadsList pendingPayloads) {
            _blockList = blockList;
            _payloads = pendingPayloads;
            _configure = configuration;
        }
        public Block SubmitNewBlock(string hash, int nonce, DateTime timestamp)
        {

            Block block = new Block(timestamp, _blockList.Chain[_blockList.Chain.Count - 1].Hash, _payloads.payloads);
            block.CalculateHash();  //re-calculate the hash for the block

            if (block.Hash == hash)
            {
                _blockList.Chain.Add(block);    //add the block to blocklist.Chain
                _payloads.removePendingPayloads(); //remove all of the pending payload items 
                return block;
            }

            return null; //if the hash does not match, return null
        }
    }
}
