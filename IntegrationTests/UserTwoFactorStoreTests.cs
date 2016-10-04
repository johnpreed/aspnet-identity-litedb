using AspNet.Identity.LiteDB;
using Microsoft.AspNet.Identity;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    public class UserTwoFactorStoreTests : UserIntegrationTestsBase
    {
        [Test]
        public void SetTwoFactorEnabled()
        {
            var user = new IdentityUser { UserName = "bob" };
            var manager = GetUserManager();
            manager.Create(user);

            manager.SetTwoFactorEnabled(user.Id, true);

            Expect(manager.GetTwoFactorEnabled(user.Id));
        }

        [Test]
        public void ClearTwoFactorEnabled_PreviouslyEnabled_NotEnabled()
        {
            var user = new IdentityUser { UserName = "bob" };
            var manager = GetUserManager();
            manager.Create(user);
            manager.SetTwoFactorEnabled(user.Id, true);

            manager.SetTwoFactorEnabled(user.Id, false);

            Expect(manager.GetTwoFactorEnabled(user.Id), Is.False);
        }
    }
}