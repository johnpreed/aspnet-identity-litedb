using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNet.Identity;

namespace AspNet.Identity.LiteDB
{
    /// <summary>
    ///     Note: Deleting and updating do not modify the roles stored on a user document. If you desire this dynamic
    ///     capability, override the appropriate operations on RoleStore as desired for your application. For example you could
    ///     perform a document modification on the users collection before a delete or a rename.
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    public class RoleStore<TRole> : IRoleStore<TRole>, IQueryableRoleStore<TRole>
        where TRole : IdentityRole, new()
    {
        private readonly LiteCollection<TRole> _Roles;

        public RoleStore(LiteCollection<TRole> roles)
        {
            _Roles = roles;
        }

        public virtual void Dispose()
        {
            // no need to dispose of anything, mongodb handles connection pooling automatically
        }

        public virtual Task CreateAsync(TRole role)
        {
            return Task.FromResult(_Roles.Insert(role));
        }

        public virtual Task UpdateAsync(TRole role)
        {
            return Task.FromResult(_Roles.Update(role.Id, role));
        }

        public virtual Task DeleteAsync(TRole role)
        {
            return Task.FromResult(_Roles.Delete(role.Id));
        }

        public virtual Task<TRole> FindByIdAsync(string roleId)
        {
            return Task.FromResult(_Roles.FindOne(r => r.Id == roleId));
        }

        public virtual Task<TRole> FindByNameAsync(string roleName)
        {
            return Task.FromResult(_Roles.FindOne(r => r.Name == roleName));
        }

        public virtual IQueryable<TRole> Roles => _Roles.FindAll().AsQueryable();
    }
}