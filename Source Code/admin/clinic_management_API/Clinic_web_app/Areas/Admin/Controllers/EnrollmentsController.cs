using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_web_app.Models;
using Microsoft.AspNetCore.Http;

namespace Clinic_web_app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EnrollmentsController : Controller
    {
        private readonly ClinicDBContext _context;

        public EnrollmentsController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: Admin/Enrollments
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var clinicDBContext = _context.Enrollments.Include(e => e.Account).Include(e => e.Course);
            return View(await clinicDBContext.ToListAsync());
        }

        // GET: Admin/Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Account)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Admin/Enrollments/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewData["AccountId"] = new SelectList(_context.StaffAccounts, "AccountId", "Username");
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: Admin/Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.StaffAccounts, "AccountId", "Username", enrollment.AccountId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", enrollment.CourseId);
            return View(enrollment);
        }

        // GET: Admin/Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.StaffAccounts, "AccountId", "Username", enrollment.AccountId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", enrollment.CourseId);
            return View(enrollment);
        }

        // POST: Admin/Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.StaffAccounts, "AccountId", "Username", enrollment.AccountId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", enrollment.CourseId);
            return View(enrollment);
        }

        // GET: Admin/Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Account)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
     
        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentId == id);
        }
    }
}
