using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokenApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TokenApi.Controllers
{
	[Route("api/[controller]")]
	public class MessagesController : Controller
	{
		readonly DatabaseContext _context;

		public MessagesController(DatabaseContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IEnumerable<MessageItem> GetAll()
		{
			return _context.MessageItem.ToList();
		}

		[HttpGet("{id}", Name = "GetMessage")]
		public IActionResult GetById(long id)
		{
			var item = _context.MessageItem.FirstOrDefault(t => t.Id == id);
			if (item == null) return NotFound();
			return new ObjectResult(item);
		}

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MessageItem item)
        {
            if (item == null) return BadRequest();
            foreach (var token in _context.TokenItems.Where(x => x.Email == item.Email))
            {
                var message = FirebaseService.GetMessage(token, item);
                await FirebaseService.FCMClient.SendMessageAsync(message);
            }
            _context.MessageItem.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetToken", new { id = item.Id }, item);
        }
    }
}