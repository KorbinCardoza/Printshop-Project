using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrintShop.Models.Entity;
using PrintShop.ViewModels;

namespace PrintShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly PrintshopContext _context;

        public AdminController(PrintshopContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var model = new AdminViewModel
            {
                UserList = await _context.Users.OrderBy(m => m.Username).ToListAsync(),
                ProductList = await _context.Products.ToListAsync(),
                System = await _context.Systems.FirstAsync()
            };

            return View(model);
        }

        // GET: Users/Create
        public IActionResult CreateUser()
        {
            var model = new User
            {
                RoleSelectList = new List<SelectListItem>
                {
                    new SelectListItem("User", "User"),
                    new SelectListItem("Admin", "Admin")
                },
                Username = "CITYOFSALEM\\"
            };
            
            return View(model);
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([Bind("Username,Role")] User user)
        {
            if (!ModelState.IsValid) return View(user);
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.RoleSelectList = new List<SelectListItem>
                {
                    new SelectListItem("User", "User"),
                    new SelectListItem("Admin", "Admin")
                };


            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, [Bind("UserId,Username,Role")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(user);
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserId))
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

        // GET: Users/Delete/5
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, [Bind("ProductPkey,ProductCategory,StockSize,ProductDesc,ProductWeight,ProductVendor,ProductVendorPrice,ProductType,UnitCost,UnitType")] Product product)
        {
            if (id != product.ProductPkey)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(product);

            product.ProductUpdateDt = System.DateTime.Now;
                _context.Update(product);
                await _context.SaveChangesAsync();
            
            
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CreateProduct()
        {          
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([Bind("ProductCategory,StockSize,ProductDesc,ProductWeight,ProductVendor,ProductVendorPrice,ProductType,UnitCost,UnitType")] Product product)
        {
            if (!ModelState.IsValid) return View(product);
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductPkey == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> EditSystem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var system = await _context.Systems.FindAsync(id);
            if (system == null)
            {
                return NotFound();
            }

            
            return View(system);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSystem(int id, [Bind("SystemPkey,CopierCost,CopyCntrLaborCst,CopierCostColor,Imagingcost,FilmRollCost")] Models.Entity.System system)
        {
            if (id != system.SystemPkey)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(system);

                _context.Update(system);
                await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
