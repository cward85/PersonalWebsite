using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace PersonalWebsite.DAL
{
    public class DALLogin
    {
        CWARDEntities _dbContext;

        public DALLogin()
        {
            _dbContext = new CWARDEntities();
        }

        public author Login(string userName, string password, string ipAddress)
        {
            var user = _dbContext.authors.Where(x => x.userName == userName).FirstOrDefault();
            loginLog loginAttempt = new loginLog();

            loginAttempt.loginDate = DateTime.Now;
            loginAttempt.userName = userName;
            loginAttempt.ipAddress = ipAddress;
            loginAttempt.loginSuccess = false;

            if (user != null)
            {
                bool isMatch = BCrypt.Net.BCrypt.Verify(password, user.password);

                if (isMatch)
                {
                    user.lastLoginDateTime = DateTime.Now;
                    _dbContext.SaveChanges();
                    loginAttempt.loginSuccess = true;

                    _dbContext.loginLogs.Add(loginAttempt);
                    _dbContext.SaveChanges();
                    author metaData = new author();

                    metaData.id = user.id;
                    metaData.name = user.name;
                    metaData.userName = userName;
                    metaData.isAdmin = user.isAdmin;

                    return metaData;
                }
                else
                {
                    _dbContext.loginLogs.Add(loginAttempt);
                    _dbContext.SaveChanges();

                    return new author();
                }
            }

            _dbContext.loginLogs.Add(loginAttempt);
            _dbContext.SaveChanges();
            return new author();
        }
    }
}