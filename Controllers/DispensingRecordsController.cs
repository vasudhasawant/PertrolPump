using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetrolPumpLog.Data;
using PetrolPumpLog.Models;
using System;
using System.IO;
using System.Linq;

namespace PetrolPumpLog.Controllers
{
    [Authorize]
    public class DispensingRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DispensingRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /DispensingRecords
        public IActionResult Index(string dispenserNo, string paymentMode, DateTime? startDate, DateTime? endDate)
        {
            var records = _context.DispensingRecords.AsQueryable();

            if (!string.IsNullOrEmpty(dispenserNo))
                records = records.Where(r => r.DispenserNo == dispenserNo);

            if (!string.IsNullOrEmpty(paymentMode))
                records = records.Where(r => r.PaymentMode == paymentMode);

            if (startDate.HasValue)
                records = records.Where(r => r.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                records = records.Where(r => r.CreatedAt <= endDate.Value);

            return View(records.ToList());
        }

        // ✅ GET: /DispensingRecords/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ✅ POST: /DispensingRecords/Create
        [HttpPost]
        public IActionResult Create(DispensingRecord record, IFormFile PaymentProof)
        {
            if (PaymentProof != null && PaymentProof.Length > 0)
            {
                var fileName = Path.GetFileName(PaymentProof.FileName);
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                var filePath = Path.Combine(uploadsDir, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    PaymentProof.CopyTo(stream);
                }

                record.PaymentProofFileName = "/uploads/" + fileName;
            }

            record.CreatedAt = DateTime.Now;
            _context.DispensingRecords.Add(record);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
