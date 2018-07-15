using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RippleDotNet.Models;

namespace RippleDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountContext _context;
        
        public AccountController(AccountContext context)
        {
            _context = context;
            this._setAll();  // DB가 없어서 강제로 주입
        }
        private void _setAll()
        {
            if (_context.AccountItems.Count() == 0)
            {
                _context.AccountItems.Add(new AccountItem("luritas.me@gmail.com"));
                _context.AccountItems.Add(new AccountItem("andy@pineapplesoft.kr"));
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<AccountItem>> GetAll()
        {
            return _context.AccountItems.ToList();
        }

        [HttpGet("{email}")]
        public ActionResult<AccountItem> GetByEmail(string email)
        {
            var item = _context.AccountItems.Find(email);
            AccountItem account = item.GetAccountInfo();
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        
        // Post 로 from, secret, to, balance, memo 받아서 거래 일으켜보기!!!!
    }
}