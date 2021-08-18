using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace CP380_B1_BlockList.Models
{
    public class Block
    {
        public int Nonce { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public List<Payload> Data { get; set; }

        public Block(DateTime timeStamp, string previousHash, List<Payload> data)
        {
            Nonce = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Data = data;
            Hash = CalculateHash();
        }

        //
        // JSON serialisation:
        //   https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0
        //
        public string CalculateHash()
        {
            var sha256 = SHA256.Create();
            var json = JsonSerializer.Serialize(Data);
            var inputString = $"{TimeStamp.Date:yyyy-MM-dd hh:mm:ss tt}-{PreviousHash}-{Nonce}-{json}"; // creating the input string in the format: "2021-07-21 1:14:57 AM-W0n2AYsQ+QiU4ffu5jV+kuAiz30LUHGmkHsjTxZJobo=-1-[{\"User\":\"user\",\"TransactionType\":2,\"Amount\":10,\"Item\":\"\"}]"

            var inputBytes = Encoding.ASCII.GetBytes(inputString);
            var outputBytes = sha256.ComputeHash(inputBytes);

            return Base64UrlEncoder.Encode(outputBytes);
        }

        public void Mine(int difficulty)
        {
            string hashValidation = new String('C', difficulty);     //to generate a string that has difficulty number of 'C' in it
            string hashedString = CalculateHash();                   //To generate the hash string for the current block   
            while(hashedString.Substring(0, difficulty) != hashValidation)  //Checking whether hash string begins with difficulty number of 'C'
            {
                Nonce++;                                            //increment Nonce of the block if the condition is wrong
                hashedString = CalculateHash();                     // Generate new hash string with the incremented Nonce Value
            }                                                       //Repeat till the hash string begins with difficulty number of 'C'
           
            Hash = hashedString;
                    
        }
    }
}
