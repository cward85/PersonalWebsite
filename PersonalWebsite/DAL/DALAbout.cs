using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalWebsite.DAL
{
    public class DALAbout
    {
        CWARDEntities _dbContext;

        public DALAbout()
        {
            _dbContext = new CWARDEntities();
        }

        public void LogResumeDownload(resumeDownload resume)
        {
            _dbContext.resumeDownloads.Add(resume);
            _dbContext.SaveChanges();
        }

    }
}