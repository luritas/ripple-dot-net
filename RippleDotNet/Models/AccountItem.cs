using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Ripple;

namespace RippleDotNet.Models
{
    public class AccountItem
    { 
        [Key]
        public string Email { get; set; }
        public string _address { get; set; }
        public string _name { get; set; }
        public string _secret { get; set; }
        public double _balance { get; set; }

        private const string _RIPPLED_URL = "http://s2.dotorie.com:8080/";
        private const string _ADDRESS_INFO = "accountinfo/";
        private const string _TRANSFER_TO = "transfer"; 

        private static readonly HttpClient client = new HttpClient();

        public AccountItem(string email)
        {
            this.Email = email;
            this._address = "";
            if (email.Equals("luritas.me@gmail.com"))
            {
                _address = "rnXevhMvyewef7CdkaDXJ5erqfKQwJYtDv";
                _name = "Wallet A";
                _secret = "ssogKLoZ6MGs5j9Cbfm5nG6tg6sT4";
                _balance = 30;
            }
            else if (email.Equals("andy@pineapplesoft.kr"))
            {
                _address = "rsZRJ9aJc2HSrkVqz2EFSp8aszEfHKFfGJ";
                _name = "Wallet B";
                _secret = "snRKnxzwMdeNEwkA6i4SREksxZjvU";
                _balance = 30;
            }
        }

        public AccountItem GetAccountInfo()
        {
            _setBalance();
            return this;
        }

        private void _setBalance()
        {
            string url = _RIPPLED_URL + _ADDRESS_INFO + _address;
            var item = _httpClientWrapperAsync(url);
            JObject json = JObject.Parse(item.Result);
            _balance = (double)json["xrpBalance"];
        }

        public string TransgerTo(string ToAddress, string Amount)
        {
            string url = _RIPPLED_URL + _TRANSFER_TO + $"?from={_address}&secret={_secret}&to={ToAddress}&amount={Amount}";
            var item = _httpClientWrapperAsync(url);
            return item.Result;
        }

        private async Task<string> _httpClientWrapperAsync(string url)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            return await client.GetStringAsync(url);    
            // TasK<T>는 (return값).Result로 간단하게 값을 얻을수 있음
        }

    }
}
