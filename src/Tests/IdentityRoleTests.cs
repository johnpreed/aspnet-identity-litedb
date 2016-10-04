using AspNet.Identity.LiteDB;
using LiteDB;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class IdentityRoleTests : AssertionHelper
    {
        [Test]
        public void Create_WithoutRoleName_HasIdAssigned()
        {
            var role = new IdentityRole();

            var parsed = role.Id.SafeParseObjectId();
            Expect(parsed, Is.Not.Null);
            Expect(parsed, Is.Not.EqualTo(ObjectId.Empty));
        }

        [Test]
        public void Create_WithRoleName_SetsName()
        {
            var name = "admin";

            var role = new IdentityRole(name);

            Expect(role.Name, Is.EqualTo(name));
        }

        [Test]
        public void Create_WithRoleName_SetsId()
        {
            var role = new IdentityRole("admin");

            var parsed = role.Id.SafeParseObjectId();
            Expect(parsed, Is.Not.Null);
            Expect(parsed, Is.Not.EqualTo(ObjectId.Empty));
        }
    }
}