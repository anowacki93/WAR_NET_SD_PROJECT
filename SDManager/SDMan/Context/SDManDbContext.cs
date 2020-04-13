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

	}
}