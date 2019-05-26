using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Issue.Function
{
	//Microsoft.AspNetCore.Identity.EntityFrameworkCore //this is what we have actually
	//Microsoft.AspNet.Identity.EntityFramework
	public class AccountManagementDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<ApplicationUser>
	{
		internal DbSet<Entity1> Companies { get; set; }

		internal DbSet<Entity2> CompanyUsers { get; set; }

		public AccountManagementDbContext(DbContextOptions<AccountManagementDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder) =>
			base.OnModelCreating(builder);
	}

	internal class Entity2
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
	{
	}

	[Table("Entity1")]
	internal class Entity1
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
