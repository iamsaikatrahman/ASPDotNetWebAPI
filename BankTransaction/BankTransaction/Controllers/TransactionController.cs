using BankTransaction.Data;
using BankTransaction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankTransaction.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public TransactionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get Transaction
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.TransactionModels.ToListAsync());
        }

        //Get Transaction AddOrEdit 
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new TransactionModel());
            else { 
                var transactionId = await _dbContext.TransactionModels.FindAsync(id);
                if (transactionId == null)
                        NotFound();
                return View(transactionId);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("")] TransactionModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    transactionModel.Date = DateTime.Now;
                    _dbContext.Add(transactionModel);
                    await _dbContext.SaveChangesAsync();
                }
                else 
                {
                    try
                    {
                        _dbContext.Update(transactionModel);
                        await _dbContext.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        return NotFound();
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.TransactionModels.ToList())});
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", transactionModel) });
        }

        public async Task<IActionResult> Delete(int? id)
        { 
            if(id == null)
                NotFound();
            var transactionModel = await _dbContext.TransactionModels.FirstOrDefaultAsync(x => x.TransactionId == id);
            if (transactionModel == null)
                NotFound();
            return View(transactionModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionModel = await _dbContext.TransactionModels.FindAsync(id);
            _dbContext.TransactionModels.Remove(transactionModel);
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.TransactionModels.ToList()) });
        }

    }
}
