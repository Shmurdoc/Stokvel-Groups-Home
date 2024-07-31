namespace Stokvel_Groups_Home.Models.Tables
{
    public class DepositSystem
    {
        public Invoice? Invoice { get; set; }
        public Deposit? Deposit { get; set; }
        public DepositStatus? PaymentStatus { get; set; }
        public DepositMethod? PaymentMethod { get; set; }
        public PreDeposit? Prepayment { get; set; }

    }
}
