using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Interfaces;

namespace PraLoup.Services
{
    public class AccountService
    {
        IRepository Repository { get; set; }

        public AccountService(IRepository gr)
        {
            this.Repository = gr;
        }

        public Account Save(Account account)
        {
            if (account.Id == 0)
            {
                this.Repository.Add(account);
            }
            else
            {
                Account a = this.Repository.Find<Account>(account.Id);
                if (a == null)
                {
                    this.Repository.Add(account);
                }
                else
                {
                    a = account;
                }
            }
            this.Repository.SaveChanges();
            // return the updated account;
            Account acct = this.Repository.Find<Account>(account.Id);
            return acct;
        }

        public Account Find(Account account)
        {
            // find by account id
            if (account.Id != 0)
            {
                return FindById(account.Id);
            }
            // 
            else if (!string.IsNullOrEmpty(account.UserName))
            {
                return FindByUserName(account.UserName);
            }
            // TODO: do we want to find by other method?
            else
            {
                return null;
            }
        }

        public bool Delete(Account acct) 
        {
            var a = this.FindById(acct.Id);
            if(a != null)
            {
                this.Repository.Delete(a);
                this.Repository.SaveChanges();
                return true;
            }
            return false;
        }

        private Account FindById(int id)
        {
            return this.Repository.Find<Account>(id);
        }

        private Account FindByUserName(string userName)
        {
            return this.Repository.FirstOrDefault<Account>(a => String.Equals(userName, a.UserName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
