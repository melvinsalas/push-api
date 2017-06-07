using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokenApi.Models;
using System.Linq;

namespace TokenApi.Controllers
{
	[Route("api/[controller]")]
	public class TokenController : Controller
	{
		private readonly DatabaseContext _context;

		public TokenController(DatabaseContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IEnumerable<TokenItem> GetAll()
		{
			return _context.TokenItems.ToList();
		}

		[HttpGet("{id}", Name = "GetToken")]
		public IActionResult GetById(long id)
		{
			var item = _context.TokenItems.FirstOrDefault(t => t.Id == id);
			if (item == null) return NotFound();
			return new ObjectResult(item);
		}

		[HttpPost]
		public IActionResult Create([FromBody] TokenItem item)
		{
			if (item == null) return BadRequest();
			_context.TokenItems.Add(item);
			_context.SaveChanges();
			return CreatedAtRoute("GetToken", new { id = item.Id }, item);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(long id)
		{
			var token = _context.TokenItems.First(t => t.Id == id);
			if (token == null) return NotFound();
			_context.TokenItems.Remove(token);
			_context.SaveChanges();
			return new NoContentResult();
		}
	}
}