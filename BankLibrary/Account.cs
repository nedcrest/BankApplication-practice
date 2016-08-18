using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public abstract class Account //: IAccount
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

        // call far events
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
    }
}
