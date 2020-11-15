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
                return BadRequest();
            }

            PurchaseDetail purchaseDetail = await _context.PurchaseDetail
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.Purchase_Id == id);

            if (purchaseDetail == null)
            {
                return NotFound();
            }


            List<PurchaseItems> purchaseItemsList = _context.PurchaseItems.Include(i => i.Item).Where(i => i.Purchase_Id == id).ToList();

            int totalCostPurchaseItems = _context.PurchaseItems.Where(i => i.Purchase_Id == id).Select(i => i.Total_Cost).Sum();

            purchaseItemsList.Add(new PurchaseItems() { Total_Cost = totalCostPurchaseItems });

            PurchaseVM purchaseVM = new PurchaseVM()
            {
                PurchaseDetail = purchaseDetail,
                PurchaseItemsList = purchaseItemsList
            };

            return View(purchaseVM);
        }

        // GET: PurchaseDetails/Create
        public IActionResult Create(int? Purchase_Id)
        {
            List<Vendor> ddl_vendor = _context.Vendor.ToList();
            ddl_vendor.Insert(0, new Vendor() { Vender_Id = 0, Vender_Name = "-Please select a vendor-" });
            List<Item> ddl_item = _context.Item.ToList();
            ddl_item.Insert(0, new Item() { Item_Id = 0, Item_Name = "-Please select a item-" });

            PurchaseDetail purchaseDetail = _context.PurchaseDetail.Find(Purchase_Id);

            List<PurchaseItems> purchaseItemsList = _context.PurchaseItems.Where(i => i.Purchase_Id == Purchase_Id).ToList();

            int totalCostPurchaseItems = _context.PurchaseItems.Where(i => i.Purchase_Id == Purchase_Id).Select(i => i.Total_Cost).Sum();
            
            if(totalCostPurchaseItems != 0)
                purchaseItemsList.Add(new PurchaseItems() { Total_Cost = totalCostPurchaseItems });

            PurchaseVM purchaseVM = new PurchaseVM()
            {
                PurchaseDetail = purchaseDetail ?? new PurchaseDetail() { Purchase_Date = DateTime.Today },
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
                if (objVM.PurchaseDetail.Purchase_Id == 0)
                {
                    objVM.PurchaseDetail.Purchase_Id = _dbFunction.GetKey(PurchaseDetail_SEQ);
                    _context.Add(objVM.PurchaseDetail);
                }
                else
                {
                    _context.Update(objVM.PurchaseDetail);
                }

                int flag = await _context.SaveChangesAsync();
                if (flag >= 0)
                    TempData["success"] = "Purchase saved successfully";
                else
                    TempData["danger"] = "Purchase not saved";

                return RedirectToAction(actionName: "Create", routeValues: new { @Purchase_Id = objVM.PurchaseDetail.Purchase_Id });
            }

            return RedirectToAction(actionName: "Create", routeValues: new { @Purchase_Id = objVM.PurchaseDetail.Purchase_Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePurchaseItem(PurchaseVM objVM)
        {

            if (ModelState.IsValid)
            {
                var totalAmount = (from p in _context.PurchaseDetail where p.Purchase_Id == objVM.PurchaseItems.Purchase_Id select new { total = p.Amount_Paid + p.Amount_Balance }).FirstOrDefault();
                int totalCostPurchaseItems;

                if (objVM.PurchaseItems.Purchase_Item_Id == 0)
                {
                    totalCostPurchaseItems = _context.PurchaseItems.Where(i => i.Purchase_Id == objVM.PurchaseItems.Purchase_Id).Select(i => i.Total_Cost).Sum();
                }
                else
                {
                    totalCostPurchaseItems = _context.PurchaseItems.Where(i => i.Purchase_Id == objVM.PurchaseItems.Purchase_Id && i.Purchase_Item_Id != objVM.PurchaseItems.Purchase_Item_Id).Select(i => i.Total_Cost).Sum();
                }

                if (objVM.PurchaseItems.Total_Cost <= totalAmount.total)
                {
                    if (objVM.PurchaseItems.Total_Cost <= (totalAmount.total - totalCostPurchaseItems))
                    {
                        objVM.PurchaseItems.Available_Quantity = objVM.PurchaseItems.Quantity;
                        objVM.PurchaseItems.Entry_Date = DateTime.Now;
                        if (objVM.PurchaseItems.Purchase_Item_Id == 0)
                        {
                            _context.Add(objVM.PurchaseItems);
                        }
                        else
                        {
                            _context.Update(objVM.PurchaseItems);
                        }

                        int flag = await _context.SaveChangesAsync();
                        if (flag >= 0)
                            TempData["success"] = "Item saved successfully";
                        else
                            TempData["danger"] = "Item not saved";
                        
                        return RedirectToAction(actionName: "Create", routeValues: new { @Purchase_Id = objVM.PurchaseItems.Purchase_Id });
                    }
                    else
                    {
                        TempData["warning"] = "Total Cost is greater than total amount";
                        return RedirectToAction(actionName: "Create", routeValues: new { @Purchase_Id = objVM.PurchaseItems.Purchase_Id });
                    }
                }
                else
                {
                    TempData["warning"] = "Total Cost is greater than total amount";
                    return RedirectToAction(actionName: "Create", routeValues: new { @Purchase_Id = objVM.PurchaseItems.Purchase_Id });
                }
            }
            return RedirectToAction(actionName: "Create", routeValues: new { @Purchase_Id = objVM.PurchaseItems.Purchase_Id });
        }

        

        public async Task<IActionResult> GetPurchaseItem(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Invalid Item Id" });
            }

            var purchaseItems = await _context.PurchaseItems.FindAsync(id);

            if (purchaseItems == null)
            {
                return Json(new { success = false, message = "Item Not Found" });
            }

            List<Item> ddl_item = _context.Item.ToList();
            ddl_item.Insert(0, new Item() { Item_Id = 0, Item_Name = "-Please select a item-" });
            IEnumerable<SelectListItem> ItemsList = ddl_item.Select(i => new SelectListItem
            {
                Text = i.Item_Id == 0 ? i.Item_Name : i.Item_Name + " | " + i.Item_Code,
                Value = i.Item_Id.ToString()
            });

            return Json(new { success = true, message = "Item Found", itemDetail = purchaseItems, itemsList = ItemsList });
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

            IEnumerable<PurchaseItems> items = _context.PurchaseItems.Where(i => i.Purchase_Id == id).ToList();

            _context.PurchaseDetail.Remove(purchaseDetail);
            _context.PurchaseItems.RemoveRange(items);

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

        [HttpDelete]
        public async Task<IActionResult> DeletePurchaseItem(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Invalid Item Id" });
            }

            var purchaseItem = await _context.PurchaseItems.FindAsync(id);

            if (purchaseItem == null)
            {
                return Json(new { success = false, message = "Item Not Found" });
            }

            _context.PurchaseItems.Remove(purchaseItem);
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
