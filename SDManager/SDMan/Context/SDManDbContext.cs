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
		public DbSet<GroupModel> Groups{ get; set; }
		public DbSet<IncidentModel> Incidents{ get; set; }
		public DbSet<PriorityModel> Priorities{ get; set; }
		public DbSet<StatusModel> Statuses{ get; set; }
		public override DbSet<UserModel> Users{ get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<IdentityRole<int>>().HasData(
        //        new IdentityRole<int> { Id = 2, Name = "Administrator", NormalizedName = "Administrator".ToUpper() }
        //        );
        //    builder.Entity<UserModel>().HasData(
        //        new UserModel { Id = 2, UserName = "Administrator", RoleId = 1 });


        //}
    }
	
}