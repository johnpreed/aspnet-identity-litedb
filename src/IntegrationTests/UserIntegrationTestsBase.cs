using AspNet.Identity.LiteDB;
using Microsoft.AspNet.Identity;
using LiteDB;
using NUnit.Framework;

namespace IntegrationTests
{
    public class UserIntegrationTestsBase : AssertionHelper
    {
        protected LiteDatabase Database;
        protected LiteCollection<IdentityUser> Users;
        protected LiteCollection<IdentityRole> Roles;

        [SetUp]
        public void BeforeEachTest()
        {
            Database = new LiteDatabase("test.db");
            Users = Database.GetCollection<IdentityUser>("users");
            Roles = Database.GetCollection<IdentityRole>("roles");

            Database.DropCollection("users");
            Database.DropCollection("roles");
        }

        protected UserManager<IdentityUser> GetUserManager()
        {
            var store = new UserStore<IdentityUser>(Users);
            return new UserManager<IdentityUser>(store);
        }

        protected RoleManager<IdentityRole> GetRoleManager()
        {
            var store = new RoleStore<IdentityRole>(Roles);
            return new RoleManager<IdentityRole>(store);
        }
    }
}