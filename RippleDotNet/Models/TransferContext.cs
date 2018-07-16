using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RippleDotNet.Models
{
    public class TransferContext : DbContext
    {

        public TransferContext(DbContextOptions<TransferContext> options)
            : base(options)
        {
        }
         
        public DbSet<TransferItem> TransferItems { get; set; }
    }
}
