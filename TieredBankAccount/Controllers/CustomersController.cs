using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TieredBankAccount.BLL;
using TieredBankAccount.Data;
using TieredBankAccount.Models;

namespace TieredBankAccount.Controllers

{

    // REFACTOR: Extract all direct repository calls to business logic layers
    public class CustomersController : Controller
    {
        private readonly AccountBusinessLogic _accountBusinessLogic;
        private readonly CustomerBusinessLogic _customerBusinessLogic;

        public CustomersController(IRepository<BankAccount> accountRepo, IRepository<Customer> customerRepo)
        {
            _accountBusinessLogic = new AccountBusinessLogic(accountRepo);
            _customerBusinessLogic = new CustomerBusinessLogic(customerRepo, accountRepo);
        }
        
        // GET: Addresses, Customers, CustomerAddresses
        public async Task<IActionResult> GetAllCustomerAddresses(int? customerId)
        {
            if(customerId == null)
            {
                return NotFound();
            }
            else
            {
                Customer customer = _customerRepo.Get(customerId);

                if(customer == null)
                {
                    return NotFound();
                } else
                {

                    List<int> AddressIds = _custAddRepo.GetAll().Where(ca => ca.CustomerId == customer.Id).Select(ca => ca.AddressId).ToList();

                    List<Address> addresses = _addressRepo.GetAll().Where(a => AddressIds.Contains(a.Id)).ToList();

                    return View(new { Customer = customer, Addresses = addresses }) ;
                }
            }
        }

        // GET: Customers


        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } else
            {
              
                Customer? customer = _customerRepo.Get(id);

                if (customer == null)
                {
                    return NotFound();
                } else
                {
                    return View(customer);
                }
            }
        }

        // get method
        public IActionResult Withdraw(int? accountId)
        {
            try
            {
                return View(_accountBusinessLogic.GetBankAccount(accountId));
            } catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Withdraw(decimal amount, int accountId)
        {

            try { 
                _accountBusinessLogic.Withdraw(amount, accountId);
                return RedirectToAction("Index");
            } catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepo.Create(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        /* GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customer == null)
            {
                return Problem("Entity set 'TieredBankAccountContext.Customer'  is null.");
            }
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */
    }
}
