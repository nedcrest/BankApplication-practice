using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        // event when money withdrawed
        protected internal event AccountStateHandler Withdrawed;
        // event when money are added to account
        protected internal event AccountStateHandler Added;
        // event when open a new account
        protected internal event AccountStateHandler Opened;
        // event when close an account
        protected internal event AccountStateHandler Closed;
        // event when % are added
        protected internal event AccountStateHandler Calculated;

        protected int _id;
        static int counter = 0;

        protected decimal _sum; // variable for temporary storing the sum of account's money
        protected int _percentage; // variable for temporary storing the %

        protected int _days = 0; // amount of days since account is opened

        public Account(decimal sum, int percentage)
        {
            _sum = sum;
            _percentage = percentage;
            _id = ++counter;
        }

        // current sum on account
        public decimal CurrentSum
        {
            get { return _sum; }
        }

        public int Percentage
        {
            get { return _percentage; }
        }

        public int Id
        {
            get { return _id; }
        }

        // call for events
        private void CallEvent(AccountEventsArgs e, AccountStateHandler handler)
        {
            if (handler == null && e == null)
            {
                handler(this, e);
            }
        }

        // call for each particular method
        // declare the new virtual method for each of them
        protected virtual void OnOpened(AccountEventsArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnWithdrawed(AccountEventsArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        protected virtual void OnAdded(AccountEventsArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnClosed(AccountEventsArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculated(AccountEventsArgs e)
        {
            CallEvent(e, Calculated);
        }

        public virtual void Put(decimal sum)
        {
            _sum += sum;
            OnAdded(new AccountEventsArgs("You received " + sum + " on your account", sum));
        }

        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if(sum <= _sum)
            {
                _sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventsArgs("Sum of " + sum + " withdrawed from account ID: " + _id, sum));
            }
            else
            {
                OnWithdrawed(new AccountEventsArgs("Not enough money on your account ID: " + _id, sum));
            }
            return result;
        }

        //open new account
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventsArgs("New debet account created! Account ID: " + this._id, this._sum));
        }
        
        //close account
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventsArgs("Account ID " + _id + " was closed. Total sum.: " + CurrentSum, CurrentSum));
        }

        protected internal void IncrementDays()
        {
            _days++;
        }

        //adding percents to your sum
        protected internal virtual void Calculate()
        {
            decimal increment = _sum * _percentage / 100;
            _sum = _sum + increment;
            OnCalculated(new AccountEventsArgs("Percents were added: " + increment, increment));
        }
    }
}
