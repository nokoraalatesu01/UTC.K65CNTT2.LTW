
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenDucTruong_241230872.Models;

public class HangHoaController : Controller
{
    private readonly QLHangHoaContext _context;

    public HangHoaController(QLHangHoaContext context)
    {
        _context = context;
    }

    // GET: HANGHOAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.HangHoas.ToListAsync());
    }

    // GET: HANGHOAS/Details/5
    public async Task<IActionResult> Details(int? mahang)
    {
        if (mahang == null)
        {
            return NotFound();
        }

        var hanghoa = await _context.HangHoas
            .FirstOrDefaultAsync(m => m.MaHang == mahang);
        if (hanghoa == null)
        {
            return NotFound();
        }

        return View(hanghoa);
    }

    // GET: HANGHOAS/Create
    public IActionResult Create()
    {
        ViewBag.MaLoai = new SelectList(_context.LoaiHangs, "MaLoai", "TenLoai");
        return View();
    }

    // POST: HANGHOAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaHang,MaLoai,TenHang,Gia,Anh,MaLoaiNavigation")] HangHoa hanghoa)
    {
        if (ModelState.IsValid)
        {
            _context.Add(hanghoa);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
        ViewBag.MaLoai = new SelectList(_context.LoaiHangs, "MaLoai", "TenLoai", hanghoa.MaLoai);
        return View(hanghoa);
    }

    // GET: HANGHOAS/Edit/5
    public async Task<IActionResult> Edit(int? mahang)
    {
        if (mahang == null)
        {
            return NotFound();
        }

        var hanghoa = await _context.HangHoas.FindAsync(mahang);
        if (hanghoa == null)
        {
            return NotFound();
        }
        return View(hanghoa);
    }

    // POST: HANGHOAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? mahang, [Bind("MaHang,MaLoai,TenHang,Gia,Anh,MaLoaiNavigation")] HangHoa hanghoa)
    {
        if (mahang != hanghoa.MaHang)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(hanghoa);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HangHoaExists(hanghoa.MaHang))
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
        return View(hanghoa);
    }

    // GET: HANGHOAS/Delete/5
    public async Task<IActionResult> Delete(int? mahang)
    {
        if (mahang == null)
        {
            return NotFound();
        }

        var hanghoa = await _context.HangHoas
            .FirstOrDefaultAsync(m => m.MaHang == mahang);
        if (hanghoa == null)
        {
            return NotFound();
        }

        return View(hanghoa);
    }

    // POST: HANGHOAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? mahang)
    {
        var hanghoa = await _context.HangHoas.FindAsync(mahang);
        if (hanghoa != null)
        {
            _context.HangHoas.Remove(hanghoa);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HangHoaExists(int? mahang)
    {
        return _context.HangHoas.Any(e => e.MaHang == mahang);
    }
}
