using System.Linq;
using AspNet.Identity.LiteDB;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    public class IndexChecksTests : UserIntegrationTestsBase
    {
        [Test]
        public void EnsureUniqueIndexOnUserName_NoIndexOnUserName_AddsUniqueIndexOnUserName()
        {
            var userCollectionName = "userindextest";
            Database.DropCollection(userCollectionName);
            var userCollection = Database.GetCollection<IdentityUser>(userCollectionName);

            IndexChecks.EnsureUniqueIndexOnUserName(userCollection);

            var users = Database.GetCollection(userCollectionName);
            var index = users.GetIndexes()
                .Where(i => i.Options.Unique)
                .FirstOrDefault(i => i.Field.Contains("UserName"));
            Assert.IsNotNull(index);
        }

        [Test]
        public void EnsureEmailUniqueIndex_NoIndexOnEmail_AddsUniqueIndexOnEmail()
        {
            var userCollectionName = "userindextest";
            Database.DropCollection(userCollectionName);
            var usersNewApi = Database.GetCollection<IdentityUser>(userCollectionName);

            IndexChecks.EnsureUniqueIndexOnEmail(usersNewApi);

            var users = Database.GetCollection(userCollectionName);
            var index = users.GetIndexes()
                .Where(i => i.Options.Unique)
                .FirstOrDefault(i => i.Field.Contains("Email"));
            Assert.IsNotNull(index);
        }

        [Test]
        public void EnsureUniqueIndexOnRoleName_NoIndexOnRoleName_AddsUniqueIndexOnRoleName()
        {
            var roleCollectionName = "roleindextest";
            Database.DropCollection(roleCollectionName);
            var rolesNewApi = Database.GetCollection<IdentityRole>(roleCollectionName);

            IndexChecks.EnsureUniqueIndexOnRoleName(rolesNewApi);

            var roles = Database.GetCollection(roleCollectionName);
            var index = roles.GetIndexes()
                .Where(i => i.Options.Unique)
                .FirstOrDefault(i => i.Field.Contains("Name"));
            Assert.IsNotNull(index);
        }
    }
}