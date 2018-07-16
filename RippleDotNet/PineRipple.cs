using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RippleDotNet
{
    public class PineRippleWallet
    {
        public PineRippleWallet(String address, String secret)
        {
            this.Address = address;
            this.Secret = secret;
        }
        public String Address { get; set; }
        public String Secret { get; set; }
    }
    public class RipplePerson
    {
        public RipplePerson(String email, String name, PineRippleWallet wallet)
        {
            this.Email = email;
            this.Name = name;
            this.Wallet = wallet;

        }
        public String Email { get; set; }
        public String Name { get; set; }
        public PineRippleWallet Wallet { get; set; }
    }
    public class PineRipplePayment
    {
        public PineRipplePayment(double amount, RipplePerson sender, RipplePerson receiver)
        {
            this.Amount = amount;
            this.Sender = sender;
            this.Receiver = receiver;
            this.Message = null;
        }
        public double Amount { get; set; }
        public RipplePerson Sender { get; set; }
        public RipplePerson Receiver { get; set; }
        public String Message { get; protected set; }
        public void Execute()
        {
            // async로 실행되기를 희망함

            Task<String> task = PineRipplePayment.ExecuteAsync(
                this
                );

            task.Wait();
            String msg = task.Result;
            Message = msg;
        }

        public static async Task<String> ExecuteAsync(PineRipplePayment payment)
        {
            var url = new Uri("http://s2.dotorie.com:8080/transfer");

            //https://stackoverflow.com/questions/4015324/how-to-make-http-post-web-request 참고
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var values = new Dictionary<string, string>
                {
                    { "from", payment.Sender.Wallet.Address },
                    { "secret", payment.Sender.Wallet.Secret},
                    { "to", payment.Receiver.Wallet.Address},
                    { "amount", payment.Amount.ToString() }
                };


            String responseString = "";
            var content = new FormUrlEncodedContent(values);

            try
            {
                var response = client.PostAsync(url, content).Result;

                responseString = await response.Content.ReadAsStringAsync();
            }

            catch (Exception ee)
            {
                Console.Write(ee);
            }

            return responseString;
        }
    }
}
