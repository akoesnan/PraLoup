using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Interfaces;

namespace PraLoup.Services.Test
{
    [TestClass]
    public class AccountServiceTest : ServiceTestBase
    {
        static AccountService AccountService = new AccountService(Repository);

        [TestMethod]
        public void Save_ValidAccount_NotExist_Success()
        {  
            var a = CreateTestAccount("tasave");
            a = AccountService.Save(a);
            Assert.AreNotEqual(0, a.Id);
            var na = AccountService.Find(a);
            Assert.IsNotNull(na);
            Assert.AreEqual(a, na);
        }

        [TestMethod]
        public void Save_ValidAccount_Update_Success()
        {
            const string updatedEmail = "update@example.com";
            const string updatedFbId = "1231231234";
            
            var a = CreateTestAccount("taupdate");
            a = AccountService.Save(a);
            a.Email = updatedEmail;
            a.FacebookId = updatedFbId;
            a = AccountService.Save(a);
            var na = AccountService.Find(a);
            Assert.AreEqual(updatedEmail, na.Email, "email should be updated to {0}", updatedEmail);
            Assert.AreEqual(updatedFbId, na.FacebookId, "fb id should be updated to {0}", updatedFbId);            
            Assert.AreEqual(a, na);            
        }

        [TestMethod]
        public void Delete_ExistAccount_Success() {
            var a = CreateTestAccount("tadelete");
            var id = a.Id;
            var deletedAccount = new Account() { Id = id };
            AccountService.Delete(deletedAccount);
            Assert.IsNull(AccountService.Find(deletedAccount));
        }        
    }
}
