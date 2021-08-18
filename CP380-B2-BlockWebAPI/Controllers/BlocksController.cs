using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP380_B2_BlockWebAPI.Models;
using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Services;

namespace CP380_B2_BlockWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BlocksController : ControllerBase
    {
        private readonly BlockList _list;
        public BlocksController(BlockList list)
        {
            _list = list;
        }

        [HttpGet]                                                //GET /Blocks
        public ActionResult<List<BlockSummary>> Get()        //retrieve the block chain with summary model without payload
        {
            List<Block> list = _list.Chain.ToList();
            List<BlockSummary> blockSummary = new List<BlockSummary>();
            foreach (var block in list)
            {
                _list.AddBlock(block);
                blockSummary.Add(new BlockSummary()
                {
                    hash = block.Hash,
                    previousHash = block.PreviousHash,
                    timestamp = block.TimeStamp
                });
            }

            return blockSummary;
        }

        [HttpGet("{hash}")]                             //return a single block, or a 404 (not found), with the given hash
        public ActionResult<Block> GetBlock(string hash)
        {
            var result = _list.Chain
                         .Where(blk => blk.Hash.Equals(hash));
            int num = result.Count();
            if (result.Count() <= 0)
            {
                return NotFound();
            }
            else
            {
                return result.First();
            }
        }
        [HttpGet("{hash}/Payloads")]                    //returns just the list of data items in it

        public ActionResult<List<Payload>> GetPayload(string hash)
        {
            var result = _list.Chain
                          .Where(blk => blk.Hash.Equals(hash));
            int num = result.Count();
            if (result.Count() <= 0)
            {
                return NotFound();
            }
            else
            {
                return (result.Select(block => block.Data)
                              .First()
                              .ToList());
            }
        }
       //Assignment Part D
        [HttpPost]
        public void PostBlock(Block block)
        {
            if (block.Hash == _list.Chain[_list.Chain.Count - 1].PreviousHash)
            {
                _list.Chain.Add(block);
            }
            else
            {
                BadRequest();
            }
        }
    }
}
