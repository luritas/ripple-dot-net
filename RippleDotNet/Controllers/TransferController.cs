using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RippleDotNet.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RippleDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : Controller
    {
        private readonly TransferContext _context;

        public TransferController(TransferContext context)
        {
            _context = context;            
        }
        
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] TransferItem transferItem)
        {
            // var accountContext = new AccountContext();

            // email 로 유저정보 가져오고 AccountItem ADO.net
            //
            /*
            _context.TransferItems.Add(new TransferItem(
                (int)transferItem.UserIdx, 
                transferItem.FromAddres, 
                transferItem.FromSecret, 
                transferItem.ToAddress, 
                (double)transferItem.Amount
                ));
                   */
          //  _context.TransferItems.Add(transferItem);
         

            PineRippleWallet walletA = new PineRippleWallet(
                "rnXevhMvyewef7CdkaDXJ5erqfKQwJYtDv"
                , "ssogKLoZ6MGs5j9Cbfm5nG6tg6sT4");
            PineRippleWallet walletB = new PineRippleWallet(
                "rsZRJ9aJc2HSrkVqz2EFSp8aszEfHKFfGJ"
                , "snRKnxzwMdeNEwkA6i4SREksxZjvU");
            var personA = new RipplePerson(
                "joe@pineapples.kr"
                , "joe"
                , walletA);
            var personB = new RipplePerson(
                "andy@pineapples.kr"
                , "andy"
                , walletB);


            var payment = new PineRipplePayment(
                0.01
                , personA
                , personB);
            payment.Execute();

            Console.Write(transferItem);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
