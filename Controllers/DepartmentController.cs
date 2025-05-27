using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;
using System;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly EmployeeManagementContext _context;

        public DepartmentController(EmployeeManagementContext context)
        {
            _context = context;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Department added successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return NotFound();

            return View(department);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,Name")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Department updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Departments.Any(e => e.DepartmentId == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Database concurrency error occurred while updating department. Please try again.");
                        TempData["ErrorMessage"] = "Database concurrency error occurred while updating department.";
                    }
                }
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Department deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EmployeeCount(int id)
        {
            int count = 0;
            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "GetEmployeeCountByDepartment";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var param = cmd.CreateParameter();
                param.ParameterName = "@DepartmentId";
                param.Value = id;
                cmd.Parameters.Add(param);
                var result = await cmd.ExecuteScalarAsync();
                count = (int)result;
            }
            await conn.CloseAsync();
            ViewBag.Count = count;
            ViewBag.Department = await _context.Departments.FindAsync(id);
            ViewData["Title"] = $"{ViewBag.Department?.Name} - Employee Count";
            return View();
        }
    }
} 