using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public delegate void AccountStateHandler(object sender, AccountEventsArgs e);

    public class AccountEventsArgs
    {
        // Message
        public string Message { get; private set; }
        // Amount of money that has been changed
        public decimal Sum { get; private set; }

        public AccountEventsArgs(string _mes, decimal _sum)
        {
            Message = _mes;
            Sum = _sum;
        }
    }

}
