using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Data;
using MyWebApiApp.Models;
using System.Linq;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaisController : ControllerBase
    {
        private readonly MyDbContext _context;
        public LoaisController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var dsLoai = _context.Loais.ToList();
                return Ok(dsLoai);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAll(int id)
        {
            var dsLoai = _context.Loais.SingleOrDefault(l => l.MaLoai == id);
            if (dsLoai != null)
            {
                return Ok(dsLoai);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateNew(LoaiModel loaiModel)
        {
            try
            {
                var loai = new Loai
                {
                    TenLoai = loaiModel.TenLoai
                };
                _context.Add(loai);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created,loai);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult GetAllById(int id,LoaiModel model)
        {
            var loai = _context.Loais.SingleOrDefault(l => l.MaLoai == id);
            if (loai != null)
            {
                loai.TenLoai = model.TenLoai;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLoaiById(int id, LoaiModel model)
        {
            var loai = _context.Loais.SingleOrDefault(l => l.MaLoai == id);
            if (loai != null)
            {
                _context.Remove(loai);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
