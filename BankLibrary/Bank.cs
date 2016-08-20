using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class Bank<T> where T : Account
    {
        T[] accounts;

        public string Name { get; private set; }

        public Bank(string name)
        {
            this.Name = name;
        }
        
        //method that creates an instance of Account
        public void Open(AccountType accountType, decimal sum,
            AccountStateHandler addSumHandler, AccountStateHandler withdrawSumHandler,
            AccountStateHandler calculationHandler, AccountStateHandler closeAccountHandler,
            AccountStateHandler openAccountHandler)
        {
            T newAccount = null;

            switch (accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccount(sum, 1) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 40) as T;
                    break;
            }

            if (newAccount == null)
                throw new Exception("En error occured while creating en Account.");
            
            //adding the new account to the accounts-list
            if (accounts == null)
                accounts = new T[] { newAccount };
            else
            {
                T[] tempAccounts = new T[accounts.Length + 1];
                for(int i = 0; i < accounts.Length; i++)
                {
                    tempAccounts[i] = accounts[i];
                }
                tempAccounts[tempAccounts.Length - 1] = newAccount;
                accounts = tempAccounts;
            }

            //setting up account's events handlers
            newAccount.Added += addSumHandler;
            newAccount.Withdrawed += withdrawSumHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Opened += openAccountHandler;
            newAccount.Calculated += calculationHandler;

            newAccount.Open();
        }

        //adding money to the account
        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Account not found.");
            account.Put(sum);
        }

        //withrdrawing from account
        public void Withdraw(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Account not found.");
            account.Withdraw(sum);
        }

        //closing account
        public void Close(int id)
        {
            int index;
            T account = FindAccount(id, out index);
            if (account == null)
                throw new Exception("Account not found");
            account.Close();

            if (accounts.Length <= 1)
                accounts = null;
            else
            {
                //decrease the amount of all accounts, by deleting the closed account
                T[] tempAccounts = new T[accounts.Length - 1];
                for (int i=1,j=0;i<accounts.Length; i++)
                {
                    if (i != index)
                        tempAccounts[j++] = accounts[i];
                }
                accounts = tempAccounts;
            }
        }

        //add percents to the account
        public void CalculatePercentage()
        {
            if (accounts == null) //if an array was not created, getting back from method
                return;
            for (int i = 1; i< accounts.Length; i++)
            {
                T account = accounts[i];
                account.IncrementDays();
                account.Calculate();
            }
        }

        //search account by ID
        public T FindAccount(int id)
        {
            for (int i =0; i< accounts.Length; i++)
            {
                if(accounts[i].Id == id)
                {
                    return accounts[i];
                }
            }
            return null;
        }

        //overrided version of account-searcher
        public T FindAccount(int id, out int index)
        {
            for(int i=1; i< accounts.Length; i++)
            {
                if(accounts[i].Id == id)
                {
                    index = i;
                    return accounts[i];
                }
            }
            index = -1;
            return null;
        }
        
        public enum AccountType
        {
            Ordinary,
            Deposit
        }
    }
}
