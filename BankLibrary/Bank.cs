using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class Bank<T> where T : Account
    {
        T[] accounts;

        public string name { get; private set; }

        public Bank(string _name)
        {
            name = _name;
        }

        public void Open(AccountType accountType, decimal _sum, AccountStateHandler withdrawHandler, AccountStateHandler addHadler,
            AccountStateHandler calculationHandler, AccountStateHandler closeHandler, AccountStateHandler openHandler)
        {
            T newAccount = null;

            switch (accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccount(_sum, 1) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(_sum, 40) as T;
                    break;
            }

            if (newAccount == null)
                throw new Exception("Account creation error");

            if (accounts == null)
                accounts = new T[] { newAccount };
            else
            {
                T[] tempAccounts = new T[accounts.Length + 1];
                int i = 0;
                foreach (T x in accounts)
                {
                    tempAccounts[i] = x;
                    i++;
                }
                tempAccounts[tempAccounts.Length - 1] = newAccount;
                accounts = tempAccounts;
            }

            newAccount.Withdrawed += withdrawHandler;
            newAccount.Added += addHadler;
            newAccount.Opened += openHandler;
            newAccount.Closed += closeHandler;
            newAccount.Calculated += calculationHandler;

            newAccount.Open();
        }

        public void Put(decimal _sum, int _id)
        {
            T account = FindAccount(_id);
            if (account == null)
                throw new Exception("Account not found");
            else
                account.Put(_sum);
        }

        public void Withdraw(decimal _sum, int _id)
        {
            T account = FindAccount(_id);
            if (account == null)
                throw new Exception("Account not found");
            else
                account.Withdraw(_sum);
        }

        public void Close(int _id)
        {
            int index;
            T account = FindAccount(_id, out index);

            if (account == null)
                throw new Exception("Account not found");

            if (accounts.Length == 1)
                accounts = null;
            else
            {
                T[] tempAccounts = new T[accounts.Length - 1];
                for (int i = 0, j = 0; i < accounts.Length; i++)
                {
                    if (i != index)
                        tempAccounts[j++] = accounts[i];
                }
            }

            account.Close();
        }

        public void CalculatePercentage()
        {
            if (accounts != null)
                foreach(T a in accounts)
                {
                    a.IncrementDays();
                    a.Calculate();
                }
        }

        public T FindAccount(int _id)
        {
            foreach (T a in accounts)
                if (a.Id == _id)
                    return a;
            return null;
        }

        public T FindAccount(int _id, out int _index)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Id == _id)
                {
                    _index = i;
                    return accounts[i];
                }
            }
            _index = -1;
            return null;
        }

    }
    public enum AccountType
    {
        Ordinary,
        Deposit
    }

}