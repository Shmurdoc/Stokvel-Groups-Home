using Stokvel_Groups_Home.Models.Tables;

namespace Stokvel_Groups_Home.Models.MainTable
{
    public class MemberDepositAccount
    {
        public GroupMembers GroupMembers { get; set; }
        public MemberInvoice MemberInvoice { get; set; }
        public DepositSystem DepositSystem { get; set; }

    }
}
