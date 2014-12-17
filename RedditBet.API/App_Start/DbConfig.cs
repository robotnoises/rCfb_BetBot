using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditBet.API.App_Start
{
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.SqlServer;
    using System.Data.SqlClient;

    public class DbConfig : DbConfiguration
    {
        private bool _isDebug { get { return ConfigurationManager.AppSettings["mode"] == "debug"; } }

        public DbConfig()
        {
            if (_isDebug)
            {
                SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0"));
                SetProviderFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            }
            else
            {
                SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0"));
                SetProviderFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            }
        }
    }
}
