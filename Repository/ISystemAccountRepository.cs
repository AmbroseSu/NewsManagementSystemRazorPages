using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessObject;

namespace Repository
{
    public interface ISystemAccountRepository
    {
        List<SystemAccount> GetSystemAccounts();
        SystemAccount GetSystemAccountByEmail(string accountEmail);
        SystemAccount GetSystemAccountById(short id);
        void SaveAccount(SystemAccount account);
        void UpdateAccount(SystemAccount account);
        void DeleteAccount(SystemAccount account);
        List<SystemAccount> GetSystemAccountsByName(string name);
        List<SystemAccount> GetSystemAccountsByEmail(string email);
        List<SystemAccount> GetSystemAccountsByRole(int roldId);
    }
}
