using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SDMan.Models;

namespace SDMan.Context
{
	public class SDManDbContext : IdentityDbContext<UserModel, IdentityRole<int>, int>
	{
		public SDManDbContext(DbContextOptions<SDManDbContext> options) : base(options)
		{
		}
		public DbSet<CategoryModel> Categories{ get; set; }
		public DbSet<DepartmentModel> Departments{ get; set; }
		public DbSet<IncidentModel> Incidents{ get; set; }
		public DbSet<PriorityModel> Priorities{ get; set; }
		public DbSet<StatusModel> Statuses{ get; set; }
		public DbSet<LogsModel> Logs{ get; set; }
		public override DbSet<UserModel> Users{ get; set; }
	}
	
}