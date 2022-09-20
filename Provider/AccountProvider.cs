using Account_Microservice.Models;
using Account_Microservice.Models.ViewModel;
using Account_Microservice.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account_Microservice.Provider
{
    public class AccountProvider : IAccountProvider
    {

        readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AccountProvider));
        IAccountRepository _Repository;
        public AccountProvider(IAccountRepository Repository)
        {
            _Repository = Repository;
        }
        // this method takes customerid and accounttype as argument and calls the repo's method for creating account
       
        public AccountCreationStatus AddAccount(int CustomerId, string AccountType)
        {
            AccountCreationStatus acs = new AccountCreationStatus();
           
                acs = _Repository.AddAccount(CustomerId, AccountType);
            return acs;
        }


        // this method takes argument accountid and amount and calls repo's method for depositing
       
        public TransactionStatus depositAccount(int AccountId, int amount)
        {
           TransactionStatus ts = new TransactionStatus();
            try
            {
                ts = _Repository.depositAccount(AccountId, amount);
               
            }
            
              catch (Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
            return ts;
        }


        // this method takes customerid as argument and calls repo's for getting account
        
        public List<AccountViewModel> getAllAccounts(int CustomerId)
        {
            List<Account> Listaccount = new List<Account>();
            List<AccountViewModel> accountViews = new List<AccountViewModel>();
            try
            {
                Listaccount = _Repository.getAllAccounts(CustomerId).ToList();
               
                AccountViewModel model;
               
                    foreach (Account acc in Listaccount)
                    {
                        model = new AccountViewModel();
                        model.Id = acc.AccountId;
                        model.Balance = acc.Balance;
                        accountViews.Add(model);
                    }
                
            }
            catch(Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
           
            return accountViews;
        }


        // this method takes and accountid for account and calls repo's method fot getting account
       
        public Account getCustomerAccount(int AccountId)
        {
            Account a = new Account();
            try
            {
                a = _Repository.getAccount(AccountId);
                
            }
            catch(Exception e)
            {
                throw e;
            }
           
            return a;
        }

        // this method takes accountid and from date and to date and calls the repo's method
        
        public IEnumerable<Statement> getStatement(int AccountId, DateTime from_date, DateTime to_date)
        {
            List<Statement> statements = new List<Statement>();
            try
            {
                statements = _Repository.getStatement(AccountId, from_date, to_date).ToList();
            
            }
            catch(Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
            
            return statements;
        }

       
        public TransactionStatus withdrawAccount(int AccountId, int amount)
        {
            TransactionStatus ts = new TransactionStatus();
            try
            {
                ts = _Repository.withdrawAccount(AccountId, amount);
            }
           catch(Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
           
            return ts;
        }


       
        public IEnumerable<Account> getCustomersAllAccounts()
        {
            List<Account> accounts = new List<Account>();
            accounts = _Repository.getCustomersAllAccounts().ToList();
            try
            {
                if (accounts.Count == 0)
                {
                    throw new System.ArgumentNullException("nothing in the list");
                }
            }
            catch(Exception e)
            {
                _log4net.Error(e.Message);
                throw e;
            }
           
            return accounts;
        }
    }
}
