using Empform.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Empform.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Empform.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly EmpDbContext empDbContext;
		public HomeController(ILogger<HomeController> logger, EmpDbContext empDbContext)
		{
			_logger = logger;
			this.empDbContext = empDbContext;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(Employee employee)
		{
			var emp = new Employee
			{
				EmpName = employee.EmpName,
				EmpEmail = employee.EmpEmail,

			};

			empDbContext.Add(emp);
			empDbContext.SaveChanges();
			return RedirectToAction("List");

		}

		[HttpGet]
		public IActionResult List()
		{
			var EmpList = empDbContext.Employees.ToList();

			return View(EmpList);
		}

		[HttpGet]
		public IActionResult Edit(Guid id)
		{
			var emp = empDbContext.Employees.Where(x => x.EmpID == id).ToList();
			if (emp == null)
			{
				var editemp = id;
				return View(editemp);
			}

			return View(null);

		}

		[HttpPost]

		public IActionResult Edit(Employee employee)
		{
			var newemp = new Employee
			{ 
				EmpID = employee.EmpID,
				EmpName = employee.EmpName,
				EmpEmail = employee.EmpEmail

			};

			var existingID = empDbContext.Find(typeof(Employee),employee.EmpID);

			if (existingID != null)
			{

				empDbContext.Entry(existingID).CurrentValues.SetValues(newemp);

				empDbContext.SaveChanges();
				return RedirectToAction("List");
			}

			return View("Edit", new {id  = employee.EmpID});
		}

		[HttpGet]
		public IActionResult Delete(Guid id) {
			var emp = empDbContext.Employees.Where(x => x.EmpID == id).ToList();
			if (emp == null)
			{
				var editemp = id;
				return View(editemp);
			}

			return View(null);
		}


		[HttpPost]

		public IActionResult Delete(Employee employee)
		{

			var existingID = empDbContext.Find(typeof(Employee), employee.EmpID);

			if (existingID != null)
			{

				empDbContext.Remove(existingID);

				empDbContext.SaveChanges();
				return RedirectToAction("List");
			}

			return View("Edit", new { id = employee.EmpID });
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}