AspNet.Identity.LiteDB 
=======================

This library is an ASP.NET Identity provider that uses LiteDB storage implementation.  I based this code on this MongoDB AspNet Identity provider [MongoDB.AspNet.Identity](https://github.com/g0t4/aspnet-identity-mongo).

## Instructions ##
1. Create a new ASP.NET MVC 5 project, choosing the Individual User Accounts authentication type.
2. Remove the Entity Framework packages and replace with AspNet:
```
Uninstall-Package Microsoft.AspNet.Identity.EntityFramework
Uninstall-Package EntityFramework
Install-Package AspNet.Identity.LiteDB
```
3. In ~/Models/IdentityModels.cs:
    - Remove the namespace: Microsoft.AspNet.Identity.EntityFramework
    - Add the namespace: AspNet.Identity.LiteDB
    - Remove the entire ApplicationDbContext class. You won't need that at all.
4. Add the following class to the App_Start folder

```
namespace IdentitySample
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using AspNet.Identity.LiteDB;
	using LiteDB;
	using Models;

	public class ApplicationIdentityContext : IDisposable
	{
		public static ApplicationIdentityContext Create()
		{
			// todo add settings where appropriate to switch server & database in your own application
			var database = new LiteDatabase("YourDatabase.db");
			var users = database.GetCollection<ApplicationUser>("users");
			var roles = database.GetCollection<IdentityRole>("roles");
			return new ApplicationIdentityContext(users, roles);
		}

		private ApplicationIdentityContext(LiteCollection<ApplicationUser> users, LiteCollection<IdentityRole> roles)
		{
			Users = users;
			Roles = roles;
		}

		public LiteCollection<IdentityRole> Roles { get; set; }

		public LiteCollection<ApplicationUser> Users { get; set; }

		public Task<List<IdentityRole>> AllRolesAsync()
		{
			return Task.FromResult(Roles.FindAll().ToList());
		}

		public void Dispose()
		{
		}
	}
}
```
5. In Startup.Auth.cs replace the following line:
    - ```app.CreatePerOwinContext(ApplicationDbContext.Create);```
    - with:
    - ```app.CreatePerOwinContext(ApplicationIdentityContext.Create);```
6. In IdentityConfig.cs:
    - Remove the namespace: Microsoft.AspNet.Identity.EntityFramework
    - In the ApplicationUserManager.Create() method, replace the line:
        - ```var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));```
        - with:
        - ```var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationIdentityContext>().Users));```

... More to come...

## Thanks To ##

Special thanks to [g0t4](https://github.com/g0t4/).  My implementation is based off his MongoDB implementation.
