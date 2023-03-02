using LendingPlatformMJS.Models;
using System;

namespace LendingPlatform
{ 
    class Program
    {

        public static List<LoanApplication>? loanApplications { get; set; }

        public static bool finished = false;

        public static void Main(string[] args)
        {
            loanApplications = new List<LoanApplication>();

            do
            {
                MainMenu();
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        ApplyForLoan();
                        break;
                    case 2:
                        ShowLoanApplicationResultsSummary();
                        break;
                    case 3:
                        Console.WriteLine("You Chose 3");
                        break;
                    case 4:
                        Console.WriteLine("You Chose 4");
                        break;
                    default:
                        Console.WriteLine("Invalid Seelction");
                        break;
                }
            } while (!finished);
            //Console.ReadLine();
        }


        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Please Enter A Number from 1 To 4 To Choose An Option From The Menu Below");
            Console.WriteLine("1 - Enter Loan Details");
            Console.WriteLine("2 - Total Applicants To date");
            Console.WriteLine("3 - Total Value Of Loans Written To date");
            Console.WriteLine("4 - Mean Average Of Loan To Value Of Applications To Date");
        }


        static void ShowLoanApplicationResultsSummary()
        {
            int totalApplications = loanApplications.Count();
            int succesfullLoans = loanApplications.Count(p => p.LoanApproved);
            int rejectedLoans = loanApplications.Count(p => p.LoanApproved == false);
            //Decimal TotalLoansWritten = loanApplications.Sum(p => p.LoanAmount where p.LoanApproved == true);

            var TotalLoansWritten =
            from la in loanApplications
            group la by la.LoanApproved into loanGroup
            select new
            {
                Team = loanGroup.Key,
                LoanTotal = loanGroup.Sum(x => x.LoanAmount),
            };


            Console.Clear();
            Console.WriteLine($"Application Summary Total: {totalApplications} Successful {succesfullLoans} Rejected {rejectedLoans}");
            Console.ReadLine();
        }

        static void ApplyForLoan()
        {
            Console.Clear();
            Console.WriteLine("Please Details Below Below");
            Console.WriteLine(" ");
            Console.WriteLine("Enter The Loan Amount");
            string? loanAmount = Console.ReadLine();
            Console.WriteLine("Enter The Asset Value");
            string? assetValue = Console.ReadLine();
            Console.WriteLine("Enter The Credit Score");
            string? creditScore = Console.ReadLine();

            decimal _loanAmount = 0;
            decimal _assetValue = 0;
            int _creditScore = 0;

           

            Decimal.TryParse(loanAmount, out _loanAmount);
            Decimal.TryParse(assetValue, out _assetValue);
            int.TryParse(creditScore, out _creditScore);

            LoanApplication loanApplication = new LoanApplication()
            {
                LoanAmount =_loanAmount,
                AssetValue = _assetValue,
                CreditScore = _creditScore
            };

            ValidateLoan(loanApplication);

            loanApplications.Add(loanApplication);

            if (loanApplication.LoanApproved)
            {
                Console.WriteLine($"Loan Application No. {loanApplication.Id} Was Successful");
            }
            else
            {
                Console.WriteLine($"Loan Application No. {loanApplication.Id} Failed, Reason: {loanApplication.RejectionReason}");
            }
            Console.ReadLine();
        }

        public static void ValidateLoan(LoanApplication loanApplication) 
        {

            if (loanApplication.LoanAmount > 1500000 || loanApplication.LoanAmount < 100000)
            {
                loanApplication.LoanApproved = false;
                loanApplication.RejectionReason = "Outside Of Scope";
            }
            else if (
                        (loanApplication.LoanAmount > 1000000) &&
                        ( (loanApplication.LTV() > 60) ||
                            (loanApplication.CreditScore <= 950))
                    )
                {
                    loanApplication.LoanApproved = false;
                    loanApplication.RejectionReason = "Invalid Loan Amount - LTV - Credit Score amounts";
                }
            else if (loanApplication.LoanAmount < 1000000)
                {
                    loanApplication.LoanApproved = true;
                    loanApplication.RejectionReason = "TODO: RAN OUT OF TIME";
                }
            else
            {
                loanApplication.LoanApproved = true;
            }
        }
    }
}
