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
    public class SaleDetailsController : Controller
    {
        private readonly SPMSContext _context;
        private readonly DbFunction _dbFunction;
        private const string SaleDetail_SEQ = "dbo.saledetail_seq";

        public SaleDetailsController(SPMSContext context, DbFunction dbFunction)
        {
            _context = context;
            _dbFunction = dbFunction;
        }

        // GET: SaleDetails
        public async Task<IActionResult> Index()
        {
            var sPMSContext = _context.SaleDetail.Include(s => s.Customer);
            return View(await sPMSContext.ToListAsync());
        }

        // GET: SaleDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetail
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.Sale_Id == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        // GET: SaleDetails/Create
        public IActionResult Create(int? Sale_Id)
        {
            List<Customer> ddl_customer = _context.Customer.ToList();
            ddl_customer.Insert(0, new Customer() { Customer_Id = 0, Customer_Name = "-Please select a customer-" });

            SaleDetail saleDetail = _context.SaleDetail.Find(Sale_Id);

            List<SaleItems> saleItemsList = _context.SaleItems.Where(s => s.Sale_Id == Sale_Id).ToList();

            int totalPriceSaleItems = _context.SaleItems.Where(s => s.Sale_Id == Sale_Id).Select(s => s.Total_Price).Sum();

            if(totalPriceSaleItems != 0 )
                saleItemsList.Add(new SaleItems() { Total_Price = totalPriceSaleItems });

            SaleVM saleVM = new SaleVM()
            {
                SaleDetail = saleDetail ?? new SaleDetail() { Sale_Date = DateTime.Today },
                SaleItems = new SaleItems(),
                SaleItemsList = saleItemsList,
                CustomerList = ddl_customer.Select(s => new SelectListItem
                {
                    Text = s.Customer_Name,
                    Value = s.Customer_Id.ToString()
                })
            };
            return View(saleVM);
        }

        // POST: SaleDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSaleDetail(SaleVM objVM)
        {
            if (ModelState.IsValid)
            {
                objVM.SaleDetail.Entry_Date = DateTime.Now;
                if(objVM.SaleDetail.Sale_Id == 0)
                {
                    objVM.SaleDetail.Sale_Id = _dbFunction.GetKey(SaleDetail_SEQ);
                    _context.Add(objVM.SaleDetail);
                }
                else
                {
                    _context.Update(objVM.SaleDetail);
                }
                
                int flag = await _context.SaveChangesAsync();

                if (flag >= 0)
                    TempData["success"] = "Sale saved successfully";
                else
                    TempData["danger"] = "Sale not saved";

                return RedirectToAction(actionName: "Create", routeValues: new { @Sale_Id = objVM.SaleDetail.Sale_Id });
            }

            return RedirectToAction(actionName: "Create", routeValues: new { @Sale_Id = objVM.SaleDetail.Sale_Id });
        }

        // GET: SaleDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetail.FindAsync(id);
            if (saleDetail == null)
            {
                return NotFound();
            }
            ViewData["Customer_Id"] = new SelectList(_context.Customer, "Customer_Id", "Customer_Name", saleDetail.Customer_Id);
            return View(saleDetail);
        }

        // POST: SaleDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sale_Id,Customer_Id,Sale_Date,Amount_Received,Amount_Balance,Expense,Entry_Date")] SaleDetail saleDetail)
        {
            if (id != saleDetail.Sale_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleDetailExists(saleDetail.Sale_Id))
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
            ViewData["Customer_Id"] = new SelectList(_context.Customer, "Customer_Id", "Customer_Name", saleDetail.Customer_Id);
            return View(saleDetail);
        }


        public async Task<IActionResult> GetPurchaseItemList(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Invalid Purchase No" });
            }

            List<PurchaseItems> purchaseItems = await _context.PurchaseItems.Include(i => i.Item).Where(i => i.Purchase_Id == id).OrderBy(i => i.Item.Item_Name).ToListAsync();

            purchaseItems.Insert(0, new PurchaseItems() { Item = new Item() { Item_Id = 0, Item_Name = "-Please select a item-" } });

            IEnumerable<SelectListItem> ItemsList = purchaseItems.Select(i => new SelectListItem
            {
               Text = i.Item.Item_Id == 0 ? i.Item.Item_Name : i.Item.Item_Name + " | " + i.Item.Item_Code,
               Value = i.Purchase_Item_Id.ToString()
            });

            return Json(new { success = true, message = "Item Found", itemsList = ItemsList });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Invalid Sale No" });
            }

            var saleDetail = await _context.SaleDetail.FindAsync(id);

            if (saleDetail == null)
            {
                return Json(new { success = false, message = "Sale No Not Found" });
            }

            IEnumerable<SaleItems> items = _context.SaleItems.Where(i => i.Sale_Id == id).ToList();

            _context.SaleDetail.Remove(saleDetail);
            _context.SaleItems.RemoveRange(items);

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

        private bool SaleDetailExists(int id)
        {
            return _context.SaleDetail.Any(e => e.Sale_Id == id);
        }
    }
}
