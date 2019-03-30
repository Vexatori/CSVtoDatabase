using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSVtoDatabase.Data;
using CSVtoDatabase.Migrations;

namespace CSVtoDatabase.Context
{
    public class UsersDatabaseContext : DbContext
    {
        static UsersDatabaseContext()
        {
            System.Data.Entity.Database.SetInitializer( new MigrateDatabaseToLatestVersion<UsersDatabaseContext, Configuration>() );
        }

        public UsersDatabaseContext() : base( "name=UsersDatabaseContext" ) { }

        public DbSet<User> Users { get; set; }
    }
}