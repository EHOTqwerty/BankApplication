using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    class DepositAccount : Account
    {
        public DepositAccount(decimal _sum, int _percentage) : base(_sum, _percentage)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs("A new deposit account has been opened. Id account: " + Id, CurrentSum));
        }

        public override void Put(decimal _sum)
        {
            if (days % 30 == 0)
                base.Put(_sum);
            else
                base.OnAdded(new AccountEventArgs("It can be deposited only after a 30-day period", 0));
        }

        public override decimal Withdraw(decimal _sum)
        {
            if (days % 30 == 0)
                return base.Withdraw(_sum);
            else
                base.OnWithdrawed(new AccountEventArgs("You can withdraw funds only after a 30 - day period", 0));
            return 0;
        }
    }
}
