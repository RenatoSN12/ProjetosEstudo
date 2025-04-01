using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Stripe
{
    public class GetTransactionsByOrderNumberRequest : Request
    {
        public string Number { get; set; } = string.Empty;
        public string ProductTitle { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public long OrderTotal { get; set; }
    }
}
