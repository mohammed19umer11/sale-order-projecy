using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaleOrderMVC.Context;
using SaleOrderMVC.Models;

namespace SaleOrderMVC.Controllers
{
    public class SaleOrdersController : Controller
    {
        private readonly SaleOrdersContext _context;

        public SaleOrdersController(SaleOrdersContext context)
        {
            _context = context;
        }

        // GET: SaleOrders
        public async Task<IActionResult> Index()
        {
            var saleOrdersContext = await _context.SaleOrders
                .Include(s => s.Customer)
                .Include(s => s.Employee)
                .Include(s => s.Orderstatus)
                .Include(s => s.SaleOrderDetails).ThenInclude(sd => sd.Item)
                .ToListAsync();

            List<SaleOrderVM> saleOrders = new List<SaleOrderVM>();
            foreach (var saleOrder in saleOrdersContext)
            {
                saleOrders.Add(new SaleOrderVM
                {
                    SaleorderId = saleOrder.SaleorderId,
                    CustomerId = saleOrder.CustomerId,
                    EmployeeId = saleOrder.EmployeeId,
                    OrderstatusId = saleOrder.OrderstatusId,
                    ItemId = saleOrder.SaleOrderDetails.FirstOrDefault().ItemId,
                    ItemQty = saleOrder.SaleOrderDetails.FirstOrDefault().ItemQty,
                    Customer = saleOrder.Customer,
                    Employee = saleOrder.Employee,
                    Orderstatus = saleOrder.Orderstatus,
                    Item = saleOrder.SaleOrderDetails.FirstOrDefault().Item,
                    Createdate = saleOrder.Createdate
                });
            }

            return View(saleOrders);
        }

        // GET: SaleOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SaleOrders == null)
            {
                return NotFound();
            }

            var saleOrder = await _context.SaleOrders
                .Include(s => s.Customer)
                .Include(s => s.Employee)
                .Include(s => s.Orderstatus)
                .FirstOrDefaultAsync(m => m.SaleorderId == id);
            if (saleOrder == null)
            {
                return NotFound();
            }

            return View(saleOrder);
        }

        // GET: SaleOrders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstname");
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName");
            ViewData["OrderstatusId"] = new SelectList(_context.OrderStatuses, "OrderstatusId", "OrderstatusName");

            return View();
        }

        // POST: SaleOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleorderId,CustomerId,EmployeeId,ItemId,ItemQty,OrderstatusId,Createdate")] SaleOrderVM saleOrderVM)
        {
            if (ModelState.IsValid || true)
            {
                var saleOrder = new SaleOrder
                {
                    EmployeeId = saleOrderVM.EmployeeId,
                    CustomerId = saleOrderVM.CustomerId,
                    OrderstatusId = saleOrderVM.OrderstatusId,
                    Createdate = DateTime.Now
                };
                _context.SaleOrders.Add(saleOrder);
                await _context.SaveChangesAsync();

                var saleOrderDetails = new SaleOrderDetail
                {
                    SaleorderId = saleOrder.SaleorderId,
                    ItemId = saleOrderVM.ItemId,
                    ItemQty = saleOrderVM.ItemQty
                };

                _context.SaleOrderDetails.Add(saleOrderDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", saleOrderVM.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeFirstname", saleOrderVM.EmployeeId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", saleOrderVM.ItemId);
            ViewData["ItemQty"] = saleOrderVM.ItemQty;
            ViewData["OrderstatusId"] = new SelectList(_context.OrderStatuses, "OrderstatusId", "OrderstatusName", saleOrderVM.OrderstatusId);
            return View(saleOrderVM);
        }

        // GET: SaleOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SaleOrders == null)
            {
                return NotFound();
            }

            var saleOrder = await _context.SaleOrders.FindAsync(id);
            if (saleOrder == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", saleOrder.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", saleOrder.EmployeeId);
            ViewData["OrderstatusId"] = new SelectList(_context.OrderStatuses, "OrderstatusId", "OrderstatusId", saleOrder.OrderstatusId);
            return View(saleOrder);
        }

        // POST: SaleOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleorderId,CustomerId,EmployeeId,OrderstatusId,Createdate")] SaleOrder saleOrder)
        {
            if (id != saleOrder.SaleorderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleOrderExists(saleOrder.SaleorderId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", saleOrder.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", saleOrder.EmployeeId);
            ViewData["OrderstatusId"] = new SelectList(_context.OrderStatuses, "OrderstatusId", "OrderstatusId", saleOrder.OrderstatusId);
            return View(saleOrder);
        }

        // GET: SaleOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SaleOrders == null)
            {
                return NotFound();
            }

            var saleOrder = await _context.SaleOrders
                .Include(s => s.Customer)
                .Include(s => s.Employee)
                .Include(s => s.Orderstatus)
                .FirstOrDefaultAsync(m => m.SaleorderId == id);
            if (saleOrder == null)
            {
                return NotFound();
            }

            return View(saleOrder);
        }

        // POST: SaleOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SaleOrders == null)
            {
                return Problem("Entity set 'SaleOrdersContext.SaleOrders'  is null.");
            }
            var saleOrder = await _context.SaleOrders.FindAsync(id);
            if (saleOrder != null)
            {
                _context.SaleOrders.Remove(saleOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleOrderExists(int id)
        {
          return (_context.SaleOrders?.Any(e => e.SaleorderId == id)).GetValueOrDefault();
        }
    }
}
