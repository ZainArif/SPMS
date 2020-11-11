using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPMS.Data;
using SPMS.Models;
using SPMS.Models.ViewModel;

namespace SPMS.Controllers
{
    public class PurchaseDetailsController : Controller
    {
        private readonly SPMSContext _context;
        private readonly DbFunction _dbFunction;
        private const string PurchaseDetail_SEQ = "dbo.purchasedetail_seq";

        public PurchaseDetailsController(SPMSContext context, DbFunction dbFunction)
        {
            _context = context;
            _dbFunction = dbFunction;
        }

        // GET: PurchaseDetails
        public async Task<IActionResult> Index()
        {
            var sPMSContext = _context.PurchaseDetail.Include(p => p.Vendor);
            return View(await sPMSContext.ToListAsync());
        }

        // GET: PurchaseDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetail = await _context.PurchaseDetail
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.Purchase_Id == id);
            if (purchaseDetail == null)
            {
                return NotFound();
            }

            return View(purchaseDetail);
        }

        // GET: PurchaseDetails/Create
        public IActionResult Create(int? Purchase_Id)
        {
            List<Vendor> ddl_vendor = _context.Vendor.ToList();
            ddl_vendor.Insert(0, new Vendor() { Vender_Id = 0, Vender_Name = "-Please select a vendor-" } );
            List<Item> ddl_item = _context.Item.ToList();
            ddl_item.Insert(0, new Item() { Item_Id = 0, Item_Name = "-Please select a item-" });

            PurchaseDetail purchaseDetail = _context.PurchaseDetail.Find(Purchase_Id);

            IEnumerable<PurchaseItems> purchaseItemsList = _context.PurchaseItems.Where(i => i.Purchase_Id == Purchase_Id).ToList();
            
            PurchaseVM purchaseVM = new PurchaseVM()
            {
                PurchaseDetail = purchaseDetail ?? new PurchaseDetail(),
                PurchaseItems = new PurchaseItems(),
                PurchaseItemsList = purchaseItemsList,
                VendorList = ddl_vendor.Select(v => new SelectListItem
                {
                    Text = v.Vender_Name,
                    Value = v.Vender_Id.ToString()
                }),
                ItemsList = ddl_item.Select(i => new SelectListItem
                {
                    Text = i.Item_Id == 0 ? i.Item_Name : i.Item_Name + " | " + i.Item_Code,
                    Value = i.Item_Id.ToString()
                })
            };
            return View(purchaseVM);
        }

        // POST: PurchaseDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Purchase_Id,Vender_Id,Purchase_Date,Purchase_Description,Amount_Paid,Amount_Balance,Entry_Date")] PurchaseDetail purchaseDetail)
        public async Task<IActionResult> CreatePurchaseDetail(PurchaseVM objVM)
        {
            
            if (ModelState.IsValid)
            {
                objVM.PurchaseDetail.Entry_Date = DateTime.Now;
                if(objVM.PurchaseDetail.Purchase_Id == 0)
                {
                    objVM.PurchaseDetail.Purchase_Id = _dbFunction.GetKey(PurchaseDetail_SEQ);
                    _context.Add(objVM.PurchaseDetail);
                }
                else
                {
                    _context.Update(objVM.PurchaseDetail);
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: "Create", routeValues: new { @Purchase_Id = objVM.PurchaseDetail.Purchase_Id });
            }
            
            return RedirectToAction(actionName: "Create", routeValues: new { @Purchase_Id = objVM.PurchaseDetail.Purchase_Id });
        }

        public async Task<IActionResult> CreatePurchaseItem(PurchaseVM objVM)
        {

            if (ModelState.IsValid)
            {
                objVM.PurchaseItems.Available_Quantity = objVM.PurchaseItems.Quantity;
                objVM.PurchaseItems.Entry_Date = DateTime.Now;
                _context.Add(objVM.PurchaseItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: "Create", routeValues: new { @Purchase_Id = objVM.PurchaseItems.Purchase_Id });
            }

            return RedirectToAction(actionName: "Create", routeValues: new { @Purchase_Id = objVM.PurchaseDetail.Purchase_Id });
        }

        // GET: PurchaseDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetail = await _context.PurchaseDetail.FindAsync(id);
            if (purchaseDetail == null)
            {
                return NotFound();
            }
            ViewData["Vender_Id"] = new SelectList(_context.Vendor, "Vender_Id", "Contact_Person_Name", purchaseDetail.Vender_Id);
            return View(purchaseDetail);
        }

        // POST: PurchaseDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Purchase_Id,Vender_Id,Purchase_Date,Purchase_Description,Amount_Paid,Amount_Balance,Entry_Date")] PurchaseDetail purchaseDetail)
        {
            if (id != purchaseDetail.Purchase_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseDetailExists(purchaseDetail.Purchase_Id))
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
            ViewData["Vender_Id"] = new SelectList(_context.Vendor, "Vender_Id", "Contact_Person_Name", purchaseDetail.Vender_Id);
            return View(purchaseDetail);
        }

        // GET: PurchaseDetails/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Invalid Purchase No" });
            }

            var purchaseDetail = await _context.PurchaseDetail.FindAsync(id);

            if (purchaseDetail == null)
            {
                return Json(new { success = false, message = "Purchase No Not Found" });
            }

            _context.PurchaseDetail.Remove(purchaseDetail);
            int flag = await _context.SaveChangesAsync();

            if (flag >= 0)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            else
            {
                return Json(new { success = false, message = "Delete Not Successful" });
            }
        }

        private bool PurchaseDetailExists(int id)
        {
            return _context.PurchaseDetail.Any(e => e.Purchase_Id == id);
        }
    }
}
