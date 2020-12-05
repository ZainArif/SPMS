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
                return BadRequest();
            }

            SaleDetail saleDetail = await _context.SaleDetail
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.Sale_Id == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            List<SaleItems> saleItemsList = _context.SaleItems.Include(s => s.PurchaseItems).ThenInclude(s => s.Item).Where(s => s.Sale_Id == id).ToList();

            int totalPriceSaleItems = _context.SaleItems.Where(s => s.Sale_Id == id).Select(s => s.Total_Price).Sum();

            saleItemsList.Add(new SaleItems() { Total_Price = totalPriceSaleItems });

            SaleVM saleVM = new SaleVM()
            {
                SaleDetail = saleDetail,
                SaleItemsList = saleItemsList
            };

            return View(saleVM);
        }

        // GET: SaleDetails/Create
        public IActionResult Create(int? Sale_Id)
        {
            List<Customer> ddl_customer = _context.Customer.ToList();
            ddl_customer.Insert(0, new Customer() { Customer_Id = 0, Customer_Name = "-Please select a customer-" });

            SaleDetail saleDetail = _context.SaleDetail.Find(Sale_Id);

            List<SaleItems> saleItemsList = _context.SaleItems.Include(s => s.PurchaseItems).ThenInclude(s => s.Item).Where(s => s.Sale_Id == Sale_Id).ToList();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSaleItem(SaleVM objVM)
        {

            if (ModelState.IsValid)
            {
                int actualQty = _context.PurchaseItems.Where(i => i.Purchase_Item_Id == objVM.SaleItems.Purchase_Item_Id).Select(i => i.Quantity).FirstOrDefault();
                int soldQty;

                if(objVM.SaleItems.Sale_Item_Id == 0) 
                {
                    soldQty = _context.SaleItems.Where(i => i.Purchase_Item_Id == objVM.SaleItems.Purchase_Item_Id).Select(i => i.Quantity).Sum();
                }
                else
                {
                    soldQty = _context.SaleItems.Where(i => i.Purchase_Item_Id == objVM.SaleItems.Purchase_Item_Id && i.Sale_Item_Id != objVM.SaleItems.Sale_Item_Id).Select(i => i.Quantity).Sum();
                }

                int availableQty = actualQty - soldQty;

                if (objVM.SaleItems.Quantity <= actualQty)
                {
                    if (objVM.SaleItems.Quantity <= availableQty)
                    {
                        var totalAmount = (from s in _context.SaleDetail where s.Sale_Id == objVM.SaleItems.Sale_Id select new { total = s.Amount_Received + s.Amount_Balance }).FirstOrDefault();
                        int totalPriceSaleItems;

                        if (objVM.SaleItems.Sale_Item_Id == 0)
                        {
                            totalPriceSaleItems = _context.SaleItems.Where(i => i.Sale_Id == objVM.SaleItems.Sale_Id).Select(i => i.Total_Price).Sum();
                        }
                        else
                        {
                            totalPriceSaleItems = _context.SaleItems.Where(i => i.Sale_Id == objVM.SaleItems.Sale_Id && i.Sale_Item_Id != objVM.SaleItems.Sale_Item_Id).Select(i => i.Total_Price).Sum();
                        }

                        if (objVM.SaleItems.Total_Price <= totalAmount.total)
                        {
                            if (objVM.SaleItems.Total_Price <= (totalAmount.total - totalPriceSaleItems))
                            {
                                objVM.SaleItems.Entry_Date = DateTime.Now;
                                if (objVM.SaleItems.Sale_Item_Id == 0)
                                {
                                    _context.Add(objVM.SaleItems);
                                }
                                else
                                {
                                    _context.Update(objVM.SaleItems);
                                }

                                int flag = await _context.SaveChangesAsync();
                                if (flag >= 0)
                                    TempData["success"] = "Item saved successfully";
                                else
                                    TempData["danger"] = "Item not saved";

                                return RedirectToAction(actionName: "Create", routeValues: new { @Sale_Id = objVM.SaleItems.Sale_Id });
                            }
                            else
                            {
                                TempData["warning"] = "Total Price is greater than total amount";
                                return RedirectToAction(actionName: "Create", routeValues: new { @Sale_Id = objVM.SaleItems.Sale_Id });
                            }
                        }
                        else
                        {
                            TempData["warning"] = "Total Price is greater than total amount";
                            return RedirectToAction(actionName: "Create", routeValues: new { @Sale_Id = objVM.SaleItems.Sale_Id });
                        }
                    }
                    else
                    {
                        TempData["warning"] = availableQty == 0 ? "Quantity not available" : "Only " + availableQty.ToString() + " quantity available";
                        return RedirectToAction(actionName: "Create", routeValues: new { @Sale_Id = objVM.SaleItems.Sale_Id });
                    }
                }
                else
                {
                    TempData["warning"] = availableQty == 0 ? "Quantity not available" : "Only " + availableQty.ToString() + " quantity available";
                    return RedirectToAction(actionName: "Create", routeValues: new { @Sale_Id = objVM.SaleItems.Sale_Id });
                }
            }
            return RedirectToAction(actionName: "Create", routeValues: new { @Sale_Id = objVM.SaleItems.Sale_Id });
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


        public IActionResult GetPurchaseItemList(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Invalid Purchase No" });
            }

            List<PurchaseItems> purchaseItemsList = _context.PurchaseItems.Include(i => i.Item).Where(i => i.Purchase_Id == id).OrderBy(i => i.Item.Item_Name).ToList();

            if (purchaseItemsList.Count > 0)
            {
                purchaseItemsList.Insert(0, new PurchaseItems() { Item = new Item() { Item_Id = 0, Item_Name = "-Please select a item-" } });

                IEnumerable<SelectListItem> ItemsList = purchaseItemsList.Select(i => new SelectListItem
                {
                    Text = i.Item.Item_Id == 0 ? i.Item.Item_Name : i.Item.Item_Name + " | " + i.Item.Item_Code,
                    Value = i.Purchase_Item_Id.ToString()
                });

                return Json(new { success = true, message = "Items Found", itemsList = ItemsList });
            }
            else
            {
                return Json(new { success = false, message = "Purchase No Not Found" });
            }
        }

        [HttpPost]
        
        public async Task<IActionResult> GetSaleItem(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Invalid Item Id" });
            }

            SaleItems saleItem = await _context.SaleItems.FindAsync(id);

            if (saleItem == null)
            {
                return Json(new { success = false, message = "Item Not Found" });
            }

            int Purchase_Id = await _context.PurchaseItems.Where(i => i.Purchase_Item_Id == saleItem.Purchase_Item_Id).Select(i => i.Purchase_Id).FirstOrDefaultAsync();

            List<PurchaseItems> purchaseItemsList = await _context.PurchaseItems.Include(i => i.Item).Where(i => i.Purchase_Id == Purchase_Id).OrderBy(i => i.Item.Item_Name).ToListAsync();

            if (purchaseItemsList.Count > 0)
            {
                purchaseItemsList.Insert(0, new PurchaseItems() { Item = new Item() { Item_Id = 0, Item_Name = "-Please select a item-" } });

                IEnumerable<SelectListItem> ItemsList = purchaseItemsList.Select(i => new SelectListItem
                {
                    Text = i.Item.Item_Id == 0 ? i.Item.Item_Name : i.Item.Item_Name + " | " + i.Item.Item_Code,
                    Value = i.Purchase_Item_Id.ToString(),
                    Selected = i.Purchase_Item_Id == saleItem.Purchase_Item_Id ? true : false
                });

                return Json(new { success = true, message = "Items Found", itemsList = ItemsList, itemDetail = saleItem, purchaseId = Purchase_Id });
            }
            else
            {
                return Json(new { success = false, message = "Item list Not Found" });
            }
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

        [HttpDelete]
        public async Task<IActionResult> DeleteSaleItem(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Invalid Item Id" });
            }

            var saleItem = await _context.SaleItems.FindAsync(id);

            if (saleItem == null)
            {
                return Json(new { success = false, message = "Item Not Found" });
            }

            _context.SaleItems.Remove(saleItem);
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
