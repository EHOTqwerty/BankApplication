using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public interface IAccount
    {
        void Put(decimal sum);
        decimal Withdraw(decimal sum);
    }
}
