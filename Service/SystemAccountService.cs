using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository iSystemAccountRepository;

        public SystemAccountService()
        {
            iSystemAccountRepository = new SystemAccountRepository();
        }

        public void SaveAccount(SystemAccount account)
        {
            iSystemAccountRepository.SaveAccount(account);
        }
        public void DeleteAccount(SystemAccount account)
        {
            iSystemAccountRepository.DeleteAccount(account);
        }
        public void UpdateAccount(SystemAccount account)
        {
            iSystemAccountRepository.UpdateAccount(account);
        }

        public List<SystemAccount> GetAccounts()
        {
            return iSystemAccountRepository.GetSystemAccounts();
        }

        public SystemAccount GetAccountByEmail(string accountEmail)
        {
            return iSystemAccountRepository.GetSystemAccountByEmail(accountEmail);
        }

        public SystemAccount GetAccountById(short id)
        {
            return iSystemAccountRepository.GetSystemAccountById(id);
        }

        public List<SystemAccount> GetSystemAccountsByName(string name)
        {
            return iSystemAccountRepository.GetSystemAccountsByName(name);
        }

        public List<SystemAccount> GetSystemAccountsByEmail(string email)
        {
            return iSystemAccountRepository.GetSystemAccountsByEmail(email);
        }

        public List<SystemAccount> GetSystemAccountsByRole(int roleId)
        {
            return iSystemAccountRepository.GetSystemAccountsByRole(roleId);
        }

    }
}
