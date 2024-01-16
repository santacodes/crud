using Empform.Models;
using Microsoft.EntityFrameworkCore;

namespace Empform.Data
{
	public class EmpDbContext : DbContext
	{
		public EmpDbContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<Employee> Employees { get; set; } 

	}
}
