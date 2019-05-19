using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    class DemandAccount : Account
    {
        public DemandAccount(decimal _sum, int _percentage) : base(_sum, _percentage)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs("A new demand account has been opened. Id account: " + this.id, this.sum));
        }
    }
}
