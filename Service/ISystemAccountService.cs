using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ISystemAccountService
    {
        void SaveAccount(SystemAccount account);
        void UpdateAccount(SystemAccount account);
        void DeleteAccount(SystemAccount account);
        List<SystemAccount> GetAccounts();
        SystemAccount GetAccountByEmail(string accountEmail);
        SystemAccount GetAccountById(short id);
        List<SystemAccount> GetSystemAccountsByName(string name);
        List<SystemAccount> GetSystemAccountsByEmail(string email);
        List<SystemAccount> GetSystemAccountsByRole(int roleId);
    }
}
