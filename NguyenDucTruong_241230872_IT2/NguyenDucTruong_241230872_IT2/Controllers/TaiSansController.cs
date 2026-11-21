
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenDucTruong_241230872_IT2.Models;

public class TaiSansController : Controller
{
    private readonly QltaiSanContext _context;

    public TaiSansController(QltaiSanContext context)
    {
        _context = context;
    }

    // GET: TAISANS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.TaiSans.ToListAsync());
    }

    // GET: TAISANS/Details/5
    public async Task<IActionResult> Details(int? mats)
    {
        if (mats == null)
        {
            return NotFound();
        }

        var taisan = await _context.TaiSans
            .FirstOrDefaultAsync(m => m.MaTs == mats);
        if (taisan == null)
        {
            return NotFound();
        }

        return View(taisan);
    }

    // GET: TAISANS/Create
    public IActionResult Create()
    {
        ViewBag.DsLoai = new SelectList(_context.LoaiTaiSans, "MaLoai", "TenLoai");
        return View();
    }

    // POST: TAISANS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaTs,MaLoai,TenTs,NguyenGia,Anh,MaLoaiNavigation")] TaiSan taisan)
    {
        if (ModelState.IsValid)
        {
            _context.Add(taisan);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        ViewBag.DsLoai = new SelectList(_context.LoaiTaiSans, "MaLoai", "TenLoai");
        return View(taisan);
    }

    // GET: TAISANS/Edit/5
    public async Task<IActionResult> Edit(int? mats)
    {
        if (mats == null)
        {
            return NotFound();
        }

        var taisan = await _context.TaiSans.FindAsync(mats);
        if (taisan == null)
        {
            return NotFound();
        }
        return View(taisan);
    }

    // POST: TAISANS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? mats, [Bind("MaTs,MaLoai,TenTs,NguyenGia,Anh,MaLoaiNavigation")] TaiSan taisan)
    {
        if (mats != taisan.MaTs)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(taisan);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaiSanExists(taisan.MaTs))
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
        return View(taisan);
    }

    // GET: TAISANS/Delete/5
    public async Task<IActionResult> Delete(int? mats)
    {
        if (mats == null)
        {
            return NotFound();
        }

        var taisan = await _context.TaiSans
            .FirstOrDefaultAsync(m => m.MaTs == mats);
        if (taisan == null)
        {
            return NotFound();
        }

        return View(taisan);
    }

    // POST: TAISANS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? mats)
    {
        var taisan = await _context.TaiSans.FindAsync(mats);
        if (taisan != null)
        {
            _context.TaiSans.Remove(taisan);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TaiSanExists(int? mats)
    {
        return _context.TaiSans.Any(e => e.MaTs == mats);
    }
}
