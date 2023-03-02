using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingPlatformMJS.Models
{
    internal class LoanApplication
    {
        public string Id = Guid.NewGuid().ToString();
        public decimal LoanAmount { get; set; }
        public decimal AssetValue { get; set; }
        public int CreditScore { get; set; }

        public bool LoanApproved { get; set; }

        public string RejectionReason { get; set; } = string.Empty;

        public Decimal LTV()

        {
            if ((LoanAmount >= 0 ) && (AssetValue >= 0 )) 
            { 
            return (LoanAmount / AssetValue) * 100;
            }
            return 0;

        }
            }
}
