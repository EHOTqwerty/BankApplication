using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        protected internal event AccountStateHandler Withdrawed;
        protected internal event AccountStateHandler Added;
        protected internal event AccountStateHandler Opened;
        protected internal event AccountStateHandler Closed;
        protected internal event AccountStateHandler Calculated;

        protected int id;
        static int counter = 0;

        protected decimal sum;
        protected int percentage;
        protected int days = 0;

        public Account(decimal _sum, int _percentage)
        {
            sum = _sum;
            percentage = _percentage;
            id = ++counter;
        }

        public decimal CurrentSum
        {
            get { return sum; }
        }

        public int Percentage
        {
            get { return percentage; }
        }

        public int Id
        {
            get { return id; }
        }

        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (handler != null && e != null)
                handler(this, e);
        }

        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }

        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }

        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }

        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }

        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }

        public virtual void Put(decimal _sum)
        {
            sum += _sum;
            OnAdded(new AccountEventArgs("Into account " + id + " has been added " + sum, sum));
        }

        public virtual decimal Withdraw(decimal _sum)
        {
            decimal result = 0;
            if (sum >= _sum)
            {
                result = _sum;
                sum -= result;
                OnWithdrawed(new AccountEventArgs("Sum " + result + " has been withdrawed from the account " + id, result));
            } else
            {
                OnWithdrawed(new AccountEventArgs("Not enough money in the account " + id, result));
            }
            return result;
        }

        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs("New Account has been opened. Id of the account: " + this.id, this.sum));
        }

        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs("Account " + id + " has been close. Final sum: " + CurrentSum, CurrentSum));
        }

        protected internal void IncrementDays()
        {
            days++;
        }

        protected internal virtual void Calculate()
        {
            decimal increment = sum * percentage / 100;
            sum += increment;
            OnCalculated(new AccountEventArgs("Calculated percentages: " + increment, increment));
        }
    }
}
