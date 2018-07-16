using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RippleDotNet.Models
{
    public class TransferItem
    {
        [Key]
        public long Id { get; set; }
        public int UserIdx { get; set; }
        public String FromAddres { get; set; }
        public String FromSecret { get; set; }
        public String ToAddress { get; set; }
        public double Amount { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public TransferItem(int userIdx, String fromAddress, String fromSecret, String toAddress, double amount)
        {
            this.UserIdx = userIdx;
            this.FromAddres = fromAddress;
            this.FromSecret = fromSecret;
            this.ToAddress  = toAddress;
            this.Amount = amount;
            this.CreatedAt = DateTimeOffset.Now;
            this.UpdatedAt = null;
            this.DeletedAt = null;
        }
    }
}
