using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class DemandAccount : Account
    {
        public DemandAccount(decimal sum, int percentage) : base(sum, percentage)
        {

        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventsArgs("New Demand Account is opened. Account ID: " + this._id, this._sum));
        }
    }
}
