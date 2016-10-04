using AspNet.Identity.LiteDB;
using LiteDB;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class IdentityUserTests : AssertionHelper
    {
        [Test]
        public void Create_NewIdentityUser_HasIdAssigned()
        {
            var user = new IdentityUser();

            var parsed = user.Id.SafeParseObjectId();
            Expect(parsed, Is.Not.Null);
            Expect(parsed, Is.Not.EqualTo(ObjectId.Empty));
        }

        [Test]
        public void Create_NewIdentityUser_RolesNotNull()
        {
            var user = new IdentityUser();

            Expect(user.Roles, Is.Not.Null);
        }

        [Test]
        public void Create_NewIdentityUser_LoginsNotNull()
        {
            var user = new IdentityUser();

            Expect(user.Logins, Is.Not.Null);
        }

        [Test]
        public void Create_NewIdentityUser_ClaimsNotNull()
        {
            var user = new IdentityUser();

            Expect(user.Claims, Is.Not.Null);
        }
    }
}