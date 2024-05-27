using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AccountDAO
    {
        public static SystemAccount GetAccountByEmail(string accountEmail)
        {
            using var db = new FunewsManagementDbContext();
            var test = db.SystemAccounts.FirstOrDefault(c => c.AccountEmail.Equals(accountEmail));
            return test;
        }

        public static SystemAccount GetAccountById(short id)
        {
            using var db = new FunewsManagementDbContext();
            var account = db.SystemAccounts.AsNoTracking().FirstOrDefault(c => c.AccountId == id);
            return account;
        }

        public static List<SystemAccount> GetSystemAccounts()
        {
            var listAccounts = new List<SystemAccount>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listAccounts = context.SystemAccounts.ToList();
                
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listAccounts;
        }

        public static void SaveAccount(SystemAccount account)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.SystemAccounts.Add(account);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateAccount(SystemAccount account)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.Entry<SystemAccount>(account).State
                    = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public static void DeleteAccount(SystemAccount account)
        {
            try
            {
                /*using var context = new FunewsManagementDbContext();
                var ac = context.SystemAccounts.SingleOrDefault(acc => acc.AccountId == account.AccountId);
                context.SystemAccounts.Remove(account);
                context.SaveChanges();*/
                using var context = new FunewsManagementDbContext();
                //var account = context.SystemAccounts.Find(accountId);

                if (account != null)
                {
                    context.SystemAccounts.Remove(account);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Account not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<SystemAccount> GetSystemAccountByName(string name)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                return context.SystemAccounts
                              .Where(c => EF.Functions.Like(c.AccountName, $"%{name}%"))
                              .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<SystemAccount> GetSystemAccountByEmail(string email)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                return context.SystemAccounts
                              .Where(c => EF.Functions.Like(c.AccountEmail, $"%{email}%"))
                              .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<SystemAccount> GetSystemAccountByRole(int roleId)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                return context.SystemAccounts.Where(c => c.AccountRole == roleId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
