using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Repository
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public void SaveAccount(SystemAccount account) => AccountDAO.SaveAccount(account);
        public void DeleteAccount(SystemAccount account) => AccountDAO.DeleteAccount(account);
        public void UpdateAccount(SystemAccount account) => AccountDAO.UpdateAccount(account);  
        public List<SystemAccount> GetSystemAccounts() => AccountDAO.GetSystemAccounts();
        public SystemAccount GetSystemAccountByEmail(string accountEmail) => AccountDAO.GetAccountByEmail(accountEmail);
        public SystemAccount GetSystemAccountById(short id) => AccountDAO.GetAccountById(id);
        public List<SystemAccount> GetSystemAccountsByName(string name) => AccountDAO.GetSystemAccountByName(name);
        public List<SystemAccount> GetSystemAccountsByEmail(string email) => AccountDAO.GetSystemAccountByEmail(email);
        public List<SystemAccount> GetSystemAccountsByRole(int roleId) => AccountDAO.GetSystemAccountByRole(roleId);
    }
}
