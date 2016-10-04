using LiteDB;
using Microsoft.AspNet.Identity;

namespace AspNet.Identity.LiteDB
{
    public class IdentityRole : IRole<string>
    {
        public IdentityRole()
        {
            Id = ObjectId.NewObjectId().ToString();
        }

        public IdentityRole(string roleName) : this()
        {
            Name = roleName;
        }

        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}