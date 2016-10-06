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
namespace YourNamespace
{
	using System;
	using System.Web.Hosting;
	using AspNet.Identity.LiteDB;
	using LiteDB;
	using Models;

	public class DbContext : IDisposable
	{
		private readonly LiteDatabase _database;

		public DbContext()
		{
			// TODO: path to your database here
			_database = new LiteDatabase(HostingEnvironment.MapPath("/App_Data/Chainline.db"));
		}

		public static DbContext Create()
		{
		    return new DbContext();
		}

		public LiteCollection<ApplicationUser> Users => _database.GetCollection<ApplicationUser>("users");

		public LiteCollection<IdentityRole> Roles => _database.GetCollection<IdentityRole>("roles");

		public void Dispose() { }
	}
}
```
5. In Startup.Auth.cs replace the following line:
    - ```app.CreatePerOwinContext(ApplicationDbContext.Create);```
    - with:
    - ```app.CreatePerOwinContext(DbContext.Create);```
6. In IdentityConfig.cs:
    - Remove the namespace: Microsoft.AspNet.Identity.EntityFramework
    - In the ApplicationUserManager.Create() method, replace the line:
        - ```var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));```
        - with:
        - ```var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<DbContext>().Users));```

... More to come...

## Thanks To ##

Special thanks to [g0t4](https://github.com/g0t4/).  My implementation is based off his MongoDB implementation.
